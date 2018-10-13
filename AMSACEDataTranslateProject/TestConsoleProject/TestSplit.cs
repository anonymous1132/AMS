using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsoleProject
{
   public class TestSplit
    {
        public  void Test(string str)
        {
            List<string> list = ItemSplit(str);
            Console.WriteLine(list.Count.ToString());
            Console.WriteLine(list[0]);
        }

        private List<string> ItemSplit(string Item)
        {

            List<string> resault_list = new List<string>();
            string[] arrlist = Item.Split(';');
            int L = arrlist.Length;
            int c = 0;
            while (L > 50)
            {
                List<string> templist = new List<string>();
                for (int i = c; i < c + 50; i++)
                {
                    templist.Add(arrlist[i]);
                }
                string temp = string.Join(";", templist);
                c = c + 50;
                L = L - 50;
                resault_list.Add(temp);
            }
            List<string> templist2 = new List<string>();
            for (int i = c; i < c + L; i++)
            {
                templist2.Add(arrlist[i]);
            }
            string temp2 = string.Join(";", templist2);
            resault_list.Add(temp2);
            return resault_list;
        }
    }
}
