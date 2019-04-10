using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace LogParser
{
    public class QueueLogEntity
    {
        public ObjectId _id { get; set; }

        public string MailID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string SendAddress { get; set; }

        public int Mask { get; set; }

        public double ReserveSize { get; set; }

        public string Details { get; set; }

        public List<QueueReserveEntity> Reservers { get; set; }
    }
}
