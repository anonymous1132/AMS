using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace AMSCPAUFileConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists(Public.SettingFilePath))
            {
                Console.Error.WriteLine("setting.json not found!");
                return;
            }
            Encoding encode = System.Text.Encoding.GetEncoding("GB2312");
            var text = File.ReadAllText(Public.SettingFilePath,encode);
            try
            {
                var model = JsonConvert.DeserializeObject<SettingModel>(text);
                if (model.DestFiles.Count == 0)
                {
                    Console.Error.WriteLine("DestFiles not set!");
                    return;
                }

                foreach (var file in model.DestFiles)
                {
                    string para = string.Format("-u {0} -p {1} -ex \"{2}\" -enc -file \"{3}.txt\"", model.UserName, model.Password, file, Path.Combine(model.DecPath, Path.GetFileNameWithoutExtension(file)));
                    if (ProcessExcute.StartProcess(model.RealCPAUPath, para))
                    {
                        Console.WriteLine(model.RealCPAUPath, para);
                    }
                    string batContext = string.Format("\"{0}\" -dec -file \"{1}.txt\"", model.CPAUPath, Path.Combine(model.KeyDirectory, Path.GetFileNameWithoutExtension(file)));
                    batContext = model.IsDomainUser ? batContext : batContext + " -lwp";
                    batContext += "\n pause";
                    File.WriteAllText(Path.Combine(model.RealBatPath, Path.GetFileNameWithoutExtension(file)+".bat"), batContext,encode);
                }

            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                return;
            }
        }
    }
}
