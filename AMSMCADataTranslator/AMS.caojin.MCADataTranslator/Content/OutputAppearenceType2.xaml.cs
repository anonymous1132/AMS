using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using MCADataTranslator.Helper;
using System.Collections.ObjectModel;
using MCADataTranslator.Bll;
using System.Globalization;

namespace MCADataTranslator.Content
{
    /// <summary>
    /// OutputAppearenceType2.xaml 的交互逻辑
    /// </summary>
    public partial class OutputAppearenceType2 : UserControl
    {
        public OutputAppearenceType2()
        {
            InitializeComponent();
        }
        private ObservableCollection<QueryDataViewModelType2> obc_qvm;
        private ReportModelBlockType2 report;
        private void bt_query_Click(object sender, RoutedEventArgs e)
        {
            SqlHelper sqlHelper = new SqlHelper();
            string sql = "";
            switch (this.comb_title.SelectedIndex)
            {
                case 0:
                    sql = "select distinct FileName, ImportDateTime from MCA_ACE_TYPE2_DATA order by ImportDateTime desc";
                    break;
                case 1:
                    sql = "select  distinct FileName, ImportDateTime from MCA_ACE_TYPE2_DATA where FileName like '%" + this.textbox_content.Text + "%' order by ImportDateTime desc";
                    break;
                case 2:
                    sql = "select  distinct FileName, ImportDateTime from MCA_ACE_TYPE2_DATA where ImportDateTime like '%" + this.textbox_content.Text + "%' order by ImportDateTime desc";
                    break;
            }
            sqlHelper.getSomeDate(sql);
            if (sqlHelper.dt.DefaultView.Count > 0)
            {
                obc_qvm = ModelConvertHelper<QueryDataViewModelType2>.ConvertToObc(sqlHelper.dt);
                DG1.ItemsSource = obc_qvm;
            }
            else
            { obc_qvm = new ObservableCollection<QueryDataViewModelType2>(); }
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
            string filepath = "";
            SaveFile(ref filepath);
            if (string.IsNullOrEmpty(filepath)) { return; }
            ExcelOper excel = new ExcelOper(filepath);
            foreach (QueryDataViewModelType2 qvm in obc_qvm)
            {
                if (qvm.IsSelected)
                {
                    this.report = new ReportModelBlockType2(qvm.FileName,qvm.ImportDateTime);
                    excel.PrintOneReportBlcok(report);
                }

            }
            excel.Save();
            excel.Quit();
            MessageBox.Show("Done!", "Information");
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
            saveFile.FileName = "VPDReport-" + DateTime.Now.ToString("yyyyMMdd");
            if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            filepath = saveFile.FileName;
            string mouldpath = "mould\\mould2.xls";
            if (!File.Exists(mouldpath)) { MessageBox.Show("Not Found The Mould File \"mould2.xls!\"", "Error"); return; }
            if (File.Exists(filepath)) { try { File.Delete(filepath); } catch (Exception ex) { MessageBox.Show(ex.Message); return; } }
            File.Copy(mouldpath, filepath);
        }
    }
}
