using System;
using System.Windows.Media;
using FirstFloor.ModernUI.Windows.Controls;
using System.Windows;
using System.Windows.Controls;
using Caojin.Common;
using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model;
using AMS.CIM.Caojin.SqlReplicationAnalysisTool.Runner;


namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Content
{
    /// <summary>
    /// ContentRunStatus.xaml 的交互逻辑
    /// </summary>
    public partial class ContentRunStatus : UserControl
    {
        public ContentRunStatus()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }

        private ContentRunStatusViewModel ViewModel=new ContentRunStatusViewModel();
        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
           string fileName=  FileHelper.SelectSingleFile(new FileFilter() { Extension="*",Description="未加密的文本文件"});
            ViewModel.TextBox_File_Text = fileName;
          
            if (string.IsNullOrEmpty(fileName)) return;

            try
            {
                RunStatusFileRunner fileRunner = new RunStatusFileRunner(fileName);
                ViewModel.DG1_ItemSource = fileRunner.RunStatusGroup;
                ViewModel.TextBlock_TargetTime_Text =string.Format( "TargetTime:{0}",fileRunner.RunStatusGroup.DtargetTime.ToString("yyyy年MM月dd日 HH:mm:ss"));
               
            }
            catch (DateTimeParseException)
            {
                ViewModel.TextBlock_TargetTime_Text = "文件名称不符合规范！";
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "不可预知的错误", MessageBoxButton.OK);
            }
        }

        private void DG1_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            var drv = e.Row.Item as RunStatusLineModel;
            if (drv.MinDeltaTimeSpan >= 6)
            {
                e.Row.Background = new SolidColorBrush(Colors.Red);
            }
            else
            {
                e.Row.Background = new SolidColorBrush(Colors.White);
            }
      
        }
    }
}
