using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailQueueQueryViewModel:NotifyPropertyChanged
    {
        private string textBox_Content_Text;
        /// <summary>
        /// TextBox_Content的Text属性绑定内容
        /// </summary>
        public string TextBox_Content_Text
        {
            get { return textBox_Content_Text; }
            set { textBox_Content_Text = value; OnPropertyChanged("TextBox_Content_Text"); }
        }

        private string textBox_Content_Visibility;
        public string TextBox_Content_Visibility
        {
            get { return textBox_Content_Visibility; }
            set { textBox_Content_Visibility = value; OnPropertyChanged("TextBox_Content_Visibility"); }
        }

        private string textBlock_Resault_Text;
        public string TextBlock_Resault_Text
        {
            get { return textBlock_Resault_Text; }
            set { textBlock_Resault_Text = value; OnPropertyChanged("TextBlock_Resault_Text"); }
        }

        private int comboTitle_SelectedIndex = 0;
        /// <summary>
        /// ComboTitle的SelectedIndex属性绑定内容
        /// </summary>
        public int ComboTitle_SelectedIndex
        {
            get { return comboTitle_SelectedIndex; }
            set
            {
                comboTitle_SelectedIndex = value;
                if (value == 2)
                {
                    StackPanel_Visibility = "Visible";
                    TextBox_Content_Visibility = "Collapsed";
                }
                else
                {
                    StackPanel_Visibility = "Collapsed";
                    TextBox_Content_Visibility = "Visible";
                }
                OnPropertyChanged("ComboTitle_SelectedIndex");
            }
        }

        private string stackPanel_Visibility = "Collapsed";
        /// <summary>
        /// StackPanel的Visibility属性绑定内容
        /// </summary>
        public string StackPanel_Visibility
        {
            get { return stackPanel_Visibility; }
            set { stackPanel_Visibility = value; OnPropertyChanged("StackPanel_Visibility"); }
        }
        private DateTime datePicker1_Value = DateTime.Now.Date;
        /// <summary>
        /// DatePicker1 选中值绑定内容
        /// </summary>
        public DateTime DatePicker1_Value
        {
            get { return datePicker1_Value; }
            set { datePicker1_Value = value; OnPropertyChanged("DatePicker1_Value"); }
        }

        private DateTime datePicker2_Value = DateTime.Now.Date;
        /// <summary>
        /// DatePicker1 选中值绑定内容
        /// </summary>
        public DateTime DatePicker2_Value
        {
            get { return datePicker2_Value; }
            set { datePicker2_Value = value; OnPropertyChanged("DatePicker2_Value"); }
        }

        private TimeSpan timePicker1_Value = new TimeSpan();
        public TimeSpan TimePicker1_Value
        {
            get { return timePicker1_Value; }
            set
            {
                timePicker1_Value = value; OnPropertyChanged("TimePicker1_Value");
            }
        }

        private TimeSpan timePicker2_Value = TimeSpan.Parse("23:59:59");
        public TimeSpan TimePicker2_Value
        {
            get { return timePicker2_Value; }
            set
            {
                timePicker2_Value = value; OnPropertyChanged("TimePicker2_Value");
            }
        }

        public DateTime DateTimePicker1
        {
            get { return datePicker1_Value.Add(timePicker1_Value); }
        }

        public DateTime DateTimePicker2
        {
            get { return datePicker2_Value.Add(timePicker2_Value); }
        }

        private System.Collections.ObjectModel.ObservableCollection<MailQueueViewModel> dG1_ItemSource;
        /// <summary>
        /// DG1 ItemSource属性绑定内容
        /// </summary>
        public System.Collections.ObjectModel.ObservableCollection<MailQueueViewModel> DG1_ItemSource
        {
            get { return dG1_ItemSource; }
            set { dG1_ItemSource = value; OnPropertyChanged("DG1_ItemSource"); if (value.Count == 0) { TextBlock_Resault_Text = ""; } else { TextBlock_Resault_Text = string.Format("共查询{0}条数据", value.Count.ToString()); } }
        }
    }
}
