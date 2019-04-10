using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogParser;
using MongoDB.Bson;

namespace TestConsoleApp
{
    public class TestMogo
    {
        public static void Test()
        {
            MongoHelper mongo = new MongoHelper("test");
            List<People> list = new List<People>();
            string test = @"MongoDB 固定集合(Capped Collections) | 菜鸟教程2017年12月12日 - MongoDB 固定集合(Capped Collections) MongoDB 固定集合(Capped Collections)是性能出色且有着固定大小的集合,对于大小固定,我们可以想象其就像一个...
www.runoob.com/mongodb...  - 百度快照";
            list.Add(new People { Name = test, Age = 30 });
            list.Add(new People { Name = "bangqing", Age = 25 });
            mongo.InsertAll<People>("test", list);

            Console.WriteLine("yes");
        }
    }

    public class People
    {
        public ObjectId _id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        private string Sex { get; set; } = "M";
    }
}
