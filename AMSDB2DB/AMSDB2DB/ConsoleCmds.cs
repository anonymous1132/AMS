using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AMSDB2DB
{
    public static class ConsoleCmds
    {
       public static void Help(string[]args)
        {
            AMSDB2DB.Cmd.Help.MainWork(args);
        }

       public static void Task(string[]args)
        {
            AMSDB2DB.Cmd.Task.MainWork(args);
        }

        public static void Exit(string[]args)
        {
            AMSDB2DB.Cmd.Exit.MainWork(args);
        }

        public static void Clear(string[]args)
        {
            AMSDB2DB.Cmd.Clear.MainWork(args);
        }

        public static readonly Dictionary<string, string> Cmds = new Dictionary<string, string>()
        {
            { "help","提供帮助信息"},
            { "task","task操作命令"},
            { "exit","退出本程序，必须在所有任务被干掉之后才可以执行"},
            { "clear","清理当前控制台屏幕"},
        };

        public static void Route(string cmd)
        {
            string[] temp = cmd.Split(' ');
            Type t = typeof(ConsoleCmds);
            var ms= t.GetMethods().Where(w=>w.Name.ToLower()==temp[0].ToLower());
            MethodInfo mt = ms.Any() ? ms.First() : null;
            if (mt == null) return;
            var temp2 = temp.Where(w =>!string.IsNullOrEmpty(w)).ToArray();
            int len = temp2.Count() - 1;
            string[] args = new string[len];
            for (int i = 0; i < len; i++)
            {
                args[i] = temp2[i+1];
            }
            mt.Invoke(null, new object[] { args });
        }

        public static bool Route(Type t, string[] args)
        {
            if (args == null || args.Length == 0) return false;
            var ms = t.GetMethods().Where(w => w.Name.ToLower() == args[0].ToLower());
            MethodInfo mt = ms.FirstOrDefault();
            if (mt == null) return false;
            var temp = args.Where(w => !string.IsNullOrEmpty(w)).ToArray();
            int len = temp.Count() - 1;
            string[] arg = new string[len];
            for (int i = 0; i < len; i++)
            {
                arg[i] = temp[i + 1];
            }
            mt.Invoke(null, new object[] { arg });
            return true;
        }

    }
}
