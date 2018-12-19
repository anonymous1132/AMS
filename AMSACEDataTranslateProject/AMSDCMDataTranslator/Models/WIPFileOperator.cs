using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;


namespace AMSDCMDataTranslator.Models
{
    public class WIPFileOperator:SiffFileOperator
    {
        public WIPFileOperator(WIP wip)
        {
            this.wip = wip;
            SetConfig();
        }

        protected WIP wip;

        private void SetConfig()
        {
            SIFF_PATH = WIPSetting.SiffPath;
            SIFF_HISTORY_PATH = WIPSetting.SiffHistoryPath;
        }

        //private string ChamberSiffPath
        //{
        //    get { return ChamberSetting.SiffPath; }
        //}

        //private string ChamberSiffHistoryPath
        //{
        //    get { return exeDirctory + ChamberSetting.SiffHistoryPath; }
        //}


        protected override void TranslateFile()
        {
            wip.GetData();
            string siffFileName = wip.WriteSiff(SiffPath);
            WIPFtpOperator.UploadFile(siffFileName);
            LogHelper.WIPInfoLog("数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
            //取消Chamber功能
            //if (wip is AMSWIP)
            //{
            //    string chamberSiffName = ((AMSWIP)wip).WriteChamberSiff(ChamberSiffPath);
            //    ChamberFtpOperator.UploadFile(chamberSiffName);
            //    LogHelper.ChamberInfoLog("数据转换成功—SiffFile:" + chamberSiffName);
            //    FileHelper.Move(chamberSiffName,ChamberSiffHistoryPath+chamberSiffName.Substring(chamberSiffName.LastIndexOf("\\")));
            //}
            wip.WriteXmlConfig();
        }

        public override void OperateFiles()
        {
            TranslateFile();
        }

        //取消该测试方法
        //public void OperateTestFiles()
        //{

        //    wip.GetData();
        //    foreach (var line in wip.WIP_lines)
        //    {
        //        line.Lot = "TEST_" + line.Lot;
        //        line.SourceLot = "TEST_" + line.SourceLot;
        //        line.Fab = "FABTEST";
        //        //line.Wafer = line.Wafer == "" ? "" : "TEST_" + line.Wafer;
        //        var list = line.WaferList.Split(',').ToList();
        //        line.WaferList = "";
        //        foreach (var l in list)
        //        {
        //            line.WaferList += string.Format("TEST_{0},",l);
        //        }
        //        line.WaferList.Trim(',');
        //    }
        //    string siffFileName = wip.WriteSiff(SiffPath);
        //    WIPFtpOperator.UploadFile(siffFileName);
        //    LogHelper.WIPInfoLog("数据转换成功—SiffFile:" + siffFileName);
        //    FileHelper.Move(siffFileName, SiffHistoryPath + siffFileName.Substring(siffFileName.LastIndexOf("\\")));
        //    if (wip is AMSWIP)
        //    {
        //        foreach (var line in ((AMSWIP)wip).Chamber_Lines)
        //        {
        //            line.Lot = "TEST_" + line.Lot;
        //            line.Wafer = line.Wafer == "" ? "" : "TEST_" + line.Wafer;
        //        }
        //        string chamberSiffName = ((AMSWIP)wip).WriteChamberSiff(ChamberSiffPath);
        //        ChamberFtpOperator.UploadFile(chamberSiffName);
        //        LogHelper.ChamberInfoLog("数据转换成功—SiffFile:" + chamberSiffName);
        //        FileHelper.Move(chamberSiffName, ChamberSiffHistoryPath + chamberSiffName.Substring(chamberSiffName.LastIndexOf("\\")));
        //    }
        //    wip.WriteXmlConfig();
        //}

        //对Defect那边进行操作
        public void OperateDefectFiles()
        {
            foreach (var line in wip.WIP_lines)
            {
             line.MoveOutTime = line.MoveInTime.AddSeconds(1);
            }

            string siffFileName = wip.WriteSiff(SiffPath);
            WIPFtpOperator.UploadDefectFile(siffFileName);

            LogHelper.WIPInfoLog("数据转换成功—SiffFile:" + siffFileName);
            FileHelper.Move(siffFileName, SiffHistoryPath + "\\Defect" +siffFileName.Substring(siffFileName.LastIndexOf("\\")+1));
        }
    }

   
}
