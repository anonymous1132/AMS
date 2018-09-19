using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace AMSACEWebMonitor
{
    public partial class ACEWebMonitor : ServiceBase
    {
        public ACEWebMonitor()
        {
            InitializeComponent();
            Setting.GetValue();
        }

        private double Interval
        {
            get { return 1000 * 60 * 60 * Setting.Interval; }
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
                MonitorExcute monitorExcute = new MonitorExcute();
                monitorExcute.DoCheck();
                tt.Enabled = true;
            }
            catch (Exception ex)
            {
                LogHelper.LogWrite("DoCheck Error:"+ex.Message);
            }

        }


        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            System.Timers.Timer t = new System.Timers.Timer
            {
                Interval = this.Interval
            };
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件；   
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；   
            LogHelper.LogWrite("服务启动成功");
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            LogHelper.LogWrite("服务已被停止");
        }
    }
}
