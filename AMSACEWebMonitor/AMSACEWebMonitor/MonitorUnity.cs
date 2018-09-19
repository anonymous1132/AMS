using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMSACEWebMonitor
{
    public class MonitorUnity
    {
        public DirectoryInfo Directory
        {
            get;
            set;
        }

        public string LastRunRecipeTime
        {
            get;
            set;
        }

        /// <summary>
        /// 是否已经发送过邮件
        /// </summary>
        public bool HasSent
        {
            get;
            set;
        } = false;
    }
}
