using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.Presentation;

namespace AMS.CIM.Caojin.SqlReplicationAnalysisTool.Model
{
    public class RunStatusLineModel:NotifyPropertyChanged
    {
        private string apply_Qual;
        public string Apply_Qual { get { return apply_Qual; } set { apply_Qual = value;OnPropertyChanged("Apply_Qual"); } }

        private string set_Name;
        public string Set_Name { get { return set_Name; } set { set_Name = value;OnPropertyChanged("Set_Name"); } }

        private string source_Server;
        public string Source_Server { get { return source_Server; } set { source_Server = value;OnPropertyChanged("Source_Server"); } }

        private string synchPoint;
        public string SynchPoint { get { return synchPoint; } set { synchPoint = value;OnPropertyChanged("SynchPoint"); } }

        private string synchTime;
        public string SynchTime { get { return synchTime; } set { synchTime = value;OnPropertyChanged("SynchTime");OnPropertyChanged("DSynchTime"); } }
        public DateTime DSynchTime
        {
            get
            {
                try
                {
                    return DateTime.ParseExact(SynchTime, "yyyy-MM-dd-HH.mm.ss.ffffff", System.Globalization.CultureInfo.CurrentCulture);
                }
                catch (Exception e)
                {
                    throw new DateTimeParseException("RunStatusGroupModel.DtargetTime时间格式转换错误", e);
                }
            }
        }

        private string status;
        public string Status { get { return status; } set { status = value;OnPropertyChanged("Status"); } }

        private TimeSpan deltaTimeSpan;
        public TimeSpan DeltaTimeSpan
        {
            get { return deltaTimeSpan; }

            set { deltaTimeSpan = value;OnPropertyChanged("DeltaTimeSpan");OnPropertyChanged("StrDeltaTimeSpan");
                //eventUpdateDeltaTimeSpan();
            }
        }
        public double MinDeltaTimeSpan { get { return DeltaTimeSpan.TotalMinutes; } }

        //public delegate void delegateUpdateDeltaTimeSpan();

        //public event delegateUpdateDeltaTimeSpan eventUpdateDeltaTimeSpan;

    }
}
