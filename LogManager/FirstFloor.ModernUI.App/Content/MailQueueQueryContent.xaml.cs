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
using System.Data;

namespace FirstFloor.ModernUI.App.Content
{
    /// <summary>
    /// MailQueueQueryContent.xaml 的交互逻辑
    /// </summary>
    public partial class MailQueueQueryContent : UserControl
    {
        public MailQueueQueryContent()
        {
            InitializeComponent();
            ViewModel = new MailQueueQueryViewModel();
            ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().MailQueueViewCollection;
            DataContext = ViewModel;

            //dg1的contextmenu
            menu = new ContextMenu();
            MenuItem item = new MenuItem { Header = "删除选中" };
            item.Click += DeleteSelected;
            menu.Items.Add(item);
            MenuItem item2 = new MenuItem { Header = "删除全部" };
            item2.Click += DeleteAll;
            menu.Items.Add(item2);
            item3 = new MenuItem { Header = "查看详情" };
            item3.Click += ViewDetails;
        }

        private ContextMenu menu;
        private MenuItem item3;
        private MailQueueQueryViewModel ViewModel;

        private void button_query_Click(object sender, RoutedEventArgs e)
        {
            switch (ViewModel.ComboTitle_SelectedIndex)
            {
                case 0:
                    QueryByReserveMailAddress(ViewModel.TextBox_Content_Text);
                    break;
                case 1:
                    QueryBySendMailAddress(ViewModel.TextBox_Content_Text);
                    break;
                case 2:
                    QueryByTime(ViewModel.DatePicker1_Value, ViewModel.DatePicker2_Value);
                    break;
            }

        }

        private void button_query_all_Click(object sender, RoutedEventArgs e)
        {
            //ShareDataEntity.GetSingleton().UpdateMailQueueCollection();
            //ShareDataEntity.GetSingleton().UpdateMailQueueReserverCollection();
            //ShareDataEntity.GetSingleton().GetMailQueueViewCollection();
            ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().MailQueueViewCollection;
            button_query_Click(sender, e);
        }

        private void DG1_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (DG1.SelectedItems.Count == 1)
            {
                if (!menu.Items.Contains(item3))
                {
                    menu.Items.Add(item3);
                }
                DG1.ContextMenu = menu;
            }
            else if (DG1.SelectedItems.Count == 0)
            {
                DG1.ContextMenu = null;
            }
            else
            {
                if (menu.Items.Contains(item3))
                {
                    menu.Items.Remove(item3);
                }
                DG1.ContextMenu = menu;
            }
        }

        private void DeleteSelected(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.OKCancel;
            string msg = "确定要删除选中项吗？";
            var resault= ModernDialog.ShowMessage(msg, "删除选中项", btn);
            if (resault.ToString() == "OK")
            {
                //var item = ViewModel.DG1_ItemSource.Where(p => p.IsSelected).Select(s => s.QueueModel).ToList();
                //if (item.Count==0)
                //{
                //    var s = DG1.SelectedItems;
                //    foreach (MailQueueViewModel vm in s)
                //    {
                //        item.Add(vm.QueueModel);
                //    }
                //}
                var s = DG1.SelectedItems;
                var item = new List<MailQueueModel>();
                foreach (MailQueueViewModel vm in s)
                {
                    item.Add(vm.QueueModel);
                }
                ShareDataEntity.GetSingleton().DeleteModelInMailQueueCollection(item.ToArray());
                ShareDataEntity.GetSingleton().GetMailQueueViewCollection();
                ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().MailQueueViewCollection;
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
                var item = ViewModel.DG1_ItemSource.Select(s => s.QueueModel);
                ShareDataEntity.GetSingleton().DeleteModelInMailQueueCollection(item.ToArray());
                ShareDataEntity.GetSingleton().GetMailQueueViewCollection();
                ViewModel.DG1_ItemSource = ShareDataEntity.GetSingleton().MailQueueViewCollection;
                button_query_Click(sender, e);
            }
        }

        private void ViewDetails(object sender, RoutedEventArgs e)
        {
            MessageBoxButton btn = MessageBoxButton.OK;
            //var item = ViewModel.DG1_ItemSource.Where(p => p.IsSelected).FirstOrDefault();
            //if (item is null)
            //{
            //    item = DG1.SelectedItem as MailQueueViewModel;
            //}
            var item= DG1.SelectedItem as MailQueueViewModel;
            string  msg = item.Details;
            if (msg.Split('\n').Count() > 35)
            {
                DetailWindow detailWindow = new DetailWindow();
                detailWindow.Message.Text = msg;
                detailWindow.ShowDialog();
            }
            else
            {
                ModernDialog.ShowMessage(msg, "详情一览", btn);
            }
           
        }

        private void QueryByReserveMailAddress(string reserveMailAddress)
        {
            if (string.IsNullOrEmpty(reserveMailAddress))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<MailQueueViewModel>(ViewModel.DG1_ItemSource.Where(p => p.ReserverAddress.Contains(reserveMailAddress.Trim())));
        }

        private void QueryBySendMailAddress(string sendMailAddress)
        {
            if (string.IsNullOrEmpty(sendMailAddress))
            { return; }
            ViewModel.DG1_ItemSource = new ObservableCollection<MailQueueViewModel>(ViewModel.DG1_ItemSource.Where(p => p.SendMailAddress.Contains(sendMailAddress.Trim())));
        }

        private void QueryByTime(DateTime from, DateTime to)
        {
            ViewModel.DG1_ItemSource = new ObservableCollection<MailQueueViewModel>(ViewModel.DG1_ItemSource.Where(p => p.StartTime <= ViewModel.DateTimePicker2 && p.StartTime >= ViewModel.DateTimePicker1));
        }

    }
}
