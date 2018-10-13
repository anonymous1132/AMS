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
    public class HlcmWATRunner
    {
        public HlcmWATRunner()
        { }

        public static void Run()
        {
            LogHelper.WATInfoLog("开始执行HlcmTranslator");
            HlcmWAT hlcm = new HlcmWAT();
            HlcmWATFileOperator fileOperator = new HlcmWATFileOperator(hlcm);
            try
            {
                fileOperator.OperateFiles();
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("Hlcm:\t", e);
            }
            //更新生成TestID的xml配置
            if (HlcmSetting.IsDicUpdated)
            {
                DataSet ds = new DataSet("HlcmSetting");
                DataTable dt = new DataTable("TestDictionary");
                dt.Columns.Add("TestDescription",typeof(string));
                dt.Columns.Add("TestID", typeof(string));
                foreach(var dic in HlcmSetting.DicTestID2Desc)
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = dic.Key;
                    dr[1] = dic.Value.ToString();
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
                ds.WriteXml(HlcmSetting.DicConfigPath);
                HlcmSetting.IsDicUpdated = false;
            }
            LogHelper.WATInfoLog("HlcmTranslator执行完毕");
        }
    }
}
