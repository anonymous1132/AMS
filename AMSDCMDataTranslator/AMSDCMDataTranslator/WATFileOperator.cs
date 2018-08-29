using System;
using System.Collections.Generic;
using System.Linq;
using AMSDCMDataTranslator.Models;
using System.IO;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator
{
    public class WATFileOperator
    {
        public static string LOCALPATH= @"App\wat_data\history";
        public static string WORKINGPATH = @"App\wat_data\current";
        public static string DIRPATH=@"C:\Users\PUI\Desktop\test";
        public static string SIFFPATH = @"App\wat_data\siff";
        public static string SIFFHISTORYPATH = @"App\wat_data\siff_history";
        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        public static string SPECPATH = @"C:\Users\PUI\Desktop\test\spec";
        //public static string WATFTPUSER = "WAT";
        //public static string WATFTPPASS = "2uYgm#1D";


        public WATFileOperator()
        {

        }

        private string exeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }

        private string localPath
        {
            get { return exeDirctory + LOCALPATH; }
        }

        private string workingPath
        {
            get { return exeDirctory + WORKINGPATH; }
        }

        private string dirPath
        {
            get { return DIRPATH; }
        }

        private string siffPath
        {
            get { return exeDirctory + SIFFPATH; }
        }

        private string siffHistoryPath
        {
            get { return exeDirctory + SIFFHISTORYPATH; }
        }

        private string[] WATFiles
        {
            get
            {
                try
                {

                    string[] files = GetFileName(Directory.GetFiles(dirPath));
                    return files;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }

        private string[] LocalFiles
        {
            get
            {
                try
                {
                    string[] files = GetFileName(Directory.GetFiles(localPath));
                    return files;
                }
                catch (Exception)
                { throw; }
            }
        }

        private List<string> _newFiles;

        public void GetNewFiles()
        {
            List<string> files = new List<string>();
            foreach (string file in WATFiles)
            {
                if (LocalFiles == null || !LocalFiles.Contains(file))
                {
                    files.Add(file);
                }
            }
            _newFiles = files;
        }


        private string[] GetFileName(string[] FilePath)
        {
            if (FilePath.Length == 0)
            {
                return null;
            };
            for (int i = 0; i < FilePath.Length; i++)
            {
                FilePath[i] = FilePath[i].Substring(FilePath[i].LastIndexOf("\\") + 1);
            }
            return FilePath;
        }

        private void CopyNewFiles()
        {
            GetNewFiles();
            if (_newFiles.Count == 0) return;
            foreach (string file in _newFiles)
            {
                try
                {
                    File.Copy(dirPath + "\\" + file, workingPath + "\\" + file);
                }
                catch (Exception)
                {
                    LogHelper.ErrorLog("WAT:\t"+file + ":\t WORKINGPATH目录已经存在该文件！");
                }
            }
        }

        // public 
        public void ReadFiles()
        {
            CopyNewFiles();
            foreach (string fileName in _newFiles)
            {
                string srcPath = workingPath + "\\" + fileName;
                string desPath = localPath + "\\" + fileName;
                string siffFile = "";
                if ((fileName.Substring(fileName.LastIndexOf(".") + 1)).ToUpper() == "DAT")
                {
                    try
                    {
                        WATFIleUtil watUtil = new WATFIleUtil(srcPath);
                        SpecDataFileUtil specUtil = new SpecDataFileUtil(SPECPATH + "\\" + watUtil.Wat.LimitFile);
                        Etest etest = new Etest(watUtil.Wat, specUtil.datalist);
                        siffFile= etest.WriteSiffFile(siffPath);
                        FtpOperator.UploadEtestFile(siffPath+"\\"+ siffFile);
                        LogHelper.WATInfoLog("数据转换成功——DataFile:"+fileName+"\tSiffFile:"+siffFile);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("WAT:\t"+siffFile+"\t"+e.Message);
                    }
                }
                try
                {
                    File.Move(srcPath, desPath);
                }
                catch (Exception e)
                {
                    throw new Exception("WAT:\t" + fileName + ":\t"+e.Message);
                }
                try
                {
                    File.Move(siffPath + "\\" + siffFile, siffHistoryPath + "\\" + siffFile);
                }
                catch (Exception e)
                {
                    throw new Exception("WAT:\t" + siffFile + ":\t" + e.Message);
                }

            }

        }
    }
}
