using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using SiviewLogLoader.Models;

namespace SiviewLogLoader
{
    public class ProgressRunner
    {
        public static void Run(string Path)
        {
          
            string RegStr = @"mmtrx_sum.[0-9,\-]{10}.HATOTAL$";
            if (!Directory.Exists(Path)) return;
            Directory.GetFiles(Path).Where(w => Regex.IsMatch(w, RegStr)).ToList().ForEach(f => new MMTRX_SUMFileReader(f));

        }
    }
}
