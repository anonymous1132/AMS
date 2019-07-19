using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDB2DB
{
    public class FromEntity:ToEntity
    {
        public List<string> Columns { get; set; }

        public string Sequence { get; set; }

        public dynamic SeqValue { get; set; }
    }
}
