using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2) { Console.WriteLine("Example：logimport smtp/queue \"%DIRPATH%\"");return; }
            string path = args[1];
            string type = args[0].ToLower();
            if (!System.IO.Directory.Exists(path))
            {
                Console.WriteLine("Directory Not Exist"); return;
            }
            if (type == "smtp")
            {
                SMTPImporter.MultiFileImport(path);
                
            }
            if (type == "queue")
            {
                QueueImporter.MultiFileImport(path);
            }
        }
    }
}
