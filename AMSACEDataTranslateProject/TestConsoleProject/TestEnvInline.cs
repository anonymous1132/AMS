﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
   public class TestEnvInline
    {
        public static void Test()
        {
            InlineDebugSetting.SetValue();
            InlineRunner.RunInlineTest();
        }
    }
}
