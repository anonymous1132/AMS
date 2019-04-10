using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogParser
{
    public abstract class LogFileReader<T1, T2> : IFileReader where T1 : new() where T2 : new()
    {
        public LogFileReader(string filepath)
        {
            Initialize(filepath);
        }

        public LogFileReader()
        {

        }

        public abstract void GetData();

        public abstract void PutDB();

        public LogObj<T1> LogObj { get; set; } = new LogObj<T1>();

        public FileObj<T2> FileObj { get; set; } = new FileObj<T2>();

        public void Initialize(string filepath)
        {
            FileObj.FilePath = filepath;
            GetData();
            PutDB();
        }

        public static List<string> ReadTxtFileToLineList(string FilePath)
        {
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }
            return list;
        }
    }
}
