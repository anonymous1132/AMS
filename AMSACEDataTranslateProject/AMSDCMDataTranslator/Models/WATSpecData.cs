using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class WATSpecData
    {
        public WATSpecData(string line)
        {
            this._line = line;
            GetData();
        }

        private string _line
        {
            get;
            set;
        }

        public string Number
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string Spec_Low
        {
            get;
            set;
        }

        public string Spec_High
        {
            get;
            set;
        }

        public string Flag
        {
            get;
            set;
        }
        private void GetData()
        {
            if (_line.Length != 73)
            {
                throw new Exception("WAT:SpecLine Lenth Not Right");
            }
            Number = _line.Substring(0,5).Trim();
            Name = _line.Substring(5,30).Trim();
            Unit = _line.Substring(35,10).Trim();
            Spec_High = _line.Substring(45,13).Trim();
            Spec_Low = _line.Substring(58,13).Trim();
            Flag = _line.Substring(71,2).Trim();
        }
    }
}
