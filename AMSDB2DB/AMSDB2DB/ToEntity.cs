using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilsLibrary.Models;

namespace AMSDB2DB
{
    public class ToEntity:OracleConPara
    {
        public string DBType { get; set; }

        public string Table { get; set; }

    }
}
