using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;
using MCADataTranslator.Helper;

namespace MCADataTranslator.Bll
{
    public class QueryDataViewModel : NotifyPropertyChanged
    {
        public QueryDataViewModel()
        { }
        private string _sampleComment;
        public string SampleComment
        {
            get { return _sampleComment; }
            set { _sampleComment = value; OnPropertyChanged("SampleComment"); }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; OnPropertyChanged("IsSelected"); }
        }

        private string _uid;
        public string UID
        {
            get { return _uid; }
            set { _uid = value;OnPropertyChanged("UID"); }
        }

        private string _updateDateTime;
        public string UpdateDateTime
        {
            get { return _updateDateTime; }
            set { _updateDateTime = value;OnPropertyChanged("UpdateDateTime"); }
        }

        private bool _ag;
        public bool Ag
        {
            get { return _ag; }
            set { _ag = value;OnPropertyChanged("Ag"); }
        }

        private bool _w;
        public bool W
        {
            get { return _w; }
            set { _w = value;OnPropertyChanged("W"); }
        }

        private bool _hideInACE;
        public bool HideInACE
        {
            get { return _hideInACE; }
            set { _hideInACE = value; UpdateHideInACE(); OnPropertyChanged("HideInACE"); }
        }

        private void UpdateHideInACE()
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = string.Format("update MCA_Pool set HideInACE= {0} where UID ='{1}'", _hideInACE == false ? 0 : 1,_uid);
            sqlHelper.getSomeDate(sql);
        }
    }
}
