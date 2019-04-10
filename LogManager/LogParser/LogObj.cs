using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public  class LogObj<T> where T:new()
    {
        public List<T> LogEntities { get; set; } = new List<T>();

    }
}
