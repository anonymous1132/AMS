using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caojin.Common;
using System.Text.RegularExpressions;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Runner
{
    public class ProgressCheckFileRunner
    {
        public ProgressCheckFileRunner(string filePath)
        {
            FilePath = filePath;
            Initialize();
        }

        private string FilePath { get; set; }

        public List<string> LineContent { get; set; } = new List<string>();

        private void Initialize()
        {
 
            List<string> lineList = FileHelper.ReadTxtFileToLineList(FilePath);
            foreach (string str in lineList)
            {
                string temp = MergeSpace(str.Trim());
                List<string> rawList = temp.Split(' ').ToList();
                if (rawList.Count < 10) continue;
                LineContent.Add(string.Format("{0} {1}",rawList[8],rawList[9]));
            }
        }

        private string MergeSpace(string str)
        {
            if (str != string.Empty &&
            str != null &&
            str.Length > 0
            )
            {
                str = new Regex("[\\s]+").Replace(str, " ");
            }
            return str;
        }
    }
}
