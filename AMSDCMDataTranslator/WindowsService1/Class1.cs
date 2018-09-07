using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using AMSDCMDataTranslator.Helper;

namespace AMSTranslatorService
{
    public delegate void TranslatorRunnerDelegate();
    public delegate void TranslatorSettingDelegate();
    public partial class ModelTranslatorService : ServiceBase
    {

        public ModelTranslatorService()
        {
            InitializeComponent();
        }

        public ModelTranslatorService(string serverName)
        {
            InitializeComponent();
            ServerNameInLog = serverName;
        }

        protected TranslatorRunnerDelegate runnerDelegate;

        protected TranslatorSettingDelegate settingDelegate;

        private string ServerNameInLog = "";

        protected static int Interval;

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            LogHelper.InfoLog(ServerNameInLog + "服务启动成功");
            System.Timers.Timer t = new System.Timers.Timer
            {
                Interval = Interval
            };
            t.Elapsed += new System.Timers.ElapsedEventHandler(ChkSrv);//到达时间的时候执行事件；   
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；   
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；   
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            LogHelper.InfoLog(ServerNameInLog + "服务被关闭");
        }

        /// <summary>  
        /// 定时检查，并执行方法  
        /// </summary>  
        /// <param name="source"></param>  
        /// <param name="e"></param>  
        protected void ChkSrv(object source, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                System.Timers.Timer tt = (System.Timers.Timer)source;
                //可防止重复执行程序
                tt.Enabled = false;
                runnerDelegate?.Invoke();
                tt.Enabled = true;
            }
            catch (Exception err)
            {
                LogHelper.ErrorLog(ServerNameInLog, err);
            }
        }

        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        protected void SetServiceName(string serviceName)
        {
            this.ServiceName = serviceName;
        }
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
        }

        #endregion
    }
}
