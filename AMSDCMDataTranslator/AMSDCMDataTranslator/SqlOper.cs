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

        //load到oracle，已作废
        public void LoadToDataBase(DCM dcm)
        {
            try
            {
                sql = string.Format("insert into ACE_LOADER.AMS_DCM values ('{0}',to_date('{1}', 'yyyy/mm/dd HH24:mi:ss'),'{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}',{13},{14},{15},{16},to_date('{17}', 'yyyy/mm/dd HH24:mi:ss'),'{18}')", dcm.Guid, dcm.CollectionDateTime, dcm.EQP_ID, dcm.WaferID, dcm.LotID, dcm.Cassette, dcm.SLOT, dcm.Status, dcm.DataType, dcm.Recipe, dcm.RCPCNT, dcm.MeasSet, dcm.MateRial, dcm.Site, dcm.SiteNo, dcm.X, dcm.Y, dcm.UpdateTime, dcm.FileName);
                sqlHelper.setSomeDate(sql);
                foreach (DCM_Value value in dcm.values)
                {
                    sql = string.Format("insert into ACE_LOADER.AMS_DCM_VALUES values ('{0}','{1}',{2})", dcm.Guid, value.ValueCategory, value.MeasureValue);
                    sqlHelper.setSomeDate(sql);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message+"\tFileName:"+dcm.FileName+"\tLotID:"+dcm.LotID+"\tWaferID:"+dcm.WaferID+"\tSiteNo"+dcm.SiteNo);
            }
        }

        public void LoadToDB2(DCMDB2 db2)
        {
            try
            {
                sql = string.Format("insert into EDA_INLINE_DCM values ('{0}',to_date('{1}', 'yyyy/mm/dd HH24:mi:ss'),'{2}','{3}','{4}',{5},'{6}',to_date('{7}', 'yyyy/mm/dd HH24:mi:ss'),'{8}')", db2.Guid,db2.CollectionDateTime,db2.EQP_ID,db2.LotID,db2.Recipe,db2.MeasureDataCount,db2.CoordinateArray,db2.UpdateTime,db2.FileName);
                SqlHelper sqlHelper = new SqlHelper();
                sqlHelper.getSomeDate(sql);
            }
            catch (Exception e)
            {
                // throw new Exception(e.Message + "\tFileName:" + db2.FileName + "\tLotID:" + db2.LotID );
                LogHelper.ErrorLog(new Exception(e.Message + "\tFileName:" + db2.FileName + "\tLotID:" + db2.LotID));
            }
        }
    }
}
