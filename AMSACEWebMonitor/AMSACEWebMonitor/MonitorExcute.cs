using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMSACEWebMonitor
{
   public class MonitorExcute
   {
        public  void DoCheck()
        {
            foreach (MonitorUnity unity in Setting.MonitorUnities)
            {
                FileInfo file = GetLatestFile(unity.Directory);
                if (file == null)
                {
                    continue;
                }
                double span = GetSubtractedDate(file.CreationTime);
                if ((span > Setting.SpecTime) && (!unity.HasSent))
                {
                    LogHelper.SendEmail("ACEWeb Recipe Schedule Alarm", "Directory:" + unity.Directory.FullName
                        + "<br>CheckTime:" + DateTime.Now.ToString()
                        + "<br>LastFileCreationTime:" + file.CreationTime.ToString()
                        + "<br>SubtractedSpanTime:" + span.ToString());
                    XmlHelper.UpdateTableCell(Setting.ConfigFilePath, "MonitorUnity", "Dir", unity.Directory.FullName, "HasSent", "True");
                }
                else if ((span <= Setting.SpecTime) && (unity.HasSent))
                {
                    XmlHelper.UpdateTableCell(Setting.ConfigFilePath, "MonitorUnity", "Dir", unity.Directory.FullName, "HasSent", "False");
                }

                if (file.CreationTime.ToString("yyyyMMddHHmmss")!=unity.LastRunRecipeTime)
                {
                    XmlHelper.UpdateTableCell(Setting.ConfigFilePath, "MonitorUnity", "Dir", unity.Directory.FullName, "LastTime", file.CreationTime.ToString("yyyyMMddHHmmss"));
                }

            }

        }

        private FileInfo GetLatestFile(DirectoryInfo directory)
        {
            var files = directory.EnumerateFiles();
            var qry = from x in files
                      orderby x.CreationTime
                      select x;
            return qry.LastOrDefault();
        }

        public double GetSubtractedDate(DateTime dateTime)
        {
            TimeSpan ts=  DateTime.Now - dateTime;
            return ts.TotalHours;
        }
   }
}
