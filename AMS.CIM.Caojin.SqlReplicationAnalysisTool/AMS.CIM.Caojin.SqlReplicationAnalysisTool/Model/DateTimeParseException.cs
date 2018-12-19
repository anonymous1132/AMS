using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class DateTimeParseException:Exception
    {
            public DateTimeParseException() { }
            public DateTimeParseException(string message) : base(message) { }
            public DateTimeParseException(string message, Exception inner) : base(message, inner) { }
    }
}
