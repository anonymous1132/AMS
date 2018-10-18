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

        protected override void TranslateFile()
        {
            inline.GetData();
            string siffFileName = inline.WriteSiff(SiffPath);
            InlineFtpOperator.UploadEtestFile( siffFileName);
            LogHelper.InlineInfoLog("数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move( siffFileName, SiffHistoryPath  + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            inline.WriteXmlConfig();
        }

        public override void OperateFiles()
        {
            TranslateFile();
        }


        public void OperateTestFiles()
        {

            inline.GetData();
            foreach (var line in inline.Inline_lines)
            {
                line.Fab = "FABTEST";
                line.Lot = "TEST_" + line.Lot;
                line.SourceLot =line.SourceLot==""?"": "TEST_"+line.SourceLot;
            }
            string siffFileName = inline.WriteSiff(SiffPath);
            InlineFtpOperator.UploadEtestFile(siffFileName);
            LogHelper.InlineInfoLog("Test数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            inline.WriteXmlConfig();
        }


    }
}
