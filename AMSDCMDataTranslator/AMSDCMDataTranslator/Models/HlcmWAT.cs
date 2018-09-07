using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public class HlcmWAT:Etest
    {
        /// <summary>
        /// 获取Hlcm数据文件中的数据
        /// </summary>
        /// <param name="filePath">数据文件全路径</param>
        /// <param name="specPath">SpecFile文件全路径</param>
        public override void GetData(string filePath, string specPath)
        {
            this.filePath = filePath;
            DataTable dt = ExcelHelper.GetContent(filePath).Tables[0];
            if (dt.Columns.Count< 8 ||dt.Columns[0].ColumnName!= "LOT ID" ||dt.Columns[1].ColumnName!= "PRODUCT NAME"||dt.Columns[5].ColumnName!="SITE" || dt.Rows.Count<3)
            {
                throw new Exception(filePath + "文件格式错误");
            }
            lot_run = new Etest_Lot_Run
            {
                etest_limits = new List<Etest_Limit>()
            };

            //参数列表赋值
            for (int i = 7; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.Substring(0, 1) == "F" && dt.Columns[i].ColumnName.Length<4)
                {
                    break;
                }
                else
                { ParameterList.Add(dt.Columns[i].ColumnName); }
            }

            for (int i = 0; i < 2; i++)
            {
                lot_run.etest_limits = GetEtestLimits(dt.Rows[i],lot_run.etest_limits);
            }

            for (int i = 2; i < dt.Rows.Count; i++)
            {
                SetLotRun(dt.Rows[i]);
            }
        }

        public void GetData(string filePath)
        {
            GetData(filePath,"");
        }
        /// <summary>
        /// 参数集合
        /// </summary>
        private List<string> ParameterList = new List<string>();
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
                lot_run.MeasureTime =((DateTime)dr[2]).ToString("yyyy/MM/dd")+"_"+ ((DateTime)dr[3]).ToString("hh:mm:ss");
                lot_run.Operation = "HLCMWAT";
                lot_run.SpecfileName = FileName;
                lot_run.TestProgram = lot_run.Product;
                lot_run.Operator = "";
                lot_run.ProbeCard = "";
                lot_run.FlatOrientation = "DOWN";
                lot_run.Owner = "";
            }
            Etest_Wafer_Run wafer_Run = GetWaferRun(dr);
            wafer_Run.sites = new List<Etest_Site>();
            Etest_Site site = new Etest_Site()
            {
                SiteID = dr[5].ToString(),
                SiteX = wafer_Run.WaferNumber,
                SiteY = dr[5].ToString(),
                etest_ts = new List<Etest_T>()
            };
            for (int i=0;i<ParameterList.Count;i++)
            {
                site.etest_ts.Add(new Etest_T() { TestID=ParameterList[i],TestValue=dr[i+7].ToString()});
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
                Comments = ""
            };
            lot_run.etest_wafers.Add(wafer_Run);
            return wafer_Run;
        }

        //文件路径
        private string filePath;

        //文件名
        private string FileName
        {
            get { return filePath.Substring(filePath.LastIndexOf("\\")+1); }
        }

        //赋值Etest_Limits
        private List<Etest_Limit> GetEtestLimits(DataRow dr,List<Etest_Limit>etest_Limits)
        {
            if (dr[6].ToString() == "SPEC HI")
            {
                for (int i = 7; i < dr.Table.Columns.Count; i++)
                {
                    Etest_Limit limit = new Etest_Limit
                    {
                        ID = dr.Table.Columns[i].ColumnName,
                        Desc = dr.Table.Columns[i].ColumnName,
                        SH= dr[i].ToString()
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
                throw new Exception(filePath + "文件格式错误");
            }
            return etest_Limits;
        }


    }
}
