using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.IO;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
    public static class HlcmSetting
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
        } = @"App\hlcm_data\current";

        public static string HistoryPath
        {
            get;
            set;
        } = @"App\hlcm_data\history";


        public static string SiffPath
        {
            get;
            set;
        } = @"App\hlcm_data\siff";

        public static string SiffHistoryPath
        {
            get;
            set;
        } = @"App\hlcm_data\siff_history";


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

        public static string SFtpServerIP
        {
            get;
            set;
        } = "112.20.189.145";

        public static string SFtpUserID
        {
            get;
            set;
        } = "ams-file";

        public static string SFtpPassword
        {
            get;
            set;
        } = "16kQgN8V";

        public static void SetValue()
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string configPath = exePath.Substring(0, exePath.LastIndexOf("\\") + 1) + "App\\config\\hlcmconfig.xml";
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
                SiffPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffPath"].ToString());
                SiffHistoryPath = DESjiami.DecryptDES(ds.Tables["FilePaths"].DefaultView[0]["SiffHistoryPath"].ToString());
                //FTP表
                FtpServerIP = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["IP"].ToString());
                FtpUserID = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["UserID"].ToString());
                FtpPassword = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Password"].ToString());
                FtpUri = DESjiami.DecryptDES(ds.Tables["FTP"].DefaultView[0]["Uri"].ToString());
                //SFTP表
                SFtpServerIP = DESjiami.DecryptDES(ds.Tables["SFTP"].DefaultView[0]["IP"].ToString());
                SFtpUserID = DESjiami.DecryptDES(ds.Tables["SFTP"].DefaultView[0]["UserID"].ToString());
                SFtpPassword= DESjiami.DecryptDES(ds.Tables["SFTP"].DefaultView[0]["Password"].ToString());
            }
        }
    }
}
