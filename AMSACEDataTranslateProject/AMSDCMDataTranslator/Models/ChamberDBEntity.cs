using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class ChamberDBEntity
    {
        public DateTime Proc_Time { get; set; }
        
        public string Lot_ID { get; set; }

        public string Wafer_ID { get; set; }

        public string MainPD_ID { get; set; }

        public string Ope_No { get; set; }

        public int Ope_Pass_Count { get; set; }

        public string Eqp_ID { get; set; }

        public string Procrsc_ID { get; set; }

        public string strProcTime { get { return Proc_Time.ToString("yyyy/MM/dd_HH:mm:ss"); } }



    }
}
