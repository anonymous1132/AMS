using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using Caojin.Common;
using FirstFloor.ModernUI.App.Model;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// MailSmtpImportContent.xaml 的交互逻辑
    /// </summary>
    public partial class MailSmtpImportContent : UserControl
    {
        public MailSmtpImportContent()
        {
            InitializeComponent();
            ViewModel = new MailSmtpImportViewModel();
            DataContext = ViewModel;
        }

        private MailSmtpImportViewModel ViewModel;

        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TextBox_File_Text = FileHelper.SelectSingleFile(new FileFilter { Description = "文本文件(*.txt)", Extension = "*.txt" });
        }

        private void button_import_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ProgressRing_IsActive = true;
            new Thread(new ThreadStart(ImportTask)).Start();
        }

        private void ImportTask()
        {
            try
            {
                Runner.SmtpFileReader fileReader = new Runner.SmtpFileReader(ViewModel.TextBox_File_Text);
                ShareDataEntity shareData = ShareDataEntity.GetSingleton();
                Dispatcher.Invoke(new Action(() => shareData.AddNewModelToMailSmtpCollection(fileReader.SmtpModelList)));
                ViewModel.DG1_ItemSource = fileReader.SmtpModelList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                ViewModel.ProgressRing_IsActive = false;
            }
        }

    }
}
