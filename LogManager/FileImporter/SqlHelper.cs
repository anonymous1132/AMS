using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Caojin.Common
{
    public class SqlHelper
    {
        public SqlDataAdapter sda;
        public DataTable dt;
        SqlConnectionStringBuilder connbuilder = new SqlConnectionStringBuilder();
        private void connect()
        {
            //sqlconnection
            connbuilder.DataSource = "10.132.0.34";
            connbuilder.IntegratedSecurity = false;
            connbuilder.InitialCatalog = "LogManagerDB";
            connbuilder.UserID = "ams_log";
            connbuilder.Password = "AMS@123456";
            // connbuilder.ConnectTimeout = 3000;
        }

        public void getSomeDate(string sql)
        {
            connect();
            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
            sda = new SqlDataAdapter(sql, conn);
            this.dt = new DataTable();
            this.sda.AcceptChangesDuringUpdate = true;
            this.sda.Fill(dt);
        }

        public void getSomeData2(string sql, SqlParameter[] paraValues)
        {
            connect();
            SqlConnection conn = new SqlConnection(connbuilder.ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = sql;
            cmd.Connection = conn;
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
    }
}
