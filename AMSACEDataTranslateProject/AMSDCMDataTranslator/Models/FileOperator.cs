using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public abstract class FileOperator:IFileOperable
    {
        
        protected string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

        public  string SOURCE_FILE_PATH;
        public  string WORKING_PATH;
        public  string HISTORY_PATH;

        protected string SourceFilePath
        {
            get { return  SOURCE_FILE_PATH; }
        }

        protected string WorkingPath
        {
            get { return exeDirctory + WORKING_PATH; }
        }

        protected string HistoryPath
        {
            get { return exeDirctory + HISTORY_PATH; }
        }

        protected string exeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }

        /// <summary>
        /// 共享目录下所有文件名
        /// </summary>
        private string[] SMBAllFileNames
        {
            get
            {
                string[] files = GetFileName(Directory.GetFiles(SourceFilePath));
                if (files == null) throw new Exception("SourcePath是空目录");
                return files;
            }
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

        private IList<string> _newFileNames;
        /// <summary>
        /// 获取所有新文件名
        /// </summary>
        protected virtual IList<string> GetNewFileNames
        {
            get
            {
                if (_newFileNames == null)
                {
                    _newFileNames = new List<string>();
                    foreach (string file in SMBAllFileNames)
                    {
                        if (HistoryFiles == null || !HistoryFiles.Contains(file))
                        {
                            _newFileNames.Add(file);
                        }
                    }
                }
                return _newFileNames;
            }
        }

        /// <summary>
        /// 获取历史文件集合
        /// </summary>
        private string[] HistoryFiles
        {
            get
            {
                try
                {
                    string[] files = GetFileName(Directory.GetFiles(HistoryPath));
                    return files;
                }
                catch (Exception)
                { throw; }
            }
        }

        /// <summary>
        /// 拷贝新文件至工作目录
        /// </summary>
        protected void CopyNewFiles()
        {
            if (GetNewFileNames.Count == 0) return;
            foreach (string file in GetNewFileNames)
            {
                try
                {
                    File.Copy(SourceFilePath + "\\" + file, WorkingPath + "\\" + file,true);
                }
                catch (Exception)
                {
                    LogHelper.ErrorLog("WAT:\t" + file + ":\t WORKINGPATH目录已经存在该文件！");
                }
            }
        }

        /// <summary>
        /// 操作文件：文件转换、上传
        /// </summary>
        public abstract void OperateFiles();

    }
}
