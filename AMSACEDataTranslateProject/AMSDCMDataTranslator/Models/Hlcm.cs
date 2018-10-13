using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public class Hlcm
    {
        public Hlcm()
        { }

        public string Lot
        {
            get;
            set;
        }

        public string SourceLot
        {
            get;
            set;
        } = "";

        public string Operation
        {
            get;
            set;
        } = "WAT";

        public string MeasureTime
        {
            get;
            set;
        }

        public string Fab
        {
            get;
            set;
        } = "FAB2";

        public string Product
        {
            get;
            set;
        }

        public string Technology
        {
            get;
            set;
        } = "";

        public string SpecfileName
        {
            get;
            set;
        }

        public string SpecfileVersion
        {
            get;
            set;
        } = "1";

        public string SpecfileInside
        {
            get;
            set;
        } = "Y";

        public string Temperature
        {
            get;
            set;
        } = "";

        public string TestProgram
        {
            get;
            set;
        }

        public string Equipment
        {
            get;
            set;
        } = "";

        public string ProbeCard
        {
            get;
            set;
        } = "";

        public string FlatOrientation
        {
            get;
            set;
        } = "DOWN";

        public string Owner
        {
            get;
            set;
        } = "";

        public string StartTime
        {
            get;
            set;
        } = "";

        public string EndTime
        {
            get;
            set;
        }

        public string Pos_X
        {
            get;
            set;
        } = "LEFT";

        public string Pos_Y
        {
            get;
            set;
        } = "DOWN";

        public IList<HlcmSpecData> SpecDatas;

        public IList<HlcmWafer> Wafers;
    }
}
