using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class ContentProgressCheckViewModel : NotifyPropertyChanged
    {
        private string textBox_File_Text;
        public string TextBox_File_Text { get { return textBox_File_Text; } set { textBox_File_Text = value; OnPropertyChanged("TextBox_File_Text"); } }

        private string textBox_File2_Text;
        public string TextBox_File2_Text
        {
            get { return textBox_File2_Text; }
            set
            {
                textBox_File2_Text = value; OnPropertyChanged("TextBox_File2_Text");
            }
        }


        public List<ProgressCheckModel> DG1_ItemSource
        {
            get
            {
                var modelList = new List<ProgressCheckModel>();
                foreach (string str in firstList)
                {
                    modelList.Add(new ProgressCheckModel() { FirstFileContent = str });
                }
                foreach (var item in modelList)
                {
                    item.SecondFileContent = secondList.Any(w => item.FirstFileContent == w) ? item.FirstFileContent : "";
                }
               secondList.Where(w => !firstList.Contains(w)).ToList().ForEach(f => modelList.Add(new ProgressCheckModel() { SecondFileContent = f }));
                return modelList;    
            }
        }

        private List<string> firstList = new List<string>();
        public List<string> FirstList { get { return firstList; }set { firstList = value;OnPropertyChanged("DG1_ItemSource"); } }

        private List<string> secondList = new List<string>();
        public List<string> SecondList { get { return secondList; } set {secondList = value;  OnPropertyChanged("DG1_ItemSource"); } }
    }
}
