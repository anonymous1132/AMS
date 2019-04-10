using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogParser
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0&&args.Contains("--today"))
            {
                Public.GetConfigValue();
                new TodayRunner();
            }
            else {
                Public.GetConfigValue();
                new Runner();
            }
        }
    }
}
