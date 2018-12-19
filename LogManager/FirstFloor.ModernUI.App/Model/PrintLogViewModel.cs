using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class PrintLogViewModel : FirstFloor.ModernUI.Presentation.NotifyPropertyChanged
    {
        public PrintLogViewModel()
        {
            _guid= System.Guid.NewGuid().ToString();
        }
        public PrintLogViewModel(string guid)
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

        private DateTime _executeTime;
        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime ExecuteTime
        {
            get { return _executeTime; }
            set { _executeTime = value; OnPropertyChanged("ExecuteTime"); }
        }


        private string _userName;
        /// <summary>
        /// 使用者
        /// </summary>
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged("UserName"); }
        }

        private string _ipAddress;
        /// <summary>
        /// 执行IP地址
        /// </summary>
        public string IPAddress
        {
            get { return _ipAddress; }
            set { _ipAddress = value; OnPropertyChanged("IPAddress"); }
        }

        private string _computeName;
        /// <summary>
        /// 计算机名称
        /// </summary>
        public string ComputerName
        {
            get { return _computeName; }
            set { _computeName = value; OnPropertyChanged("ComputerName"); }
        }

        private string _macAddress;
        /// <summary>
        /// 网卡地址
        /// </summary>
        public string MACAddress
        {
            get { return _macAddress; }
            set { _macAddress = value; OnPropertyChanged("MACAddress"); }
        }

        private string _programName;
        /// <summary>
        /// 行程名称
        /// </summary>
        public string ProgramName
        {
            get { return _programName; }
            set { _programName = value; OnPropertyChanged("ProgramName"); }
        }

        private string _printType;
        /// <summary>
        /// 类型
        /// </summary>
        public string PrintType
        {
            get { return _printType; }
            set { _printType = value;OnPropertyChanged("PrintType"); }
        }

        private string _fileName;
        /// <summary>
        /// 档案
        /// </summary>
        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value;OnPropertyChanged("FileName"); }
        }

        private bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value;OnPropertyChanged("IsSelected"); }
        }
    }
}
