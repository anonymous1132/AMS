using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilsLibrary.Utils;
using System.Threading;

namespace AMSDB2DB
{
   public class Program
    {
       public static void Main(string[] args)
        {
            RunTask();

        }

        static void GetPutIn()
        {
            Console.Write("$ ");
            var text = Console.ReadLine();
            ConsoleCmds.Route(text.Trim());
        }

        static void KeepMsg(string msg,bool errorLog=false)
        {
            Console.WriteLine(msg);
            if (errorLog)
            {
                LogUtils.ErrorLog(msg);
            }
            else
            {
                LogUtils.InfoLog(msg);
            }
        }

        static void RunTask()
        {
            KeepMsg("AMSDB2DB已启动");
            PublicValues.GetAppPath();
            PublicValues.GetTasks();
            Console.Title = "DBCopy Task Console";
            //Tasks
            foreach (var task in PublicValues.Tasks)
            {
                var taskHandler = new TaskHandler(task.Item2, task.Item1);
                StartOneTask(taskHandler);
            }
            while (Console.CursorTop <= 1)
            {
                Thread.Sleep(1000);
            }
            while (!(PublicValues.Handles.Count== 0&&PublicValues.WantExit))
            {
                GetPutIn();
            }
            KeepMsg("退出AMSDB2DB");
        }

        public static void StartOneTask(TaskHandler taskHandler)
        {
            taskHandler.EventAfterRun += new TaskHandler.delegateAfterRun(() => { KeepMsg(string.Format("Task:{0}已停止", taskHandler.TaskName)); PublicValues.Handles.Remove(taskHandler); });

            taskHandler.EventBeforeRun += new TaskHandler.delegateBeforeRun(() => { KeepMsg(string.Format("Task:{0}已开启线程", taskHandler.TaskName)); PublicValues.Handles.Add(taskHandler); });

            taskHandler.EventWhenError += new TaskHandler.delegateWhenError(error => { KeepMsg(string.Format("Task:{0}发现异常信息；Error Message:{1}", taskHandler.TaskName, error), true);  });

            taskHandler.EventWhenTaskDone += new TaskHandler.delegateWhenTaskDone(ts => { KeepMsg(string.Format("{1}:   Task:{0}Done! Duration:{2}s", taskHandler.TaskName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Math.Round(ts.TotalSeconds, 5))); });
            ThreadStart t = new ThreadStart(taskHandler.RunTaskMainThread);
            
            taskHandler.TaskThread= new Thread(t) { IsBackground=true};

            taskHandler.TaskThread.Start();
        }
    }
}
