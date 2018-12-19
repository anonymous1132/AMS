using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator
{
   public class SqlOper
    {
        public SqlOper()
        { }

        private string sql;

        private SqlHelper sqlHelper = new SqlHelper();

        public void LoadToDB2(DCMDB2 db2)
        {
            try
            {
                sql = string.Format("insert into ISTRPT.EDA_INLINE_DCM values ('{0}',to_date('{1}', 'yyyy/mm/dd HH24:mi:ss'),'{2}','{3}','{4}',{5},'{6}',to_date('{7}', 'yyyy/mm/dd HH24:mi:ss'),'{8}')", db2.Guid,db2.CollectionDateTime,db2.EQP_ID,db2.LotID,db2.Recipe,db2.MeasureDataCount,db2.CoordinateArray,db2.UpdateTime,db2.FileName);
                SqlHelper sqlHelper = new SqlHelper();
                sqlHelper.GetSomeDate(sql);
            }
            catch (Exception e)
            {
                // throw new Exception(e.Message + "\tFileName:" + db2.FileName + "\tLotID:" + db2.LotID );
                LogHelper.ErrorLog(new Exception(e.Message + "\tFileName:" + db2.FileName + "\tLotID:" + db2.LotID));
            }
        }
    }
}
