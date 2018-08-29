using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
   public class RunningCongfig
    {
        public RunningCongfig()
        {
            //GetData();
        }

        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private string _configFile
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1) + "App\\config\\config.xml"; }
        }
        public int INTERVAL
        {
            get;
            set;
        } = 60 * 1000 * 10;

        public string LOCALPATH
        {
            get;
            protected set;
        } = @"App\data\history";

        public string WORKINGPATH
        {
            get;
            protected set;
        }= @"App\data\current";

        public string DIRPATH
        {
            get;
            set;
        } = @"\\10.8.0.252\Data\OutputData\EAP";


        public string DATASOURCE
        {
            get;
            set;
        } = "10.132.0.38";

        public bool SECURITY
        {
            get;
            set;
        } = false;

        public string CATALOG
        {
            get;
            set;
        }= "acexp";

        public string USERID
        {
            get;
            set;
        }= "ace_loader";

        public string PASSWORD
        {
            get;
            set;
        } = "KT4LOADER";

        public int PORT
        {
            get;
            set;
        } = 1521;


        public void GetData()
        {
            try
            {
                DataSet ds = XmlHelper.GetDataSet(_configFile, XmlHelper.XmlType.File);
                //Timer表
                INTERVAL = Convert.ToInt32(DESjiami.DecryptDES(ds.Tables["Timer"].DefaultView[0][0].ToString()));
                //FilePaths表
                LOCALPATH = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["LocalPath"].ToString());
                WORKINGPATH = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["WorkingPath"].ToString());
                DIRPATH = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["DirPath"].ToString());
                //DB表
                DATASOURCE = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["DataSource"].ToString());
                SECURITY = Convert.ToBoolean(DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Security"].ToString()));
                CATALOG = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Catalog"].ToString());
                USERID = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["UserID"].ToString());
                PASSWORD = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Password"].ToString());
                PORT = Convert.ToInt32(DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Port"].ToString()));
            }
            catch (Exception)
            {
                
            }

        }

  

        public void WriteXML()
        {
            DataSet ds = new DataSet();

            DataTable dt = new DataTable("Timer");
            dt.Columns.Add("Interval",typeof(string));
            DataRow dr = dt.NewRow();
            dr[0] = DESjiami.EncryptDES(INTERVAL.ToString());
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            dt = new DataTable("FilePaths");
            dt.Columns.Add("LocalPath",typeof(string));
            dt.Columns.Add("WorkingPath",typeof(string));
            dt.Columns.Add("DirPath",typeof(string));
            dr = dt.NewRow();
            dr["LocalPath"]= DESjiami.EncryptDES(LOCALPATH);
            dr["WorkingPath"]= DESjiami.EncryptDES(WORKINGPATH);
            dr["DirPath"] = DESjiami.EncryptDES(DIRPATH);
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            dt = new DataTable("DB");
            dt.Columns.Add("DataSource",typeof(string));
            dt.Columns.Add("Security",typeof(string));
            dt.Columns.Add("Catalog",typeof(string));
            dt.Columns.Add("UserID",typeof(string));
            dt.Columns.Add("Password",typeof(string));
            dt.Columns.Add("Port",typeof(string));
            dr = dt.NewRow();
            dr["DataSource"] = DESjiami.EncryptDES(DATASOURCE);
            dr["Security"] = DESjiami.EncryptDES(SECURITY.ToString());
            dr["Catalog"] = DESjiami.EncryptDES(CATALOG);
            dr["UserID"] = DESjiami.EncryptDES(USERID);
            dr["Password"] = DESjiami.EncryptDES(PASSWORD);
            dr["Port"] = DESjiami.EncryptDES(PORT.ToString());
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            ds.WriteXml(_configFile);
        
        }

    }
}
