using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;

namespace AMSACEWebMonitor
{
    public static class Setting
    {
        /// <summary>
        /// 设置监控的目录
        /// </summary>
        public static List<MonitorUnity> MonitorUnities
        {
            get;
            set;
        } = new List<MonitorUnity>();

        /// <summary>
        /// 执行任务时间间隔，单位：小时
        /// </summary>
        public static double Interval
        {
            get;
            set;
        } = 1;

        /// <summary>
        /// 超过该值则促发SendMail动作
        /// </summary>
        public static double SpecTime
        {
            get;
            set;
        } = 4.02;

        public static EmailParameter emailParameter
        {
            get;
            set;
        } = new EmailParameter();

        /// <summary>
        /// 执行的程序路径
        /// </summary>
        private static string exePath
        {
            get { return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName; }
        }

        /// <summary>
        /// 执行程序所在目录
        /// </summary>
        public static string exeDir
        {
            get { return exePath.Substring(0, exePath.LastIndexOf("\\") + 1); }
        }

        /// <summary>
        /// Config.xml路径
        /// </summary>
        public static string ConfigFilePath
        {
            get { return exeDir + "App\\config.xml"; }
        }

        /// <summary>
        /// 读取Directory、Interval值
        /// </summary>
        public static void GetValue()
        {
            DataSet ds= XmlHelper.GetDataSet(ConfigFilePath,XmlHelper.XmlType.File);
            DataTable dt = new DataTable();
            try
            {
                dt = ds.Tables["Timer"];
                Interval =Convert.ToDouble(dt.Rows[0]["Interval"].ToString());
                SpecTime= Convert.ToDouble(dt.Rows[0]["SpecTime"].ToString());
            }
            catch (Exception e)
            {
                LogHelper.LogWrite("获取配置文件【Timer】异常告警："+ e.Message);
            }

            try
            {
                dt = ds.Tables["MonitorUnity"];
                foreach (DataRow dr in dt.Rows)
                {
                    MonitorUnity unity = new MonitorUnity();
                    unity.Directory=new DirectoryInfo(dr["Dir"].ToString());
                    unity.LastRunRecipeTime = dr["LastTime"].ToString();
                    unity.HasSent= Convert.ToBoolean(dt.Rows[0]["HasSent"].ToString());
                    MonitorUnities.Add(unity);
                }
            }
            catch (Exception e)
            {
                LogHelper.LogWrite("获取配置文件【MonitorUnity】异常告警：" + e.Message);
            }

            try
            {
                dt = ds.Tables["Mail"];
                emailParameter.From = dt.Rows[0]["From"].ToString();
                emailParameter.To = dt.Rows[0]["To"].ToString();
                emailParameter.SMTPServer= dt.Rows[0]["SMTPServer"].ToString();
                emailParameter.Port = Convert.ToInt16(dt.Rows[0]["Port"].ToString());
                emailParameter.Password= dt.Rows[0]["Password"].ToString();
            }
            catch (Exception e)
            {
                LogHelper.LogWrite("获取配置文件【Mail】异常告警：" + e.Message);
            }


        }
    }
}
