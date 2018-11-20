using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
    public class AMSChamberRunner
    {
        public static void Run()
        {
            ChamberSetting.SetValue();
            ChamberRunner.RunAMSChamber();
        }
    }
}
