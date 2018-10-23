using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class PDModel
    {
        public PDModel()
        { }
        public string MainPD_ID
        {
            get;
            set;
        }

        public string PD_ID
        {
            get;
            set;
        }

        public string OPE_NO
        {
            get;
            set;
        }

        public string Route
        {
            get { return MainPD_ID.TrimEnd(new char[] { '.','0'}); }
        }

        public string Step
        {
            get { return PD_ID.TrimEnd(new char[] { '.', '0' }); }
        }
    }
}
