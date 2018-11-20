using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public abstract class WIP:ISiffable
    {
        public string DataSource
        {
            get { return "WIP_TURN"; }
        }

        public string FormatVersion
        {
            get;
            set;
        } = "1.1";

        public IList<WIP_SingleLine> WIP_lines;

        public abstract void GetData();

        public abstract string WriteSiff(string fileParth);

        public abstract void WriteXmlConfig();
    }
}
