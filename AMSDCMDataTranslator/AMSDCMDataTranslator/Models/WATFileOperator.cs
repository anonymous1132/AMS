using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.IO;

namespace AMSDCMDataTranslator.Models
{
   public class WATFileOperator:SiffFileOperator
    {

        protected Etest Etest;

        protected override void TranslateFile(string filePath, string fileSuffix)
        {
            if (string.IsNullOrEmpty(fileSuffix) || (filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper() == fileSuffix.ToUpper()))
            {
                Etest.GetData(filePath,SpecPath);
                string siffFileName = Etest.WriteSiff(SiffPath);
                EtestFtpOperator.UploadEtestFile(SiffPath + "\\" + siffFileName);
                LogHelper.WATInfoLog("数据转换成功——DataFile:" +filePath.Substring(filePath.LastIndexOf("\\")+1) + "\tSiffFile:" + siffFileName);
                FileHelper.Move(SiffPath + "\\" + siffFileName, SiffHistoryPath + "\\" + siffFileName);
            }
        }

        public override void OperateFiles(string fileSuffix)
        {
            OperateFiles(TranslateFile,fileSuffix);
        }

        protected override void OperateFiles(TranslateDelegate translateDelegate,string fileSuffix)
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
