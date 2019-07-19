using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;


namespace AMSDCMDataTranslator
{
   public class InlineRunner
    {
        //public static void RunDebug()
        //{
        //    LogHelper.InlineInfoLog("开始执行InlineDebugTranslator");
        //    //InlineTempDebug inline = new InlineTempDebug();
        //    MESInline inline = new MESInline();
        //    InlineFileOperator fileOperator = new InlineFileOperator(inline);
        //    try
        //    {
        //        fileOperator.OperateFiles();
        //        SshOper ssh = new SshOper();
        //        LogHelper.InlineInfoLog("Inline\t"+ssh.GetResault("sh ~/eda/inline.sh"));
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelper.ErrorLog("Inline", e);
        //    }
        //    //try
        //    //{
        //    //    EDC_Lot_Summary summary = new EDC_Lot_Summary();
        //    //}
        //    //catch (Exception e)
        //    //{
        //    //    LogHelper.ErrorLog("Inline", e);
        //    //}

        //    LogHelper.InlineInfoLog("InlineDebugTranslator执行完毕");
        //}

        //public static void RunTest()
        //{
        //    LogHelper.InlineInfoLog("开始执行InlineTestTranslator");
        //    MESInline inline = new MESInline();
        //    InlineFileOperator fileOperator = new InlineFileOperator(inline);
        //    try
        //    {
        //        fileOperator.OperateTestFiles();
        //        SshOper ssh = new SshOper();
        //        LogHelper.InlineInfoLog("Inline\t" + ssh.GetResault("sh ~/eda/inline.sh"));
        //    }
        //    catch (Exception e)
        //    {
        //        LogHelper.ErrorLog("InlineTest", e);
        //    }
        //    LogHelper.InlineInfoLog("InlineTestTranslator执行完毕");
        //}

        public static void RunInlineTest()
        {
            LogHelper.InlineInfoLog("开始RunInlineTestTranslator");
            AMSInline inline = new AMSInline();
            InlineFileOperator fileOperator = new InlineFileOperator(inline);
  
                fileOperator.OperateFiles();
               // SshOper ssh = new SshOper();
   
        }

        public static void RunAMSInline()
        {
            LogHelper.InlineInfoLog("开始RunAMSInline");
            AMSInline inline = new AMSInline();
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
    }
}
