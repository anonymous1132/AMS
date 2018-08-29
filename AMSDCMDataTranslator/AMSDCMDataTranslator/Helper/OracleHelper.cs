using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;
using System.Collections;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Types;

namespace AMSDCMDataTranslator.Helper
{
    class OracleHelper
    {
        private static string connStr =string.Format("User Id={0};Password={1};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3})))(CONNECT_DATA=(SERVICE_NAME={4})))",PUBLICSTRING.UserID,PUBLICSTRING.Password,PUBLICSTRING.DataSource,PUBLICSTRING.Port,PUBLICSTRING.Catalog);
       // private static string connStr = "User Id=ace_loader;Password=KT4LOADER;Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.132.0.38)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=acexp)))";


        #region 执行SQL语句,返回受影响行数
        public static int ExecuteNonQuery(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        #endregion

        public static int ExecuteNonQuery(string sql)
        {
            return ExecuteNonQuery(sql,new OracleParameter());
        }


        #region 执行SQL语句,返回DataTable;只用来执行查询结果比较少的情况
        public static DataTable ExecuteDataTable(string sql, params OracleParameter[] parameters)
        {
            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable datatable = new DataTable();
                    adapter.Fill(datatable);
                    return datatable;
                }
            }
        }
        #endregion

        public static DataTable ExecuteDataTable(string sql)
        {
            return ExecuteDataTable(sql,new OracleParameter());

        }
    }
}
