using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caojin.Common
{
    public class DateTimeBuilder
    {
        public static string CommonStrDateTime(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd HH:mm:ss");
        }

        public static DateTime dtDateTime(string strDateTime)
        {
            return Convert.ToDateTime(strDateTime);
        }
    }
}
