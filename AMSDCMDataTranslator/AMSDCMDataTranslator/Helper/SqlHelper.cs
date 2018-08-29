using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace AMSDCMDataTranslator.Helper
{
   public class SqlHelper2
    {
        public SqlDataAdapter sda;
        public DataTable dt;
        SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();
        private void connect()
        {
            //sqlconnection
            //connbuilder.DataSource = "10.132.0.34";
            //connbuilder.IntegratedSecurity = false;
            //connbuilder.InitialCatalog = "ACEDB";
            //connbuilder.UserID = "ams_ace";
            //connbuilder.Password = "AMS@123456";
            // connbuilder.ConnectTimeout = 3000;
            connbuilder.DataSource = PUBLICSTRING.DataSource;
            connbuilder.IntegratedSecurity = PUBLICSTRING.Security;
            connbuilder.InitialCatalog = PUBLICSTRING.Catalog;
            connbuilder.UserID = PUBLICSTRING.UserID;
            connbuilder.Password = PUBLICSTRING.Password;
        }

        public void getSomeDate(string sql)
        {
            connect();
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

        public void getSomeData2(string sql, SqlParameter[] paraValues)
        {
            connect();
            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = conn;
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

        public void getSomeDate(string sql)
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

        public void setSomeDate(string sql)
        {
            getSomeDate(sql);
        }

    }
}
