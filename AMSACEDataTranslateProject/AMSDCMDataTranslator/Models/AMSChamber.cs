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
    public class AMSChamber:Chamber
    {
        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private string ExeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }

        private readonly string Config = @"App\config\chambertime.config";
        private string ConfigPath
        {
            get { return ExeDirctory + Config; }
        }

        private string GetLastDBLine()
        {
            DataTable dt = XmlHelper.GetTable(ConfigPath, XmlHelper.XmlType.File, "Chamber");
            return dt.DefaultView[0][0].ToString();
        }

        private string LastDbLineThisQuery;

        public override void GetData()
        {
            ChamberEntityGroup chamberEntityGroup = new ChamberEntityGroup();
            DateTime dateTime = new DateTime();
            DateTime.TryParseExact(GetLastDBLine(), "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dateTime);
            chamberEntityGroup.StartTime = dateTime;
            chamberEntityGroup.GetData();
            Chamber_lines = chamberEntityGroup.GetChamberLists();
            LastDbLineThisQuery = chamberEntityGroup.ChamberDBEntities.Max(p => p.Proc_Time).ToString("yyyy-MM-dd-HH.mm.ss.ffffff");
        }

        public override string WriteSiff(string filePath)
        {
            string filepath = filePath + "\\Chamber" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
            var sifffile = File.Create(filepath);
            sifffile.Close();
            using (FileStream fs = new FileStream(filepath, FileMode.Append, FileAccess.Write))
            {
                fs.Lock(0, fs.Length);
                StreamWriter sw = new StreamWriter(fs);
                string fistLine = "#\"DataSource\",\"FormatVersion\",\"Lot\",\"Wafer\",\"Scribe\",\"Equipment\",\"Route\",\"RouteVersion\",\"Step\",\"StepVersion\",\"DateTimeList\",\"ChamberList\",\"RecipeList\"";
                sw.WriteLine(fistLine);
                foreach (Chamber_SingleLine chamber in Chamber_lines)
                {
                    try
                    {
                        string siffline = chamber.GetSingleSiffLine();
                        string tempstr = string.Format("\"{0}\",\"{1}\",{2}",DataSource,FormatVersion,siffline);
                        sw.WriteLine(tempstr);
                    }
                    catch (Exception e)
                    {
                        LogHelper.ErrorLog("ChamberError AMSChamber.WriteSiff() ", e);
                    }

                }
                //fs.Unlock(0, fs.Length);//一定要用在Flush()方法以前，否则抛出异常。//用了反而异常-v-！
                sw.Flush();
            }

            return filepath;
        }

        public override void WriteXmlConfig()
        {
            DataSet ds = new DataSet("ACE");
            DataTable dt = new DataTable("Chamber");
            dt.Columns.Add("Sequence");
            DataRow dr = dt.NewRow();
            dr[0] = LastDbLineThisQuery;
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            ds.WriteXml(ConfigPath);
        }
    }
}
