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

        protected override void TranslateFile(string filePath, string fileSuffix)
        {
            if (string.IsNullOrEmpty(fileSuffix) || (filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper() == fileSuffix.ToUpper()))
            {
                Etest.GetData(filePath, SpecPath);
                string siffFileName = Etest.WriteSiff(SiffPath);
                // EtestFtpOperator.UploadEtestFile(SiffPath + "\\" + siffFileName);
                SiffFileList.Add(SiffPath + "\\" + siffFileName);
                LogHelper.WATInfoLog("数据转换成功——DataFile:" + filePath.Substring(filePath.LastIndexOf("\\") + 1) + "\tSiffFile:" + siffFileName);
                // FileHelper.Move(SiffPath + "\\" + siffFileName, SiffHistoryPath + "\\" + siffFileName);
                //System.Threading.Thread.Sleep(1000);
            }
        }
        List<string> SiffFileList = new List<string>();


        //public override void OperateFiles(string fileSuffix)
        //{
        //    CopyNewFiles();
        //    string[] srcpaths = (from n in GetNewFileNames select WorkingPath + "\\" + n).ToArray();
        //    string[] destpaths= (from n in GetNewFileNames select HistoryPath + "\\" + n).ToArray();

        //}
        public override void OperateFiles(string fileSuffix)
        {
            OperateFiles(TranslateFile, fileSuffix);
            string[] filepaths = SiffFileList.ToArray();
            EtestFtpOperator.UploadEtestMultiFile(filepaths);
            foreach (string path in SiffFileList)
            {
                FileHelper.Move(path, SiffHistoryPath + path.Substring(path.LastIndexOf("\\")));
            }
        }


        protected override void OperateFiles(TranslateDelegate translateDelegate, string fileSuffix)
        {
            CopyNewFiles();
            foreach (string fileName in GetNewFileNames)
            {
                try
                {
                    string srcPath = WorkingPath + "\\" + fileName;
                    string desPath = HistoryPath + "\\" + fileName;
                    translateDelegate(srcPath, fileSuffix);
                    FileHelper.Move(srcPath, desPath);
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog("WAT:\t" + fileName + "\t", e);
                }
            }
        }



    }
}
