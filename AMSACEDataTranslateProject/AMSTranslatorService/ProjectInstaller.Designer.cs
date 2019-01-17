namespace AMSTranslatorService
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.serviceProcessInstaller1 = new System.ServiceProcess.ServiceProcessInstaller();
            this.serviceInstaller1 = new System.ServiceProcess.ServiceInstaller();
            this.serviceInstaller2 = new System.ServiceProcess.ServiceInstaller();

            // 
            // serviceProcessInstaller1
            // 
            this.serviceProcessInstaller1.Password = "amsc@2018";
            this.serviceProcessInstaller1.Username = ".\\Administrator";
            // 
            // serviceInstaller1
            // 
            this.serviceInstaller1.Description = "AMSDCMTranslate Service";
            this.serviceInstaller1.DisplayName = "DCMTranslate Service";
            this.serviceInstaller1.ServiceName = "DCMTranslateService";
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceInstaller2
            // 
            this.serviceInstaller2.Description = "AMSWATTranslate Service";
            this.serviceInstaller2.DisplayName = "WATTranslate Service";
            this.serviceInstaller2.ServiceName = "WATTranslateService";
            this.serviceInstaller2.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceInstaller3
            // 
            //this.serviceInstaller3.Description = "HlmcWATTranslate Service";
            //this.serviceInstaller3.DisplayName = "HlmcTranslate Service";
            //this.serviceInstaller3.ServiceName = "HlmcTranslateService";
            //this.serviceInstaller3.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // serviceInstaller4
            // 
            //this.serviceInstaller4.Description = "MesInlineTranslate Service";
            //this.serviceInstaller4.DisplayName = "InlineTranslate Service";
            //this.serviceInstaller4.ServiceName = "InlineTranslateService";
            //this.serviceInstaller4.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.serviceProcessInstaller1,
            this.serviceInstaller1,
            this.serviceInstaller2
            });

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller serviceProcessInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller1;
        private System.ServiceProcess.ServiceInstaller serviceInstaller2;

    }
}