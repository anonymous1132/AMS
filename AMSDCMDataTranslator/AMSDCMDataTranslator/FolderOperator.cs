using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AMSDCMDataTranslator.Helper;
using System.Data;

namespace AMSDCMDataTranslator
{
    public class FolderOperator
    {
       // public  const string DIRPATH = @"\\10.8.0.252\it信息\个人目录\曹晋";
        public string LOCALPATH ;
        public string WORKINGPATH ;
        public string DIRPATH ;
        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        public FolderOperator()
        {
            this.LOCALPATH = PUBLICSTRING.LocalPath;
            this.WORKINGPATH = PUBLICSTRING.WorkingPath;
            this.DIRPATH = PUBLICSTRING.DirPath;
        }

        private string exeDirctory
        {
            get { return exePath.Substring(0,exePath.LastIndexOf("\\")+1); }
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

        private string[] DCMFiles
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
                List<string> files=new List<string>();
                foreach (string file in DCMFiles)
                {
                    if (LocalFiles==null || !LocalFiles.Contains(file))
                    {
                        files.Add(file);
                    }
                }
            _newFiles = files;
        }

        private string[] GetFileName(string[]FilePath)
        {
            if (FilePath.Length == 0)
            {
                return null;
            };
            for (int i = 0; i < FilePath.Length; i++)
            {
                FilePath[i] = FilePath[i].Substring(FilePath[i].LastIndexOf("\\")+1);
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
                    LogHelper.ErrorLog("DCM:\t"+file + ":\t WORKINGPATH目录已经存在该文件！");
                }
            }
        }

        public DataSet ReadFile()
        {
            DataSet ds = new DataSet();
            CopyNewFiles();
            foreach(string fileName in _newFiles)
            {
                string srcPath = workingPath + "\\" + fileName;
                string desPath = localPath + "\\" + fileName;
                if ((fileName.Substring(fileName.LastIndexOf(".") + 1)).ToUpper() == "CSV")
                {
                    try
                    {
                        CsvHelper csv = new CsvHelper();
                        DataTable dt = csv.readCsvTxtWithColumnName(srcPath);
                        ds.Tables.Add(dt);
                    } catch (Exception e)
                    {
                        LogHelper.ErrorLog(e);
                    }
                }
                    try
                {
                    File.Move(srcPath, desPath);
                }
                catch (Exception)
                {
                    LogHelper.ErrorLog(fileName + ":\t无法移动该文件！");
                }
               
            }

            return ds;
        }
    }
}
