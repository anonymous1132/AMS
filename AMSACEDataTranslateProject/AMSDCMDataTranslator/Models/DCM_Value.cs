using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class DCM_Value
    {
        public DCM_Value()
        {

        }

        public string ValueCategory
        {
            get;
            set;
        }

        public float MeasureValue
        {
            get;
            set;
        }
    }
}
