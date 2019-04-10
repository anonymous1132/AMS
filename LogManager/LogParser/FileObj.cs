using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public  class FileObj<T> where T:new()
    {
        public List<T> Entities { get; set; } = new List<T>();

        public  string FilePath { get; set; }
    }
}
