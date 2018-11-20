using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
    public class WIPFtpOperator
    {
        public static void UploadFile(string filePath)
        {
            FTPHelper.FtpServerIP = WIPSetting.FtpServerIP;
            FTPHelper.FtpUserID = WIPSetting.FtpUserID;
            FTPHelper.FtpPassword = WIPSetting.FtpPassword;
            FTPHelper.ftpURI = WIPSetting.FtpUri;
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

        public static void UploadDefectFile(string filePath)
        {
            FTPHelper.FtpServerIP = "10.132.0.35";
            FTPHelper.FtpUserID = "defect";
            FTPHelper.FtpPassword = "defect";
            FTPHelper.ftpURI = "ftp://10.132.0.35/ps_share/tooldata/WIP/directlink/";
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
