using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace MCADataTranslator.Bll
{
    public class ImportDGViewModel : NotifyPropertyChanged
    {
        public ImportDGViewModel()
        { }

        public ImportDGViewModel(string fp)
        {
            FilePath = fp;
        }

        
        public string FileName
        {
            get { return csvFile.FileName; }
          
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value;csvFile = new CsvFileObject(value);OnPropertyChanged("FilePath"); }
        }

        private CsvFileObject csvFile;

        private string _csvType;
        public string CsvType
        {
            get { return _csvType; }
            set { _csvType = value; OnPropertyChanged("CsvType"); }
        }

        private string _sampleComment;
        public string SampleComment
        {
            get { return _sampleComment; }
            set { _sampleComment = value; OnPropertyChanged("SampleComment"); }
        }


        private string _fileDir;
        public string FileDir
        {
            get { return _fileDir; }
            set { _fileDir = value;OnPropertyChanged("FileDir"); }
        }

        private string _operationResult;
        public string OperationResult
        {
            get { return _operationResult; }
            set { _operationResult = value;OnPropertyChanged("OperationResult"); }

        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set { _comment = value;OnPropertyChanged("Comment"); }
        }

        public CSVValueViewModel Csv_VM;
    }
}
