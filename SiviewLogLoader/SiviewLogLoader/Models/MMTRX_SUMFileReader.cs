using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caojin.Common;

namespace SiviewLogLoader.Models
{
    public class MMTRX_SUMFileReader
    {
        public MMTRX_SUMFileReader(string filePath)
        {
            FilePath = filePath;
            Initialize();
        }

        public string FilePath
        {
            get;
            set;
        }

        LogContext db = new LogContext();
        private void Initialize()
        {
            if (db.MMTRX_SUMFiles.Any(a => a.FileName == FilePath))
            {
                return;
            }

            List<string> list = FileHelper.ReadTxtFileToLineList(FilePath);
            foreach (string line in list)
            {
                var arry = line.Split('|');
                DateTime logTime;
                if (DateTime.TryParse(arry[0], out logTime)&&arry.Length==8)
                {
                    db.MMTRX_SUMEntities.Add(new MMTRX_SUMEntity() {
                        Log_Time = logTime,
                        TRX_ID = arry[1],
                        RETN_CODE = arry[2],
                        RESP_sum = double.Parse(arry[3]),
                        TRX_ID_count = int.Parse(arry[4]),
                        RESP_mean = double.Parse(arry[5]),
                        RESP_min = double.Parse(arry[6]),
                        RESP_max = double.Parse(arry[7]),
                    });
                }
            }
            db.MMTRX_SUMFiles.Add(new MMTRX_SUMFilesEntity() {FileName=FilePath });
            db.SaveChanges();
        }

    }
}
