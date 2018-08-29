using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;

namespace AMSDCMDataTranslator.Helper
{
    class DB2Helper
    {
        private string strConn = "Provider=IBMDADB2;Data Source=AMRPTDB;UID=istrpt;PWD=istrpt;";
        public DataTable dt;

        public void GetSomeData(string strSql)
        {
            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                OleDbCommand cmd = new OleDbCommand(strSql, conn);
                try
                {
                    conn.Open();
                    OleDbDataAdapter adp = new OleDbDataAdapter(cmd);
                    this.dt = new DataTable();
                    adp.Fill(dt);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


    }
}
