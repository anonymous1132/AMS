using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleProject
{
   public class TestParameter
    {
        public TestParameter()
        {
            SetTeststr("123");
            Console.WriteLine(teststr);
        }

        private string teststr;
        private void SetTeststr(string test)
        {
            teststr = test;
        }
    }
}
