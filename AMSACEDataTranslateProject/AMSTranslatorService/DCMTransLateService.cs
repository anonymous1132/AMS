using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using AMSDCMDataTranslator.Helper;

namespace AMSTranslatorService
{
    public partial class DCMTransLateService : ServiceBase
    {
        public DCMTransLateService()
        {
            InitializeComponent();
            DCMSetting.SetValue();
        }



        protected override void OnStart(string[] args)
        {
            LogHelper.InfoLog("DCM服务启动成功");
            System.Timers.Timer t = new System.Timers.Timer
            {
                Interval = DCMSetting.Interval
            };
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件；   
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；   
            // AMSDCMDataTranslator.Program.Main(null);
        }

        /// <summary>  
        /// 定时检查，并执行方法  
        /// </summary>  
        /// <param name="source"></param>  
        /// <param name="e"></param>  
        public void ChkSrv(object source, System.Timers.ElapsedEventArgs e)
        {
                try
                {
                    System.Timers.Timer tt = (System.Timers.Timer)source;
                 //可防止重复执行程序
                    tt.Enabled = false;
                    AMSDCMDataTranslator.DCMRunner.Run();
                    tt.Enabled = true;
                }
                catch (Exception err)
                {
                    LogHelper.ErrorLog(err);
                }
            
        }

        ///在指定时间过后执行指定的表达式  
        ///  
        ///事件之间经过的时间（以毫秒为单位）  
        ///要执行的表达式  
        public static void SetTimeout(double interval, Action action)
        {
            System.Timers.Timer timer = new System.Timers.Timer(interval);
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                timer.Enabled = false;
                action();
            };
            timer.Enabled = true;
        }


        protected override void OnStop()
        {
            LogHelper.InfoLog("DCM服务被关闭");
        }
    }
}
