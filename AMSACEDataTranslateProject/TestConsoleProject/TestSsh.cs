using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
   public class TestSsh
    {
        public static void Test()
        {
            //try
            //{
                SshOper ssh = new SshOper();
            // string str= ssh.GetResault("ksh ~/loader/batch/inline.sh");

            string str = ssh.GetResault("sh ~/eda/test.sh");
            Console.WriteLine(str);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }
    }
}
