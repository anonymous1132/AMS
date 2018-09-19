using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestProgram
{
   public class TestSpanTime
    {
        public string GetSubtractedDate(DateTime dateTime)
        {
            TimeSpan ts = DateTime.Now - dateTime;
            return ts.TotalHours.ToString();
        }

        public DirectoryInfo directory
        {
            get { return new DirectoryInfo(@"C:\Users\caojin\Documents\NetSarang"); }
        }
    }
}
