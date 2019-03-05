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
    public class QueueImporter
    {
        public static void MultiFileImport(string path)
        {
            DirectoryInfo root = new DirectoryInfo(path);
            FileInfo[] files = root.GetFiles();
            foreach (var file in files)
            {
                try
                {
                   QueueFileReader fileReader = new QueueFileReader(file.FullName);
                    ShareDataEntity shareData = ShareDataEntity.GetSingleton();
                    shareData.AddNewModelToMailQueueCollection(fileReader.QueueModelList.Select(s => s.QueueModel).ToList());
                    fileReader.QueueModelList.ForEach(f => shareData.AddNewModelToMailQueueReserverCollection(f.ReserverEntities));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
