using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class ProgressCheckModel:NotifyPropertyChanged
    {
        private string first;
        public string FirstFileContent { get { return first; } set { first = value;OnPropertyChanged("FirstFileContent");OnPropertyChanged("Sign"); } }

        private string second;
        public string SecondFileContent { get { return second; }set { second = value;OnPropertyChanged("SecondFileContent"); OnPropertyChanged("Sign"); } }

        public string Sign
        {
            get {

                if (first == second) return "√";
                else if (string.IsNullOrEmpty(first)) return "-";
                else return "+";
            }
        }
    }
}
