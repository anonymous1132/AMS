
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator;

namespace AMSTranslatorService
{
    public class HlcmWATTranslateService:ModelTranslatorService
    {
        public HlcmWATTranslateService() : base("HlcmTranslateService")
        {
            runnerDelegate += HlcmWATRunner.Run;
            settingDelegate += HlcmSetting.SetValue;
            settingDelegate?.Invoke();
            SetServiceName("HlcmTranslateService");
        }
    }
}