using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using CommonUtilsLibrary.Utils;
using CommonUtilsLibrary.Models;
using Newtonsoft.Json;
using System.Threading;

namespace AMSDB2DB
{
    public class TaskHandler
    {
        public TaskHandler(TaskEntity task, string taskName)
        {
            Task = task;
            TaskName = taskName;
            DbType = Task.From.DBType.ToLower();
            //RunTaskMainThread();
        }

        public string TaskName { get; set; }

        string SeqValue { get; set; }

        public bool Active { get; private set; } = true;

        public Thread TaskThread { get; set; }

        readonly TaskEntity Task;

        private DateTime LastBeginTime { get; set; } = DateTime.MinValue;
        private DateTime LastEndTime { get; set; } = DateTime.MinValue;

        private string _dbType = "";
        private string DbType
        {
            get { return _dbType; }
            set { _dbType = value; }
        }

        string fixSeqValue = "";

        public delegate void delegateBeforeRun();

        public delegate void delegateAfterRun();

        public delegate void delegateWhenError(object error);

        public delegate void delegateWhenTaskDone(TimeSpan ts);

        public event delegateBeforeRun EventBeforeRun;

        public event delegateAfterRun EventAfterRun;

        public event delegateWhenError EventWhenError;

        public event delegateWhenTaskDone EventWhenTaskDone;

        public void RunTaskMainThread()
        {
            EventBeforeRun();
            while (Active)
            {
                RunTask();
            }
            EventAfterRun();
        }

        void RunTask()
        {
            try
            {
                LastBeginTime = DateTime.Now;
                GetFromDBDatas();
                ImportToDB();
                LastEndTime = DateTime.Now;
                EventWhenTaskDone(LastEndTime - LastBeginTime);
            }
            catch (Exception ex)
            {
                EventWhenError(ex);
            }
            finally
            {
                System.Threading.Thread.Sleep(Task.Interval * 1000);
            }
        }

        public DBDynamicEntities entities;


        void GetFromDBDatas()
        {
            string sql = string.Format("select {0} from {1}", string.Join(",", Task.From.Columns), Task.From.Table);
            if (!string.IsNullOrEmpty(Task.From.Sequence))
            {
                fixSeqValue = FixSeqValue(Task.From.SeqValue);
                if (fixSeqValue.Trim('\'') != "")
                    sql = string.Format("{0} where {1} > {2}", sql, Task.From.Sequence, fixSeqValue);
                sql = string.Format("{0} order by {1}", sql, Task.From.Sequence);
            }
            //限制每次最多复制500条数据
           // sql = sql + " fetch first 500 rows only";
            OracleConPara para = new OracleConPara()
            {
                HostName = Task.From.HostName,
                Password = Task.From.Password,
                Port = Task.From.Port,
                ServiceName = Task.From.ServiceName,
                UserID = Task.From.UserID
            };
            if (DbType == "db2")
            {
                DB2DynamicDataCatcher db2 = new DB2DynamicDataCatcher(Task.From.Table, para, sql);
                entities = db2.GetEntities();
            }
            else if (DbType == "oracle")
            {
                OracleDynamicDataCatcher oracle = new OracleDynamicDataCatcher(Task.From.Table, para, sql);
                entities = oracle.GetEntities();
            }
            else if (DbType == "sqlserver")
            {
                MsDynamicDataCatcher ms = new MsDynamicDataCatcher(Task.From.Table, para, sql);
                entities = ms.GetEntities();
            }
        }

        void ImportToDB()
        {
            if (!entities.EntityList.Any()) return;
            OracleConPara para = new OracleConPara()
            {
                HostName = Task.To.HostName,
                Password = Task.To.Password,
                Port = Task.To.Port,
                ServiceName = Task.To.ServiceName,
                UserID = Task.To.UserID
            };
            var obj = entities.EntityList.First();
            var dict = (IDictionary<string, object>)obj;
            List<string> cols = Task.From.Columns.Any(a => a == "*") ? ((IDictionary<string, object>)entities.EntityList.First()).Keys.ToList() : Task.From.Columns;
            string sql = string.Format("insert into {0} ({1})", Task.To.Table, string.Join(",", cols));
            List<string> values = new List<string>();
            foreach (IDictionary<string, object> item in entities.EntityList)
            {
                List<string> list = new List<string>();
                foreach (string c in cols)
                {
                    list.Add(FixSqlValue(item[c]));
                }
                values.Add(string.Join(",", list));
            }
            sql = string.Format("{0} values ({1})", sql, string.Join("),(", values));
            List<string> sqls = new List<string>();
            if (string.IsNullOrEmpty(Task.From.Sequence))
            {
                sqls.Add(string.Format("delete from {0}", Task.To.Table));
            }
            else if (fixSeqValue.Trim('\'') != "")
            {
                sqls.Add(string.Format("delete from {0} where {1} > {2}", Task.To.Table, Task.From.Sequence, fixSeqValue));
            }
            sqls.Add(sql);
            if (Task.To.DBType.ToLower() == "db2")
            {
                DB2Util db2 = new DB2Util(para);
                db2.UpdateBatchCommand(sqls);
            }
            else if (Task.To.DBType.ToLower() == "oracle")
            {
                OracleUtil oracle = new OracleUtil(para);
                oracle.UpdateBatchCommand(sqls);
            }
            else if (Task.To.DBType.ToLower() == "sqlserver")
            {
                MsUtil ms = new MsUtil(para);
                ms.UpdateBatchCommand(sqls);
            }
            WriteSeqValue();
        }

        public void RemoveTask()
        {
            Active = false;
            if (GetRunningStage() != TaskRunStage.Running)
            {
                TaskThread.Abort();
                EventAfterRun();
            }
        }

        public void RemoveTaskCancel()
        {
            Active = true;
        }

        public TaskRunStage GetRunningStage()
        {
            double dur = (LastEndTime - LastBeginTime).TotalSeconds;
            if (dur < 0)
            {
                return TaskRunStage.Running;
            }
            if (dur == 0)
            {
                return TaskRunStage.NotBegin;
            }
            else
            {
                return TaskRunStage.Sleeping;
            }
        }

        public enum TaskRunStage {NotBegin,Running,Sleeping }
            

        string FixSqlValue(object obj)
        {
            if (obj == null) return "null";
            Type type = obj.GetType();
            if (Type.GetTypeCode(type) == TypeCode.DBNull) return "null";
            if (type.IsNumbericType()) return obj.ToString();
            if (Type.GetTypeCode(type) == TypeCode.DateTime) return string.Format("to_date('{0}','YYYY-MM-DD HH24:MI:SS')",((DateTime)obj).ToString("yyyy-MM-dd HH:mm:ss"));
            return string.Format("'{0}'", obj.ToString().Replace("'","''"));
        }

        string FixSeqValue(dynamic seqValue)
        {
            if (seqValue == null) return "";
            if (seqValue.Type.ToString() == "Date")
            {
                return string.Format("to_date('{0}','YYYY-MM-DD HH24:MI:SS')", ((DateTime)seqValue).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            else if (seqValue.Type.ToString() == "Integer" || seqValue.Type.ToString() == "Float")
            {
                return string.Format("{0}",seqValue.Value);
            }
            else
            {
                return string.Format("'{0}'",seqValue.Value);
            }
        }

        void WriteSeqValue()
        {
            if (string.IsNullOrEmpty(Task.From.Sequence)) return;
            var value = ((IDictionary<string, object>)entities.EntityList.Last())[Task.From.Sequence.ToUpper()];
            string path = Path.Combine(PublicValues.AppPath, TaskName+".seq.json");
            string ctx= JsonConvert.SerializeObject(new { SeqValue=value});
            File.WriteAllText(path,ctx);
            Task.From.SeqValue = JsonConvert.DeserializeObject<dynamic>(ctx).SeqValue;
        }

        
    }
}
