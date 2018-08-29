using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using System.Data;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator
{
   
   public class Program
    {

       // public RunningCongfig congfig;
      
        public static void SMain(RunningCongfig congfig)
        {
            //DCM数据上传测试
            LogHelper.InfoLog("开始执行DCMTranslator");

            try
            {
                PUBLICSTRING.Catalog = congfig.CATALOG;
                PUBLICSTRING.DataSource = congfig.DATASOURCE;
                PUBLICSTRING.DirPath = congfig.DIRPATH;
                PUBLICSTRING.Interval = congfig.INTERVAL;
                PUBLICSTRING.LocalPath = congfig.LOCALPATH;
                PUBLICSTRING.Password = congfig.PASSWORD;
                PUBLICSTRING.Security = congfig.SECURITY;
                PUBLICSTRING.UserID = congfig.USERID;
                PUBLICSTRING.WorkingPath = congfig.WORKINGPATH;
                PUBLICSTRING.Port = congfig.PORT;
                FolderOperator folder = new FolderOperator();
                DataSet ds = folder.ReadFile();

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
                LogHelper.ErrorLog(e);
            }
            LogHelper.InfoLog("DCMTranslator执行完毕");

        }


     



        public static void Main(string[] args)
        {
           

          RunningCongfig congfig = new RunningCongfig();
            congfig.GetData();
            SMain(congfig);

        }


    }
}
