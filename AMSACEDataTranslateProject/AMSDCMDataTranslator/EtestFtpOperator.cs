using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
   public class EtestFtpOperator
    {
        public static void UploadEtestFile(string filePath)
        {
            FTPHelper.FtpServerIP =WATSetting.FtpServerIP;
            FTPHelper.FtpUserID =WATSetting.FtpUserID ;
            FTPHelper.FtpPassword = WATSetting.FtpPassword;
            FTPHelper.ftpURI = WATSetting.FtpUri;
            try
            {
                FTPHelper.FtpUploadFile(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void UploadEtestMultiFile(string[] filePaths)
        {
            FTPHelper.FtpServerIP = WATSetting.FtpServerIP;
            FTPHelper.FtpUserID = WATSetting.FtpUserID;
            FTPHelper.FtpPassword = WATSetting.FtpPassword;
            FTPHelper.ftpURI = WATSetting.FtpUri;
            try
            {
                FTPHelper ftp = new FTPHelper();
                ftp.FtpUploadFileForMulti(filePaths);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
