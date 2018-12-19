using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model;
using System.IO;
using Caojin.Common;
using System.Text.RegularExpressions;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Runner
{
    public class RunStatusFileRunner
    {
        public RunStatusFileRunner(string filePath)
        {
            this.filePath = filePath;
            Initialize();
        }

        private string filePath { get; set; }

        public RunStatusGroupModel RunStatusGroup { get;private set; } = new RunStatusGroupModel();

        private void Initialize()
        {
            RunStatusGroup.TargetTime = Regex.Match(filePath,"[0-9]{14}$").Value;
            
            string regexStr = "[0-9]{4}[-][0-9]{2}[-][0-9]{2}[-][0-9]{2}[.][0-9]{2}[.][0-9]{2}[.][0-9]{4}";
            List<string> lineList = FileHelper.ReadTxtFileToLineList(filePath).Where(w=>Regex.IsMatch(w,regexStr)).ToList();
            foreach (var item in lineList)
            {
                string rawLine = MergeSpace(item);
                var rawList = rawLine.Split(' ');
                if (rawList.Length < 6) continue;

                RunStatusGroup.LineModels.Add(new RunStatusLineModel()
                {
                    Apply_Qual=rawList[0],
                    Set_Name=rawList[1],
                    Source_Server=rawList[2],
                    SynchPoint=rawList[3],
                    SynchTime=rawList[4],
                    Status=rawList[5]
                });
            }
            RunStatusGroup.UpdateDelta();
        }

        private  string MergeSpace(string str)
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
