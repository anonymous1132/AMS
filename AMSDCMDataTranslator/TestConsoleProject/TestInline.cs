using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
   public class TestInline
    {
        public static void Test()
        {
            InlineDebugSetting.SetValue();
            InlineRunner.RunDebug();
            Console.WriteLine("OK");
        }

        public static void TestAMSInline()
        {
            InlineDebugSetting.SetValue();
            InlineRunner.RunInlineTest();
            Console.WriteLine("OK");
        }
    }
}
