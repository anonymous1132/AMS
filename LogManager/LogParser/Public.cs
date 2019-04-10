using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace LogParser
{
    public static class Public
    {
        public static  string ConfigDir { get { return Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); } }

        public static string ConfigFileName { get; set; } = "App\\config.json";

        public static string ConfigFileFullPath { get { return Path.Combine(ConfigDir,ConfigFileName); } }

        public static Config Config { get; set; }

        public static MongoHelper Mongo { get; set; }

        public static void GetConfigValue()
        {
            Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(ConfigFileFullPath));
            Mongo = new MongoHelper(Config.MongodbServer,Config.MongodbPort,Config.DataBase);
        }
    }
}
