using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class WIPDbEntity
    {
        public string Lot_ID { get; set; }

        public string Fab { get; set; }

        public string Slot { get; set; }

        public string Technology { get; set; }
        
        public string Product { get; set; }

        public string Lot_Type { get; set; }

        public string RouteVersion { get; set; }

        public string Stage_ID { get; set; }

        public string Route { get; set; }

        public string Step { get; set; }

        public string Sequence { get; set; }

        public string Version_ID { get; set; }

        public string Description { get; set; }

        public string EQP_ID { get; set; }

        public string EquipmentGroup { get; set; }

        public DateTime MoveInTime { get; set; }

        public int MoveInWaferCount { get; set; }
        
        public string MoveInOperator { get; set; }

        public DateTime MoveOutTime { get; set; }

        public int MoveOutWaferCount { get; set; }

        public string MoveOutOperator { get; set; }

        public string Reticle { get; set; }

        public string Recipe { get; set; }

        public int BatchID { get; set; }
        
        public int QueueTime { get; set; }

        public string Owner { get; set; }

        public string CustomerCode { get; set; }

        public string LotStatus { get; set; }

        public string CarrierID { get; set; }

        public string WafeList { get; set; }
    }
}
