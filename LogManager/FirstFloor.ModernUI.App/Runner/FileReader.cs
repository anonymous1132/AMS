using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.App.Model;
using System.Data;
using Caojin.Common;

namespace FirstFloor.ModernUI.App.Runner
{
    public class FileReader
    {
        public FileReader(string filePath)
        {
            FilePath = filePath;
            PrintLogList = new List<PrintLogViewModel>();
            GetData();
        }

        public string FilePath
        {
            get;
            set;
        }

        public  List<PrintLogViewModel> PrintLogList;

        private void GetData()
        {
            DataTable dt = new ExcelOper(FilePath).GetContentFromExcel();
            int startRow = DataTableHelper.RowIndex(dt,"执行时间",0);
            //生成模型
            for (int i = startRow+1; i < dt.Rows.Count; i++)
            {
                PrintLogViewModel printLog = new PrintLogViewModel();
                printLog.ExecuteTime =DateTimeBuilder.dtDateTime(dt.Rows[i][0].ToString());
                printLog.UserName = dt.Rows[i][1].ToString();
                printLog.IPAddress = dt.Rows[i][2].ToString();
                printLog.ComputerName = dt.Rows[i][3].ToString();
                printLog.MACAddress = dt.Rows[i][4].ToString();
                printLog.ProgramName = dt.Rows[i][5].ToString();
                printLog.PrintType = dt.Rows[i][6].ToString();
                printLog.FileName = dt.Rows[i][7].ToString();
                if (ShareDataEntity.GetSingleton().PrintLogCollection.Where(p=>p.ExecuteTime==printLog.ExecuteTime&&p.UserName==printLog.UserName&&p.IPAddress==printLog.IPAddress&&p.FileName==printLog.FileName).ToList().Count==0)
                {
                    PrintLogList.Add(printLog);
                }
            }


        }
    }
}
