using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
   public class InlineFileOperator:SiffFileOperator
    {
        public InlineFileOperator(Inline inline)
        {
            this.inline = inline;
            SetConfig();
        }

        protected Inline inline;

        private void SetConfig()
        {
            SIFF_PATH = InlineDebugSetting.SiffPath;
            SIFF_HISTORY_PATH = InlineDebugSetting.SiffHistoryPath;
        }

        protected override void TranslateFile(string filePath, string fileSuffix)
        {
            inline.GetData(filePath,fileSuffix);
            string siffFileName = inline.WriteSiff(SiffPath);
            InlineFtpOperator.UploadEtestFile( siffFileName);
            LogHelper.InlineInfoLog("数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move( siffFileName, SiffHistoryPath  + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            inline.WriteXmlConfig();
        }

        protected override void OperateFiles(TranslateDelegate translateDelegate, string fileSuffix)
        {
            translateDelegate("", fileSuffix);
        }

        public override void OperateFiles(string fileSuffix)
        {
            OperateFiles( TranslateFile,fileSuffix);
        }

        public void OperateFiles()
        {
            OperateFiles("");
        }

        public void OperateTestFiles()
        {
            ((MESInline)inline).GetTestData();
            foreach (var line in inline.Inline_lines)
            {
                line.Fab = "FABTEST";
                line.Lot = "TEST_" + line.Lot;
                line.SourceLot = "TEST_"+line.SourceLot;
            }
            string siffFileName = inline.WriteSiff(SiffPath);
            InlineFtpOperator.UploadEtestFile(siffFileName);
            LogHelper.InlineInfoLog("Test数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            inline.WriteXmlConfig();
        }
    }
}
