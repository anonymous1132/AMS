using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSACEWebMonitor
{
    public class EmailParameter
    {
        public string From
        {
            get;
            set;
        }

        public string To
        {
            get;
            set;
        }

        public string SMTPServer
        {
            get;
            set;
        }

        public int Port
        {
            get;
            set;
        }

        public string Password
        {
            get;
            set;
        }

    }
}
