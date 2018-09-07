using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public static class ACEDBSetting
    {
        public static string UserID
        {
            get;
            set;
        } = "acme";

        public static string Password
        {
            get;
            set;
        } = "acme";

        /// <summary>
        /// IP或者DNS主机名
        /// </summary>
        public static string HostName
        {
            get;
            set;
        } = "10.132.0.38";

        /// <summary>
        /// 端口号
        /// </summary>
        public static string Port
        {
            get;
            set;
        } = "1521";

        public static string ServerName
        {
            get;
            set;
        } = "acexp";
    }
}
