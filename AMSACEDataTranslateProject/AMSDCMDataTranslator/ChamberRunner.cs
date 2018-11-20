using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
    public class ChamberRunner
    {
        public static void RunAMSChamber()
        {
            LogHelper.ChamberInfoLog("开始RunAMSChamber");
            AMSChamber chamber = new AMSChamber();
            ChamberFileOperator fileOperator = new ChamberFileOperator(chamber);
            try
            {
                fileOperator.OperateFiles();
                SshOper ssh = new SshOper();
                LogHelper.ChamberInfoLog("AMSChamber\t" + ssh.GetResault("sh ~/eda/chamber.sh"));
            }
            catch (NoQueryDataException ne)
            {
                LogHelper.ChamberInfoLog(ne.Message);
            }
            catch (Exception e)
            {
                LogHelper.ErrorLog("AMSChamber", e);
            }
            LogHelper.ChamberInfoLog("RunAMSChamber执行完毕");
        }
    }
}
