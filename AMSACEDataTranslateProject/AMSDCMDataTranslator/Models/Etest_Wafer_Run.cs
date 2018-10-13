using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public class Etest_Wafer_Run
    {
        public Etest_Wafer_Run()
        { }

        public string WaferNumber
        {
            get;
            set;
        }

        public string SiteCount
        {
            get { return sites.Count.ToString(); }
            
        }

        public string ParameterCount
        {
            get;
            set;
        }
        
        public string WaferPass
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public List<Etest_Site> sites;

        public XmlNode SetWaferRunNode(XmlNode xn)
        {
            XmlHelper.InsertAttribute(ref xn,"WaferNumber",WaferNumber);
            XmlHelper.InsertAttribute(ref xn,"SiteCount",SiteCount);
            XmlHelper.InsertAttribute(ref xn,"ParameterCount",ParameterCount);
            XmlHelper.InsertAttribute(ref xn,"WaferPass",WaferPass);
            XmlHelper.InsertAttribute(ref xn,"Comments",Comments);
            foreach (Etest_Site site in sites)
            {
                XmlElement xe = (xn.OwnerDocument).CreateElement("SITE");
                XmlNode xnn = site.SetSiteNode(xe);
                xn.AppendChild(xnn);
            }
            return xn;
        }

        public void GetData(WATWafer wafer,WAT wat)
        {
            WaferNumber = wafer.WaferID;
           // SiteCount = wat.TestSite;
            ParameterCount = wafer.parameters.Count.ToString();
            WaferPass = "";
            Comments = "";
            sites = new List<Etest_Site>();
            for (int i = 0; i < Convert.ToInt32(wat.TestSite); i++)
            {
                Etest_Site site = new Etest_Site();
                site.GetData(wat.site_coordinates[i],wafer,i);
                sites.Add(site);
            }
        }
    }
}
