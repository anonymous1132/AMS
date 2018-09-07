using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;

namespace TestConsoleProject
{
   public class TestExcelDate
    {
        public static void Test()
        {
            string Path = @"C:\Users\PUI\Desktop\hlcm2.xlsx";
            //string datetest = ExcelHelper.GetContent(Path).Tables[0].DefaultView[10][2].ToString();
            DataRow dr = ExcelHelper.GetContent(Path).Tables[0].Rows[10];
            string datatest = ((DateTime)dr[2]).ToString("yyyy/MM/dd") + "_" + ((DateTime)dr[3]).ToString("hh:mm:ss");
            Console.WriteLine(datatest);
        }
    }
}
