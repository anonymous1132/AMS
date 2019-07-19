using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDB2DB
{
    public class TaskEntity
    {
        public FromEntity From { get; set; }

        public ToEntity To { get; set; }

        public int Interval { get; set; }
    }
}
