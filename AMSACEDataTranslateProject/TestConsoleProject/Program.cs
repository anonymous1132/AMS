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
            TestWIP.Test();
           // WIPFtpOperator.UploadFile(@"C:\Users\caojin\Desktop\RPT需求申请单.xlsx");
           //// WIPFtpOperator.UploadDefectFile(@"C:\Users\caojin\Desktop\RPT需求申请单.xlsx");
           // ChamberFtpOperator.UploadFile(@"C:\Users\caojin\Desktop\RPT需求申请单.xlsx");

            //WIPFtpOperator.UploadDefectFile(@"C:\Users\caojin\Desktop\test.txt");
        }
    }
}
