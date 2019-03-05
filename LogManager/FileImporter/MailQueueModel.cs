using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailQueueModel
    {
        public MailQueueModel()
        {
            _guid = System.Guid.NewGuid().ToString();
        }

        public MailQueueModel(string guid)
        {
            _guid = guid;
        }

        private string _guid;
        public string Guid
        {
            get
            {
                if (_guid == null)
                {
                    _guid = System.Guid.NewGuid().ToString();
                }
                return _guid;
            }
        }

        public string MailID
        {
            get;
            set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public int Mask
        {
            get;
            set;
        }

        public double SendSize
        {
            get;
            set;
        }

        public string SendMailAddress
        {
            get;
            set;
        }

        public string Details
        {
            get;
            set;
        }
    }
}
