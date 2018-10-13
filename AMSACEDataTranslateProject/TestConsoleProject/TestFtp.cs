using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace TestConsoleProject
{
    public class TestFtp
    {
        public static void Test(string[] filepaths)
        {
            FTPHelper.FtpServerIP = WATSetting.FtpServerIP;
            FTPHelper.FtpUserID = WATSetting.FtpUserID;
            FTPHelper.FtpPassword = WATSetting.FtpPassword;
            FTPHelper.ftpURI = WATSetting.FtpUri;
            FTPHelper ftp = new FTPHelper();
            bool loadresault= ftp.FtpUploadFileForMulti(filepaths);
            Console.WriteLine(string.Join(" ",filepaths.ToList()) +loadresault.ToString());
        }

    }
}
