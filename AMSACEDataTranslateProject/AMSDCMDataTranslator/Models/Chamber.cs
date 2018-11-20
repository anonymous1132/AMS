using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public abstract class Chamber:ISiffable
    {
        public string DataSource
        {
            get { return "CHAMBER"; }
        }

        public string FormatVersion
        {
            get;
            set;
        } = "1.1";

        public IList<Chamber_SingleLine> Chamber_lines;

        public abstract void GetData();

        public abstract string WriteSiff(string fileParth);

        public abstract void WriteXmlConfig();
    }
}
