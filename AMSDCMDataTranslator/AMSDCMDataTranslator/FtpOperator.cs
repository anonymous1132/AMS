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
        public static string FtpServerIP = "10.132.0.38";
        public static string FtpUserID="ace";
        public static string FtpPassword="Ams_ace";
        public static string FtpUri= "ftp://10.132.0.38/data/siff/Etest/import/";

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
