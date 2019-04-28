using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace TestConsoleProject
{
    public class DefectUploadFile
    {
        public static void UploadFile()
        {
            var files = System.IO.Directory.GetFiles("files").Where(w=>w.Substring(w.Length-3)=="xml");
            if (!files.Any()) Console.WriteLine("files文件夹内没有文件");
            string file = files.First();
            Console.WriteLine("获取到文件为{0},按回车确认上传,否则请关闭本窗口",file);
            Console.ReadLine();
            FTPHelper.FtpServerIP = "10.132.0.35";
            FTPHelper.FtpUserID = "defect";
            FTPHelper.FtpPassword = "defect";
            FTPHelper.ftpURI = "ftp://10.132.0.35/ps_share/tooldata/EDA/";
            FTPHelper ftp = new FTPHelper();
            ftp.UploadFile(file);
            Console.WriteLine("上传成功");
        }
    }
}
