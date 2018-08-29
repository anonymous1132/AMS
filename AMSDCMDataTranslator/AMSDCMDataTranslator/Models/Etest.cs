using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using AMSDCMDataTranslator.Helper;
using System.Xml;

namespace AMSDCMDataTranslator.Models
{
   public class Etest
    {
        public Etest(WAT wAT,List<WATSpecData>specDatas )
        {
            this.wat = wAT;
            this.specs = specDatas;
            GetDate();
        }

        private WAT wat;

        private List<WATSpecData>specs;

        public string DataSource
        {
            get { return "ETEST_RAW"; }
        }

        public string FormatVersion
        {
            get { return "1.1"; }
        }

        public Etest_Lot_Run lot_run;

        private void GetDate()
        {
            this.lot_run = new Etest_Lot_Run();
            lot_run.GetData(wat,specs);
        }

        public string WriteSiffFile(string filePath)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            string siff_file_name = wat.LotID + wat.TestWafer + DateTime.Now.ToString("yyyyMMddHHmmss")+".txt";
            XmlDocument document= XmlHelper.CreateXmlDocument("ADB_DOCUMENT",DataSource,FormatVersion);
            XmlNode xn = document.SelectSingleNode("/ADB_DOCUMENT");
            XmlNode xn2= document.CreateElement("ETEST_LOT_RUN");
            xn2 = lot_run.SetLotRunNode(xn2);
            xn.AppendChild(xn2);
            document.Save(filePath+"\\"+siff_file_name);
            return  siff_file_name;
        }
    }
}
