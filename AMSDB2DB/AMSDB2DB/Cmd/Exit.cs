using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDB2DB.Cmd
{
    public static class Exit
    {
        public static void MainWork(string[]args)
        {
            if (PublicValues.Handles.Any())
            {
                Console.WriteLine("只有当所有任务都是removed状态才会执行");
                Task.List(new string[0]);
            }
            else
            {
                PublicValues.WantExit = true;
                Console.WriteLine("正在退出，请稍后");
                System.Threading.Thread.Sleep(1000);
            }
        }

        public static void ShowHelp()
        {
            Console.WriteLine("退出本程序，只有当所有任务都是removed状态才会执行");
        }

        public static void HelpDetail()
        {
            ShowHelp();
            Console.WriteLine();
        }
    }
}
