using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
   public class InlineFtpOperator
    {
        public static void UploadEtestFile(string filePath)
        {
            FTPHelper.FtpServerIP =InlineDebugSetting.FtpServerIP;
            FTPHelper.FtpUserID = InlineDebugSetting.FtpUserID;
            FTPHelper.FtpPassword = InlineDebugSetting.FtpPassword;
            FTPHelper.ftpURI = InlineDebugSetting.FtpUri;
            try
            {
                FTPHelper.FtpUploadFile(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
