using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSACEWebMonitor;

namespace TestProgram
{
    public class TestSetting
    {
        public static void Test()
        {
            Setting.GetValue();
            Console.WriteLine("===========Timer=====================");
            Console.WriteLine(Setting.Interval.ToString());
            Console.WriteLine(Setting.SpecTime.ToString());
            Console.WriteLine("===========Mail======================");
            Console.WriteLine(Setting.emailParameter.From);
            Console.WriteLine(Setting.emailParameter.To);
            Console.WriteLine(Setting.emailParameter.SMTPServer);
            Console.WriteLine(Setting.emailParameter.Password);
            Console.WriteLine(Setting.emailParameter.Port.ToString());
            Console.WriteLine("===========Unity=====================");
            Console.WriteLine(Setting.MonitorUnities.Count.ToString());
            foreach (MonitorUnity unity in Setting.MonitorUnities)
            {
                Console.WriteLine(unity.Directory.FullName+unity.LastRunRecipeTime+unity.HasSent.ToString());
            }
           
        }
    }
}
