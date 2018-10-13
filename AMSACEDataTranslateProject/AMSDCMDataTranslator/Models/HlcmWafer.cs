using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class HlcmWafer
    {
        public HlcmWafer()
        { }

        public string WaferNumber
        {
            get;
            set;
        }

        public string SiteCount
        {
            get { return Sites.Count.ToString(); }
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
        } = "";

        public IList<HlcmSite> Sites;
    }
}
