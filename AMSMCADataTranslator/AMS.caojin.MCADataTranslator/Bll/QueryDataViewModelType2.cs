using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace MCADataTranslator.Bll
{
   public class QueryDataViewModelType2:NotifyPropertyChanged
    {
        public QueryDataViewModelType2()
        { }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged("FileName"); }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private string _importDateTime;
        public string ImportDateTime
        {
            get { return _importDateTime; }
            set { _importDateTime = value; OnPropertyChanged("ImportDateTime"); }
        }

    }
}
