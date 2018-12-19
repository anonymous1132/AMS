using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.App.Model;
using Caojin.Common;

namespace FirstFloor.ModernUI.App
{
    public class ShareDataEntity
    {
        //单例模式
        private static ShareDataEntity _singleton;
        public static ShareDataEntity GetSingleton()
        {
            if (_singleton == null)
            {
                _singleton = new ShareDataEntity();
            }
            return _singleton;
        }
        //构造函数
        public ShareDataEntity()
        {
            PrintLogCollection = new ObservableCollection<PrintLogViewModel>();
            MailSmtpCollection = new ObservableCollection<MailSmtpModel>();
            MailQueueCollection = new ObservableCollection<MailQueueModel>();
            MailQueueReserverCollection = new ObservableCollection<MailQueueReserverEntity>();
            MailQueueViewCollection = new ObservableCollection<MailQueueViewModel>();
        }

        #region PrintLog
        /// <summary>
        ///获取 PrintLogViewModel数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<PrintLogViewModel> PrintLogCollection;
        private SqlHelper sqlo = new SqlHelper();
        private void GetPrintLogCollection()
        {
            string sql = "select * from T_PrintLog";
            sqlo.getSomeDate(sql);
            foreach (DataRow dr in sqlo.dt.Rows)
            {
                PrintLogViewModel printModel = new PrintLogViewModel(dr["GUID"].ToString())
                {
                    ExecuteTime = (DateTime)dr["ExecuteTime"],
                    UserName = dr["UserName"].ToString(),
                    IPAddress = dr["IPAddress"].ToString(),
                    ComputerName = dr["ComputerName"].ToString(),
                    MACAddress = dr["MACAddress"].ToString(),
                    ProgramName = dr["ProgramName"].ToString(),
                    PrintType = dr["PrintType"].ToString(),
                    FileName = dr["FileName"].ToString()
                };
                PrintLogCollection.Add(printModel);
            }
        }

        public void UpdatePrintLogCollection()
        {
            PrintLogCollection.Clear();
            GetPrintLogCollection();
        }

        public void AddNewModelToPrintLogCollection(List<PrintLogViewModel> printLogList)
        {
            int c = printLogList.Count / 50;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 50; j++)
                {
                    var printLog = printLogList[i * 50 + j];
                    string temp = string.Format("('{0}','{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}'),", printLog.Guid, DateTimeBuilder.CommonStrDateTime(printLog.ExecuteTime), printLog.UserName, printLog.IPAddress, printLog.ComputerName, printLog.MACAddress, printLog.ProgramName, printLog.PrintType, printLog.FileName);

                    value.Append(temp);

                }
                string sql = string.Format("insert into T_PrintLog values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (printLogList.Count - c * 50 > 0)
            {
                string value2 = "";
                for (int i = c * 50; i < printLogList.Count; i++)
                {
                    var printLog = printLogList[i];
                    string temp = string.Format("('{0}','{1}',N'{2}',N'{3}',N'{4}',N'{5}',N'{6}',N'{7}',N'{8}'),", printLog.Guid, DateTimeBuilder.CommonStrDateTime(printLog.ExecuteTime), printLog.UserName, printLog.IPAddress, printLog.ComputerName, printLog.MACAddress, printLog.ProgramName, printLog.PrintType, printLog.FileName);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_PrintLog values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }

            UpdatePrintLogCollection();
        }

        public void DeleteModelInPrintLogCollection(PrintLogViewModel printLog)
        {
            string sql = "delete from T_PrintLog where GUID ='" + printLog.Guid + "'";
            sqlo.getSomeDate(sql);
            PrintLogCollection.Remove(printLog);
        }

        public void DeleteModelInPrintLogCollection(PrintLogViewModel[] printLogs)
        {
            var value = string.Join("", printLogs.Select(p => "'" + p.Guid + "',").ToList());
            string sql = string.Format("delete from T_PrintLog where GUID in ({0})", value.TrimEnd(','));
            sqlo.getSomeDate(sql);
            UpdatePrintLogCollection();
        }
        #endregion

        #region MailSmtpLog
        /// <summary>
        /// 获取 MailSmtpModel数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailSmtpModel> MailSmtpCollection;

        private void GetMailSmtpCollection()
        {
            string sql = "select * from T_SmtpLog";
            sqlo.getSomeDate(sql);
            foreach (DataRow dr in sqlo.dt.Rows)
            {
                MailSmtpModel smtpModel = new MailSmtpModel(dr["GUID"].ToString());
                smtpModel.StartTime = (DateTime)dr["StartTime"];
                smtpModel.EndTime = (DateTime)dr["EndTime"];
                smtpModel.IPAddress = dr["IPAddress"].ToString();
                smtpModel.Mask = Convert.ToInt32(dr["Mask"].ToString());
                smtpModel.SendMailAddress = dr["SendMailAddress"].ToString();
                smtpModel.ReserveMailAddress = dr["ReserveMailAddress"].ToString();
                smtpModel.MaxSize = Convert.ToDouble(dr["MaxSize"]);
                smtpModel.ReserveSize = Convert.ToDouble(dr["ReserveSize"]);
                smtpModel.Details = dr["Details"].ToString();
                MailSmtpCollection.Add(smtpModel);
            }
        }

        public void UpdateMailSmtpCollection()
        {
            MailSmtpCollection.Clear();
            GetMailSmtpCollection();
        }

        public void AddNewModelToMailSmtpCollection(List<MailSmtpModel>smtpList)
        {
            int c = smtpList.Count / 5;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 5; j++)
                {
                    var smtpLog = smtpList[i * 5 + j];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',N'{5}',N'{6}',{7},{8},N'{9}'),", smtpLog.Guid, DateTimeBuilder.CommonStrDateTime(smtpLog.StartTime), DateTimeBuilder.CommonStrDateTime(smtpLog.EndTime), smtpLog.Mask.ToString(), smtpLog.IPAddress, smtpLog.SendMailAddress, smtpLog.ReserveMailAddress, smtpLog.MaxSize.ToString(), smtpLog.ReserveSize.ToString(),smtpLog.Details);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_SmtpLog values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (smtpList.Count - c * 5 > 0)
            {
                string value2 = "";
                for (int i = c * 5; i < smtpList.Count; i++)
                {
                    var smtpLog = smtpList[i];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',N'{5}',N'{6}',{7},{8},N'{9}'),", smtpLog.Guid, DateTimeBuilder.CommonStrDateTime(smtpLog.StartTime), DateTimeBuilder.CommonStrDateTime(smtpLog.EndTime), smtpLog.Mask.ToString(), smtpLog.IPAddress, smtpLog.SendMailAddress, smtpLog.ReserveMailAddress, smtpLog.MaxSize.ToString(), smtpLog.ReserveSize.ToString(), smtpLog.Details);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_SmtpLog values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }

            UpdateMailSmtpCollection();
        }

        public void DeleteModelInMailSmtpCollection(MailSmtpModel smtpModel)
        {
            string sql = "delete from T_SmtpLog where GUID ='" + smtpModel.Guid + "'";
            sqlo.getSomeDate(sql);
            MailSmtpCollection.Remove(smtpModel);
        }

        public void DeleteModelInMailSmtpCollection(MailSmtpModel[] smtpModels)
        {
            var value =string.Join("",smtpModels.Select(p => "'" + p.Guid + "',").ToList());
            string sql = string.Format("delete from T_SmtpLog where GUID in ({0})", value.TrimEnd(','));
            sqlo.getSomeDate(sql);
            UpdateMailSmtpCollection();
        }

        #endregion

        #region MailQueueLog
        /// <summary>
        /// 获取 MailQueueModel数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailQueueModel> MailQueueCollection;

        private void GetMailQueueCollection()
        {
            string sql = "select * from T_QueueLog";
            sqlo.getSomeDate(sql);
            foreach (DataRow dr in sqlo.dt.Rows)
            {
                MailQueueModel queueModel = new MailQueueModel(dr["GUID"].ToString());
                queueModel.StartTime = (DateTime)dr["StartTime"];
                queueModel.EndTime = (DateTime)dr["EndTime"];
                queueModel.Mask = Convert.ToInt32(dr["Mask"]);
                queueModel.SendMailAddress = dr["SendMailAddress"].ToString();
                queueModel.SendSize = Convert.ToDouble(dr["SendSize"]);
                queueModel.Details = dr["Details"].ToString();
                queueModel.MailID = dr["MailID"].ToString();
                MailQueueCollection.Add(queueModel);
            }

        }

        public void UpdateMailQueueCollection()
        {
            MailQueueCollection.Clear();
            GetMailQueueCollection();
        }

        public void AddNewModelToMailQueueCollection(List<MailQueueModel> queueList)
        {
            int c = queueList.Count / 50;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 50; j++)
                {
                    var queueLog = queueList[i * 50 + j];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',{5},N'{6}',N'{7}'),", queueLog.Guid, DateTimeBuilder.CommonStrDateTime(queueLog.StartTime), DateTimeBuilder.CommonStrDateTime(queueLog.EndTime), queueLog.Mask.ToString(), queueLog.SendMailAddress, queueLog.SendSize.ToString(), queueLog.Details,queueLog.MailID);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_QueueLog values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (queueList.Count - c * 50 > 0)
            {
                string value2 = "";
                for (int i = c * 50; i < queueList.Count; i++)
                {
                    var queueLog = queueList[i];
                    string temp = string.Format("('{0}','{1}','{2}',{3},N'{4}',{5},N'{6}',N'{7}'),", queueLog.Guid, DateTimeBuilder.CommonStrDateTime(queueLog.StartTime), DateTimeBuilder.CommonStrDateTime(queueLog.EndTime), queueLog.Mask.ToString(), queueLog.SendMailAddress, queueLog.SendSize.ToString(), queueLog.Details,queueLog.MailID);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_QueueLog values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }

            UpdateMailQueueCollection();
        }

        public void DeleteModelInMailQueueCollection(MailQueueModel queueModel)
        {
            string sql = "delete from T_QueueLog where GUID ='" + queueModel.Guid + "'";
            sqlo.getSomeDate(sql);
            MailQueueCollection.Remove(queueModel);
        }

        public void DeleteModelInMailQueueCollection(MailQueueModel[] queueModels)
        {
            var value = string.Join("", queueModels.Select(p => "'" + p.Guid + "',").ToList());
            string sql = string.Format("delete from T_QueueLog where GUID in ({0})", value.TrimEnd(','));
            sqlo.getSomeDate(sql);
            UpdateMailQueueCollection();
        }
        #endregion

        #region MailQueueReserver
        /// <summary>
        /// 获取 MailQueueModel的Reserver数据集合，所有查询界面的数据从此获取
        /// </summary>
        public ObservableCollection<MailQueueReserverEntity> MailQueueReserverCollection;
        
        private void GetMailQueueReserverCollection()
        {
            string sql = "select * from T_QueueLog_ReserverDetail";
            sqlo.getSomeDate(sql);
            foreach (DataRow dr in sqlo.dt.Rows)
            {
                MailQueueReserverEntity queueReserverEntity = new MailQueueReserverEntity(dr["GUID"].ToString());
                queueReserverEntity.ReserverAddress = dr["ReserverAddress"].ToString();
                queueReserverEntity.IsLocalServer = (bool)dr["IsLocalServer"];
                queueReserverEntity.IsSuccessful = (bool)dr["IsSuccessful"];
                queueReserverEntity.OutSideServerAddress = dr["OutSideServerAddress"].ToString();
                queueReserverEntity.SetMailGuid(dr["MailGuid"].ToString());
                MailQueueReserverCollection.Add(queueReserverEntity);
            }
        }

        public void UpdateMailQueueReserverCollection()
        {
            MailQueueReserverCollection.Clear();
            GetMailQueueReserverCollection();
        }

        public void AddNewModelToMailQueueReserverCollection(List<MailQueueReserverEntity> reserverEntities)
        {
            int c = reserverEntities.Count / 50;
            for (int i = 0; i < c; i++)
            {
                StringBuilder value = new StringBuilder();
                for (int j = 0; j < 50; j++)
                {
                    var reserverEntity = reserverEntities[i * 50 + j];
                    string temp = string.Format("('{0}','{1}','{2}','{3}','{4}','{5}'),", reserverEntity.Guid, reserverEntity.ReserverAddress, reserverEntity.IsLocalServer, reserverEntity.IsSuccessful, reserverEntity.OutSideServerAddress,reserverEntity.MailGuid);
                    value.Append(temp);
                }
                string sql = string.Format("insert into T_QueueLog_ReserverDetail values {0}", value.ToString().TrimEnd(','));
                sqlo.getSomeDate(sql);
            }

            if (reserverEntities.Count - c * 50 > 0)
            {
                string value2 = "";
                for (int i = c * 50; i < reserverEntities.Count; i++)
                {
                    var reserverEntity = reserverEntities[i];
                    string temp = string.Format("('{0}','{1}','{2}','{3}','{4}','{5}'),", reserverEntity.Guid, reserverEntity.ReserverAddress, reserverEntity.IsLocalServer, reserverEntity.IsSuccessful, reserverEntity.OutSideServerAddress,reserverEntity.MailGuid);
                    value2 += temp;
                }
                string sql2 = string.Format("insert into T_QueueLog_ReserverDetail values {0}", value2.TrimEnd(','));
                sqlo.getSomeDate(sql2);
            }
            UpdateMailQueueReserverCollection();
        }
        #endregion

        public ObservableCollection<MailQueueViewModel> MailQueueViewCollection;
        public void GetMailQueueViewCollection()
        {
            MailQueueViewCollection.Clear();
            MailQueueCollection.ToList().ForEach(p=>MailQueueViewCollection.Add(new MailQueueViewModel() {QueueModel=p ,ReserverEntities=MailQueueReserverCollection.Where(a=>a.MailGuid==p.Guid).ToList()}));
        }

    }
}
