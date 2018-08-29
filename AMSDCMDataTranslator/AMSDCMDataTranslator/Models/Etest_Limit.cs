using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
   public class Etest_Limit
    {
        public Etest_Limit()
        { }

        public string ID
        {
            get;
            set;
        }

        public string Desc
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string Tgt
        {
            get;
            set;
        }

        public string VL
        {
            get;
            set;
        }

        public string VH
        {
            get;
            set;
        }

        public string SL
        {
            get;
            set;
        }

        public string SH
        {
            get;
            set;
        }

        public string CL
        {
            get;
            set;
        }

        public string CH
        {
            get;
            set;
        }

        public string Key
        {
            get;
            set;
        }

        public string SiteFails
        {
            get;
            set;
        }

        public string KeyItem
        {
            get;
            set;
        }

        public string Module
        {
            get;
            set;
        }

        public string MinValidSites
        {
            get;
            set;
        }

        public string Comments
        {
            get;
            set;
        }

        public XmlNode SetSpecNode(XmlNode xn)
        {
            XmlHelper.InsertAttribute(ref xn,"ID",ID);
            XmlHelper.InsertAttribute(ref xn,"Desc",Desc);
            XmlHelper.InsertAttribute(ref xn,"Unit",Unit);
            XmlHelper.InsertAttribute(ref xn,"Tgt",Tgt);
            XmlHelper.InsertAttribute(ref xn,"VL",VL);
            XmlHelper.InsertAttribute(ref xn, "VH", VH);
            XmlHelper.InsertAttribute(ref xn, "SL", SL);
            XmlHelper.InsertAttribute(ref xn, "SH", SH);
            XmlHelper.InsertAttribute(ref xn, "CL", CL);
             XmlHelper.InsertAttribute(ref xn, "CH", CH);
            XmlHelper.InsertAttribute(ref xn, "Key",Key);
            XmlHelper.InsertAttribute(ref xn, "SiteFails", SiteFails);
            XmlHelper.InsertAttribute(ref xn, "KeyItem", KeyItem);
            XmlHelper.InsertAttribute(ref xn, "Module", Module);
            XmlHelper.InsertAttribute(ref xn, "MinValidSites", MinValidSites);
            XmlHelper.InsertAttribute(ref xn, "Comments", Comments);
            return xn;
        }

        public void GetData(WATSpecData spec)
        {
            ID = spec.Number;
            Desc = spec.Name;
            Unit = spec.Unit;
            SL = spec.Spec_Low;
            SH = spec.Spec_High;
            Key= (spec.Flag == "12") ? "Y" : "N";
        }
    }
}
