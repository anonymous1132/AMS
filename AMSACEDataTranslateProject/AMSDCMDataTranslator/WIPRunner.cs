using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
    public class WIPRunner
    {
        public static void RunAmsWIP()
        {
            LogHelper.WIPInfoLog("开始RunAMSWIP");
            AMSWIP wip = new AMSWIP();
            WIPFileOperator fileOperator = new WIPFileOperator(wip);
            try
            {
                fileOperator.OperateFiles();
                //SshOper ssh = new SshOper();
                //LogHelper.WIPInfoLog("AMSWIP\t" + ssh.GetResault("sh ~/eda/wip.sh\nsh ~/eda/chamber.sh"));
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

        public static void RunAmsWIPTest()
        {
            LogHelper.WIPInfoLog("开始RunAMSWIPTest");
            AMSWIP wip = new AMSWIP();
            WIPFileOperator fileOperator = new WIPFileOperator(wip);
            try
            {
                fileOperator.OperateFiles();
                //SshOper ssh = new SshOper();
                //LogHelper.WIPInfoLog("AMSWIPTest\t" + ssh.GetResault("sh ~/eda/wip.sh\nsh ~/eda/chamber.sh"));
                fileOperator.OperateDefectFiles();
            }
            catch (NoQueryDataException ne)
            {
                LogHelper.WIPInfoLog(ne.Message);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("AMSWIPTest", e);
            }
            LogHelper.WIPInfoLog("RunAMSWIPTest执行完毕");

        }
    }
}
