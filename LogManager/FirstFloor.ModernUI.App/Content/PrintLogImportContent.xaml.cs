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
    /// PrintLogImportContent.xaml 的交互逻辑
    /// </summary>
    public partial class PrintLogImportContent : UserControl
    {
        public PrintLogImportContent()
        {
            InitializeComponent();
            ViewModel = new PrintLogImportViewModel();
            DataContext = ViewModel;
        }

        private PrintLogImportViewModel ViewModel;
        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.TextBox_File_Text = FileHelper.SelectSingleFile(new FileFilter { Description = "Microsoft Excel文件(*.xlsx)", Extension = "*.xlsx" });
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
                Runner.FileReader fileReader = new Runner.FileReader(ViewModel.TextBox_File_Text);
                ShareDataEntity shareData = ShareDataEntity.GetSingleton();
                Dispatcher.Invoke(new Action(() => shareData.AddNewModelToPrintLogCollection(fileReader.PrintLogList)));

                ViewModel.DG1_ItemSource = fileReader.PrintLogList;
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
