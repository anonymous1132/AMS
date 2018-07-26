using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace MCADataTranslator.Bll
{
   public class WSeriesViewModel
        :NotifyPropertyChanged
    {
        public WSeriesViewModel() { }
        private string _index;
        public string Index
        {
            get { return _index; }
            set { _index = value; OnPropertyChanged("Index"); }
        }

        private string w_x;
        public string W_X
        {
            get { return w_x; }
            set { w_x = value; OnPropertyChanged("W_X"); }
        }

        private string w_y;
        public string W_Y
        {
            get { return w_y; }
            set { w_y = value; OnPropertyChanged("W_Y"); }
        }

        private string w_r;
        public string W_R
        {
            get { return w_r; }
            set { w_r = value; OnPropertyChanged("W_R"); }
        }

        private string w_t;
        public string W_T
        {
            get { return w_t; }
            set { w_t = value; OnPropertyChanged("W_T"); }
        }

        private string w_z;
        public string W_Z
        {
            get { return w_z; }
            set { w_z = value; OnPropertyChanged("W_Z"); }
        }

        private string w_incidentAngle;
        public string W_IncidentAngle
        {
            get { return w_incidentAngle; }
            set { w_incidentAngle = value; OnPropertyChanged("W_IncidentAngle"); }
        }

        private string w_phi;
        public string W_Phi
        {
            get { return w_phi; }
            set { w_phi = value; OnPropertyChanged("W_Phi"); }
        }


        private string w_si_e10;
        public string W_Si_E10
        {
            get { return w_si_e10; }
            set { w_si_e10 = value; OnPropertyChanged("W_Si_E10"); }
        }

        private string w_si_c;
        public string W_Si_C
        {
            get { return w_si_c; }
            set { w_si_c = value; OnPropertyChanged("W_Si_C"); }
        }

        private string w_si_r;
        public string W_Si_R
        {
            get { return w_si_r; }
            set { w_si_r = value; OnPropertyChanged("W_Si_R"); }
        }

        private string w_si_lld;
        public string W_Si_LLD
        {
            get { return w_si_lld; }
            set { w_si_lld = value; OnPropertyChanged("W_Si_LLD"); }
        }


        private string w_s_e10;
        public string W_S_E10
        {
            get { return w_s_e10; }
            set { w_s_e10 = value; OnPropertyChanged("W_S_E10"); }
        }

        private string w_s_c;
        public string W_S_C
        {
            get { return w_s_c; }
            set { w_s_c = value; OnPropertyChanged("W_S_C"); }
        }

        private string w_s_r;
        public string W_S_R
        {
            get { return w_s_r; }
            set { w_s_r = value; OnPropertyChanged("W_S_R"); }
        }

        private string w_s_lld;
        public string W_S_LLD
        {
            get { return w_s_lld; }
            set { w_s_lld = value; OnPropertyChanged("W_S_LLD"); }
        }


        private string w_cl_e10;
        public string W_Cl_E10
        {
            get { return w_cl_e10; }
            set { w_cl_e10 = value; OnPropertyChanged("W_Cl_E10"); }
        }

        private string w_cl_c;
        public string W_Cl_C
        {
            get { return w_cl_c; }
            set { w_cl_c = value; OnPropertyChanged("W_Cl_C"); }
        }

        private string w_cl_r;
        public string W_Cl_R
        {
            get { return w_cl_r; }
            set { w_cl_r = value; OnPropertyChanged("W_Cl_R"); }
        }

        private string w_cl_lld;
        public string W_Cl_LLD
        {
            get { return w_cl_lld; }
            set { w_cl_lld = value; OnPropertyChanged("W_Cl_LLD"); }
        }


        private string w_k_e10;
        public string W_K_E10
        {
            get { return w_k_e10; }
            set { w_k_e10 = value; OnPropertyChanged("W_K_E10"); }
        }

        private string w_k_c;
        public string W_K_C
        {
            get { return w_k_c; }
            set { w_k_c = value; OnPropertyChanged("W_K_C"); }
        }

        private string w_k_r;
        public string W_K_R
        {
            get { return w_k_r; }
            set { w_k_r = value; OnPropertyChanged("W_K_R"); }
        }

        private string w_k_lld;
        public string W_K_LLD
        {
            get { return w_k_lld; }
            set { w_k_lld = value; OnPropertyChanged("W_K_LLD"); }
        }


        private string w_ca_e10;
        public string W_Ca_E10
        {
            get { return w_ca_e10; }
            set { w_ca_e10 = value; OnPropertyChanged("W_Ca_E10"); }
        }

        private string w_ca_c;
        public string W_Ca_C
        {
            get { return w_ca_c; }
            set { w_ca_c = value; OnPropertyChanged("W_Ca_C"); }
        }

        private string w_ca_r;
        public string W_Ca_R
        {
            get { return w_ca_r; }
            set { w_ca_r = value; OnPropertyChanged("W_Ca_R"); }
        }

        private string w_ca_lld;
        public string W_Ca_LLD
        {
            get { return w_ca_lld; }
            set { w_ca_lld = value; OnPropertyChanged("W_Ca_LLD"); }
        }


        private string w_ti_e10;
        public string W_Ti_E10
        {
            get { return w_ti_e10; }
            set { w_ti_e10 = value; OnPropertyChanged("W_Ti_E10"); }
        }

        private string w_ti_c;
        public string W_Ti_C
        {
            get { return w_ti_c; }
            set { w_ti_c = value; OnPropertyChanged("W_Ti_C"); }
        }

        private string w_ti_r;
        public string W_Ti_R
        {
            get { return w_ti_r; }
            set { w_ti_r = value; OnPropertyChanged("W_Ti_R"); }
        }

        private string w_ti_lld;
        public string W_Ti_LLD
        {
            get { return w_ti_lld; }
            set { w_ti_lld = value; OnPropertyChanged("W_Ti_LLD"); }
        }


        private string w_v_e10;
        public string W_V_E10
        {
            get { return w_v_e10; }
            set { w_v_e10 = value; OnPropertyChanged("W_V_E10"); }
        }

        private string w_v_c;
        public string W_V_C
        {
            get { return w_v_c; }
            set { w_v_c = value; OnPropertyChanged("W_V_C"); }
        }

        private string w_v_r;
        public string W_V_R
        {
            get { return w_v_r; }
            set { w_v_r = value; OnPropertyChanged("W_V_R"); }
        }

        private string w_v_lld;
        public string W_V_LLD
        {
            get { return w_v_lld; }
            set { w_v_lld = value; OnPropertyChanged("W_V_LLD"); }
        }


        private string w_cr_e10;
        public string W_Cr_E10
        {
            get { return w_cr_e10; }
            set { w_cr_e10 = value; OnPropertyChanged("W_Cr_E10"); }
        }

        private string w_cr_c;
        public string W_Cr_C
        {
            get { return w_cr_c; }
            set { w_cr_c = value; OnPropertyChanged("W_Cr_C"); }
        }

        private string w_cr_r;
        public string W_Cr_R
        {
            get { return w_cr_r; }
            set { w_cr_r = value; OnPropertyChanged("W_Cr_R"); }
        }

        private string w_cr_lld;
        public string W_Cr_LLD
        {
            get { return w_cr_lld; }
            set { w_cr_lld = value; OnPropertyChanged("W_Cr_LLD"); }
        }


        private string w_mn_e10;
        public string W_Mn_E10
        {
            get { return w_mn_e10; }
            set { w_mn_e10 = value; OnPropertyChanged("W_Mn_E10"); }
        }

        private string w_mn_c;
        public string W_Mn_C
        {
            get { return w_mn_c; }
            set { w_mn_c = value; OnPropertyChanged("W_Mn_C"); }
        }

        private string w_mn_r;
        public string W_Mn_R
        {
            get { return w_mn_r; }
            set { w_mn_r = value; OnPropertyChanged("W_Mn_R"); }
        }

        private string w_mn_lld;
        public string W_Mn_LLD
        {
            get { return w_mn_lld; }
            set { w_mn_lld = value; OnPropertyChanged("W_Mn_LLD"); }
        }


        private string w_fe_e10;
        public string W_Fe_E10
        {
            get { return w_fe_e10; }
            set { w_fe_e10 = value; OnPropertyChanged("W_Fe_E10"); }
        }

        private string w_fe_c;
        public string W_Fe_C
        {
            get { return w_fe_c; }
            set { w_fe_c = value; OnPropertyChanged("W_Fe_C"); }
        }

        private string w_fe_r;
        public string W_Fe_R
        {
            get { return w_fe_r; }
            set { w_fe_r = value; OnPropertyChanged("W_Fe_R"); }
        }

        private string w_fe_lld;
        public string W_Fe_LLD
        {
            get { return w_fe_lld; }
            set { w_fe_lld = value; OnPropertyChanged("W_Fe_LLD"); }
        }


        private string w_co_e10;
        public string W_Co_E10
        {
            get { return w_co_e10; }
            set { w_co_e10 = value; OnPropertyChanged("W_Co_E10"); }
        }

        private string w_co_c;
        public string W_Co_C
        {
            get { return w_co_c; }
            set { w_co_c = value; OnPropertyChanged("W_Co_C"); }
        }

        private string w_co_r;
        public string W_Co_R
        {
            get { return w_co_r; }
            set { w_co_r = value; OnPropertyChanged("W_Co_R"); }
        }

        private string w_co_lld;
        public string W_Co_LLD
        {
            get { return w_co_lld; }
            set { w_co_lld = value; OnPropertyChanged("W_Co_LLD"); }
        }


        private string w_ni_e10;
        public string W_Ni_E10
        {
            get { return w_ni_e10; }
            set { w_ni_e10 = value; OnPropertyChanged("W_Ni_E10"); }
        }

        private string w_ni_c;
        public string W_Ni_C
        {
            get { return w_ni_c; }
            set { w_ni_c = value; OnPropertyChanged("W_Ni_C"); }
        }

        private string w_ni_r;
        public string W_Ni_R
        {
            get { return w_ni_r; }
            set { w_ni_r = value; OnPropertyChanged("W_Ni_R"); }
        }

        private string w_ni_lld;
        public string W_Ni_LLD
        {
            get { return w_ni_lld; }
            set { w_ni_lld = value; OnPropertyChanged("W_Ni_LLD"); }
        }


        private string w_cu_e10;
        public string W_Cu_E10
        {
            get { return w_cu_e10; }
            set { w_cu_e10 = value; OnPropertyChanged("W_Cu_E10"); }
        }

        private string w_cu_c;
        public string W_Cu_C
        {
            get { return w_cu_c; }
            set { w_cu_c = value; OnPropertyChanged("W_Cu_C"); }
        }

        private string w_cu_r;
        public string W_Cu_R
        {
            get { return w_cu_r; }
            set { w_cu_r = value; OnPropertyChanged("W_Cu_R"); }
        }

        private string w_cu_lld;
        public string W_Cu_LLD
        {
            get { return w_cu_lld; }
            set { w_cu_lld = value; OnPropertyChanged("W_Cu_LLD"); }
        }


        private string w_zn_e10;
        public string W_Zn_E10
        {
            get { return w_zn_e10; }
            set { w_zn_e10 = value; OnPropertyChanged("W_Zn_E10"); }
        }

        private string w_zn_c;
        public string W_Zn_C
        {
            get { return w_zn_c; }
            set { w_zn_c = value; OnPropertyChanged("W_Zn_C"); }
        }

        private string w_zn_r;
        public string W_Zn_R
        {
            get { return w_zn_r; }
            set { w_zn_r = value; OnPropertyChanged("W_Zn_R"); }
        }

        private string w_zn_lld;
        public string W_Zn_LLD
        {
            get { return w_zn_lld; }
            set { w_zn_lld = value; OnPropertyChanged("W_Zn_LLD"); }
        }

        private string w_sb_e10;
        public string W_Sb_E10
        {
            get { return w_sb_e10; }
            set { w_sb_e10 = value; OnPropertyChanged("W_Sb_E10"); }
        }

        private string w_sb_c;
        public string W_Sb_C
        {
            get { return w_sb_c; }
            set { w_sb_c = value; OnPropertyChanged("W_Sb_C"); }
        }

        private string w_sb_r;
        public string W_Sb_R
        {
            get { return w_sb_r; }
            set { w_sb_r = value; OnPropertyChanged("W_Sb_R"); }
        }

        private string w_sb_lld;
        public string W_Sb_LLD
        {
            get { return w_sb_lld; }
            set { w_sb_lld = value; OnPropertyChanged("W_Sb_LLD"); }
        }

        private string w_te_e10;
        public string W_Te_E10
        {
            get { return w_te_e10; }
            set { w_te_e10 = value; OnPropertyChanged("W_Te_E10"); }
        }

        private string w_te_c;
        public string W_Te_C
        {
            get { return w_te_c; }
            set { w_te_c = value; OnPropertyChanged("W_Te_C"); }
        }

        private string w_te_r;
        public string W_Te_R
        {
            get { return w_te_r; }
            set { w_te_r = value; OnPropertyChanged("W_Te_R"); }
        }

        private string w_te_lld;
        public string W_Te_LLD
        {
            get { return w_te_lld; }
            set { w_te_lld = value; OnPropertyChanged("W_Te_LLD"); }
        }

        private string w_na_e10;
        public string W_Na_E10
        {
            get { return w_na_e10; }
            set { w_na_e10 = value; OnPropertyChanged("W_Na_E10"); }
        }

        private string w_na_c;
        public string W_Na_C
        {
            get { return w_na_c; }
            set { w_na_c = value; OnPropertyChanged("W_Na_C"); }
        }

        private string w_na_r;
        public string W_Na_R
        {
            get { return w_na_r; }
            set { w_na_r = value; OnPropertyChanged("W_Na_R"); }
        }

        private string w_na_lld;
        public string W_Na_LLD
        {
            get { return w_na_lld; }
            set { w_na_lld = value; OnPropertyChanged("W_Na_LLD"); }
        }

        private string w_mg_e10;
        public string W_Mg_E10
        {
            get { return w_mg_e10; }
            set { w_mg_e10 = value; OnPropertyChanged("W_Mg_E10"); }
        }

        private string w_mg_c;
        public string W_Mg_C
        {
            get { return w_mg_c; }
            set { w_mg_c = value; OnPropertyChanged("W_Mg_C"); }
        }

        private string w_mg_r;
        public string W_Mg_R
        {
            get { return w_mg_r; }
            set { w_mg_r = value; OnPropertyChanged("W_Mg_R"); }
        }

        private string w_mg_lld;
        public string W_Mg_LLD
        {
            get { return w_mg_lld; }
            set { w_mg_lld = value; OnPropertyChanged("W_Mg_LLD"); }
        }


        private string w_al_e10;
        public string W_Al_E10
        {
            get { return w_al_e10; }
            set { w_al_e10 = value; OnPropertyChanged("W_Al_E10"); }
        }

        private string w_al_c;
        public string W_Al_C
        {
            get { return w_al_c; }
            set { w_al_c = value; OnPropertyChanged("W_Al_C"); }
        }

        private string w_al_r;
        public string W_Al_R
        {
            get { return w_al_r; }
            set { w_al_r = value; OnPropertyChanged("W_Al_R"); }
        }

        private string w_al_lld;
        public string W_Al_LLD
        {
            get { return w_al_lld; }
            set { w_al_lld = value; OnPropertyChanged("W_Al_LLD"); }
        }
    }
}
