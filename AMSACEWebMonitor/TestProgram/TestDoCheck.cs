using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSACEWebMonitor;

namespace TestProgram
{
    public class TestDoCheck
    {
        public static void Test()
        {
            Setting.GetValue();
            MonitorExcute monitorExcute = new MonitorExcute();
            monitorExcute.DoCheck(); ;
        }
    }
}
