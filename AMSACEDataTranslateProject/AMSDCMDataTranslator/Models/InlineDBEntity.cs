using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class InlineDBEntity
    {
        public enum CollectedTypes
        {
            L,
            W,
            S
        }

        public DateTime ClaimTime
        {
            get;
            set;
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



        public string MeasItem
        {
            get;
            set;
        }

        public DateTime MeasTime
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



        public DateTime? ProcTime
        {
            get;
            set;
        }

        public string StrProcTime
        {
            get
            {
                return ProcTime.HasValue ? ProcTime.Value.ToString("yyyy/MM/dd_HH:mm:ss") : "";
            }
        }

        public string ProcOperatorUser
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


        public string MeasType
        {
            get;
            set;
        }

        public CollectedTypes CollectedType
        {
            get
            {
                if (MeasType.ToUpper().Contains("WAFER"))
                {
                    return CollectedTypes.W;
                }
                else if (MeasType.ToUpper().Contains("SITE"))
                {
                    return CollectedTypes.S;
                }
                else
                {
                    return CollectedTypes.L;
                }
            }
        }

        public string WaferSeq
        {
            get;
            set;
        }

        public string WaferPosition
        {
            get;
            set;
        }

        public string SitePosition
        {
            get;
            set;
        }

        //获取WaferSiteArray元素
        public string WaferSiteArrayElement
        {
            get
            {
                string resault="";
                switch (CollectedType)
                {
                    case CollectedTypes.W:
                        resault= FixWaferLevelArrayElement();
                        break;
                    case CollectedTypes.S:
                        resault= FixSiteLevelArratElement();
                        break;
                    case CollectedTypes.L:
                        resault = "0000";
                        break;
                }
                return resault;
            }
        }

        public double DCItemValue
        {
            get;
            set;
        }

        public string Target
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


        public string ProcStepDesc
        {
            get;
            set;
        }

        public string MeasDcdefID
        {
            get;
            set;
        }

        public string ItemType
        {
            get;
            set;
        }


        private string FixWaferLevelArrayElement()
        {
            string s = WaferSeq;
            if (string.IsNullOrEmpty(WaferSeq))
            {
                s = "";
            }
            else if ( WaferSeq == "*")
            {
                s = "*";
            }
            else if (s.Length == 1)
            {
                s = string.Format("0{0}00", s);
            }
            else
            {
                s = string.Format("{0}00", s);
            }
            return s;
        }

        private string FixSiteLevelArratElement()
        {
            if (string.IsNullOrEmpty(WaferSeq))
            {
                return "";
            }

            string w = WaferSeq;
            string s = SitePosition;
            if (w.Length == 1)
            {
                w = string.Format("0{0}", w);
            }
            if (s.Length == 1)
            {
                s = string.Format("0{0}", s);
            }
            return w+s;
        }

    }
}
