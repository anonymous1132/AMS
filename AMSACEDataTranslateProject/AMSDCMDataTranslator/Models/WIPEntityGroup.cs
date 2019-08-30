using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
    public class WIPEntityGroup
    {
        public List<WIPDbEntity> WIPDbEntities { get; set; } = new List<WIPDbEntity>();

        //取消Chamber方案
        //public List<ChamberDBViewEntity> ChamberDBViewEntities { get; set; } = new List<ChamberDBViewEntity>();

        public DateTime StartTime
        {
            get;
            set;
        } = DateTime.Now.AddHours(-1);
        private string startTimeStamp
        {
            get { return StartTime.ToString("yyyy-MM-dd-HH.mm.ss.ffffff"); }
        }

        public DateTime EndTime
        {
            get;
            set;
        } = DateTime.Now;
        public string endTimeStamp
        {
            get { return EndTime.ToString("yyyy-MM-dd-HH.mm.ss.ffffff"); }
        }

        private string Sql_WIP
        {
            get
            {
                string sqltemp = string.Format("select Lot_ID,Fab,Slot,Technology,Product,Lot_Type,RouteVersion,Stage_ID,Route,Step,Sequence,Version_ID,Description,EQP_ID,EquipmentGroup,MoveInTime,MoveOutTime,MoveInWaferCount,MoveOutWaferCount,MoveInOperator,MoveOutOperator," +
                    "Reticle,Recipe,BatchID,QueueTime,Owner,CustomerCode,LotStatus,CarrierID,WafeList from ISTRPT.FVACE_WIP_DATA_V2 where MoveOutTime > '{0}' and MoveOutTime <= '{1}'", startTimeStamp, endTimeStamp);
                return sqltemp;
            }
        }

        //取消Chamber方案
        //private string Sql_Chamber
        //{
        //    get
        //    {
        //        string sqltemp = string.Format("select *  from ISTRPT.Fvace_Chamber_Base where  MoveOutTime > '{0}' and MoveOutTime <= '{1}'", startTimeStamp, endTimeStamp);
        //        return sqltemp;
        //    }
        //}

        public void GetData()
        {
            DB2Helper dB2 = new DB2Helper();
            dB2.GetSomeData(Sql_WIP);
            if (dB2.dt.Rows.Count == 0)
            {
                throw new NoQueryDataException("没有新的WIP数据产生");
            }
            foreach (DataRow dr in dB2.dt.Rows)
            {

                try
                {
                    WIPDbEntity wipDbEntity = new WIPDbEntity
                    {
                        Lot_ID = dr["Lot_ID"].ToString(),
                        // Fab = dr["Fab"].ToString(),
                        Slot = dr["Slot"].ToString(),
                        Technology = dr["Technology"].ToString(),
                        Product = dr["Product"].ToString(),
                        Lot_Type = dr["Lot_Type"].ToString(),
                        RouteVersion = dr["RouteVersion"].ToString(),
                        Stage_ID = dr["Stage_ID"].ToString(),
                        Route = dr["Route"].ToString(),
                        Step = dr["Step"].ToString(),
                        Sequence = dr["Sequence"].ToString(),
                        Version_ID = dr["Version_ID"].ToString(),
                        Description = dr["Description"].ToString(),
                        EQP_ID = dr["Eqp_ID"].ToString(),
                        EquipmentGroup = dr["EquipmentGroup"].ToString(),
                        MoveInTime = (DateTime)dr["MoveInTime"],
                        MoveInWaferCount = ((int?)dr["MoveInWaferCount"]).HasValue ? ((int?)dr["MoveInWaferCount"]).Value : 0,
                        MoveInOperator = dr["MoveInOperator"].ToString(),
                        MoveOutTime = (DateTime)dr["MoveOutTime"],
                        MoveOutWaferCount = dr["MoveOutWaferCount"] == DBNull.Value ? 0 : (int)dr["MoveOutWaferCount"],
                        MoveOutOperator = dr["MoveOutOperator"].ToString(),
                        Reticle = dr["Reticle"] == DBNull.Value ? "" : dr["Reticle"].ToString(),
                        Recipe = dr["Recipe"].ToString(),
                        BatchID = ((int?)dr["BatchID"]) ?? 0,
                        QueueTime = dr["QueueTime"] == DBNull.Value ? 0 : (int)dr["QueueTime"],
                        Owner = dr["Owner"].ToString(),
                        CustomerCode = dr["CustomerCode"].ToString(),
                        LotStatus = dr["LotStatus"].ToString(),
                        CarrierID = dr["CarrierID"].ToString(),
                        WafeList = dr["WafeList"].ToString()
                    };
                    WIPDbEntities.Add(wipDbEntity);
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(string.Format("WIP WIPEntityGroup.cs GetData() Lot:{0},MoveInTime:{1}", dr["Lot_ID"]??"Null", dr["MoveInTime"])??"Null", ex);
                }
            }
            //取消Chamber的方案
            //dB2.GetSomeData(Sql_Chamber);
            //foreach (DataRow dr in dB2.dt.Rows)
            //{
            //    ChamberDBViewEntity chamberDB = new ChamberDBViewEntity()
            //    {
            //        Eqp_ID = dr["Eqp_ID"].ToString(),
            //        Lot_ID = dr["Lot_ID"].ToString(),
            //        MainPD_ID = dr["MainPD_ID"].ToString(),
            //        Stage_ID = dr["Stage_ID"].ToString(),
            //        Ope_No = dr["Ope_NO"].ToString(),
            //        Ope_Pass_Count = (int)dr["Ope_Pass_Count"],
            //        Procrsc_ID = dr["Procrsc_ID"].ToString(),
            //        Wafer_ID = dr["Wafer_ID"].ToString(),
            //        Slot=dr["Slot"].ToString(),
            //        Technology=dr["Technology"].ToString(),
            //        Product=dr["Product"].ToString(),
            //        Lot_type=dr["Lot_Type"].ToString(),
            //        PD_ID=dr["PD_ID"].ToString(),
            //        Version_ID=dr["Version_ID"].ToString(),
            //        Description=dr["Description"].ToString(),
            //        EquipmentGroup=dr["EquipmentGroup"].ToString(),
            //        MoveInTime=(DateTime)dr["MoveInTime"],
            //        MoveOutTime=(DateTime)dr["MoveOutTime"],
            //        MoveInOperator=dr["MoveInOperator"].ToString(),
            //        MoveOutOperator=dr["MoveOutOperator"].ToString(),
            //        Reticle=dr["Reticle"]==DBNull.Value?"": dr["Reticle"].ToString(),
            //        Recipe=dr["Recipe"].ToString(),
            //        Owner=dr["Owner"].ToString(),
            //        Cast_ID=dr["Cast_ID"].ToString()
            //    };
            //    ChamberDBViewEntities.Add(chamberDB);
            //}
            
        }
        //取消Chamber方案
        //public List<Chamber_SingleLine> GetChamberLists()
        //{
        //    if (ChamberDBViewEntities.Count == 0) throw new NoQueryDataException("没有新的Chamber数据产生");
        //    List<Chamber_SingleLine> lines = new List<Chamber_SingleLine>();
        //    foreach (ChamberDBViewEntity entity in ChamberDBViewEntities)
        //    {
        //        Chamber_SingleLine singleLine = new Chamber_SingleLine()
        //        {
        //            Lot=entity.Lot_ID,
        //            Wafer=entity.Wafer_ID.Substring(entity.Wafer_ID.Length - 2, 2),
        //            Scribe=entity.Wafer_ID,
        //            Equipment=entity.Eqp_ID,
        //            Route=entity.MainPD_ID.Split('.')[0],
        //            RouteVersion=entity.MainPD_ID.Split('.').Length == 2 ? entity.MainPD_ID.Split('.')[1] : "",
        //            DateTimeList=entity.MoveOutTime.ToString("yyyy/MM/dd_HH:mm:ss"),
        //            ChamberList =entity.Procrsc_ID,
        //            Step=entity.PD_ID.Split('.')[0],
        //            StepVersion=entity.Version_ID,
        //            RecipeList=entity.Recipe
        //        };
        //        lines.Add(singleLine);
        //    }
        //    return lines;
        //}

        public List<WIP_SingleLine> GetWIPLists()
        {
            List<WIP_SingleLine> lines = new List<WIP_SingleLine>();
            foreach (WIPDbEntity entity in WIPDbEntities)
            {
                WIP_SingleLine singleLine = new WIP_SingleLine()
                {
                    Lot = entity.Lot_ID,
                    Fab="FAB2",
                    SourceLot = entity.Slot,
                    Technology=entity.Technology,
                    Product=entity.Product,
                    LotType=entity.Lot_Type,
                    Route=entity.Route,
                    RouteVersion=entity.RouteVersion,
                    Stage=entity.Stage_ID,
                    Step=entity.Step,
                    StepVersion=entity.Version_ID,
                    StepDesc=entity.Description,
                    Equipment=entity.EQP_ID,
                    EquipmentGroup=entity.EquipmentGroup,
                    MoveInTime=entity.MoveInTime,
                    MoveOutTime=entity.MoveOutTime,
                    MoveInWaferCount=entity.MoveInWaferCount,
                    MoveOutWaferCount=entity.MoveOutWaferCount,
                    MoveInOperator=entity.MoveInOperator,
                    MoveOutOperator=entity.MoveOutOperator,
                    Recipe=entity.Recipe,
                    Reticle=entity.Reticle,
                    Sequence=entity.Sequence,
                    BatchID=entity.BatchID.ToString(),
                    QueueTime=entity.QueueTime==0?"":entity.QueueTime.ToString(),
                    Owner=entity.Owner,
                    CustomerCode=entity.CustomerCode,
                    WaferList=entity.WafeList,
                    LotStatus=entity.LotStatus,
                    CarrierID=entity.CarrierID
                };
                lines.Add(singleLine);
            }
            //取消chamber方案
            //foreach (ChamberDBViewEntity entity in ChamberDBViewEntities)
            //{
            //    WIP_SingleLine singleLine = new WIP_SingleLine()
            //    {
            //        Lot=entity.Lot_ID,
            //        Fab="Fab2",
            //        SourceLot=entity.Slot,
            //        Technology=entity.Technology,
            //        Product = entity.Product,
            //        LotType = entity.Lot_type,
            //        Route = entity.MainPD_ID.Split('.')[0],
            //        RouteVersion = entity.MainPD_ID.Split('.').Length == 2 ? entity.MainPD_ID.Split('.')[1] : "",
            //        Stage = entity.Stage_ID,
            //        Step = entity.PD_ID.Split('.')[0],
            //        StepVersion = entity.Version_ID,
            //        StepDesc = entity.Description,
            //        Equipment = entity.Eqp_ID,
            //        EquipmentGroup = entity.EquipmentGroup,
            //        MoveInTime = entity.MoveInTime,
            //        MoveOutTime = entity.MoveOutTime,
            //        MoveInWaferCount = 1,
            //        MoveOutWaferCount =1,
            //        MoveInOperator = entity.MoveInOperator,
            //        MoveOutOperator = entity.MoveOutOperator,
            //        Recipe = entity.Recipe,
            //        Reticle = entity.Reticle,
            //        Sequence = "",
            //        BatchID = entity.Ope_No.ToString(),
            //        QueueTime = "",
            //        Owner = entity.Owner,
            //        CustomerCode = "",
            //        WaferList = entity.Wafer_ID,
            //        LotStatus = "",
            //        CarrierID = entity.Cast_ID
            //    };
            //    lines.Add(singleLine);
            //}
            return lines;
        }
    }
}
