using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCADataTranslator.Helper;

namespace MCADataTranslator.Bll
{
    public class ReportModelBlock
    {
        public ReportModelBlock(string UID)
        {
            this.UID = UID;
            GetUnitLists();
            GetSpecUnit();
        }
        public string UID;

        public string SampleComment;

        public string EQP
        {
            get { return SampleComment.Substring(0, (SampleComment).Trim().IndexOf(" ")).Trim(); }
        }

        public string MEMO1
        {
            get { return SampleComment.Substring(SampleComment.Trim().IndexOf(" ")).Trim(); }
        }

        public List<ReportModelUnit> repotunits=new List<ReportModelUnit>();
        public ReportModelUnit SpecUnit;

        public void GetUnitLists()
        {
            if (string.IsNullOrEmpty(UID.Trim(' '))) { return; }

            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select * from AMS_MCA_RPT_DATA where UID = '" + UID + "' order by IndexNo";
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count <= 0) return;
            SampleComment = sqlHelper.dt.DefaultView[0]["SampleComment"].ToString();
            IList<ReportModelUnit> ilist = ModelConvertHelper<ReportModelUnit>.ConvertToModel(sqlHelper.dt);
            List<ReportModelUnit> list = ModelConvertHelper<ReportModelUnit>.ConvertIListToList<ReportModelUnit>(ilist);
            repotunits = list;
        }

        public void GetSpecUnit()
        {
            if (string.IsNullOrEmpty(SampleComment.Trim())) { return; }
            SpecUnit = new ReportModelUnit();
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select * from mca_spec where eqp = '" + EQP + "'";
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count <= 0) return;
            SpecUnit.Na = sqlHelper.dt.Rows[0]["Na"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Na"].ToString();
            SpecUnit.Mg = sqlHelper.dt.Rows[0]["Mg"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Mg"].ToString();
            SpecUnit.Al = sqlHelper.dt.Rows[0]["Al"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Al"].ToString();
            SpecUnit.K = sqlHelper.dt.Rows[0]["K"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["K"].ToString();
            SpecUnit.Ca = sqlHelper.dt.Rows[0]["Ca"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Ca"].ToString();
            SpecUnit.Ti = sqlHelper.dt.Rows[0]["Ti"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Ti"].ToString();
            SpecUnit.V = sqlHelper.dt.Rows[0]["V"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["V"].ToString();
            SpecUnit.Cr = sqlHelper.dt.Rows[0]["Cr"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Cr"].ToString();
            SpecUnit.Mn = sqlHelper.dt.Rows[0]["Mn"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Mn"].ToString();
            SpecUnit.Fe = sqlHelper.dt.Rows[0]["Fe"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Fe"].ToString();
            SpecUnit.Ni = sqlHelper.dt.Rows[0]["Ni"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Ni"].ToString();
            SpecUnit.Cu = sqlHelper.dt.Rows[0]["Cu"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Cu"].ToString();
            SpecUnit.Zn = sqlHelper.dt.Rows[0]["Zn"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Zn"].ToString();
            SpecUnit.Ge = sqlHelper.dt.Rows[0]["Ge"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Ge"].ToString();
            SpecUnit.Sb = sqlHelper.dt.Rows[0]["Sb"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Sb"].ToString();
            SpecUnit.Te = sqlHelper.dt.Rows[0]["Te"] == DBNull.Value ? "" : sqlHelper.dt.Rows[0]["Te"].ToString();
        }
    }
}
