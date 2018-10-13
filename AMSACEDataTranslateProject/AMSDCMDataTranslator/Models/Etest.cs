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
   public abstract class Etest:ISiffable
    {

        public string DataSource
        {
            get { return "ETEST_RAW"; }
        }

        public string FormatVersion
        {
            get { return "1.1"; }
        }

        public string FilePath
        {
            get;
            set;
        }
        public Etest_Lot_Run lot_run;

        public abstract void GetData();

        /// <summary>
        /// 生成siff，
        /// </summary>
        /// <param name="filePath">siffPath</param>
        /// <returns>SiffFileName</returns>
        public string WriteSiff(string filePath)
        {
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            string siff_file_name = lot_run.Lot+  DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".txt";
            XmlDocument document = XmlHelper.CreateXmlDocument("ADB_DOCUMENT", DataSource, FormatVersion);
            XmlNode xn = document.SelectSingleNode("/ADB_DOCUMENT");
            XmlNode xn2 = document.CreateElement("ETEST_LOT_RUN");
            xn2 = lot_run.SetLotRunNode(xn2);
            xn.AppendChild(xn2);
            document.Save(filePath + "\\" + siff_file_name);
            return siff_file_name;
        }

    }
}
