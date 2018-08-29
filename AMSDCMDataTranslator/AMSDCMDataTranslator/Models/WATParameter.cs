using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class WATParameter
    {
        public WATParameter()
        { }

        public List<string> ValueList=new List<string>();

        public string ItemNo
        {
            get;
            set;
        }

        public string ParameterName
        {
            get;
            set;
        }

        public string unit
        {
            get;
            set;
        }
    }
}
