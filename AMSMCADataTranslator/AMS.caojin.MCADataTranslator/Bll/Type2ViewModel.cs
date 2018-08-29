using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCADataTranslator.Bll
{
    public class Type2ViewModel
    {
        public Type2ViewModel(string fn)
        {
            this.FileName = fn;
        }

        public string FileName
        {
            get;
            set;
        }

        public string SampleName
        {
            get;
            set;
        }

        public double Na
        {
            get {
                double temp = 0;
                try {
                    temp = Convert.ToDouble(strNa);
                    temp = temp > 0 ? temp : 0;
                }
                catch (Exception) { }
                return temp;
            }
        }
        public string strNa
        {
            get;
            set;
        }

        public string strAl
        {
            get;
            set;
        }
        public double Al
        {
            get {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strAl); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strCa
        {
            get;
            set;
        }
        public double Ca
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strCa); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strCr
        {
            get;
            set;
        }
        public double Cr
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strCr); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strFe
        {
            get;
            set;
        }
        public double Fe
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strFe); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strNi
        {
            get;
            set;
        }
        public double Ni
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strNi); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strCu
        {
            get;
            set;
        }
        public double Cu
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strCu); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string strZn
        {
            get;
            set;
        }
        public double Zn
        {
            get
            {
                double temp = 0;
                try
                { temp = Convert.ToDouble(strZn); temp = temp > 0 ? temp : 0; }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string AcqDateTime
        {
            get;
            set;
        }
            

    }
}
