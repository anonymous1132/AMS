using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class WATWafer
    {
        public WATWafer()
        { }

        public string WaferID
        {
            get;
            set;
        }

        public List<WATParameter> parameters=new List<WATParameter>();
    }
}
