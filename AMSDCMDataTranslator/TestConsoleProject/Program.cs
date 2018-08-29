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

            //FTPHelper.FtpServerIP = "112.20.189.145";
            //FTPHelper.FtpUserID = "ams-file";
            //FTPHelper.FtpPassword = "16kQgN8V";
            //FTPHelper.ftpURI = "ftp://112.20.189.145:22";
            ////FTPHelper.FtpUploadFile(@" C:\Users\PUI\Desktop\csv\v20180726A_test.csv");
            //string res=   string.Join("-",FTPHelper.GetFileList(""));
            //Console.WriteLine(res);
            //Console.ReadLine();

            SFTPHelper sftp = new SFTPHelper("112.20.189.145","22","ams-file", "16kQgN8V");
            string res = string.Join("-",sftp.GetFileList("/",""));
            Console.WriteLine(res);
            Console.ReadLine();
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

            //WATRunningConfig config = new WATRunningConfig();
            //config.GetData();
            //WATRunner.Run(config);
            //Console.ReadLine();

        }
    }
}
