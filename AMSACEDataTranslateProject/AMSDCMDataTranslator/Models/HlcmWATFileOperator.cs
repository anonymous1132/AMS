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
            //测试临时使用
            SOURCE_FILE_PATH = @"C:\Users\caojin\Desktop\test";
        }

        private IList<string> _newFileNames;
        /// <summary>
        /// 获取所有新文件名
        /// </summary>
        protected override IList<string> GetNewFileNames
        {
            get
            {
                if (_newFileNames == null)
                {
                    _newFileNames = new List<string>();
                    //foreach (string file in SMBAllFileNames)
                    //{
                    //    if (HistoryFiles == null || !HistoryFiles.Contains(file))
                    //    {
                    //        _newFileNames.Add(file);
                    //    }
                    //}
                }
                return _newFileNames;
            }
        }
    }
}
