using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class QueueReserveEntity
    {
        public string ReserveAddress { get; set; }

        public bool IsLocalServer
        {
            get;
            set;
        }

        public bool IsSuccessful
        {
            get;
            set;
        }

        public string OutSideServerAddress
        {
            get;
            set;
        }

    }
}
