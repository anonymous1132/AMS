using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator
{
   public class FtpOperator
    {
        public static string FtpServerIP ;
        public static string FtpUserID;
        public static string FtpPassword;
        public static string FtpUri;

        public static void UploadEtestFile(string filePath)
        {
            FTPHelper.FtpServerIP =FtpServerIP;
            FTPHelper.FtpUserID =FtpUserID ;
            FTPHelper.FtpPassword = FtpPassword;
            FTPHelper.ftpURI = FtpUri;
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
