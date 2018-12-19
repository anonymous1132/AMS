using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Presentation;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailSmtpImportViewModel:NotifyPropertyChanged
    {
        private string tb_file_text;
        /// <summary>
        ///TextBox tb_file的Text属性绑定内容
        /// </summary>
        public string TextBox_File_Text
        {
            get { return tb_file_text; }
            set
            {
                tb_file_text = value; OnPropertyChanged("TextBox_File_Text");
                //如果内容为空，则import按钮不可见
                if (string.IsNullOrEmpty(value))
                { ButtonImport_IsEnabled = false; }
                else
                { ButtonImport_IsEnabled = true; }
            }
        }

        private bool progressRing_IsActive = false;
        /// <summary>
        /// ProgressRing的IsActive属性绑定内容
        /// </summary>
        public bool ProgressRing_IsActive
        {
            get { return progressRing_IsActive; }
            set { progressRing_IsActive = value; ButtonImport_IsEnabled = !value; OnPropertyChanged("ProgressRing_IsActive"); }
        }

        private string tb_resault_text;
        /// <summary>
        /// TextBlock_Text的Text属性绑定内容
        /// </summary>
        public string TextBlock_Resault_Text
        {
            get { if (DG1_ItemSource.Count == 0) tb_resault_text = ""; else tb_resault_text = string.Format(@"共插入{0}条记录", DG1_ItemSource.Count.ToString()); return tb_resault_text; }
        }

        private bool buttonImport_IsEnabled = false;
        /// <summary>
        /// ButtonImport的IsEnabled属性绑定值
        /// </summary>
        public bool ButtonImport_IsEnabled
        {
            get { return buttonImport_IsEnabled; }
            set { buttonImport_IsEnabled = value; OnPropertyChanged("ButtonImport_IsEnabled"); }
        }

        private List<MailSmtpModel> dG1_ItemSource = new List<MailSmtpModel>();
        /// <summary>
        /// DG1的ItemSource属性绑定值
        /// </summary>
        public List<MailSmtpModel> DG1_ItemSource
        {
            get { return dG1_ItemSource; }
            set { dG1_ItemSource = value; OnPropertyChanged("DG1_ItemSource"); OnPropertyChanged("TextBlock_Resault_Text"); }
        }

    }
}
