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

namespace ACEPurgeTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        SshOper ssh = new SshOper();
        private string exePath = "/kla-tencor/adb/utility/purge/";
        private string exeProgram = "AcePurge";
        private string configFile = "AcePurge.ini";
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<string> LotID = (new TextRange(richtext.Document.ContentStart, richtext.Document.ContentEnd)).Text.Split(new char[] { '\r', '\n' }).Where(p=>!string.IsNullOrEmpty(p)).ToList();
            ssh.Commands = new List<string>();
            foreach (string lot in LotID)
            {
                ssh.Commands.Add("source /etc/profile&&" + exePath+exeProgram+" INL -ini="+exePath+configFile+" -l='"+lot+"' -c=100");
            }
            richresault.AppendText(ssh.GetResault());
            richresault.AppendText("Ok");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ssh.Commands = new List<string>();
            ssh.Commands.Add("source /etc/profile&&echo $PATH");
            richresault.AppendText(ssh.GetResault());
        }
    }
}
