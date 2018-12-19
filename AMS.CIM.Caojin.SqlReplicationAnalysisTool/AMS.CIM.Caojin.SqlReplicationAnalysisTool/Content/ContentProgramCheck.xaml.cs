using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using Caojin.Common;
using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model;
using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Runner;
using System.Windows.Media;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Content
{
    /// <summary>
    /// ContentProgramCheck.xaml 的交互逻辑
    /// </summary>
    public partial class ContentProgramCheck : UserControl
    {
        public ContentProgramCheck()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
        private ContentProgressCheckViewModel ViewModel = new ContentProgressCheckViewModel();
        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileHelper.SelectSingleFile(new FileFilter() { Extension = "*.log", Description = "日志文件" });
            ViewModel.TextBox_File_Text = fileName;
            if (string.IsNullOrEmpty(fileName)) {ViewModel.FirstList=new List<string>(); return; }
            try
            {
                var runner = new ProgressCheckFileRunner(fileName);
                ViewModel.FirstList = runner.LineContent;
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "不可预知的错误", MessageBoxButton.OK);
            }
        }

        private void DG1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var drv = e.Row.Item as ProgressCheckModel;
            if (drv.Sign == "√")
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
            }
            else if (drv.Sign == "-")
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                e.Row.Background = new SolidColorBrush(Colors.Green);
            }
        }

        private void button_selectfile2_Click(object sender, RoutedEventArgs e)
        {
            string fileName = FileHelper.SelectSingleFile(new FileFilter() { Extension = "*.log", Description = "日志文件" });
            ViewModel.TextBox_File2_Text = fileName;
            if (string.IsNullOrEmpty(fileName)) { ViewModel.SecondList = new List<string>(); return; }
            try
            {
                var runner = new ProgressCheckFileRunner(fileName);
                ViewModel.SecondList= runner.LineContent;
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "不可预知的错误", MessageBoxButton.OK);
            }
        }
    }
}
