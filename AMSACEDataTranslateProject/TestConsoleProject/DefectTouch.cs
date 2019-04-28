using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator;

namespace TestConsoleProject
{
    public class DefectTouch
    {

        public static void Run(string filename="*")
        {
            SshOper ssh = new SshOper("10.132.0.35", "defect", "defect");
            ssh.GetResault(string.Format( "cd  /home/defect/ps_share/tooldata/EDA/ && touch {0}",filename));
        }
        
     
    }
}
