using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using FirstFloor.ModernUI.App.Model;
using System.Collections.ObjectModel;
using FirstFloor.ModernUI.Windows.Controls;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// PrintLogQueryContent.xaml 的交互逻辑
    /// </summary>
    public partial class PrintLogQueryContent : UserControl
    {
        public PrintLogQueryContent()
        {
            InitializeComponent();
            ViewModel = new PrintLogQueryViewModel();
            ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().PrintLogCollection;
            DataContext = ViewModel;
            //dg1的contextmenu
            menu = new ContextMenu();
            MenuItem item = new MenuItem { Header = "删除选中" };
            item.Click += DeleteSelected;
            menu.Items.Add(item);
            MenuItem item2 = new MenuItem { Header = "删除全部" };
            item2.Click += DeleteAll;
            menu.Items.Add(item2);
        }

        private PrintLogQueryViewModel ViewModel;
        private void button_query_Click(object sender, RoutedEventArgs e)
        {

            switch (ViewModel.ComboTitle_SelectedIndex)
            {
                case 0:
                    QueryByUserName(ViewModel.TextBox_Content_Text);
                    break;
                case 1:
                    QueryByTime(ViewModel.DatePicker1_Value,ViewModel.DatePicker2_Value);
                    break;
                case 2:
                    QueryByComputerName(ViewModel.TextBox_Content_Text);
                    break;
                case 3:
                    QueryByIP(ViewModel.TextBox_Content_Text);
                    break;
                case 4:
                    QueryByMAC(ViewModel.TextBox_Content_Text);
                    break;
                case 5:
                    QueryByProgress(ViewModel.TextBox_Content_Text);
                    break;
                case 6:
                    QueryByPrintType(ViewModel.TextBox_Content_Text);
                    break;
                case 7:
                    QueryByFileName(ViewModel.TextBox_Content_Text);
                    break;
            }
        }


        private void button_query_all_Click(object sender, RoutedEventArgs e)
        {
            ShareDataEntity.GetSingleton().UpdatePrintLogCollection();
            ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().PrintLogCollection;
            button_query_Click(sender,e);
        }

        private void QueryByUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.UserName.Contains(userName.Trim())));
        }

        private void QueryByTime(DateTime from,DateTime to)
        {
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.ExecuteTime<=ViewModel.DateTimePicker2 && p.ExecuteTime>=ViewModel.DateTimePicker1 ));
        }

        private void QueryByComputerName(string computerName)
        {
            if (string.IsNullOrEmpty(computerName))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.ComputerName.Contains(computerName.Trim())));
        }

        private void QueryByIP(string IP)
        {
            if (string.IsNullOrEmpty(IP))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.IPAddress==IP.Trim()));
        }

        private void QueryByMAC(string MAC)
        {
            if (string.IsNullOrEmpty(MAC))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.MACAddress == MAC.Trim()));
        }

        private void QueryByProgress(string progress)
        {
            if (string.IsNullOrEmpty(progress))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.ProgramName == progress.Trim()));
        }

        private void QueryByPrintType(string type)
        {
            if (string.IsNullOrEmpty(type))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.PrintType == type.Trim()));
        }

        private void QueryByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p =>string.IsNullOrEmpty(p.FileName)));
            }
            else
            {
                ViewModel.DG1_ItemSource = new ObservableCollection<PrintLogViewModel>(ViewModel.DG1_ItemSource.Where(p => p.FileName == fileName.Trim()));
            }
        }

        private void dgmenu1_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DG1.SelectedItems.Count > 0)
            {
              
                DG1.ContextMenu = menu;
            }
            else
            {
                DG1.ContextMenu = null;
            }

        }

        private ContextMenu menu ;
       
        private void DeleteSelected(object sender,RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.OKCancel;
            string msg = "确定要删除选中项吗？";
            var resault = ModernDialog.ShowMessage(msg, "删除选中项", btn);
            if (resault.ToString() == "OK")
            {
                //var item = ViewModel.DG1_ItemSource.Where(p => p.IsSelected).ToList();
                //if (item.Count==0)
                //{
                //    foreach (PrintLogViewModel vm in DG1.SelectedItems)
                //    {
                //        item.Add(vm);
                //    }
                //}
                var item = new List<PrintLogViewModel>();
                foreach (PrintLogViewModel vm in DG1.SelectedItems)
                {
                    item.Add(vm);
                }
                ShareDataEntity.GetSingleton().DeleteModelInPrintLogCollection(item.ToArray());
                ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().PrintLogCollection;
                button_query_Click(sender, e);
            }
        }

        private void DeleteAll(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.OKCancel;
            string msg = "确定要删除所有项吗？";
            var resault = ModernDialog.ShowMessage(msg, "删除所有项", btn);
            if (resault.ToString() == "OK")
            {
                ShareDataEntity.GetSingleton().DeleteModelInPrintLogCollection(ViewModel.DG1_ItemSource.ToArray());
                ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().PrintLogCollection;
                button_query_Click(sender, e);
            }
        }
    }
}
