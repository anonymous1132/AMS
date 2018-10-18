using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
   public class WAT
    {
        public WAT()
        { }

        public string RecipeID
        {
            get;
            set;
        }

        public string LotID
        {
            get;
            set;
        }

        public string FoupID
        {
            get;
            set;
        }

        public string TesterID
        {
            get;
            set;
        }

        public string ProbeCardID
        {
           get;
           set;
        }

        public string UserID
        {
            get;
            set;
        }

       public string TestProgram
        {
            get;
            set;
        }

        public string LimitFile
        {
            get;
            set;
        }

        public string DateTime
        {
            get;
            set;
        }

        public string TestWafer
        {
            get;
            set;
        }

        public string TestSite
        {
            get;
            set;
        }

        public string TestItem
        {
            get;
            set;
        }

        public string Notch
        {
            get;
            set;
        }

        public List<string[,]> site_coordinates=new List<string[,]>();

        public List<WATWafer> wafers=new List<WATWafer>();
    }
}
