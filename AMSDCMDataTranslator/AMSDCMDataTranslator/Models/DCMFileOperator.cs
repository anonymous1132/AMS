using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
    public class DCMFileOperator:FileOperator
    {
        public DCMFileOperator()
        {
            SetConfig();
        }
        private void SetConfig()
        {
            WORKING_PATH = DCMSetting.WorkingPath;
            SOURCE_FILE_PATH = DCMSetting.SourcePath;
            HISTORY_PATH = DCMSetting.HistoryPath;
        }

        public DataSet DCMData;
        public override void OperateFiles(string fileSuffix)
        {
            DCMData = new DataSet();
            CopyNewFiles();
            foreach (string fileName in GetNewFileNames)
            {
                string srcPath = WorkingPath + "\\" + fileName;
                string desPath = HistoryPath + "\\" + fileName;
                try
                {
                    if (string.IsNullOrEmpty(fileSuffix) || (fileName.Substring(fileName.LastIndexOf(".") + 1).ToUpper() == fileSuffix.ToUpper()))
                    {
                        CsvHelper csv = new CsvHelper();
                        DataTable dt = csv.readCsvTxtWithColumnName(srcPath);
                        DCMData.Tables.Add(dt);
                    }
                    FileHelper.Move(srcPath, desPath);
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog("DCM:\t",e);
                }
            }
        }
    }
}
