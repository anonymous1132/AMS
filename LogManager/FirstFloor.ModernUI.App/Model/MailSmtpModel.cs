using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailSmtpModel:NotifyPropertyChanged
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
            set { startTime = value;OnPropertyChanged("StartTime"); }
        }

        private DateTime endTime;
        public DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; OnPropertyChanged("EndTime"); }
        }

        private int mask;
        public int Mask
        {
            get { return mask; }
            set { mask = value;OnPropertyChanged("Mask"); }
        }

        private string ipAddress;
        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value;OnPropertyChanged("IPAddress"); }
        }

        private string sendMailAddress;
        public string SendMailAddress
        {
            get { return sendMailAddress; }
            set { sendMailAddress = value;OnPropertyChanged("SendMailAddress"); }
        }

        private string reserveMailAddress;
        public string ReserveMailAddress
        {
            get { return reserveMailAddress; }
            set { reserveMailAddress = value;OnPropertyChanged("ReserveMailAddress"); }
        }

        private double maxSize;
        public double MaxSize
        {
            get { return maxSize; }
            set { maxSize = value;OnPropertyChanged("MaxSize"); }
        }

        private double reserveSize;
        public double ReserveSize
        {
            get { return reserveSize; }
            set { reserveSize = value;OnPropertyChanged("ReserveSize"); }
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
            set { _isSelected = value;OnPropertyChanged("IsSelected"); }
        }
    }
}
