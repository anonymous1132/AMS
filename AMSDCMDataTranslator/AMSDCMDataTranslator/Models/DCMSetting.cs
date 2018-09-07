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
    public static class DCMSetting
    {
        public static int Interval
        {
            get;
            set;
        } = 60 * 1000 * 10;

        public static string SourcePath
        {
            get;
            set;
        }= @"\\10.8.0.252\Data\OutputData\EAP";

        public static string HistoryPath
        {
            get;
            set;
        }= @"App\dcm_data\history";

        public static string WorkingPath
        {
            get;
            set;
        }= @"App\dcm_data\current";


        public static string DataSource
        {
            get;
            set;
        }= "10.132.0.38";

        public static bool Security
        {
            get;
            set;
        } = false;

        public static string Catalog
        {
            get;
            set;
        }= "acexp";

        public static string UserID
        {
            get;
            set;
        }= "ace_loader";

        public static string Password
        {
            get;
            set;
        } = "KT4LOADER";

        public static int Port
        {
            get;
            set;
        } = 1521;

        public static void SetValue()
        {
            string exePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            string configPath= exePath.Substring(0, exePath.LastIndexOf("\\") + 1) + "App\\config\\dcmconfig.xml";
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
                //DB表
                DataSource = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["DataSource"].ToString());
                Security = Convert.ToBoolean(DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Security"].ToString()));
                Catalog = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Catalog"].ToString());
                UserID = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["UserID"].ToString());
                Password = DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Password"].ToString());
                Port = Convert.ToInt32(DESjiami.DecryptDES(ds.Tables["DB"].DefaultView[0]["Port"].ToString()));
            }
        }



    }
}
