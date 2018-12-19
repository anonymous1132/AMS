using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailReadLineEntity
    {
        public DateTime NoteTime
        {
            get;
            set;
        }

        public int Mask
        {
            get;
            set;
        }

        public string Command
        {
            get;
            set;
        }

        public string Content
        {
            get;
            set;
        }

        public string Detail
        {
            get { return string.Format("{0} {1} {2} {3}", NoteTime.ToString("yyyy/MM/dd-HH:mm:ss"), Mask.ToString(), Command, Content); }
        }
    }
}
