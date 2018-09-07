using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleProject
{
    public delegate void TestDelegate();

   public class TestService
    {
        public TestService()
        { }
        public TestDelegate testDelegate;

        public void Test()
        {
            testDelegate?.Invoke();
        }

    }

    public static class StaticClassTest
    {
        public static void Echo()
        {
            Console.WriteLine("ehco");
        }

    }

    public class TestService2 : TestService
    {
        public TestService2() : base()
        {
            testDelegate += StaticClassTest.Echo;
        }
    }
}
