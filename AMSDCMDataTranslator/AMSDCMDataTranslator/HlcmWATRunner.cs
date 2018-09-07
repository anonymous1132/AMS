using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
    public class HlcmWATRunner
    {
        public HlcmWATRunner()
        { }

        public static void Run()
        {
            LogHelper.WATInfoLog("开始执行HlcmTranslator");
            HlcmWAT hlcm = new HlcmWAT();
            HlcmWATFileOperator fileOperator = new HlcmWATFileOperator(hlcm);
            try
            {
                fileOperator.OperateFiles();
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("Hlcm:\t", e);
            }
            LogHelper.WATInfoLog("HlcmTranslator执行完毕");
        }
    }
}
