using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCADataTranslator.Helper;

namespace MCADataTranslator.Bll
{
   public class ReportModelBlockType2
    {
        public ReportModelBlockType2(string fileName,string importDateTime)
        {
            this.FileName = fileName;
            this.ImportDateTime = importDateTime;
            GetUnitLists();
        }

        public string FileName;

        public string ImportDateTime;

        public List<ReportModelUnitType2> repotunits = new List<ReportModelUnitType2>();

        public void GetUnitLists()
        {
            if (string.IsNullOrEmpty(FileName.Trim())) { return; }
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "select * from MCA_ACE_TYPE2_DATA where FileName = '" + FileName + "' and ImportDateTime ='"+ ImportDateTime+"' order by AcqDateTime";
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count <= 0) return;
            IList<ReportModelUnitType2> ilist = ModelConvertHelper<ReportModelUnitType2>.ConvertToModel(sqlHelper.dt);
            List<ReportModelUnitType2> list = ModelConvertHelper<ReportModelUnitType2>.ConvertIListToList<ReportModelUnitType2>(ilist);
            this.repotunits = list;
        }
    }
}
