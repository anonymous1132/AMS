using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailSmtpModel
    {
        public MailSmtpModel()
        {
            _guid = System.Guid.NewGuid().ToString();
        }
        public MailSmtpModel(string guid)
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

        private DateTime startTime;
        public DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }
        public DateTime EndTime { get; set; }

        private int mask;
        public int Mask
        {
            get { return mask; }
            set { mask = value; }
        }

        private string ipAddress;
        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private string sendMailAddress;
        public string SendMailAddress
        {
            get { return sendMailAddress; }
            set { sendMailAddress = value; }
        }

        private string reserveMailAddress;
        public string ReserveMailAddress
        {
            get { return reserveMailAddress; }
            set { reserveMailAddress = value; }
        }

        private double maxSize;
        public double MaxSize
        {
            get { return maxSize; }
            set { maxSize = value; }
        }

        private double reserveSize;
        public double ReserveSize
        {
            get { return reserveSize; }
            set { reserveSize = value; }
        }

        public string Details
        {
            get;
            set;
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }
    }
}
