using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Globalization;
using MCADataTranslator.Helper;
using MCADataTranslator.Bll;
using System.Collections.ObjectModel;

namespace MCADataTranslator.Content
{
    /// <summary>
    /// OutputAppearance.xaml 的交互逻辑
    /// </summary>
    public partial class OutputAppearance : UserControl
    {
        public OutputAppearance()
        {
            InitializeComponent();

        }


        private ReportModelBlock report;
        private ObservableCollection<QueryDataViewModel> obc_qvm;
        private void bt_query_Click(object sender, RoutedEventArgs e)
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "";
            switch (this.comb_title.SelectedIndex)
            {
                case 0:
                    sql = "select * from MCA_Pool order by UpdateDateTime desc";
                    break;
                case 1:
                    sql = "select * from MCA_Pool where SampleComment like '%" + this.textbox_content.Text + "%' order by UpdateDateTime desc";
                    break;
                case 2:
                    sql = "select * from MCA_Pool where UpdateDateTime like '%" + this.textbox_content.Text + "%' order by UpdateDateTime desc";
                    break;
            }
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count > 0)
            {
                obc_qvm = ModelConvertHelper<QueryDataViewModel>.ConvertToObc(sqlHelper.dt);
                DG1.ItemsSource = obc_qvm;
            }
            else
            { obc_qvm = new ObservableCollection<QueryDataViewModel>(); }
            DG1.ItemsSource = obc_qvm;
        }


        private void CheckAll_Click(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == false)
            { DG1.SelectedItems.Clear(); }
            else
            { DG1.SelectAll(); }

        }

  
        private void bt_export_Click(object sender, RoutedEventArgs e)
        {
            if (GetSelectItemsCount() <= 0) { return; }
            string filepath="";
            SaveFile(ref filepath);
            if (string.IsNullOrEmpty(filepath)) { return; }
            ExcelOper excel = new ExcelOper(filepath);
            foreach (QueryDataViewModel qvm in obc_qvm)
            {
                if (qvm.IsSelected)
                {
                    this.report = new ReportModelBlock(qvm.SampleComment);
                    excel.PrintOneReportBlcok(report);
                }
   
            }
            excel.Save();
            excel.Quit();
            MessageBox.Show("Done!","Information");
        }

        private int GetSelectItemsCount()
        {
            return DG1.SelectedItems.Count;
        }

        private void SaveFile(ref string filepath)
        {
            System.Windows.Forms.SaveFileDialog saveFile = new System.Windows.Forms.SaveFileDialog();
            saveFile.Filter = "Excel 97-2003工作表(*.xls)|*.xls|Excel工作表(*.xlsx)|*.xlsx";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture("en-US");
            saveFile.FileName = "Report-" + DateTime.Now.ToString("yyyyMMdd");
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            filepath = saveFile.FileName;
            string mouldpath = "mould\\mould.xls";
            if (!File.Exists(mouldpath)) { MessageBox.Show("Not Found The Mould File \"mould.xls!\"", "Error"); return; }
            if (File.Exists(filepath)) { try { File.Delete(filepath); } catch (Exception ex) { MessageBox.Show(ex.Message); return; } }
            File.Copy(mouldpath, filepath);
        }




    }


}
