using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
     public class ChamberOphsEntity
     {
        public string Lot_ID { get; set; }

        public string MainPD_ID { get; set; }

        public string PD_ID { get; set; }

        public string Ope_No { get; set; }

        public int Ope_Pass_Count { get; set; }

        public DateTime Claim_Time { get; set; }

        public string Eqp_ID { get; set; }

        public string Recipe_ID { get; set; }
     }
}
