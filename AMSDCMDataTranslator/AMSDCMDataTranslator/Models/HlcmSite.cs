using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class HlcmSite
    {
        public HlcmSite()
        {
        }

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

        public IList<HlcmParameter> Parameters;
    }
}
