using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using MCADataTranslator.Bll;

namespace MCADataTranslator.Content
{
    /// <summary>
    /// ImportAppearanceType2.xaml 的交互逻辑
    /// </summary>
    public partial class ImportAppearanceType2 : UserControl
    {
        public ImportAppearanceType2()
        {
            InitializeComponent();
        }
        private ObservableCollection<ImportDGViewType2> obc_type2;
        private void button_selectfile_Click(object sender, RoutedEventArgs e)
        {
            string[] filepaths = selectfile();
            obc_type2 = new ObservableCollection<ImportDGViewType2>();
            if (filepaths == null)
            {
                DG1.ItemsSource = obc_type2;
                return;
            }

            foreach (string file in filepaths)
            {
               ImportDGViewType2 type2 = new ImportDGViewType2(file);
                obc_type2.Add(type2);
            }
            DG1.ItemsSource = obc_type2;
            this.button_import.IsEnabled = true;
        }

        private void button_import_Click(object sender, RoutedEventArgs e)
        {
            if (obc_type2.Count == 0) return;
            foreach (ImportDGViewType2 type2 in obc_type2)
            {
                type2.ImportDatas();
            }
            this.button_import.IsEnabled = false;
        }

        private string[] selectfile()
        {

            System.Windows.Forms.OpenFileDialog openFile = new System.Windows.Forms.OpenFileDialog();
            openFile.Filter = "CSV(逗号分隔)(*.csv)|*.csv";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = true;
            if (openFile.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return null;
            }
            return openFile.FileNames;
        }
    }
}
