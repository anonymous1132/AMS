using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class HlcmWATFileOperator:WATFileOperator
    {
        public HlcmWATFileOperator(Etest etest)
        {
            Etest = etest;
            SetConfig();
            fileSuffix = "xlsx";
        }


        //private string SFtpServerIP;

        //private string SFtpUserID;

        //private string SFtpPassword;

        private void SetConfig()
        {
            WORKING_PATH = HlcmSetting.WorkingPath;
            HISTORY_PATH = HlcmSetting.HistoryPath;
            SIFF_PATH = HlcmSetting.SiffPath;
            SIFF_HISTORY_PATH = HlcmSetting.SiffHistoryPath;
            SOURCE_FILE_PATH = HlcmSetting.SourcePath;
        }

        //private IList<string> _newFileNames;
        ///// <summary>
        ///// 获取所有新文件名
        ///// </summary>
        //protected override IList<string> GetNewFileNames
        //{
        //    get
        //    {
        //        if (_newFileNames == null)
        //        {
        //            _newFileNames = new List<string>();

        //        }
        //        return _newFileNames;
        //    }
        //}
    }
}
