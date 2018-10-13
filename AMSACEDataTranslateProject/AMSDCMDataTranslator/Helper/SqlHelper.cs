using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using AMSDCMDataTranslator.Models;

namespace AMSDCMDataTranslator.Helper
{
   public class SqlHelper2
    {
        public SqlDataAdapter sda;
        public DataTable dt;
        SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();
        private void Connect()
        {
            //sqlconnection
            connbuilder.DataSource = DCMSetting.DataSource;
            connbuilder.IntegratedSecurity = DCMSetting.Security;
            connbuilder.InitialCatalog = DCMSetting.Catalog;
            connbuilder.UserID = DCMSetting.UserID;
            connbuilder.Password = DCMSetting.Password;
        }

        public void GetSomeDate(string sql)
        {
            Connect();
            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
            sda = new SqlDataAdapter(sql, conn);
            this.dt = new DataTable();
            this.sda.AcceptChangesDuringUpdate = true;
            try
            {
                this.sda.Fill(dt);
            }
            catch (Exception e)
            { throw e; }
            finally { conn.Close(); }
        }

        public void GetSomeData2(string sql, SqlParameter[] paraValues)
        {
            Connect();
            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand
            {
                CommandText = sql,
                //cmd.CommandType = CommandType.StoredProcedure;
                Connection = conn
            };
            try
            {
                conn.Open();
                this.dt = new DataTable();
                if (paraValues != null)
                {
                    cmd.Parameters.AddRange(paraValues);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                {
                    adapter.Fill(dt);
                }
            conn.Close();
            }
            catch (Exception e)
            { throw e; }
            finally { conn.Close(); }
        }
    }

    public class SqlHelper
    {
        public DataTable dt;

        public void GetSomeDate(string sql)
        {

            DB2Helper db2 = new DB2Helper();
            try
            {
                db2.GetSomeData(sql);
                dt = db2.dt;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public void SetSomeDate(string sql)
        {
            GetSomeDate(sql);
        }

    }
}
