using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSCPAUFileConsole
{
    public static class Public
    {
        public static string ExePath
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName; }
        }

        public static string ExeDirectory
        {
            get { return System.IO.Path.GetDirectoryName(ExePath); }
        }

        public static string SettingFilePath
        {
            get;
            set;
        } = System.IO.Path.Combine(ExeDirectory, "setting.json");


    }
}
