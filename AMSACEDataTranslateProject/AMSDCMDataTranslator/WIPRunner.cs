using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;
using System.Data;

namespace AMSDCMDataTranslator
{
    public class WIPRunner
    {
        private static string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private static string ExeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }

        private static string Config = @"App\config\wiptime.config";
        public static string ConfigPath
        {
            get { return ExeDirctory + Config; }
        }
        private static string GetLastDBLine()
        {
            DataTable dt = XmlHelper.GetTable(ConfigPath, XmlHelper.XmlType.File, "WIP");
            return dt.DefaultView[0][0].ToString();
        }
        public static void RunAmsWIP()
        {
            DateTime dt = new DateTime();
            DateTime.TryParseExact(GetLastDBLine(), "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);
            RunAmsWIP(dt,DateTime.Now);
        }

        public static void RunAmsWIPTest()
        {
            DateTime dt = new DateTime();
            DateTime.TryParseExact(GetLastDBLine(), "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);
            RunAmsWIPTest(dt,DateTime.Now);
        }

        //public static bool IsRunToNow { get; set; } = true;

        public static void RunAmsWIP(DateTime from, DateTime to,bool isRun2Now=true)
        {
            LogHelper.WIPInfoLog(string.Format("开始RunAMSInline,From:{0},To:{1}", from.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), to.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
            AMSWIP wip = new AMSWIP()
            {
                StartTime = from,
                EndTime = to,
                IsRun2Now=isRun2Now
            };
            WIPFileOperator fileOperator = new WIPFileOperator(wip);
            try
            {
                fileOperator.OperateFiles();
                SshOper ssh = new SshOper();
                LogHelper.WIPInfoLog("AMSWIP\t" + ssh.GetResault("sh ~/eda/wip.sh"));
                //单独loadDefect WIP
                fileOperator.OperateDefectFiles();
            }
            catch (NoQueryDataException ne)
            {
                LogHelper.WIPInfoLog(ne.Message);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("AMSWIP", e);
            }
            LogHelper.WIPInfoLog("RunAMSWIP执行完毕");

        }

        public static void RunAmsWIPTest(DateTime from, DateTime to,bool isRun2Now=true)
        {
            LogHelper.WIPInfoLog(string.Format("开始RunWIPTest,From:{0},To:{1}", from.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), to.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
            AMSWIP wip = new AMSWIP()
            {
                StartTime=from,
                EndTime=to,
                IsRun2Now=isRun2Now
            };
            WIPFileOperator fileOperator = new WIPFileOperator(wip);
            fileOperator.OperateFiles();
        }
    }
}
