using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;

namespace AMSTranslatorService
{
    public class HlmcWATTranslateService : ModelTranslatorService
    {
        public HlmcWATTranslateService() : base("HlmcTranslateService")
        {
            runnerDelegate += HlcmWATRunner.Run;
            settingDelegate += SetInterver;
        }

        private void SetInterver()
        {
            HlcmSetting.SetValue();
            Interval = HlcmSetting.Interval;
        }

    }
}
