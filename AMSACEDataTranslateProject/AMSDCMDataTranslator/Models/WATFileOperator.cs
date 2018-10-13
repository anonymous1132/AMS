using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.IO;

namespace AMSDCMDataTranslator.Models
{
    public class WATFileOperator : SiffFileOperator
    {

        protected Etest Etest;

        public string fileSuffix
        {
            get;
            set;
        }

        public string filePath
        {
            get;
            set;
        }

        protected override void TranslateFile()
        {
            if (string.IsNullOrEmpty(fileSuffix) || (filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper() == fileSuffix.ToUpper()))
            {
                Etest.FilePath = filePath;
                Etest.GetData();
                string siffFileName = Etest.WriteSiff(SiffPath);
                SiffFileList.Add(SiffPath + "\\" + siffFileName);
                LogHelper.WATInfoLog("数据转换成功——DataFile:" + filePath.Substring(filePath.LastIndexOf("\\") + 1) + "\tSiffFile:" + siffFileName);
            }
        }
        List<string> SiffFileList = new List<string>();

        public override void OperateFiles()
        {
            CopyNewFiles();
            foreach (string fileName in GetNewFileNames)
            {
                try
                {
                    string srcPath = WorkingPath + "\\" + fileName;
                    string desPath = HistoryPath + "\\" + fileName;
                    filePath = srcPath;
                    TranslateFile();
                    FileHelper.Move(srcPath, desPath);
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog("WAT:\t" + fileName + "\t", e);
                }
            }

            string[] filepaths = SiffFileList.ToArray();
            EtestFtpOperator.UploadEtestMultiFile(filepaths);
            foreach (string path in SiffFileList)
            {
                FileHelper.Move(path, SiffHistoryPath + path.Substring(path.LastIndexOf("\\")));
            }
        }
    }
}
