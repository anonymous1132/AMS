using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;


namespace SiviewLogLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            // string RegStr = @"mmtrx_sum.[0-9,\-]{10}.HATOTAL$";
            string dirPath = @"D:\DATA2";
            ProgressRunner.Run(dirPath);
        }
    }
}
