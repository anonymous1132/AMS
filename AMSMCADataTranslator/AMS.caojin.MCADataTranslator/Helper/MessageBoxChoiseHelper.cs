using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCADataTranslator.Helper
{
   public class MessageBoxChoiseHelper
    {
        public static bool IsConfirmed(string message)
        {
            bool isConfirmed = MessageBox.Show(message, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK;
            return isConfirmed;
        }
    }
}
