using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDB2DB.Cmd
{
    public static class Clear
    {
        public static void MainWork(string[] args)
        {
            Console.Clear();
        }

        public static void ShowHelp()
        {
            Console.WriteLine("清除当前屏幕");
        }

        public static void HelpDetail()
        {
            ShowHelp();
            Console.WriteLine();
        }
    }
}
