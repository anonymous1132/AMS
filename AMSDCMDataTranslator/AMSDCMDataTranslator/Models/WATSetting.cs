using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public static class WATSetting
    {
        public static int Interval
        {
            get;
            set;
        } = 60 * 1000 * 10;

        public static string WorkingPath
        {
            get;
            set;
        } = @"App\wat_data\current";

        public static string HistoryPath
        {
            get;
            set;
        } = @"App\wat_data\history";

        public static string SourcePath
        {
            get;
            set;
        } = @"\\10.8.0.252\Data\OutputData\WAT";

        public static string SiffPath
        {
            get;
            set;
        } = @"App\wat_data\siff";

        public static string SiffHistoryPath
        {
            get;
            set;
        } = @"App\wat_data\siff_history";

        public static string SpecPath
        {
            get;
            set;
        } = @"\\10.8.0.252\Data\OutputData\Limit File";

        public static string FtpServerIP
        {
            get;
            set;
        } = "10.132.0.38";

        public static string FtpUserID
        {
            get;
            set;
        } = "ace";

        public static string FtpPassword
        {
            get;
            set;
        } = "Ams_ace";

        public static string FtpUri
        {
            get;
            set;
        } = "ftp://10.132.0.38/data/siff/Etest/import/";

        public static void SetValue()
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string configPath = exePath.Substring(0, exePath.LastIndexOf("\\") + 1) + "App\\config\\watconfig.xml";
            if (!File.Exists(configPath))
            {
                return;
            }
            else
            {
                DataSet ds = XmlHelper.GetDataSet(configPath, XmlHelper.XmlType.File);
                //Timer表
                Interval = Convert.ToInt32(DESjiami.DecryptDES(ds.Tables["Timer"].DefaultView[0][0].ToString()));
                //FilePaths表
                HistoryPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["HistoryPath"].ToString());
                WorkingPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["WorkingPath"].ToString());
                SourcePath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SourcePath"].ToString());
                SpecPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SpecPath"].ToString());
                SiffPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffPath"].ToString());
                SiffHistoryPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffHistoryPath"].ToString());
                //FTP表
                FtpServerIP = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["IP"].ToString());
                FtpUserID = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["UserID"].ToString());
                FtpPassword = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Password"].ToString());
                FtpUri = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Uri"].ToString());
            }
        }
    }
}
