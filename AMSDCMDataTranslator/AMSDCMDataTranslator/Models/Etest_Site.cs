using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Xml;

namespace AMSDCMDataTranslator.Models
{
   public class Etest_Site
    {
        public Etest_Site()
        { }

        public string SiteID
        {
            get;
            set;
        }

        public string SiteX
        {
            get;
            set;
        }

        public string SiteY
        {
            get;
            set;
        }

        public List<Etest_T> etest_ts;

        public XmlNode SetSiteNode(XmlNode xn)
        {
            XmlHelper.InsertAttribute(ref xn,"SiteID",SiteID);
            XmlHelper.InsertAttribute(ref xn,"SiteX",SiteX);
            XmlHelper.InsertAttribute(ref xn,"SiteY",SiteY);
            foreach (Etest_T T in etest_ts)
            {
               xn= T.SetTNode(xn);
            }
            return xn;
        }

        public void GetData(string[,] coordinate,WATWafer wafer,int i)
        {
            SiteID = (i+1).ToString();
            SiteX =coordinate[0,0];
            SiteY = coordinate[0, 1];
            etest_ts = new List<Etest_T>();
            foreach (WATParameter parameter in wafer.parameters)
            {
                Etest_T t = new Etest_T();
                t.GetData(parameter,i);
                etest_ts.Add(t);
            }
        }
    }
}
