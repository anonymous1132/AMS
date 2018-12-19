using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstFloor.ModernUI.App.Model
{
    public class MailQueueViewModel
    {
        public MailQueueModel QueueModel
        {
            get;
            set;
        }

        public List<MailQueueReserverEntity> ReserverEntities
        {
            get;
            set;
        } = new List<MailQueueReserverEntity>();

        public string Guid
        {
            get { return QueueModel.Guid; }
        }

        public string MailID
        {
            get { return QueueModel.MailID; }
        }

        public DateTime StartTime
        {
            get { return QueueModel.StartTime; }
        }

        public DateTime EndTime
        {
            get { return QueueModel.EndTime; }
        }

        public int Mask
        {
            get { return QueueModel.Mask; }
        }

        public double SendSize
        {
            get { return QueueModel.SendSize; }
        }

        public string SendMailAddress
        {
            get { return QueueModel.SendMailAddress; }
        }

        public string Details
        {
            get { return QueueModel.Details; }
        }

        public string ReserverAddress
        {
            get { return string.Join(";",ReserverEntities.Select(p => p.ReserverAddress).ToArray()); }
        }

        public bool IsSelected
        {
            get;
            set;
        } = false;

    }
}
