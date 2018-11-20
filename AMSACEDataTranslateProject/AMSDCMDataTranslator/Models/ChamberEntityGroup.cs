using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;


namespace AMSDCMDataTranslator.Models
{
    public class ChamberEntityGroup
    {
        public List<ChamberDBEntity> ChamberDBEntities { get; set; } = new List<ChamberDBEntity>();

        private string paraEqp
        {
            get { return string.Join(",", ChamberDBEntities.Select(s =>"'"+ s.Eqp_ID+"'").ToList()); }
        }

        private string paraLot
        {
            get { return string.Join(",", ChamberDBEntities.Select(s => "'" + s.Lot_ID + "'").ToList()); }
        }

        private string paraMainPD
        {
            get { return string.Join(",", ChamberDBEntities.Select(s => "'" + s.MainPD_ID + "'").ToList()); }
        }
        public DateTime StartTime
        {
            get;
            set;
        } = DateTime.Now.AddHours(-1);
        private string satrtTimeStamp
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

        public List<ChamberOphsEntity> ChamberOphsEntities { get; set; } = new List<ChamberOphsEntity>();

        private string Sql_chamber
        {
            get
            {
                string sqltemp =string.Format("select Lot_ID,Wafer_ID,MainPD_ID,Ope_NO,Ope_Pass_Count,EQP_ID,Procrsc_ID,Proc_Time  from MMVIEW.FHWCPHS where Action_Code='ProcessEnd' and Proc_Time > '{0}' and Proc_Time<= '{1}'", satrtTimeStamp, endTimeStamp);
                return sqltemp;
            }
        }

        public string Sql_History
        {
            get
            {
                return string.Format("select Lot_ID,MainPD_ID,Ope_NO,Ope_Pass_Count,EQP_ID,PD_ID,Claim_Time,Recipe_ID from MMVIEW.FHOPEHS where Ope_Category='ProcessEnd' and Lot_ID in ({1}) and MainPD_ID in ({2}) and Eqp_ID in ({3}) and Claim_Time >'{0}'",satrtTimeStamp,paraLot,paraMainPD,paraEqp);
            }
        }

        //为ChamberOphsEntities赋值
        public void GetData()
        {
            DB2Helper dB2 = new DB2Helper();
            dB2.GetSomeData(Sql_chamber);
            if (dB2.dt.Rows.Count == 0)
            {
                throw new NoQueryDataException("没有新的Chamber数据产生");
            }

            foreach (DataRow dr in dB2.dt.Rows)
            {
                ChamberDBEntity chamberDB = new ChamberDBEntity()
                {
                    Eqp_ID = dr["Eqp_ID"].ToString(),
                    Lot_ID = dr["Lot_ID"].ToString(),
                    MainPD_ID = dr["MainPD_ID"].ToString(),
                    Ope_No = dr["Ope_NO"].ToString(),
                    Ope_Pass_Count = (int)dr["Ope_Pass_Count"],
                    Procrsc_ID=dr["Procrsc_ID"].ToString(),
                    Proc_Time=(DateTime)dr["Proc_Time"],
                    Wafer_ID=dr["Wafer_ID"].ToString()
                };
                ChamberDBEntities.Add(chamberDB);
            }

            dB2.GetSomeData(Sql_History);
            foreach (DataRow dr in dB2.dt.Rows)
            {
                ChamberOphsEntity chamberOphs = new ChamberOphsEntity()
                {
                    Claim_Time=(DateTime)dr["Claim_Time"],
                    Eqp_ID=dr["Eqp_ID"].ToString(),
                    Lot_ID=dr["Lot_ID"].ToString(),
                    MainPD_ID=dr["MainPD_ID"].ToString(),
                    Ope_No=dr["Ope_NO"].ToString(),
                    Ope_Pass_Count=(int)dr["Ope_Pass_Count"],
                    Recipe_ID=dr["Recipe_ID"].ToString(),
                    PD_ID=dr["PD_ID"].ToString()
                };
                ChamberOphsEntities.Add(chamberOphs);
            }

        }

        //给外部调用
        public List<Chamber_SingleLine> GetChamberLists()
        {
            List<Chamber_SingleLine> lines = new List<Chamber_SingleLine>();
            foreach (ChamberDBEntity dBEntity in ChamberDBEntities)
            {
                Chamber_SingleLine singleLine = new Chamber_SingleLine();
                singleLine.Lot = dBEntity.Lot_ID;
                singleLine.Wafer = dBEntity.Wafer_ID.Substring(dBEntity.Wafer_ID.Length - 2, 2);
                singleLine.Scribe = dBEntity.Wafer_ID;
                singleLine.Equipment = dBEntity.Eqp_ID;
                singleLine.Route = dBEntity.MainPD_ID.Split('.')[0];
                singleLine.RouteVersion = dBEntity.MainPD_ID.Split('.').Length==2? dBEntity.MainPD_ID.Split('.')[1]:"";
                singleLine.DateTimeList = dBEntity.strProcTime;
                singleLine.ChamberList = dBEntity.Procrsc_ID;
                var item = ChamberOphsEntities.Where(w => w.Eqp_ID == dBEntity.Eqp_ID && w.MainPD_ID == dBEntity.MainPD_ID && w.Ope_No == dBEntity.Ope_No && w.Ope_Pass_Count == w.Ope_Pass_Count).OrderBy(o => o.Claim_Time).First();
                singleLine.Step = item.PD_ID.Split('.')[0];
                singleLine.StepVersion = item.PD_ID.Split('.').Length==2?dBEntity.MainPD_ID.Split('.')[1]:"";
                singleLine.RecipeList = item.Recipe_ID;
                lines.Add(singleLine);
            }
            return lines;
        }


    }
}
