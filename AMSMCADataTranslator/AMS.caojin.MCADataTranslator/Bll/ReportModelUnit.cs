using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCADataTranslator.Helper;

namespace MCADataTranslator.Bll
{
   public class ReportModelUnit
    {
        public ReportModelUnit()
        { }

        private string _sampleComment;
        public string SampleComment
        {
            get { return _sampleComment; }
            set { _sampleComment = value; }
        }

        public string IndexNo
        {
            get;
            set;
        }


        public string S
        { get; set; }

        public string Cl
        { get; set; }

        public string K
        { get; set; }

        public string Ca
        { get; set; }

        public string Ti
        { get; set; }

        public string V
        { get; set; }

        public string Cr { get; set; }

        public string Mn { get; set; }

        public string Fe { get; set; }

        public string Co { get; set; }

        public string Ni { get; set; }

        public string Cu { get; set; }

        public string Zn { get; set; }

        public string Sb { get; set; }

        public string Te { get; set; }

        public string Na { get; set; }

        public string Mg { get; set; }

        public string Al { get; set; }

        public string Ge { get; set; }

        public string As { get; set; }



    }
}
