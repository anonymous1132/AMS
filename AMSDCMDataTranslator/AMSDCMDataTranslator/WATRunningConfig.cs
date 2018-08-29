using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using AMSDCMDataTranslator.Helper;
using System.IO;

namespace AMSDCMDataTranslator
{
   public class WATRunningConfig
    {
        public WATRunningConfig()
        { }

        public string LocalPath
        {
            get;
            set;
        }= @"App\wat_data\history";

        public string WorkingPath
        {
            get;
            set;
        }= @"App\wat_data\current";

        public string DirPath
        {
            get;
            set;
        }=@"C:\Users\PUI\Desktop\test";

        public string SiffPath
        {
            get;
            set;
        }= @"App\wat_data\siff";

        public string SiffHistoryPath
        {
            get;
            set;
        } =@"App\wat_data\siff_history";

        public string SpecPath
        {
            get;
            set;
        }= @"C:\Users\PUI\Desktop\test\spec";

        private string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
        private string _configFile
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1) + "App\\config\\watconfig.xml"; }
        }

        public int INTERVAL
        {
            get;
            set;
        } = 10 * 60 * 1000;

        public string FtpServerIP
        {
            get;
            set;
        }= "10.132.0.38";

        public string FtpUserID
        {
            get;
            set;
        } = "ace";

        public string FtpPassword
        {
            get;
            set;
        } = "Ams_ace";

        public string FtpUri
        {
            get;
            set;
        } = "ftp://10.132.0.38/data/siff/Etest/import/";

        public void GetData()
        {
            if (!File.Exists(_configFile))
            {
                return;
            }

            DataSet ds = XmlHelper.GetDataSet(_configFile, XmlHelper.XmlType.File);

            //Timer表
            INTERVAL = Convert.ToInt32(DESjiami.DecryptDES(ds.Tables["Timer"].DefaultView[0][0].ToString()));
            //FilePaths表
            LocalPath= DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["LocalPath"].ToString());
            WorkingPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["WorkingPath"].ToString());
            DirPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["DirPath"].ToString());
            SpecPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SpecPath"].ToString());
            SiffPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffPath"].ToString());
            SiffHistoryPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffHistoryPath"].ToString());
            //FTP表
            FtpServerIP= DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["IP"].ToString());
            FtpUserID= DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["UserID"].ToString());
            FtpPassword= DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Password"].ToString());
            FtpUri= DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Uri"].ToString());
        }

        public void WriteXml()
        {
            DataSet ds = new DataSet();
            //Timer
            DataTable dt = new DataTable("Timer");
            dt.Columns.Add("Interval", typeof(string));
            DataRow dr = dt.NewRow();
            dr[0] = DESjiami.EncryptDES(INTERVAL.ToString());
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            //FilePaths
            dt = new DataTable("FilePaths");
            dt.Columns.Add("LocalPath", typeof(string));
            dt.Columns.Add("WorkingPath", typeof(string));
            dt.Columns.Add("DirPath", typeof(string));
            dt.Columns.Add("SpecPath", typeof(string));
            dt.Columns.Add("SiffPath", typeof(string));
            dt.Columns.Add("SiffHistoryPath", typeof(string));
            dr = dt.NewRow();
            dr["LocalPath"] = DESjiami.EncryptDES(LocalPath);
            dr["WorkingPath"] = DESjiami.EncryptDES(WorkingPath);
            dr["DirPath"] = DESjiami.EncryptDES(DirPath);
            dr["SpecPath"] = DESjiami.EncryptDES(SpecPath);
            dr["SiffPath"] = DESjiami.EncryptDES(SiffPath);
            dr["SiffHistoryPath"] = DESjiami.EncryptDES(SiffHistoryPath);
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            //FTP
            dt = new DataTable("FTP");
            dt.Columns.Add("IP", typeof(string));
            dt.Columns.Add("UserID", typeof(string));
            dt.Columns.Add("Password", typeof(string));
            dt.Columns.Add("Uri", typeof(string));
            dr = dt.NewRow();
            dr["IP"]= DESjiami.EncryptDES(FtpServerIP);
            dr["UserID"]= DESjiami.EncryptDES(FtpUserID);
            dr["Password"]= DESjiami.EncryptDES(FtpPassword);
            dr["Uri"]= DESjiami.EncryptDES(FtpUri);
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);

            ds.WriteXml(_configFile);
        }
    }
}
