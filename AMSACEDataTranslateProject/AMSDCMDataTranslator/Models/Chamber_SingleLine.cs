using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class Chamber_SingleLine
    {
        public string Lot
        {
            get;
            set;
        }

        public string Wafer
        {
            get;
            set;
        }

        public string Scribe
        {
            get;
            set;
        }

        public string Equipment
        {
            get;
            set;
        }

        public string Route
        {
            get;
            set;
        }

        public string RouteVersion
        {
            get;
            set;
        }

        public string Step
        {
            get;
            set;
        }

        public string StepVersion { get; set; }

        public string DateTimeList { get; set; }

        public string ChamberList { get; set; }

        public string RecipeList { get; set; }


        public string GetSingleSiffLine()
        {
            List<string> list = new List<string>()
            {
                Lot,Wafer,Scribe,Equipment,Route,RouteVersion,Step,StepVersion,DateTimeList,ChamberList,RecipeList
            };
            return string.Format("\"{0}\"",string.Join("\",\"",list));
        }
    }
}
