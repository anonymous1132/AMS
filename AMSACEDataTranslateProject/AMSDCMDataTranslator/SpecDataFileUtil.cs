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
        public string FilePath { get; set; }

        public List<WATSpecData> datalist;

        private void GetData()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                datalist = null;
                throw new Exception("SpecFile文件路径为空");
            }
            if (!File.Exists(FilePath))
            {
                throw new Exception("SpecFile:\t"+FilePath+"\t文件不存在");
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
