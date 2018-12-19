using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Threading;
using FirstFloor.ModernUI.App.Model;
using Caojin.Common;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// MailQueueImportContent.xaml 的交互逻辑
    /// </summary>
    public partial class MailQueueImportContent : UserControl
    {
        public MailQueueImportContent()
        {
            InitializeComponent();
            ViewModel = new MailQueueImportViewModel();
            DataContext = ViewModel;
        }

        private MailQueueImportViewModel ViewModel;

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
                Runner.QueueFileReader fileReader = new Runner.QueueFileReader(ViewModel.TextBox_File_Text);
                ShareDataEntity shareData = ShareDataEntity.GetSingleton();
                    shareData.AddNewModelToMailQueueCollection(fileReader.QueueModelList.Select(s => s.QueueModel).ToList());
                    fileReader.QueueModelList.ForEach(f => shareData.AddNewModelToMailQueueReserverCollection(f.ReserverEntities));
                ViewModel.DG1_ItemSource = fileReader.QueueModelList;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {

                Dispatcher.Invoke(new Action(() => ShareDataEntity.GetSingleton().GetMailQueueViewCollection()));

                ViewModel.ProgressRing_IsActive = false;
            }
        }
    }
}
