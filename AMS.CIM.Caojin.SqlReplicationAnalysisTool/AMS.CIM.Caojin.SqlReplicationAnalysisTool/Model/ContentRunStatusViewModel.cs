using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class ContentRunStatusViewModel : NotifyPropertyChanged
    {
        private string textBox_File_Text;
        public string TextBox_File_Text
        {
            get { return textBox_File_Text; }
            set { textBox_File_Text = value;OnPropertyChanged("TextBox_File_Text"); }
        }

        private string textBlock_TargetTime_Text;
        public string TextBlock_TargetTime_Text { get { return textBlock_TargetTime_Text; } set { textBlock_TargetTime_Text = value;OnPropertyChanged("TextBlock_TargetTime_Text"); } }

        private RunStatusGroupModel dG1_ItemSource=new RunStatusGroupModel();
        public RunStatusGroupModel DG1_ItemSource
        {
            get { return dG1_ItemSource; }
            set { dG1_ItemSource = value;OnPropertyChanged("DG1_ItemSource"); }
        }

    }
}
