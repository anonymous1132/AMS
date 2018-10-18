using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
   public class WATRunner
    {
        public WATRunner()
        { }
        /// <summary>
        /// WAT转换主线程
        /// </summary>
        public static void Run()
        {
            LogHelper.WATInfoLog("开始执行WATTranslator");
            AMSWAT wat = new AMSWAT();
            AMSWATFileOperator fileOperator = new AMSWATFileOperator(wat);
            try
            {
                fileOperator.OperateFiles();
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("WAT:\t", e);
            }
            LogHelper.WATInfoLog("WATTranslator执行完毕");
        }

    }
}
