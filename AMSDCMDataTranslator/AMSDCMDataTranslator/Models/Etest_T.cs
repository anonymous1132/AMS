using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Xml;

namespace AMSDCMDataTranslator.Models
{
   public class Etest_T
    {
        public Etest_T()
        { }

        public string TestID
        {
            get;
            set;
        }

        public string TestValue
        {
            get;
            set;
        }

        public XmlNode SetTNode(XmlNode xn)
        {
            XmlHelper.InsertElement(ref xn,"T",TestID+","+TestValue);
            return xn;
        }
        public void GetData(WATParameter parameter,int i)
        {
            int posleft = parameter.ItemNo.IndexOf("[");
            int posright = parameter.ItemNo.IndexOf("]");

            TestID = parameter.ItemNo.Substring(posleft+1,posright-posleft-1);

            TestValue = parameter.ValueList[i];
        }
    }
}
