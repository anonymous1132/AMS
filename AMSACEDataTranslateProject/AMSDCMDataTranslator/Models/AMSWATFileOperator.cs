using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class AMSWATFileOperator:WATFileOperator
    {
        public AMSWATFileOperator(Etest etest)
        {
            Etest = etest;
            SetConfig();
            fileSuffix = "DAT";
          //  ((AMSWAT)etest).filePath = filePath;
            ((AMSWAT)etest).specPath = SpecPath;
        }


        private void SetConfig()
        {
            WORKING_PATH = WATSetting.WorkingPath;
            SOURCE_FILE_PATH = WATSetting.SourcePath;
            HISTORY_PATH = WATSetting.HistoryPath;
            SIFF_PATH = WATSetting.SiffPath;
            SIFF_HISTORY_PATH = WATSetting.SiffHistoryPath;
            SPEC_PATH = WATSetting.SpecPath;
        }
    }
}
