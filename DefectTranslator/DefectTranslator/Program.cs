using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilsLibrary.Utils;

namespace DefectTranslator
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                new Translator();
            }
            catch (Exception e)
            {
                LogUtils.ErrorLog(e.Message);
                Console.WriteLine(e.Message);
            }
        }
    }
}
