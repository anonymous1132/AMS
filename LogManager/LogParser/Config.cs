using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class Config
    {
        public string DirectoryPath { get; set; }

        public string SmtpSslTop { get; set; }

        public string SmtpMsaTop { get; set; }

        public string SmtpTop { get; set; }

        public string QueueTop { get; set; }

        public string ImapSslTop { get; set; }

        public string ImapTop { get; set; }

        public string Pop3SslTop { get; set; }

        public string Pop3Top { get; set; }

        public string MongodbServer { get; set; }

        public int MongodbPort { get; set; }

        public string DataBase { get; set; }
    }
}
