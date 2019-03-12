using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System.Collections.ObjectModel;

namespace MCADataTranslator.Bll
{
   public class CSVValueViewModel
         : NotifyPropertyChanged
    {
        public CSVValueViewModel()
        { }

        private string _guid;
        public string GUID
        {
            get
            {
                if (string.IsNullOrEmpty(_guid))
                { _guid = Guid.NewGuid().ToString(); }
                return _guid;
             }
            set { _guid = value; OnPropertyChanged("GUID"); }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set { _user = value;OnPropertyChanged("User"); }
        }

        private string _dataTime;
        public string DataTime
        {
            get { return _dataTime; }
            set { _dataTime = value;OnPropertyChanged("DataTime"); }
        }

        private string _sample;
        public string Sample
        {
            get { return _sample; }
            set { _sample = value;OnPropertyChanged("Sample"); }
        }

        private string _sampleComment;
        public string SampleComment
        {
            get { return _sampleComment; }
            set { _sampleComment = value;OnPropertyChanged("SampleComment"); }
        }

        private string _conditionName;
        public string ConditionName
        {
            get { return _conditionName; }
            set { _conditionName = value;OnPropertyChanged("ConditionName"); }
        }

        private string _conditionComment;
        public string ConditionComment
        {
            get { return _conditionComment; }
            set { _conditionComment = value;OnPropertyChanged("ConditionComment"); }
        }

        private string _repeat;
        public string Repeat
        {
            get { return _repeat; }
            set { _repeat = value;OnPropertyChanged("Repeat"); }
        }

        private string _repeatMax;
        public string RepeatMax
        {
            get { return _repeatMax; }
            set { _repeatMax = value;OnPropertyChanged("RepeatMax"); }
        }

        private string _recipe;
        public string Recipe
        {
            get { return _recipe; }
            set { _recipe = value;OnPropertyChanged("Recipe"); }
        }

        private string _recipeComment;
        public string RecipeComment
        {
            get { return _recipeComment; }
            set { _recipeComment = value;OnPropertyChanged("RecipeComment"); }
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set { _version = value;OnPropertyChanged("Version"); }
        }

        private string _fileDir;
        public string FileDir
        {
            get { return _fileDir; }
            set { _fileDir = value;OnPropertyChanged("FileDir"); }
        }

        private string _fileName;
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; OnPropertyChanged("FileName"); }
        }

        public bool Ag { get; set; } = false;

        public bool W { get; set; } = false;

        public ObservableCollection<AgSeriesViewModel> agvm_list=new ObservableCollection<AgSeriesViewModel>();
       public ObservableCollection<WSeriesViewModel> wvm_list=new ObservableCollection<WSeriesViewModel>();
    }
}
