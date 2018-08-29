using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator
{
   public class WATRunner
    {
        public WATRunner()
        { }

        public static void Run(WATRunningConfig config)
        {
            LogHelper.WATInfoLog("开始执行WATTranslator");
            WATFileOperator fileOperator = new WATFileOperator();
            WATFileOperator.DIRPATH = config.DirPath;
            WATFileOperator.LOCALPATH = config.LocalPath;
            WATFileOperator.WORKINGPATH = config.WorkingPath;
            WATFileOperator.SPECPATH = config.SpecPath;
            WATFileOperator.SIFFPATH = config.SiffPath;
            WATFileOperator.SIFFHISTORYPATH = config.SiffHistoryPath;
            FtpOperator.FtpServerIP = config.FtpServerIP;
            FtpOperator.FtpUserID = config.FtpUserID;
            FtpOperator.FtpPassword = config.FtpPassword;
            FtpOperator.FtpUri = config.FtpUri;
            try
            {
                fileOperator.ReadFiles();
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("WAT:\t",e);
            }
            LogHelper.WATInfoLog("WATTranslator执行完毕");
        }

    }
}
