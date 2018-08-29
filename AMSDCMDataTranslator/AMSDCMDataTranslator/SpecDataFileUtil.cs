using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using System.IO;

namespace AMSDCMDataTranslator
{
   public class SpecDataFileUtil
    {
        public SpecDataFileUtil(string filePath)
        {
            FilePath = filePath;
            GetData();
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (File.Exists(value))
                {
                    _filePath = value;
                }
                else { _filePath = null; return; }
            }
        }

        public List<WATSpecData> datalist;

        private void GetData()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                datalist = null;
                return;
            }
            datalist = new List<WATSpecData>();
            StreamReader sr = new StreamReader(FilePath, Encoding.Default);
            String line;
            while ((line = sr.ReadLine()) != null)
            {
                WATSpecData data = new WATSpecData(line);
                datalist.Add(data);
            }
        }


    }
}
