using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDB2DB;
using System.Reflection;

namespace AMSDB2DB.Cmd
{
    public static class Help
    {
        public static void HelpDetail()
        {
            ShowHelp();
            Console.WriteLine("HELP [command]\n\tcommand - 显示该命令的帮助信息");
        }

        public static void ShowHelp()
        {
            Console.WriteLine("提供帮助信息");
            Console.WriteLine();
        }

        public static void MainWork(string[]args)
        {
            string space = MethodBase.GetCurrentMethod().DeclaringType.Namespace;
            if (args.Length == 0)
            {
                var types = Assembly.GetExecutingAssembly().GetTypes().Where(w => w.Namespace == space);
                foreach (var t in types)
                {
                    MethodInfo mt = t.GetMethod("ShowHelp");
                    if (mt != null)
                    {
                        Console.Write(t.Name+"\t\t");
                        mt.Invoke(null, null);
                    }
                }
            }
            else
            {
               Type t = Type.GetType(space+"."+args[0], false, true);
                if (t != null)
                {
                    MethodInfo mt = t.GetMethod("HelpDetail");
                    if (mt != null)
                    {
                        mt.Invoke(null, null);
                    }
                }
                else
                {
                    Console.WriteLine("帮助工具不支持此命令。");
                }
            }
        }
    }
}
