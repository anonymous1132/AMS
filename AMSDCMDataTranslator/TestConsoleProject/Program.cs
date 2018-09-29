using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator.Helper;
using System.Data;
using System.Data.OleDb;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] path = new string[] { @"C:\Users\caojin\Desktop\test\Hlcm-WAT-format(test).xlsx", @"C:\Users\caojin\Desktop\test\Hlcm-WAT-format(test) (2).xlsx" };
            //TestFtp.Test(path);
            //Console.ReadKey();
            TestWat.Test();
            Console.WriteLine("ok");
            Console.ReadKey();
        }
    }
}
