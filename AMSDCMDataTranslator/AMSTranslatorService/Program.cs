using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace AMSTranslatorService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        static void Main()
        {

            try
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                     new DCMTransLateService(),
                     new WATTranslateService()
                };
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("服务启动失败",e);
            }
        }

      
    }
}
