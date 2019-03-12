using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;
using System.Data;
using AMSDCMDataTranslator;

namespace AMSDCMDataTranslator.Models
{
    /// <summary>
    /// 使用方法：先设置StartTime，EndTime。然后调用GetData（）抓取数据库数据，最后调用GetInlineList（）获取Inline集合对象。
    /// </summary>
    public class InlineEntityGroup
    {
        public List<InlineDBEntity> inlineDBEntities
        {
            get;
            set;
        } = new List<InlineDBEntity>();

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

        private List<InlineDCMEntity> DCMEntities
        {
            get;
            set;
        } = new List<InlineDCMEntity>();

        private List<PDModel> PDModels
        {
            get;
            set;
        } = new List<PDModel>();

        public List<InlineDBEntity> LotLevelEntities
        {
            get { return inlineDBEntities.Where(a => a.CollectedType == InlineDBEntity.CollectedTypes.L).ToList(); }
        }

        public List<InlineDBEntity> WaferLevelEntities
        {
            get
            {
                var list = inlineDBEntities.Where(a => a.CollectedType == InlineDBEntity.CollectedTypes.W).OrderBy(p => int.Parse(p.WaferSeq)).ToList();
                return list;
            }
        }

        public List<InlineDBEntity> SiteLevelEntities
        {
            get { return inlineDBEntities.Where(a => a.CollectedType == InlineDBEntity.CollectedTypes.S).OrderBy(p => int.Parse(p.WaferSeq)).ThenBy(p => int.Parse(p.SitePosition)).ToList(); }
        }

        private string sql
        {
            get
            {
                string sqltemp = string.Format("select distinct claim_time,lot,sourcelot,Technology,Product,lotype,owner,MEASROUTE,  MEASROUTEVER," +
                    "MEASSTEP,MEASITEM,MEASTIME,MEASOPERATOR,MEASEQUIPMENT,MEASRECIPE,PROCROUTE,PROCROUTEVER,PROCSTEP, PROCTIME," +
                    "PROCOPERATORUSER,PROCEQUIPMENT,PROCRECIPE,PROCRETICLE,MEAS_TYPE,WAFER_SEQ,WAFER_POSITION, SITE_POSITION," +
                    "DCITEM_VALUE,TARGET, SPECLOW,SPECHIGH,CTRLLOW,CTRLHIGH,PROCSTEPDESC, MEAS_DCDEF_ID,ITEM_TYPE from ISTRPT.FVACE_INLINE_DC where claim_time  >'{0}' and claim_time <= '{1}' order by claim_time,lot,wafer_seq,site_position", satrtTimeStamp, endTimeStamp);

                return sqltemp;
            }
        }

        public void GetData()
        {
            DB2Helper dB2 = new DB2Helper();
            //获取PD信息，为Inline的Step赋值
            dB2.GetSomeData("select distinct mainpd_id,pd_id,ope_no from istrpt.fvace_wip_pdhis ");
            foreach (DataRow dr in dB2.dt.Rows)
            {
                try
                {
                    PDModel model = new PDModel();
                    model.MainPD_ID = dr["MainPD_ID"].ToString();
                    model.PD_ID = dr["PD_ID"].ToString();
                    model.OPE_NO = dr["OPE_NO"].ToString();
                    PDModels.Add(model);
                }
                catch (Exception)
                {
                }
            }

            dB2.GetSomeData(sql);
            double d = 0;
            if (dB2.dt.Rows.Count == 0)
            {
                throw new NoQueryDataException("没有新的Inline数据");
            }
            //DB2中获取的DateTable转换为类
            foreach (DataRow dr in dB2.dt.Rows)
            {
                try
                {
                    InlineDBEntity entity = new InlineDBEntity();

                    if (double.TryParse(dr["DCITEM_VALUE"].ToString(), out d))
                    {
                        entity.DCItemValue = d;
                    }
                    else
                    {
                        continue;
                    }

                    entity.ClaimTime = (DateTime)dr["Claim_Time"];
                    entity.Lot = dr["Lot"].ToString();
                    entity.SourceLot = dr["SourceLot"].ToString();
                    entity.Technology = dr["Technology"].ToString();
                    entity.Product = dr["Product"].ToString();
                    entity.LotType = dr["Lotype"].ToString();
                    entity.Owner = dr["Owner"].ToString();
                    entity.MeasRoute = dr["MeasRoute"].ToString();
                    entity.MeasRouteVer = dr["MeasRouteVer"].ToString();
                    entity.MeasItem = dr["MeasItem"].ToString();
                    entity.MeasTime = (DateTime)dr["MeasTime"];
                    entity.MeasOperator = dr["MeasOperator"].ToString();
                    entity.MeasEquipment = dr["MeasEquipment"].ToString();
                    entity.MeasRecipe = dr["MeasRecipe"].ToString();
                    entity.ProcRoute = dr["ProcRoute"].ToString();
                    entity.ProcRouteVer = dr["ProcRouteVer"].ToString();
                    //entity.ProcStep = dr["ProcStep"].ToString();
                    var temp = PDModels.Where(w => w.Route == entity.MeasRoute && w.OPE_NO == dr["MeasStep"].ToString()).FirstOrDefault();
                    entity.MeasStep = temp == null ? dr["MeasStep"].ToString() : temp.Step;
                    temp = PDModels.Where(w => w.Route == entity.ProcRoute && w.OPE_NO == dr["ProcStep"].ToString()).FirstOrDefault();
                    entity.ProcStep = temp == null ? dr["ProcStep"].ToString() : temp.Step;
                    if (dr["ProcTime"] == DBNull.Value)
                    {
                        entity.ProcTime = null;
                    }
                    else
                    {
                        entity.ProcTime = (DateTime)dr["ProcTime"];
                    }

                    entity.ProcOperatorUser = dr["PROCOPERATORUSER"].ToString();
                    entity.ProcEquipment = dr["PROCEQUIPMENT"].ToString();
                    entity.ProcRecipe = dr["ProcRecipe"].ToString();
                    entity.ProcReticle = dr["ProcReticle"].ToString();
                    entity.MeasType = dr["Meas_Type"].ToString();
                    entity.WaferSeq = dr["Wafer_Seq"].ToString().Trim();
                    entity.WaferPosition = dr["WAFER_POSITION"].ToString();
                    entity.SitePosition = dr["Site_Position"].ToString();
                    entity.Target = dr["Target"].ToString();
                    entity.SpecLow = dr["SpecLow"].ToString();
                    entity.SpecHigh = dr["SpecHigh"].ToString();
                    entity.CtrlLow = dr["CtrlLow"].ToString();
                    entity.CtrlHigh = dr["CtrlHigh"].ToString();
                    entity.ProcStepDesc = dr["PROCSTEPDESC"].ToString().Replace("\n", "");
                    entity.MeasDcdefID = dr["Meas_Dcdef_ID"].ToString();
                    entity.ItemType = dr["Item_Type"].ToString();
                    inlineDBEntities.Add(entity);
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetData() ClaimTime:{0},Lot:{1}。", dr["Claim_Time"].ToString(), dr["Lot"].ToString()), e);
                }
            }
            //对wafer_seq为空或者为*号的数据去取相似案例的数据
            foreach (InlineDBEntity entity in inlineDBEntities)
            {
                try
                {
                    if (string.IsNullOrEmpty(entity.WaferSeq))
                    {
                        InlineDBEntity likelyEntity = GetLikelyEntity(entity);
                        entity.WaferSeq = likelyEntity.WaferSeq;
                        entity.SourceLot = likelyEntity.SourceLot;
                        entity.Technology = likelyEntity.Technology;
                        entity.Product = likelyEntity.Product;
                        entity.LotType = likelyEntity.LotType;
                        entity.Owner = likelyEntity.Owner;
                        entity.MeasOperator = likelyEntity.MeasOperator;
                        entity.MeasRecipe = likelyEntity.MeasRecipe;
                        entity.ProcRoute = likelyEntity.ProcRoute;
                        entity.ProcRouteVer = likelyEntity.ProcRouteVer;
                        entity.ProcStep = likelyEntity.ProcStep;
                        entity.ProcTime = likelyEntity.ProcTime;
                        entity.ProcOperatorUser = likelyEntity.ProcOperatorUser;
                        entity.ProcEquipment = likelyEntity.ProcEquipment;
                        entity.ProcRecipe = likelyEntity.ProcRecipe;
                    }
                
                    else if (string.IsNullOrEmpty(entity.Product) && entity.ItemType == "Derived")
                    {
                        InlineDBEntity likelyEntity = GetLikelyEntity(entity);
                        entity.SourceLot = likelyEntity.SourceLot;
                        entity.Technology = likelyEntity.Technology;
                        entity.Product = likelyEntity.Product;
                        entity.LotType = likelyEntity.LotType;
                        entity.Owner = likelyEntity.Owner;
                        entity.MeasOperator = likelyEntity.MeasOperator;
                        entity.MeasRecipe = likelyEntity.MeasRecipe;
                        entity.ProcRoute = likelyEntity.ProcRoute;
                        entity.ProcRouteVer = likelyEntity.ProcRouteVer;
                        entity.ProcStep = likelyEntity.ProcStep;
                        entity.ProcTime = likelyEntity.ProcTime;
                        entity.ProcOperatorUser = likelyEntity.ProcOperatorUser;
                        entity.ProcEquipment = likelyEntity.ProcEquipment;
                        entity.ProcRecipe = likelyEntity.ProcRecipe;
                    }

                    else if (entity.WaferSeq == "*")
                    {
                        InlineDBEntity likelyEntity = GetLikelyEntity(entity);
                        entity.WaferSeq = likelyEntity.WaferSeq;
                    }

                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetData()从DB2中获取相似InlineEntity失败 ClaimTime:{0},LotID:{1}", entity.ClaimTime.ToString(), entity.Lot), e);
                }
            }
            //获取DCM坐标信息
            string dcmSql = string.Format("select lotid,eqpid,measuredatacount,recipe,coordinate,newdate from istrpt.fvace_inline_dcm where newdate between '{0}' and '{1}'", StartTime.AddMinutes(-2).ToString("yyyy-MM-dd-HH.mm.ss.ffffff"), endTimeStamp);
            dB2.GetSomeData(dcmSql);
            foreach (DataRow dr in dB2.dt.Rows)
            {
                try
                {
                    InlineDCMEntity dcm = new InlineDCMEntity();
                    dcm.LotID = dr["LotID"].ToString();
                    dcm.EQPID = dr["EQPID"].ToString();
                    dcm.MeasureDataCount = Convert.ToInt16(dr["MeasureDataCount"].ToString());
                    dcm.Recipe = dr["Recipe"].ToString();
                    dcm.Coordinate = dr["Coordinate"].ToString();
                    dcm.NewDate = (DateTime)dr["NewDate"];
                    DCMEntities.Add(dcm);
                }
                catch (Exception ex)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetData()从DCM中获取坐标错误 NewDate:{0},LotID:{1}", dr["NewDate"].ToString(), dr["LotID"].ToString()), ex);
                }
            }
        }

        private Inline_SigleLine GetInlineSigleLineByEntityList(List<InlineDBEntity> entities)
        {
            InlineDBEntity entity = entities.FirstOrDefault();
            Inline_SigleLine line = new Inline_SigleLine();
            //基础数据赋值
            line.Lot = entity.Lot;
            line.SourceLot = entity.SourceLot;
            line.Technology = entity.Technology;
            line.Product = entity.Product;
            line.LotType = entity.LotType;
            line.Owner = entity.Owner;
            line.MeasRoute = entity.MeasRoute;
            line.MeasRouteVer = entity.MeasRouteVer;
            line.MeasStep = entity.MeasStep;
            line.MeasItem = entity.MeasItem;
            line.MeasTime = entity.MeasTime.ToString("yyyy/MM/dd_HH:mm:ss");
            line.MeasOperator = entity.MeasOperator;
            line.MeasEquipment = entity.MeasEquipment;
            line.MeasRecipe = entity.MeasRecipe;
            line.ProcRoute = entity.MeasRoute;
            line.ProcRouteVer = entity.MeasRouteVer;
            line.ProcStep = entity.ProcStep;
            line.ProcTime = entity.StrProcTime;
            line.ProcOperator = entity.ProcOperatorUser;
            line.ProcEquipment = entity.ProcEquipment;
            line.ProcRecipe = entity.ProcRecipe;
            line.ProcReticle = entity.ProcReticle;
            line.CollectedType = entity.CollectedType.ToString();
            line.Target = entity.Target;
            line.SpecHigh = entity.SpecHigh;
            line.SpecLow = entity.SpecLow;
            line.CtrlLow = entity.CtrlLow;
            line.CtrlHigh = entity.CtrlHigh;
            line.ProcStepDesc = entity.ProcStepDesc;
            line.SetClaimTime(entity.ClaimTime);
            line.strMeasureDataCount = entities.Count.ToString();
            //分组数据赋值
            List<string> datalist = new List<string>();
            StringBuilder waferarraysb = new StringBuilder();
            foreach (InlineDBEntity dBEntity in entities)
            {
                datalist.Add(dBEntity.DCItemValue.ToString());
                waferarraysb.Append(dBEntity.WaferSiteArrayElement);
            }
            line.MeasureDataArray = string.Join(";", datalist);
            line.WaferSiteArray = waferarraysb.ToString();
            line.SiteCoordArray = DCMEntities.Where(p => (p.NewDate - line.ClaimTime).Duration() < TimeSpan.FromMinutes(2) && p.LotID == line.Lot && p.EQPID == line.MeasEquipment && p.MeasureDataCount == line.MeasureDataCount && line.MeasRecipe.Contains(p.Recipe)).Select(a => a.Coordinate).FirstOrDefault();
            return line;
        }

        public List<Inline_SigleLine> GetInlineList()
        {
            List<Inline_SigleLine> lines = new List<Inline_SigleLine>();
            foreach (InlineDBEntity entity in LotLevelEntities)
            {
                try
                {
                    lines.Add(GetInlineSigleLineByEntityList(new List<InlineDBEntity> { entity }));
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetInlineList() LotLevel Claim_Time:{0},Lot:{1}。", entity.ClaimTime.ToString(), entity.Lot), e);
                }
            }
            //此处group可能逻辑存在问题
            var waferGroup = WaferLevelEntities.GroupBy(a => new { a.ClaimTime, a.Lot, a.MeasItem, a.ProcRoute,a.ProcEquipment}).Select(g => new { Body = g.Key });
            var siteGroup = SiteLevelEntities.GroupBy(a => new { a.ClaimTime, a.Lot, a.MeasItem, a.ProcRoute,a.ProcEquipment }).Select(g => new { Body = g.Key });

            foreach (var wafer in waferGroup)
            {
                try
                {
                    var list = WaferLevelEntities.Where(p => p.ClaimTime == wafer.Body.ClaimTime && p.Lot == wafer.Body.Lot && p.MeasItem == wafer.Body.MeasItem && p.ProcRoute == wafer.Body.ProcRoute&&p.ProcEquipment==wafer.Body.ProcEquipment).ToList();
                    lines.Add(GetInlineSigleLineByEntityList(list));
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetInlineList() WaferLevel Claim_Time:{0},Lot:{1}。", wafer.Body.ClaimTime.ToString(), wafer.Body.Lot), e);
                }
            }

            foreach (var site in siteGroup)
            {
                try
                {
                    var list = SiteLevelEntities.Where(p => p.ClaimTime == site.Body.ClaimTime && p.Lot == site.Body.Lot && p.MeasItem == site.Body.MeasItem && p.ProcRoute == site.Body.ProcRoute&&p.ProcEquipment==site.Body.ProcEquipment).ToList();
                    lines.Add(GetInlineSigleLineByEntityList(list));
                }
                catch (Exception e)
                {
                    LogHelper.ErrorLog(string.Format("InlineError InlineEntityGroup.GetInlineList() SiteLevel Claim_Time:{0},Lot:{1}。", site.Body.ClaimTime.ToString(), site.Body.Lot), e);
                }
            }
            return lines;
        }

        private InlineDBEntity GetLikelyEntity(InlineDBEntity entity)
        {
            var list = inlineDBEntities.Where(a => a.Lot == entity.Lot && a.WaferPosition == entity.WaferPosition  && a.WaferSeq != "*" &&a.ItemType!= "Derived").OrderBy(p => (p.ClaimTime - entity.ClaimTime).Duration());
            InlineDBEntity likelyEntity = list.FirstOrDefault();
            //如果存在不一致的情况要记录
            var testmax = list.Max(p => p.WaferSeq);
            var testmin = list.Min(p => p.WaferSeq);
            if (testmax != testmin)
            {
                LogHelper.InlineInfoLog(string.Format("testmax:{0},testmin:{1},lot:{2},claimtime:{3}", testmax, testmin, entity.Lot, entity.ClaimTime.ToString()));
            }
            //如果没找到，就到数据库中去找
            if (likelyEntity is null)
            {
                string sqlt = string.Format("(select claim_time, wafer_seq,sourcelot,techbology, product,lotype,owner,measoperator,measequipment,measrecipe,procroute,procroutever,procstep,proctime,procoperatoruser,procequipment,procrecipe from istrpt.fvace_inline_dc where claim_time >= '{0}' and wafer_seq not in ('', '*') order by claim_time FETCH FIRST 1 ROWS ONLY) " +
                    "union (select claim_time, wafer_seq,sourcelot,techbology, product,lotype,owner,measoperator,measequipment,measrecipe,procroute,procroutever,procstep,proctime,procoperatoruser,procequipment,procrecipe from istrpt.fvace_inline_dc where claim_time <= '{0}' and wafer_seq not in ('', '*')  order by claim_time desc FETCH FIRST 1 ROWS ONLY)", entity.ClaimTime.ToString("yyyy-MM-dd-HH.mm.ss.ffffff"));
                DB2Helper dB2Helper = new DB2Helper();
                dB2Helper.GetSomeData(sqlt);
                likelyEntity = new InlineDBEntity();
                DataRow dr = dB2Helper.dt.NewRow();
                if (dB2Helper.dt.Rows.Count == 2 && dB2Helper.dt.Rows[0][1].ToString() != dB2Helper.dt.Rows[1][1].ToString())
                {
                    dr = (DateTime)dB2Helper.dt.Rows[0][0] - entity.ClaimTime < entity.ClaimTime - (DateTime)dB2Helper.dt.Rows[1][0] ? dB2Helper.dt.Rows[0] : dB2Helper.dt.Rows[1];
                }
                else if (dB2Helper.dt.Rows.Count > 0)
                {
                    dr = dB2Helper.dt.Rows[0];
                }
                likelyEntity.ClaimTime = (DateTime)dr[0];
                likelyEntity.WaferSeq = dr[1].ToString();
                likelyEntity.SourceLot = dr[2].ToString();
                likelyEntity.Technology = dr[3].ToString();
                likelyEntity.Product = dr[4].ToString();
                likelyEntity.LotType = dr[5].ToString();
                likelyEntity.Owner = dr[6].ToString();
                likelyEntity.MeasOperator = dr[7].ToString();
                likelyEntity.MeasEquipment = dr[8].ToString();
                likelyEntity.MeasRecipe = dr[9].ToString();
                likelyEntity.ProcRoute = dr[10].ToString();
                likelyEntity.ProcRouteVer = dr[11].ToString();
                likelyEntity.ProcStep = dr[12].ToString();
                if (dr[13] is null)
                {
                    likelyEntity.ProcTime = null;
                }
                else
                { likelyEntity.ProcTime = (DateTime)dr[13]; }
                likelyEntity.ProcOperatorUser = dr[14].ToString();
                likelyEntity.ProcRecipe = dr[15].ToString();
            }

            return likelyEntity;
        }

    }
}
