using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;
namespace TestConsoleProject
{
    public class TestWat
    {
       public static void Test()
        {
            WATSetting.SetValue();
            WATRunner.Run();
        }
    }
}
