using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
    public class ChamberFtpOperator
    {
        public static void UploadFile(string filePath)
        {
            FTPHelper.FtpServerIP = ChamberSetting.FtpServerIP;
            FTPHelper.FtpUserID = ChamberSetting.FtpUserID;
            FTPHelper.FtpPassword = ChamberSetting.FtpPassword;
            FTPHelper.ftpURI = ChamberSetting.FtpUri;
            try
            {
                FTPHelper ftp = new FTPHelper();
                ftp.UploadFile(filePath);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
