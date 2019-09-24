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
    public class AMSWIP:WIP
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public bool IsRun2Now { get; set; } = true;

        private string LastDbLineThisQuery;

        //取消Chamber方案
        //public IList<Chamber_SingleLine> Chamber_Lines;

        public override void GetData()
        {
            WIPEntityGroup entityGroup = new WIPEntityGroup();
            entityGroup.StartTime = StartTime;
            entityGroup.EndTime = EndTime;
            entityGroup.GetData();
            WIP_lines = entityGroup.GetWIPLists();
            //取消Chamber方案
            //Chamber_Lines = entityGroup.GetChamberLists();
            LastDbLineThisQuery = entityGroup.WIPDbEntities.Max(p => p.MoveOutTime).ToString("yyyy-MM-dd-HH.mm.ss.ffffff");
        }

        public override string WriteSiff(string filePath)
        {
            string filepath = filePath + "\\WIP" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            var sifffile = File.Create(filepath);
            sifffile.Close();
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write))
            {
                fs.Lock(0, fs.Length);
                StreamWriter sw = new StreamWriter(fs);
                string fistLine = "#\"DataSource\",\"FormatVersion\",\"Lot\",\"Fab\",\"SourceLot\",\"Technology\",\"Product\",\"LotType\",\"Route\",\"RouteVersion\",\"Stage\",\"Step\",\"StepVersion\",\"StepDesc\",\"Equipment\",\"EquipmentGroup\",\"MoveInTime\",\"MoveOutTime\",\"AutoEndTime\",\"MoveInWaferCount\",\"MoveOutWaferCount\",\"MoveInOperator\",\"MoveOutOperator\",\"Recipe\",\"Reticle\",\"Sequence\",\"BatchID\",\"QueueTime\",\"Owner\",\"CustomerCode\",\"WaferList\",\"LotStatus\",\"CarrierID\",\"String_Option1\",\"String_Option2\",\"String_Option3\",\"String_Option4\",\"String_Option5\",\"String_Option6\",\"String_Option7\",\"String_Option8\",\"String_Option9\",\"String_Option10\",\"Number_Option1\",\"Number_Option2\",\"Number_Option3\",\"Number_Option4\",\"Number_Option5\"";
                sw.WriteLine(fistLine);
                foreach (WIP_SingleLine wip in WIP_lines)
                {
                    try
                    {
                        string siffline = wip.GetSingleSiffLine();
                        string tempstr = string.Format("\"{0}\",\"{1}\",{2}", DataSource, FormatVersion, siffline);
                        sw.WriteLine(tempstr);
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog("WIPError AMSWIP.WriteSiff() ", e);
                    }

                }
                //fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。//用了反而异常-v-！
                sw.Flush();
            }

            return filepath;
        }

        //取消Chamber方案
        //public string WriteChamberSiff(string filePath)
        //{
        //    string filepath = filePath + "\\Chamber" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
        //    var sifffile = File.Create(filepath);
        //    sifffile.Close();
        //    using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write))
        //    {
        //        fs.Lock(0, fs.Length);
        //        StreamWriter sw = new StreamWriter(fs);
        //        string fistLine = "#\"DataSource\",\"FormatVersion\",\"Lot\",\"Wafer\",\"Scribe\",\"Equipment\",\"Route\",\"RouteVersion\",\"Step\",\"StepVersion\",\"DateTimeList\",\"ChamberList\",\"RecipeList\"";
        //        sw.WriteLine(fistLine);
        //        foreach (Chamber_SingleLine chamber in Chamber_Lines)
        //        {
        //            try
        //            {
        //                string siffline = chamber.GetSingleSiffLine();
        //                string tempstr = string.Format("\"{0}\",\"{1}\",{2}", "CHAMBER", FormatVersion, siffline);
        //                sw.WriteLine(tempstr);
        //            }
        //            catch (Exception e)
        //            {
        //                LogHelper.ErrorLog("ChamberError AMSWIP.WriteChamberSiff() ", e);
        //            }

        //        }
        //        //fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。//用了反而异常-v-！
        //        sw.Flush();
        //    }

        //    return filepath;
        //}

        public override void WriteXmlConfig()
        {
            if (!IsRun2Now) return;
            DataSet ds = new DataSet("ACE");
            DataTable dt = new DataTable("WIP");
            dt.Columns.Add("Sequence");
            DataRow dr = dt.NewRow();
            dr[0] = LastDbLineThisQuery;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml(WIPRunner.ConfigPath);
        }
    }
}
