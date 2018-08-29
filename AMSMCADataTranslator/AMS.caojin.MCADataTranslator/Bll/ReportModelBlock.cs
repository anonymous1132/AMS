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
        public ReportModelBlock(string sampleComment)
        {
            this.SampleComment = sampleComment;
            GetUnitLists();
        }

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

        public void GetUnitLists()
        {
            if (string.IsNullOrEmpty(SampleComment.Trim())) { return; }

            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select * from AMS_MCA_ACE_DATA where SampleComment = '" + SampleComment + "' order by IndexNo";
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count <= 0) return;
            IList<ReportModelUnit> ilist = ModelConvertHelper<ReportModelUnit>.ConvertToModel(sqlHelper.dt);
            List<ReportModelUnit> list = ModelConvertHelper<ReportModelUnit>.ConvertIListToList<ReportModelUnit>(ilist);
            this.repotunits = list;
        }
    }
}
