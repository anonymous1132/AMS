using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public class EDC_Lot_Summary
    {

        public EDC_Lot_Summary()
        {
            GetData();
            InsertToDB();
            WriteXmlConfig();
        }
        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private string ExeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }
        private readonly string Config = @"App\config\edc_lot_summary.config";
        private string ConfigPath
        {
            get { return ExeDirctory + Config; }
        }

        private string GetLastDBLine()
        {
            DataTable dt = XmlHelper.GetTable(ConfigPath, XmlHelper.XmlType.File, "EDCLotSummary");
            return dt.DefaultView[0][0].ToString();
        }

        private string LastDbLineThisQuery;

        private List<EDC_Lot_SingleSummary> singleSummaries = new List<EDC_Lot_SingleSummary>();

        public  void WriteXmlConfig()
        {
            DataSet ds = new DataSet("ACE");
            DataTable dt = new DataTable("EDCLotSummary");
            dt.Columns.Add("Sequence");
            DataRow dr = dt.NewRow();
            dr[0] = LastDbLineThisQuery;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml(ConfigPath);
        }

        public void GetData()
        {
            string sql = "";
            try
            {
                sql = string.Format("select product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME,N from " +
                    "(select product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME,SUM(site_count) n FROM acme.edc_wafer_summary  GROUP BY product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME HAVING measure_time>=to_date('{0}', 'yyyy/mm/dd HH24:mi:ss'))" +
                    "  order by measure_time", GetLastDBLine());
            }
            catch (Exception)
            {
                sql = string.Format("select product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME,N from " +
                    "(select product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME,SUM(site_count) n FROM acme.edc_wafer_summary  GROUP BY product_id,lot_id,EDC_PARAMETER_ID,MEASURE_TIME HAVING measure_time>to_date('2018-04-17 16:02:51', 'yyyy/mm/dd HH24:mi:ss'))" +
                    "  order by measure_time");
            }
            DataTable dt= OracleHelper.ExecuteDataTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                try
                {
                    EDC_Lot_SingleSummary singleSummary = new EDC_Lot_SingleSummary();
                    singleSummary.GetData(dr);
                    singleSummaries.Add(singleSummary);
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog("EDC_LOT_SUMMARY",e);
                }
            }
            LastDbLineThisQuery= dt.DefaultView[dt.Rows.Count - 1]["MEASURE_TIME"].ToString();
        }

        public void InsertToDB()
        {
            string sql = "";
            string msg = "";
            foreach (EDC_Lot_SingleSummary singleSummary in singleSummaries)
            {
                sql =string.Format("select * from acme.edc_lot_summary where product_id={0} and lot_id= {1} and edc_parameter_id={2} and measure_time=to_date('{3}','yyyy/mm/dd HH24:mi:ss')",
                singleSummary.ProductID,singleSummary.Lot_ID,singleSummary.EDC_Parameter_ID,singleSummary.MeasureDateTime);
                DataTable dt = OracleHelper.ExecuteDataTable(sql);
                if (dt.DefaultView.Count == 0)
                {
                    try
                    {
                        sql = singleSummary.InsertSql();
                        int c = OracleHelper.ExecuteNonQuery(sql);
                        if (c > 0)
                        {
                            msg += sql + "\n";
                        }
                        else
                        {
                            LogHelper.ErrorLog("Inline:\t" + sql + "\t 导入失败");
                        }
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog("Inline:\t" + sql + "\t 导入失败",e);
                    }
                }
            }
            if (!string.IsNullOrEmpty(msg))
            {
                msg += "以上insert成功";
                LogHelper.InlineInfoLog(msg);
            }

        }

    }
}
