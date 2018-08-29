using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCADataTranslator.Helper;

namespace MCADataTranslator.Bll
{
   public class ReportModelUnitType2
    {
        public ReportModelUnitType2()
        { }

        public string AcqDateTime
        {
            get;
            set;
        }

        public string Na { get; set; }

        public string Al { get; set; }

        public string Ca { get; set; }

        public string Cr { get; set; }

        public string Fe { get; set; }

        public string Ni { get; set; }

        public string Cu { get; set; }

        public string Zn { get; set; }

        public string EQP { get; set; }

        public string WaferID { get; set; }
    }
}
