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
        public static string LOCALPATH;
        public static string WORKINGPATH ;
        public static string SIFFPATH ;
        public static string SIFFHISTORYPATH ;
        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        public static string DIRPATH;
        public static string SPECPATH ;
        ////获取文件改为FTP目录后新增（后取消）
        //public static string WATFTPUSER ;
        //public static string WATFTPPASS ;

        //构造函数
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

        /// <summary>
        /// 获取DIR共享目录下所有文件名
        /// </summary>
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
        /// <summary>
        ///排除本地已有的文件，获取新增加的文件名
        ///结果保存在_newFiles中
        /// </summary>
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

        /// <summary>
        /// 根据文件全路径获取文件名
        /// </summary>
        /// <param name="FilePath">文件全路径集合</param>
        /// <returns>文件名集合</returns>
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

        /// <summary>
        /// 从共享目录下拷贝新文件
        /// </summary>
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

        /// <summary>
        /// 通过FTP方式获取文件（已取消）
        /// </summary>
        private void GetFtpNewFiles()
        {

        }

        /// <summary>
        /// 文件读取、转换主进程
        /// </summary> 
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
