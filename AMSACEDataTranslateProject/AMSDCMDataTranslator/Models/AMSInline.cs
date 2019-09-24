using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;
using System.IO;

namespace AMSDCMDataTranslator.Models
{
    public class AMSInline:Inline
    {


        private string LastDbLineThisQuery;

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsRun2Now { get; set; } = true;

        public override void GetData()
        {
            InlineEntityGroup entityGroup = new InlineEntityGroup();

            entityGroup.StartTime = StartTime;
            entityGroup.EndTime =EndTime;
            entityGroup.GetData();
            Inline_lines = entityGroup.GetInlineList();
            LastDbLineThisQuery = entityGroup.inlineDBEntities.Max(p=>p.ClaimTime).ToString("yyyy-MM-dd-HH.mm.ss.ffffff");
        }

        public override string WriteSiff(string filePath)
        {
            string filepath = filePath + "\\Inline" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            var sifffile = File.Create(filepath);
            sifffile.Close();
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write))
            {
                fs.Lock(0, fs.Length);
                StreamWriter sw = new StreamWriter(fs);
                string fistLine = "#\"DataSource\",\"FormatVersion\",\"Lot\",\"SourceLot\",\"Fab\",\"Technology\",\"Product\",\"LotType\",\"Owner\",\"MeasRoute\",\"MeasRouteVer\",\"MeasStep\",\"MeasStepVer\",\"MeasItem\",\"MeasTime\",\"MeasOperator\",\"MeasEquipment\",\"MeasRecipe\",\"ProcRoute\",\"ProcRouteVer\",\"ProcStep\",\"ProcStepVer\",\"ProcTime\",\"ProcOperator\",\"ProcEquipment\",\"ProcRecipe\",\"ProcReticle\",\"LimitsInside\",\"CollectedType\",\"MeasureDataCount\",\"WaferSiteArray\",\"MeasureDataArray\",\"Target\",\"ValidLow\",\"ValidHigh\",\"SpecLow\",\"SpecHigh\",\"CtrlLow\",\"CtrlHigh\",\"Unit\",\"ProcStepDesc\",\"BatchID\",\"DuplicateFlag\",\"TextVal\",\"SiteCoordArray\",\"WaferPos\",\"String_Option1\",\"String_Option2\",\"String_Option3\",\"String_Option4\",\"String_Option5\"";
                sw.WriteLine(fistLine);
                foreach (Inline_SigleLine inline in Inline_lines)
                {
                    try
                    {
                        string siffline = inline.GetSingleSiffLine();
                        sw.WriteLine("\"" + DataSource + "\",\"" + FormatVersion + "\"," + siffline);
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog("InlineError AMSInline.WriteSiff() ", e);
                    }

                }
                //fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。
                sw.Flush();
            }

            return filepath;
        }

        public override void WriteXmlConfig()
        {
            if (!IsRun2Now) return;
            DataSet ds = new DataSet("ACE");
            DataTable dt = new DataTable("Inline");
            dt.Columns.Add("Sequence");
            DataRow dr = dt.NewRow();
            dr[0] = LastDbLineThisQuery;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml(InlineRunner.ConfigPath);
        }

    }
}
