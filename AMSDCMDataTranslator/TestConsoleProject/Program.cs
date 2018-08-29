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
            //RunningCongfig congfig = new RunningCongfig();
            //congfig.SetOracleDefaultValue();

            //string sql = "select lotno from lot where lot_id='2023'";
            // DataTable dt=  OracleHelper.ExecuteDataTable(sql,new Oracle.ManagedDataAccess.Client.OracleParameter());
            //Console.WriteLine(dt.DefaultView[0][0].ToString());
            //Console.ReadLine();

            //test ftp

            //FTPHelper.FtpServerIP = "10.132.0.38";
            //FTPHelper.FtpUserID = "ace";
            //FTPHelper.FtpPassword = "Ams_ace";
            //FTPHelper.ftpURI= "ftp://10.132.0.38/data/siff/Etest/import/";
            //FTPHelper.FtpUploadFile(@" C:\Users\PUI\Desktop\csv\v20180726A_test.csv");
            //Console.WriteLine("ok");
            //Console.Read();

            //test siff writer
            //WATFileOperator fileOperator = new WATFileOperator();
            //fileOperator.ReadFiles();
            //Console.Write("ok");

            //Class1.str1 = "123";
            //Class1 class1 = new Class1();
            //class1.Echo();
            //Class1.str1 = "456";
            //class1.Echo();
            //Console.ReadKey();
           
            WATRunningConfig config = new WATRunningConfig();
            config.GetData();
            WATRunner.Run(config);
            Console.ReadLine();

        }
    }
}
