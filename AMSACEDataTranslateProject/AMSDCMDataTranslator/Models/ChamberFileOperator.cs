using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public class ChamberFileOperator:SiffFileOperator
    {
        public ChamberFileOperator(Chamber chamber)
        {
            this.chamber = chamber;
            SetConfig();
        }

        protected Chamber chamber;

        private void SetConfig()
        {
            SIFF_PATH = ChamberSetting.SiffPath;
            SIFF_HISTORY_PATH = ChamberSetting.SiffHistoryPath;
        }

        protected override void TranslateFile()
        {
            chamber.GetData();
            string siffFileName = chamber.WriteSiff(SiffPath);
            ChamberFtpOperator.UploadFile(siffFileName);
            LogHelper.ChamberInfoLog("数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            chamber.WriteXmlConfig();
        }

        public override void OperateFiles()
        {
            TranslateFile();
        }


        public void OperateTestFiles()
        {

            chamber.GetData();
            foreach (var line in chamber.Chamber_lines)
            {
                line.Lot = "TEST_" + line.Lot;
                line.Wafer = line.Wafer == "" ? "" : "TEST_" + line.Wafer;
            }
            string siffFileName = chamber.WriteSiff(SiffPath);
            ChamberFtpOperator.UploadFile(siffFileName);
            LogHelper.ChamberInfoLog("Test数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            chamber.WriteXmlConfig();
        }
    }
}
