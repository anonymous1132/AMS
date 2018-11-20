using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class WIP_SingleLine
    {
        public string Lot { get; set; }

        public string Fab { get; set; }

        public string SourceLot { get; set; }

        public string Technology { get; set; }

        public string Product { get; set; }

        public string LotType { get; set; }

        public string Route { get; set; }

        public string RouteVersion { get; set; }

        public string Stage { get; set; }

        public string Step { get; set; }

        public string StepVersion { get; set; }

        public string StepDesc { get; set; }

        public string FormatStepDesc { get { return StepDesc.Replace('\n',' '); } }

        public string Equipment { get; set; }

        public string EquipmentGroup { get; set; }

        public DateTime MoveInTime { get; set; }

        public string strMoveInTime { get { return MoveInTime.ToString("yyyy/MM/dd_HH:mm:ss"); } }

        public DateTime MoveOutTime { get; set; }

        public string strMoveOutTime { get { return MoveOutTime.ToString("yyyy/MM/dd_HH:mm:ss"); } }

        public string AutoEndTime { get; set; } = "";

        public int MoveInWaferCount { get; set; }

        public string strMoveInWaferCount { get { return MoveInWaferCount.ToString(); } }

        public int MoveOutWaferCount { get; set; }

        public string strMoveOutWaferCount { get { return MoveOutWaferCount == 0 ? "" : MoveOutWaferCount.ToString(); } }

        public string MoveInOperator { get; set; }

        public string MoveOutOperator { get; set; }

        public string Recipe { get; set; }

        public string Reticle { get; set; }

        public string Sequence { get; set; }

        public string BatchID { get; set; }

        public string QueueTime { get; set; }

        public string Owner { get; set; }

        public string CustomerCode { get; set; }

        public string WaferList { get; set; }

        public string FormatWaferList { get { return WaferList.Trim(','); } }

        public string LotStatus { get; set; }

        public string CarrierID { get; set; }

        public string String_Option1 { get; set; } = "";

        public string String_Option2 { get; set; } = "";

        public string String_Option3 { get; set; } = "";

        public string String_Option4 { get; set; } = "";

        public string String_Option5 { get; set; } = "";

        public string String_Option6 { get; set; } = "";

        public string String_Option7 { get; set; } = "";

        public string String_Option8 { get; set; } = "";

        public string String_Option9 { get; set; } = "";

        public string String_Option10 { get; set; } = "";

        public string Number_Option1 { get; set; } = "";

        public string Number_Option2 { get; set; } = "";

        public string Number_Option3 { get; set; } = "";

        public string Number_Option4 { get; set; } = "";

        public string Number_Option5 { get; set; } = "";


        public string GetSingleSiffLine()
        {
            List<string> list = new List<string>()
            {
                Lot,Fab,SourceLot,Technology,Product,LotType,Route,RouteVersion,Stage,Step,StepVersion,FormatStepDesc,Equipment,EquipmentGroup,strMoveInTime,
                strMoveOutTime,AutoEndTime,strMoveInWaferCount,strMoveOutWaferCount,MoveInOperator,MoveOutOperator,Recipe,Reticle,Sequence,BatchID,QueueTime,
                Owner,CustomerCode,FormatWaferList,LotStatus,CarrierID,String_Option1,String_Option2,String_Option3,String_Option4,String_Option5,String_Option6,
                String_Option7,String_Option8,String_Option9,String_Option10,Number_Option1,Number_Option2,Number_Option3,Number_Option4,Number_Option5
            };
            return string.Format("\"{0}\"", string.Join("\",\"", list));
        }
    }
}
