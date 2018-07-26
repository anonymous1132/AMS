using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCADataTranslator.Bll
{
   public class CsvFileObject
    {
        public CsvFileObject()
        { }
        public CsvFileObject(string filePath)
        {
            this._filePath = filePath;
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value; }
        }

        public string Extension
        {
            get { return _filePath.Contains(".")?_filePath.Substring(_filePath.LastIndexOf(".")):""; }
        }

        public string Directory
        {
            get { return _filePath.Substring(0, _filePath.LastIndexOf("\\")+1); }
        }

        public string FileNameWithExtension
        {
            get { return _filePath.Substring(_filePath.LastIndexOf("\\")+1); }
        }

        public string FileName
        {
            get { return FileNameWithExtension.Substring(0,FileNameWithExtension.LastIndexOf(".")); }
        }
    }
}
