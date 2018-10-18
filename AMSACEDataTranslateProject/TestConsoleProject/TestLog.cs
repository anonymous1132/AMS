using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace TestConsoleProject
{
   public class TestLog
    {
        public static void Test()
        {
            LogHelper.ErrorLog("ErroTest");
            LogHelper.InfoLog("DCMTest");
            LogHelper.WATInfoLog("WATTest");
        }
    }
}
