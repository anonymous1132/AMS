using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class HlcmSpecData
    {
        public HlcmSpecData()
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
        } = "";

        public string Tgt
        {
            get;
            set;
        } = "";

        public string VL
        {
            get;
            set;
        } = "";

        public string VH
        {
            get;
            set;
        } = "";

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
        } = "";

        public string CH
        {
            get;
            set;
        } = "";

        public string Key
        {
            get;
            set;
        } = "Y";

        public string SiteFails
        {
            get;
            set;
        } = "";

        public string KeyItem
        {
            get;
            set;
        } = "";

        public string Module
        {
            get;
            set;
        } = "";

        public string MinValidSites
        {
            get;
            set;
        } = "";

        public string Comments
        {
            get;
            set;
        } = "";

        public string FilterCriterion
        {
            get;
            set;
        } = "";
    }
}
