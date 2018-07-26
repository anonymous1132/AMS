using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;

namespace MCADataTranslator.Bll
{
   public class AgSeriesViewModel
        :NotifyPropertyChanged
    {
        public AgSeriesViewModel()
        {
            
        }
        private string _index;
        public string Index
        {
            get { return _index; }
            set { _index = value;OnPropertyChanged("Index"); }
        }

        private string ag_x;
        public string Ag_X
        {
            get { return ag_x; }
            set { ag_x = value;OnPropertyChanged("Ag_X"); }
        }

        private string ag_y;
        public string Ag_Y
        {
            get { return ag_y; }
            set { ag_y = value; OnPropertyChanged("Ag_Y"); }
        }

        private string ag_r;
        public string Ag_R
        {
            get { return ag_r; }
            set { ag_r = value; OnPropertyChanged("Ag_R"); }
        }

        private string ag_t;
        public string Ag_T
        {
            get { return ag_t; }
            set { ag_t = value; OnPropertyChanged("Ag_T"); }
        }

        private string ag_z;
        public string Ag_Z
        {
            get { return ag_z; }
            set { ag_z = value;OnPropertyChanged("Ag_Z"); }
        }

        private string ag_incidentAngle;
        public string Ag_IncidentAngle
        {
            get { return ag_incidentAngle; }
            set { ag_incidentAngle = value;OnPropertyChanged("Ag_IncidentAngle"); }
        }

        private string ag_phi;
        public string Ag_Phi
        {
            get { return ag_phi; }
            set { ag_phi = value;OnPropertyChanged("Ag_Phi"); }
        }

        private string ag_si_e10;
        public string Ag_Si_E10
        {
            get { return ag_si_e10; }
            set { ag_si_e10 = value;OnPropertyChanged("Ag_Si_E10"); }
        }

        private string ag_si_c;
        public string Ag_Si_C
        {
            get { return ag_si_c; }
            set { ag_si_c = value;OnPropertyChanged("Ag_Si_C"); }
        }

        private string ag_si_r;
        public string Ag_Si_R
        {
            get { return ag_si_r; }
            set { ag_si_r = value;OnPropertyChanged("Ag_Si_R"); }
        }

        private string ag_si_lld;
        public string Ag_Si_LLD
        {
            get { return ag_si_lld; }
            set { ag_si_lld = value; OnPropertyChanged("Ag_Si_LLD"); }
        }

        private string ag_ge_e10;
        public string Ag_Ge_E10
        {
            get { return ag_ge_e10; }
            set { ag_ge_e10 = value; OnPropertyChanged("Ag_Ge_E10"); }
        }

        private string ag_ge_c;
        public string Ag_Ge_C
        {
            get { return ag_ge_c; }
            set { ag_ge_c = value; OnPropertyChanged("Ag_Ge_C"); }
        }

        private string ag_ge_r;
        public string Ag_Ge_R
        {
            get { return ag_ge_r; }
            set { ag_ge_r = value; OnPropertyChanged("Ag_Ge_R"); }
        }

        private string ag_ge_lld;
        public string Ag_Ge_LLD
        {
            get { return ag_ge_lld; }
            set { ag_ge_lld = value; OnPropertyChanged("Ag_Ge_LLD"); }
        }

        private string ag_as_e10;
        public string Ag_As_E10
        {
            get { return ag_as_e10; }
            set { ag_as_e10 = value; OnPropertyChanged("Ag_As_E10"); }
        }

        private string ag_as_c;
        public string Ag_As_C
        {
            get { return ag_as_c; }
            set { ag_as_c = value; OnPropertyChanged("Ag_As_C"); }
        }

        private string ag_as_r;
        public string Ag_As_R
        {
            get { return ag_as_r; }
            set { ag_as_r = value; OnPropertyChanged("Ag_As_R"); }
        }

        private string ag_as_lld;
        public string Ag_As_LLD
        {
            get { return ag_as_lld; }
            set { ag_as_lld = value; OnPropertyChanged("Ag_As_LLD"); }
        }

        private string ag_na_e10;
        public string Ag_Na_E10
        {
            get { return ag_na_e10; }
            set { ag_na_e10 = value; OnPropertyChanged("Ag_Na_E10"); }
        }

        private string ag_na_c;
        public string Ag_Na_C
        {
            get { return ag_na_c; }
            set { ag_na_c = value; OnPropertyChanged("Ag_Na_C"); }
        }

        private string ag_na_r;
        public string Ag_Na_R
        {
            get { return ag_na_r; }
            set { ag_na_r = value; OnPropertyChanged("Ag_Na_R"); }
        }

        private string ag_na_lld;
        public string Ag_Na_LLD
        {
            get { return ag_na_lld; }
            set { ag_na_lld = value; OnPropertyChanged("Ag_Na_LLD"); }
        }

        private string ag_mg_e10;
        public string Ag_Mg_E10
        {
            get { return ag_mg_e10; }
            set { ag_mg_e10 = value; OnPropertyChanged("Ag_Mg_E10"); }
        }

        private string ag_mg_c;
        public string Ag_Mg_C
        {
            get { return ag_mg_c; }
            set { ag_mg_c = value; OnPropertyChanged("Ag_Mg_C"); }
        }

        private string ag_mg_r;
        public string Ag_Mg_R
        {
            get { return ag_mg_r; }
            set { ag_mg_r = value; OnPropertyChanged("Ag_Mg_R"); }
        }

        private string ag_mg_lld;
        public string Ag_Mg_LLD
        {
            get { return ag_mg_lld; }
            set { ag_mg_lld = value; OnPropertyChanged("Ag_Mg_LLD"); }
        }

        private string ag_al_e10;
        public string Ag_Al_E10
        {
            get { return ag_al_e10; }
            set { ag_al_e10 = value; OnPropertyChanged("Ag_Al_E10"); }
        }

        private string ag_al_c;
        public string Ag_Al_C
        {
            get { return ag_al_c; }
            set { ag_al_c = value; OnPropertyChanged("Ag_Al_C"); }
        }

        private string ag_al_r;
        public string Ag_Al_R
        {
            get { return ag_al_r; }
            set { ag_al_r = value; OnPropertyChanged("Ag_Al_R"); }
        }

        private string ag_al_lld;
        public string Ag_Al_LLD
        {
            get { return ag_al_lld; }
            set { ag_al_lld = value; OnPropertyChanged("Ag_Al_LLD"); }
        }

        
    }
}
