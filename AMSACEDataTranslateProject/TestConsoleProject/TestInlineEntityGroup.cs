using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;

namespace TestConsoleProject
{
   public class TestInlineEntityGroup
    {
        public static void Test()
        {
            InlineEntityGroup entityGroup = new InlineEntityGroup();
            DateTime dateTime = new DateTime();
            DateTime.TryParse("2018-08-31 09:25:20",out dateTime);
            entityGroup.StartTime = dateTime;
            entityGroup.GetData();
            var list= entityGroup.GetInlineList();
            Console.WriteLine(entityGroup.inlineDBEntities.Count.ToString());
            Console.WriteLine(list.Count.ToString());
        }
    }
}
