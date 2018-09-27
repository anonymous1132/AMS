using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
   public class EDC_Lot_SingleSummary
    {
        public int ProductID
        {
            get;
            set;
        }

        public int Lot_ID
        {
            get;
            set;
        }

        public int EDC_Parameter_ID
        {
            get;
            set;
        }

        public string MeasureDateTime
        {
            get;
            set;
        }

        public int RangeIndex
        {
            get;
            set;
        } = 0;

        public string DuplicateFlage
        {
            get;
            set;
        } = "N";

        public void GetData(DataRow dr)
        {
            ProductID =Convert.ToInt32(dr[0].ToString());
            Lot_ID= Convert.ToInt32(dr[1].ToString());
            EDC_Parameter_ID= Convert.ToInt32(dr[2].ToString());
            MeasureDateTime = dr[3].ToString();
        }

        

        public string InsertSql()
        {
            string sql = string.Format("insert into acme.edc_lot_summary (product_id,lot_id,edc_parameter_id,range_index,measure_time,duplicate_flag) values ({0},{1},{2},{3},to_date('{4}','yyyy/mm/dd HH24:mi:ss'),'{5}')", ProductID,Lot_ID,EDC_Parameter_ID,RangeIndex,MeasureDateTime,DuplicateFlage);
            return sql;
        }

    }
}
