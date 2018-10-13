using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleProject
{
   public class TestForeach
    {
        public static void Test()
        {
            List<string> list =new List<string>();
            int i = 0;
            foreach (string str in list)
            {
                i++;
            }
            Console.WriteLine(i.ToString());
        }
    }
}
