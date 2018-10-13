using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public class HlcmWAT : Etest
    {

        public HlcmWAT()
        {

        }
        
        /// <summary>
        /// 获取Hlcm数据文件中的数据
        /// </summary>
        /// <param name="filePath">数据文件全路径</param>
        /// <param name="specPath">SpecFile文件全路径</param>
        public override void GetData()
        {
            ParameterList = new List<string>();
            //  ExcelOper excelOper = new ExcelOper(filePath);
            //  DataTable dt = excelOper.GetContentFromExcel();
            NPOIExcelHelper excelHelper = new NPOIExcelHelper(FilePath);
            DataTable dt = excelHelper.ExcelToDataTable("Raw data",true);
            int j= dt.Columns.Count;
            if (dt.Columns.Count < 8 || dt.Columns[0].ColumnName != "LOT ID" || dt.Columns[1].ColumnName != "PRODUCT NAME" || dt.Columns[5].ColumnName != "SITE" || dt.Rows.Count < 3)
            {
                throw new Exception(FilePath + "文件格式错误");
            }
            lot_run = new Etest_Lot_Run
            {
                etest_limits = new List<Etest_Limit>()
            };

            //参数列表赋值
            for (int i = 7; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Substring(0, 1) == "F" && dt.Columns[i].ColumnName.Length < 4)
                {
                    break;
                }
                else
                { ParameterList.Add(dt.Columns[i].ColumnName); }
            }

            for (int i = 0; i < 2; i++)
            {
                lot_run.etest_limits = GetEtestLimits(dt.Rows[i], lot_run.etest_limits);
            }

            for (int i = 2; i < dt.Rows.Count; i++)
            {
                SetLotRun(dt.Rows[i]);
            }
        }

        /// <summary>
        /// 参数集合
        /// </summary>
        private List<string> ParameterList ;
        /// <summary>
        /// Lot_Run赋值
        /// </summary>
        /// <param name="dr">行数据</param>
        private void SetLotRun(DataRow dr)
        {
            if (string.IsNullOrEmpty(lot_run.Lot))
            {
                lot_run.Lot = dr[0].ToString();
                lot_run.Product = dr[1].ToString();
                //debug:需要根据日期格式来判断
                lot_run.MeasureTime = FixHlcmDate(dr[2].ToString()) + "_" + FixTime(dr[3]);
                lot_run.Operation = "HLCMWAT";
                lot_run.SpecfileName = FileName;
                lot_run.TestProgram = lot_run.Product;
                lot_run.Operator = "";
                lot_run.ProbeCard = "";
                lot_run.FlatOrientation = "DOWN";
                lot_run.Owner = "";
            }
            Etest_Wafer_Run wafer_Run = GetWaferRun(dr);
            
            Etest_Site site = new Etest_Site()
            {
                SiteID = dr[5].ToString(),
                SiteX = wafer_Run.WaferNumber,
                SiteY = dr[5].ToString(),
                etest_ts = new List<Etest_T>()
            };
            for (int i = 0; i < ParameterList.Count; i++)
            {
                site.etest_ts.Add(new Etest_T() { TestID =GetTestIDByDescription(ParameterList[i]).ToString(), TestValue = dr[i + 7].ToString() });
            }
            wafer_Run.sites.Add(site);
        }


        /// <summary>
        /// 判断是否已经存在WaferRun，并返回该WaferRun
        /// </summary>
        /// <param name="dr">行数据</param>
        /// <returns>LotRun中的WaferRun</returns>
        private Etest_Wafer_Run GetWaferRun(DataRow dr)
        {
            try
            {
                foreach (Etest_Wafer_Run wafer in lot_run.etest_wafers)
                {
                    if (wafer.WaferNumber == dr[4].ToString())
                    {
                        return wafer;
                    }
                }
            }
            catch (Exception) { }
            Etest_Wafer_Run wafer_Run = new Etest_Wafer_Run
            {
                WaferNumber = dr[4].ToString(),
                Comments = "",
                ParameterCount = ParameterList.Count.ToString()
            };
            wafer_Run.sites = new List<Etest_Site>();
            lot_run.etest_wafers.Add(wafer_Run);
            return wafer_Run;
        }

        //文件路径
        //public string filePath;

        //文件名
        private string FileName
        {
            get { return FilePath.Substring(FilePath.LastIndexOf("\\") + 1); }
        }

        //赋值Etest_Limits
        private List<Etest_Limit> GetEtestLimits(DataRow dr, List<Etest_Limit> etest_Limits)
        {
            if (dr[6].ToString() == "SPEC HI")
            {
                for (int i = 7; i < dr.Table.Columns.Count; i++)
                {
                    Etest_Limit limit = new Etest_Limit
                    {
                        ID =GetTestIDByDescription(dr.Table.Columns[i].ColumnName).ToString(),
                        Desc = dr.Table.Columns[i].ColumnName,
                        SH = dr[i].ToString(),
                        Key="N"
                    };
                    etest_Limits.Add(limit);
                }
            }
            else if (dr[6].ToString() == "SPEC LO")
            {
                for (int i = 7; i < dr.Table.Columns.Count; i++)
                {
                    etest_Limits[i - 7].SL = dr[i] is null ? "0" : dr[i].ToString();
                }
            }
            else
            {
                throw new Exception(FilePath + "文件格式错误");
            }
            return etest_Limits;
        }

        private int GetTestIDByDescription(string desc)
        {
            if (HlcmSetting.DicTestID2Desc.ContainsKey(desc))
            {
                return HlcmSetting.DicTestID2Desc.FirstOrDefault(q => q.Key == desc).Value;
            }
            else
            {
                int value = HlcmSetting.DicTestID2Desc.Count+1;
                HlcmSetting.DicTestID2Desc.Add(desc,value);
                HlcmSetting.IsDicUpdated = true;
                return value;
            }

        }

        private string FixHlcmDate(string datestring)
        {
            try {
                 DateTime dt = DateTime.ParseExact(datestring, "dd-MM-yy", System.Globalization.CultureInfo.CurrentCulture);
                datestring = dt.ToString("yyyy/MM/dd");
                 }
            catch (Exception)
            { 
            if (datestring.Contains("-"))
            {
                string[] list = datestring.Split('-');
                if (list.Length == 3)
                {
                    return FixYear(list[2]) + "/" + FixMoon(list[1]) + "/" + FixDay(list[0]);
                }
            }
            }

            return datestring;
        }

        private Dictionary<string, string> dic = new Dictionary<string, string>
        { { "Jan", "01" },{ "Feb","02"},{ "Mar", "03" },{ "Apr","04"},{ "May","05"},{ "Jun","06"},{ "Jul","07"},{"Aug","08" },{ "Sep","09" },{ "Oct","10"},{ "Nov","11"},{ "Dec","12"} };


        private string FixDay(string day)
        {
            if (day.Length > 1)
            { return day; }
            else
            {
                return "0" + day;
            }
        }

        private string FixMoon(string moon)
        {
            string resault= dic.Where(p => p.Key == moon).FirstOrDefault().Value;
            return string.IsNullOrEmpty(resault) ? moon : resault;
        }
        private string FixYear(string year)
        {
            if (year.Length == 2)
            {
                return "20" + year;
            }
            else
            {
                return year;
            }
        }

        private string FixTime(object time)
        {
            string resault = "";
            try
            {
                resault = ((TimeSpan)time).ToString("HH:mm:ss");
            }
            catch (Exception)
            {
                string[] arry = time.ToString().Split(':');
                
                //如果出现PM、AM则需要处理
                if (arry[2].Contains(" "))
                {
                    arry[2] = arry[2].Split(' ')[0];
                }

                for (int i = 0; i < 3; i++)
                {
                    if (arry[i].Length == 1)
                    {
                        arry[i] += "0";
                    }
                }
                resault = string.Join(":",arry);
            }
            
            return resault;
        }
    }
}
