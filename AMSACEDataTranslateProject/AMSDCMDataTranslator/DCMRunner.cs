using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;
using System.Data;

namespace AMSDCMDataTranslator
{
    /// <summary>
    /// DCM主线程
    /// </summary>
   public class DCMRunner
    {
        public static void Run()
        {
            //DCM数据上传测试
            LogHelper.InfoLog("开始执行DCMTranslator");

            try
            {
                DCMFileOperator dCMFileOperator = new DCMFileOperator();
                dCMFileOperator.OperateFiles();
                DataSet ds = dCMFileOperator.DCMData;
                SqlOper sqlo = new SqlOper();
                string info = "";
                foreach (DataTable dt in ds.Tables)
                {
                    info = info + dt.TableName + "\t" + dt.Rows.Count.ToString() + "\n";
                    DCMDB2 db2 = new DCMDB2(dt.TableName);
                    db2.GetData(dt);
                    sqlo.LoadToDB2(db2);
                }
                if (!string.IsNullOrEmpty(info))
                { LogHelper.InfoLog(info); }
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("DCM:\t",e);
            }
            LogHelper.InfoLog("DCMTranslator执行完毕");

        }
    }
}
