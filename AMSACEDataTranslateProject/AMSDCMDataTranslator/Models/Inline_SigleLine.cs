using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
    public class Inline_SigleLine
    {
        public Inline_SigleLine()
        { }
        public DateTime ClaimTime
        {
            get;
            private set;
        }
        public void SetClaimTime(DateTime date)
        {
            ClaimTime = date;
        }

        public string Lot
        {
            get;
            set;
        }

        public string SourceLot
        {
            get;
            set;
        }

        public string Fab
        {
            get;
            set;
        } = "FAB2";

        public string Technology
        {
            get;
            set;
        }

        public string Product
        {
            get;
            set;
        }

        public string LotType
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }

        public string MeasRoute
        {
            get;
            set;
        }

        public string MeasRouteVer
        {
            get;
            set;
        }

        public string MeasStep
        {
            get;
            set;
        }

        public string MeasStepVer
        {
            get;
            set;
        }

        public string MeasItem
        {
            get;
            set;
        }
        
        public string MeasTime
        {
            get;
            set;
        }

        public string MeasOperator
        {
            get;
            set;
        }

        public string MeasEquipment
        {
            get;
            set;
        }

        public string MeasRecipe
        {
            get;
            set;
        }

        public string ProcRoute
        {
            get;
            set;
        }

        public string ProcRouteVer
        {
            get;
            set;
        }

        public string ProcStep
        {
            get;
            set;
        }

        public string ProcStepVer
        {
            get;
            set;
        }

        public string ProcTime
        {
            get;
            set;
        }

        public string ProcOperator
        {
            get;
            set;
        }

        public string ProcEquipment
        {
            get;
            set;
        }

        public string ProcRecipe
        {
            get;
            set;
        }

        public string ProcReticle
        {
            get;
            set;
        }

        public string LimitsInside
        {
            get;
            set;
        } = "Y";

        public string CollectedType
        {
            get;
            set;
        }

        public string strMeasureDataCount
        {
            get;
            set;
        }

        public int MeasureDataCount
        {
            get
            {
                int temp = 0;
                try
                {
                    temp = Convert.ToInt32(strMeasureDataCount);
                }
                catch (Exception)
                { }
                return temp;
            }
        }

        public string WaferSiteArray
        {
            get;
            set;
        }

        public string MeasureDataArray
        {
            get;
            set;
        }

        public string Target
        {
            get;
            set;
        }

        public string ValidLow
        {
            get;
            set;
        }

        public string ValidHigh
        {
            get;
            set;
        }

        public string SpecLow
        {
            get;
            set;
        }

        public string SpecHigh
        {
            get;
            set;
        }

        public string CtrlLow
        {
            get;
            set;
        }

        public string CtrlHigh
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        public string ProcStepDesc
        {
            get;
            set;
        }

        public string BatchID
        {
            get;
            set;
        }

        public string DuplicateFlag
        {
            get;
            set;
        } = "N";

        public string TextVal
        {
            get;
            set;
        }

        public string SiteCoordArray
        {
            get;
            set;
        }

        public string WaferPos
        {
            get;
            set;
        }

        public string String_Option1
        {
            get;
            set;
        }

        public string String_Option2
        {
            get;
            set;
        }

        public string String_Option3
        {
            get;
            set;
        }

        public string String_Option4
        {
            get;
            set;
        }

        public string String_Option5
        {
            get;
            set;
        }

        //public void GetData(DataRow dr)
        //{
        //    Lot = dr[3].ToString();
        //    SourceLot = dr[4].ToString();
        //    Technology = dr[6].ToString();
        //    Product = dr[7].ToString();
        //    LotType = dr[8].ToString();
        //    Owner = dr[9].ToString();
        //    MeasRoute = dr[10].ToString();
        //    MeasRouteVer = dr[11].ToString();
        //    MeasStep = dr[12].ToString();
        //    MeasStepVer = dr[13].ToString();
        //    MeasItem = dr[14].ToString();
        //    MeasTime = dr[15].ToString();
        //    MeasOperator = dr[16].ToString();
        //    MeasEquipment = dr[17].ToString();
        //    MeasRecipe = dr[18].ToString();
        //    ProcRoute = dr[19].ToString();
        //    ProcRouteVer = dr[20].ToString();
        //    ProcStep = dr[21].ToString();
        //    ProcStepVer = dr[22].ToString();
        //    ProcTime = dr[23].ToString();
        //    ProcOperator = dr[24].ToString();
        //    ProcEquipment = dr[25].ToString();
        //    ProcRecipe = dr[26].ToString();
        //    ProcReticle = dr[27].ToString();
        //    CollectedType = dr[29].ToString();
        //    strMeasureDataCount = dr[30].ToString();
        //    WaferSiteArray = dr[31].ToString();
        //    MeasureDataArray = dr[32].ToString();
        //    Target = dr[33].ToString();
        //    ValidLow = "";
        //    ValidHigh = "";
        //    SpecLow = dr[36].ToString();
        //    SpecHigh = dr[37].ToString();
        //    CtrlLow = dr[38].ToString();
        //    CtrlHigh = dr[39].ToString();
        //    Unit = dr[40].ToString();
        //    ProcStepDesc = dr[41].ToString().Replace("\n"," ");
        //    BatchID = dr[42].ToString();
        //    TextVal = dr[44].ToString();
        //    SiteCoordArray = dr[52]==DBNull.Value?"":dr[52].ToString();
        //    WaferPos = dr[46].ToString();
        //    String_Option1 = dr[47].ToString();
        //    String_Option2 = dr[48].ToString();
        //    String_Option3 = dr[49].ToString();
        //    String_Option4 = dr[50].ToString();
        //    String_Option5 = dr[51].ToString();
        //}

        /// <summary>
        /// 返回行结果，当MeasureDataCount>50时，以50位为一组分组(已经取消)。
        /// </summary>
        /// <returns></returns>
        //public List<string> OldGetSiffLines()
        //{
        //    List<string> list = new List<string>();
        //    if (MeasureDataCount > 50)
        //    {
        //        List<string> siteArray = WaferSiteListSplit(WaferSiteArray);
        //        List<string> dataArray = ItemSplit(MeasureDataArray);
        //        List<string>coorArray= ItemSplit(SiteCoordArray);
        //        for (int i = 0; i < siteArray.Count; i++)
        //        {
        //            string site = siteArray[i];
        //            string data = dataArray[i];
        //            string coord = SiteCoordArray == "" ? "" : coorArray[i];
        //            int count = (i == siteArray.Count - 1 )? MeasureDataCount%50:50;
        //            list.Add(GetAppendString(count,site,data,coord));
        //        }
        //    }
        //    else
        //    {
        //        list.Add( GetAppendString(MeasureDataCount, WaferSiteArray, MeasureDataArray, SiteCoordArray));
        //    }
        //    return list;
        //}

        ////返回行结果
        //public List<string> GetSiffLines()
        //{
        //    List<string> list = new List<string>();
        //    list.Add(GetAppendString(MeasureDataCount, WaferSiteArray, MeasureDataArray, SiteCoordArray));
        //    return list;
        //}

        public string GetSingleSiffLine()
        {
            return GetAppendString(MeasureDataCount, WaferSiteArray, MeasureDataArray, SiteCoordArray);
        }

        //已作废
        //private List<string> ItemSplit(string Item)
        //{

        //    List<string> resault_list = new List<string>();
        //    string[] arrlist = Item.Split(';');
        //    int L = arrlist.Length;
        //    int c = 0;
        //    while (L > 50)
        //    {
        //        List<string> templist = new List<string>();
        //        for (int i = c; i < c+50; i++)
        //        {
        //            templist.Add(arrlist[i]);
        //        }
        //        string temp = string.Join(";",templist);
        //        c = c + 50;
        //        L = L - 50;
        //        resault_list.Add(temp);
        //    }
        //    List<string> templist2 = new List<string>();
        //    for (int i = c; i < c+L; i++)
        //    {
        //        templist2.Add(arrlist[i]);
        //    }
        //    string temp2= string.Join(";", templist2);
        //    resault_list.Add(temp2);
        //    return resault_list;
        //}

        //已作废
        //private List<string> WaferSiteListSplit(string Item)
        //{
        //    List<string> resault_list = new List<string>();
        //    List<string> arrylist = new List<string>();
        //    for (int i = 0; i < Item.Length; i++)
        //    {
        //        arrylist.Add(Item[i].ToString());
        //    }

        //    List<string> arrylist2 = new List<string>();
        //    string temp="";
        //    for (int i = 0; i < arrylist.Count; i++)
        //    {
        //        if (i % 4 == 0)
        //        {
        //            temp = arrylist[i];
        //        }
        //        else if (i % 4 == 3)
        //        {

        //            temp = temp + arrylist[i];
        //            arrylist2.Add(temp);
        //        }
        //        else
        //        {
        //            temp = temp + arrylist[i];
        //        }

        //    }
        //    int L = arrylist2.Count;
        //    int c = 0;
        //    while (L > 50)
        //    {
        //        List<string> templist = new List<string>();
        //        for (int i = c; i < c + 50; i++)
        //        {
        //            templist.Add(arrylist2[i]);
        //        }
        //        temp = string.Join("", templist);
        //        c = c + 50;
        //        L = L - 50;
        //        resault_list.Add(temp);
        //    }

        //    List<string> templist2 = new List<string>();
        //    for (int i = c; i < c + L; i++)
        //    {
        //        templist2.Add(arrylist2[i]);
        //    }
        //    string temp2 = string.Join("", templist2);
        //    resault_list.Add(temp2);
        //    return resault_list;

        //}

        public string GetAppendString(int count,string siteArray,string dataArray,string coorArray)
        {
            List<string> list = new List<string>()
            {
                Lot,SourceLot,Fab,Technology,
               Product ,
               LotType ,
                Owner  ,
                MeasRoute ,
            MeasRouteVer ,
            MeasStep,
            MeasStepVer,
            MeasItem,
            MeasTime,
            MeasOperator,
            MeasEquipment,
            MeasRecipe,
            ProcRoute,
            ProcRouteVer,
            ProcStep,
            ProcStepVer,
            ProcTime ,
            ProcOperator,
            ProcEquipment,
            ProcRecipe,
            ProcReticle,
            LimitsInside,
            CollectedType,
            count.ToString(),
            siteArray,
            dataArray,
            Target,
            ValidLow,
            ValidHigh,
            SpecLow,
            SpecHigh,
            CtrlLow,
            CtrlHigh ,
            Unit ,
            ProcStepDesc,
            BatchID ,
            DuplicateFlag,
            TextVal,
            coorArray,
            WaferPos,
            String_Option1,
            String_Option2,
            String_Option3,
            String_Option4,
            String_Option5
        };

            return "\""+ string.Join("\",\"",list)+"\"";
        }

    }
}
