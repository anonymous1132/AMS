using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMSCPAUFileConsole
{
    public class SettingModel
    {
        public string CPAUPath { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public List<string> DestFiles { get; set; }
        
        public string DecPath { get; set; }

        public string BatPath { get; set; }

        public string KeyDirectory { get; set; }

        public bool IsDomainUser { get; set; }

        public string RealCPAUPath { get { return GetRealCPAUPath(CPAUPath); } }

        public string RealDecPath { get { return GetRealDirectory(DecPath); } }

        public string RealBatPath { get { return GetRealDirectory(BatPath); } }

        public static string GetRealDirectory(string orinalDic)
        {
            if (Directory.Exists(orinalDic)) return orinalDic;
            string path = Path.Combine(Public.ExeDirectory, orinalDic);
            if (Directory.Exists(path))
            {
                return path;
            }
            throw new Exception("Directory not found：" + orinalDic);
        }

        public static string GetRealCPAUPath(string orinalPath)
        {
            string curPath = Path.Combine(Public.ExeDirectory, "CPAU.exe");
            if (File.Exists(curPath)) return curPath;
            if (File.Exists(orinalPath)) return orinalPath;
            string path = Path.Combine(Public.ExeDirectory, orinalPath);
            if (Directory.Exists(path))
            {
                return path;
            }
            throw new Exception("File not found：" + orinalPath);
        }
    }
}
