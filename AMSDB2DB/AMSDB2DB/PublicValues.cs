using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using CommonUtilsLibrary.Utils;

namespace AMSDB2DB
{
    public static class PublicValues
    {
        public static string AppPath { get; set; }
        public static List<Tuple<string,TaskEntity>> Tasks { get; set; }
        public static List<TaskHandler> Handles = new List<TaskHandler>();
        public static void GetAppPath()
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string fullPath = Path.GetDirectoryName(exePath);
            AppPath = Path.Combine(fullPath,"App");
        }
        public static void GetTasks()
        {
            string text = File.ReadAllText(Path.Combine(AppPath,"service.json"));
            var list = JsonConvert.DeserializeObject<dynamic>(text);
            Tasks = new List<Tuple<string, TaskEntity>>();
            if (list.Tasks.Count==0)
            {
                return;
            }
            foreach (string task in list.Tasks)
            {
                try
                {
                    if (Tasks.Any(a => a.Item1 == task)) throw new Exception("任务池中已经存在同名任务");
                    string taskPath = Path.Combine(AppPath, task + ".json");
                    string ctx = File.ReadAllText(taskPath);
                    string seqPath = Path.Combine(AppPath, task + ".seq.json");
                    var taskEntity = JsonConvert.DeserializeObject<TaskEntity>(ctx);
                    if (File.Exists(seqPath))
                    {
                      string  ctx_seq = File.ReadAllText(seqPath);
                      taskEntity.From.SeqValue = JsonConvert.DeserializeObject<dynamic>(ctx_seq).SeqValue;
                    }
                    Tasks.Add(Tuple.Create<string,TaskEntity>(task,taskEntity));
                }
                catch (Exception ex)
                {
                    LogUtils.ErrorLog(string.Format("初始化任务：{0}失败",task),ex);
                }
            }
        }
        public static bool WantExit = false;
    }
}
