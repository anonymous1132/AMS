using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    /// <summary>
    /// FVACE_Chamber_Base
    /// </summary>
    public class ChamberDBViewEntity
    {
        public string Lot_ID { get; set; }

        public string Slot { get; set; }

        public string Wafer_ID { get; set; }

        public string Procrsc_ID { get; set; }

        public string Technology { get; set; }

        public string Product { get; set; }

        public string Lot_type { get; set; }

        public string MainPD_ID { get; set; }

        public string Stage_ID { get; set; }

        public string PD_ID { get; set; }

        public string Ope_No { get; set; }

        public string Version_ID { get; set; }

        public string Description { get; set; }

        public string Eqp_ID { get; set; }

        public string EquipmentGroup { get; set; }

        public DateTime MoveInTime { get; set; }
        
        public DateTime MoveOutTime { get; set; }

        public string MoveInOperator { get; set; }

        public string MoveOutOperator { get; set; }

        public string Reticle { get; set; }

        public string Recipe { get; set; }

        public int Ope_Pass_Count { get; set; }

        public string Owner { get; set; }

        public string Cast_ID { get; set; }

    }
}
