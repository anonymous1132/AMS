using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailQueueReserverEntity
    {
        public MailQueueReserverEntity(string guid)
        {
            _guid = guid;
        }

        public MailQueueReserverEntity()
        {
            _guid = System.Guid.NewGuid().ToString();
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

        public string ReserverAddress
        {
            get;
            set;
        }

        public bool IsLocalServer
        {
            get;
            set;
        }

        public bool IsSuccessful
        {
            get;
            set;
        }
        
        public string OutSideServerAddress
        {
            get;
            set;
        }

        public string MailGuid
        {
            get;
            private set;
        }

        public void SetMailGuid(string mailGuid)
        {
            MailGuid = mailGuid;
        }
    }
}
