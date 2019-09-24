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
   public class InlineRunner
    {
        private static string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private static string ExeDirctory
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }
        private static string Config = @"App\config\inlinetime.config";
        public static string ConfigPath
        {
            get { return ExeDirctory + Config; }
        }

        private static string GetLastDBLine()
        {
            DataTable dt = XmlHelper.GetTable(ConfigPath, XmlHelper.XmlType.File, "Inline");
            return dt.DefaultView[0][0].ToString();
        }

        public static void RunInlineTest()
        {

            DateTime dt = new DateTime();
            DateTime.TryParseExact(GetLastDBLine(), "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);
            RunInlineTest(dt,DateTime.Now);
        }

        public static void RunAMSInline()
        {
            DateTime dt = new DateTime();
            DateTime.TryParseExact(GetLastDBLine(), "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out dt);
            RunAMSInline(dt,DateTime.Now);

        }

        public static void RunAMSInline(DateTime from,DateTime to,bool isRun2Now=true)
        {
            LogHelper.InlineInfoLog(string.Format( "开始RunAMSInline,From:{0},To:{1}",from.ToString("yyyy-MM-dd HH:mm:ss.ffffff"),to.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
            AMSInline inline = new AMSInline
            {
                StartTime = from,
                EndTime = to,
                IsRun2Now=isRun2Now
            };
            InlineFileOperator fileOperator = new InlineFileOperator(inline);
            try
            {
                fileOperator.OperateFiles();
                SshOper ssh = new SshOper();
                LogHelper.InlineInfoLog("AMSInline\t" + ssh.GetResault("sh ~/eda/inline.sh"));
            }
            catch (NoQueryDataException ne)
            {
                LogHelper.InlineInfoLog(ne.Message);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("AMSInline", e);
            }
            LogHelper.InlineInfoLog("RunAMSInline执行完毕");
        }

        public static void RunInlineTest(DateTime from, DateTime to,bool isRun2Now=true)
        {

            LogHelper.InlineInfoLog(string.Format("开始RunInlineTest,From:{0},To:{1}", from.ToString("yyyy-MM-dd HH:mm:ss.ffffff"), to.ToString("yyyy-MM-dd HH:mm:ss.ffffff")));
            AMSInline inline = new AMSInline
            {
                StartTime = from,
                EndTime = to,
                IsRun2Now=isRun2Now
            };
            InlineFileOperator fileOperator = new InlineFileOperator(inline);

            fileOperator.OperateFiles();
            // SshOper ssh = new SshOper();

        }
    }
}
