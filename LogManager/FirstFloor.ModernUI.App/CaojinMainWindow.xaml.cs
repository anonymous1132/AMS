using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Presentation;
using Caojin.Common;
using System.Threading;

namespace FirstFloor.ModernUI.App
{
    /// <summary>
    /// CaojinMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaojinMainWindow:ModernWindow
    {
        public CaojinMainWindow()
        {
            InitializeComponent();
            //多线程获取数据
            Thread t = new Thread(new ThreadStart(InitialEntity));
            t.Start();
            try
            {
                DataTable dt = XmlHelper.GetTable(@"App\config.xml", XmlHelper.XmlType.File, "AppearenceSetting");
                AppearanceManager.Current.AccentColor = (Color)ColorConverter.ConvertFromString(dt.Rows[0]["AccentColor"].ToString());
                AppearanceManager.Current.FontSize = dt.Rows[0]["FontSize"].ToString() == "Small" ? Presentation.FontSize.Small : Presentation.FontSize.Large;
                AppearanceManager.Current.ThemeSource = new Uri(dt.Rows[0]["ThemeSource"].ToString(), UriKind.Relative);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// 初始化数据实体
        /// </summary>
        private void InitialEntity()
        {
            try
            {
                ShareDataEntity shareDataEntity = ShareDataEntity.GetSingleton();
                shareDataEntity.UpdatePrintLogCollection();
                shareDataEntity.UpdateMailSmtpCollection();
                shareDataEntity.UpdateMailQueueCollection();
                shareDataEntity.UpdateMailQueueReserverCollection();
                shareDataEntity.GetMailQueueViewCollection();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("数据更新出现错误:"+e.Message);
            }
        }
    }
}
