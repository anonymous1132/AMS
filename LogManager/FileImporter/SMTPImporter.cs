using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FirstFloor.ModernUI.App.Runner;
using FirstFloor.ModernUI.App;

namespace FileImporter
{
    public class SMTPImporter
    {
        public static void MultiFileImport(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            foreach (var file in files)
            {
                try
                {
                    SmtpFileReader fileReader = new SmtpFileReader(file.FullName);
                    ShareDataEntity shareData = ShareDataEntity.GetSingleton();
                    shareData.AddNewModelToMailSmtpCollection(fileReader.SmtpModelList);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            
        }
    }
}
