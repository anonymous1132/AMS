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
            try
            {
                //if (args.Length == 1)
                //{
                //    DefectTouch.Run("*" + args[0] + "*");
                //}
                //else
                //{
                //    Console.WriteLine("请输入要更新的文件对应的LotID：");
                //    string lot = Console.ReadLine();
                //    DefectTouch.Run("*" + lot + "*");
                //}
                //Console.WriteLine("ok");

                  DefectUploadFile.UploadFile();
            }
            catch (Exception ex)
            {
                Console.WriteLine("error\n"+ex.Message);
            }
        
            Console.ReadLine();
        }
    }
}
