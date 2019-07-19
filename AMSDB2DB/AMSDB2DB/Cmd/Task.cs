using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Threading;

namespace AMSDB2DB.Cmd
{
    public static class Task
    {
        public static void MainWork(string[] args)
        {
            var t=  new System.Diagnostics.StackTrace().GetFrame(0).GetMethod().ReflectedType;
            bool res = ConsoleCmds.Route(t,args);
            if (!res) { HelpDetail(); }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("task操作命令");
        }

        public static void HelpDetail()
        {
            ShowHelp();
            Console.WriteLine("Task [List|Remove|Add] [TaskName]");
            Console.WriteLine();
        }

        public static void List(string[] args)
        {
            Console.WriteLine("\tTaskName\t\tState\t\tInterval");
            if (args.Length == 0)
            {
                //查询所有任务
                foreach (var task in PublicValues.Tasks)
                {
                    string name = task.Item1;
                    string state = "";
                    string interval = (task.Item2.Interval/60).ToString()+"min";
                    var find_handles = PublicValues.Handles.Where(w => w.TaskName.Contains(name));
                    if (!find_handles.Any())
                    {
                        state = "removed";
                    }
                    else
                    {
                        var handle = find_handles.First();
                        state = handle.Active ? "active" : "removing";
                    }
                    Console.WriteLine("\t{0}\t\t{1}\t\t{2}",name,state,interval);
                }
            }
            else
            {
                string name = "";
                //查询指定任务
                if (args[0] != "\"" && args[0].First() == '"')
                {
                    name = string.Join("", args);
                    name = name.Split('"')[1];
                }
                else { name = args[0]; }
                                    var find_tasks = PublicValues.Tasks.Where(w => w.Item1 == name);
                    if (find_tasks.Any())
                    {
                        string interval = (find_tasks.First().Item2.Interval / 60).ToString() + "min";
                        string state = "";
                        var find_handles = PublicValues.Handles.Where(w => w.TaskName.Contains(name));
                        if (!find_handles.Any())
                        {
                            state = "removed";
                        }
                        else
                        {
                            var handle = find_handles.First();
                            state = handle.Active ? "active" : "removing";
                        }
                        Console.WriteLine("\t{0}\t\t{1}\t\t{2}", name, state, interval);
                    }
            }

        }

        public static void Remove(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Task Remove [TaskName]\n\tFor Example:Task Remove TaskA");
                Console.WriteLine("If your task name contains space,you can remove it like this:Task Remove \"Task A\"");
            }
            else
            {
                string name = "";
                if (args[0] != "\"" && args[0].First() == '"')
                {
                    name = string.Join("", args);
                    name = name.Split('"')[1];
                }
                else
                {
                    name = args[0];
                }
                var find_tasks = PublicValues.Tasks.Where(w => w.Item1 == name);
                if (find_tasks.Any())
                {
                    var find_handles = PublicValues.Handles.Where(w => w.TaskName.Contains(name));
                    if (find_handles.Any())
                    {
                        var handle = find_handles.First();
                        handle.RemoveTask();
                        Console.WriteLine("Task:{0} changed state to \"removing\",it will last to the running task end!",name);
                        while (handle.TaskThread.ThreadState != ThreadState.Aborted&&handle.TaskThread.ThreadState!=ThreadState.Stopped)
                        {
                            Thread.Sleep(500);
                        }
                        Console.WriteLine("Task:{0} changed state to \"removed\"",name);
                    }
                    else
                    {
                        Console.WriteLine("Task:{0}'s state is \"removed\",it's no need to remove it again!",name);
                    }
                }
                else
                {
                    Console.WriteLine("Task:{0} not found",name);
                }
            }
        }

        public static void Add(string[]args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Task Add [TaskName]\n\tFor Example:Task Add TaskA");
                Console.WriteLine("If your task name contains space,you can add it like this:Task Remove \"Task A\"");
            }
            else
            {
                string name = "";
                if (args[0] != "\"" && args[0].First() == '"')
                {
                    name = string.Join("", args);
                    name = name.Split('"')[1];
                }
                else
                {
                    name = args[0];
                }
                var find_tasks = PublicValues.Tasks.Where(w => w.Item1 == name);
                if (find_tasks.Any())
                {
                    var find_handles = PublicValues.Handles.Where(w => w.TaskName.Contains(name));
                    if (find_handles.Any())
                    {
                        var handle = find_handles.First();
                        if (handle.Active)
                        {
                            Console.WriteLine("Task:{0}'s state is \"active\",it's no need to add it again!", name);
                        }
                        else
                        {
                            handle.RemoveTaskCancel();
                            Console.WriteLine("Task:{0} state changed to \"active\"", name);
                        } 
                    }
                    else
                    {
                        try
                        {
                            Program.StartOneTask(new TaskHandler(find_tasks.First().Item2, name));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error:{0}",ex.Message);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Task:{0} not found", name);
                }
            }
        }
    }
}
