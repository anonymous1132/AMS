using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AMSDCMDataTranslator.Helper;

namespace AMSDCMDataTranslator.Models
{
   public class Etest_Lot_Run
    {
        public Etest_Lot_Run()
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
        } = "WATProduct";

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
            get { return "1.0"; }
        }

        public string SpecfileInside
        {
            get { return "Y"; }
        }

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

        public string Operator
        {
            get;
            set;
        }

        public string ProbeCard
        {
            get;
            set;
        }

        public string FlatOrientation
        {
            get;
            set;
        }

        public string Owner
        {
            get;
            set;
        }

        public string Pos_X
        {
            get { return "LEFT"; }
        }

        public string Pos_Y
        {
            get { return "UP"; }
        }

        public List<Etest_Limit>etest_limits=new List<Etest_Limit>();

        public List<Etest_Wafer_Run> etest_wafers=new List<Etest_Wafer_Run>();

        public XmlNode SetLotRunNode(XmlNode xn)
        {
            XmlHelper.InsertAttribute(ref xn,"Lot",Lot);
            XmlHelper.InsertAttribute(ref xn,"SourceLot",SourceLot);
            XmlHelper.InsertAttribute(ref xn,"Operation",Operation);
            XmlHelper.InsertAttribute(ref xn,"MeasureTime",MeasureTime);
            XmlHelper.InsertAttribute(ref xn,"Fab",Fab);
            XmlHelper.InsertAttribute(ref xn,"Product",Product);
            XmlHelper.InsertAttribute(ref xn,"Technology",Technology);
            XmlHelper.InsertAttribute(ref xn, "SpecfileName",SpecfileName);
            XmlHelper.InsertAttribute(ref xn,"SpecfileVersion",SpecfileVersion);
            XmlHelper.InsertAttribute(ref xn,"SpecfileInside",SpecfileInside);
            XmlHelper.InsertAttribute(ref xn, "Temperature", Temperature);
            XmlHelper.InsertAttribute(ref xn, "TestProgram", TestProgram);
            XmlHelper.InsertAttribute(ref xn, "Equipment", Equipment);
            XmlHelper.InsertAttribute(ref xn, "Operator", Operator);
            XmlHelper.InsertAttribute(ref xn, "ProbeCard", ProbeCard);
            XmlHelper.InsertAttribute(ref xn, "FlatOrientation", FlatOrientation);
            XmlHelper.InsertAttribute(ref xn, "Owner", Owner);
            XmlHelper.InsertAttribute(ref xn, "Pos_X", Pos_X);
            XmlHelper.InsertAttribute(ref xn, "Pos_Y", Pos_Y);
            xn = SetLimitsNode(xn);
            foreach (Etest_Wafer_Run wafer in etest_wafers)
            {
                XmlNode childxn = (xn.OwnerDocument).CreateElement("ETEST_WAFER_RUN");
                childxn = wafer.SetWaferRunNode(childxn);
                xn.AppendChild(childxn);
            }
            return xn;
        }

        private XmlNode SetLimitsNode(XmlNode xn)
        {
             XmlElement xe = (xn.OwnerDocument).CreateElement("ETEST_LIMITS");
            
            foreach (Etest_Limit limit in etest_limits)
            {
                XmlElement xee = (xe.OwnerDocument).CreateElement("LIM");
                XmlNode _xn = limit.SetSpecNode(xee);
                xe.AppendChild(_xn);
            }
            xn.AppendChild(xe);
            return xn;
        }

        public  void GetData(WAT wat,IList<WATSpecData>specDatas)
        {
            Lot = wat.LotID;
            SourceLot = "";
            Operation = "WAT";
            MeasureTime = wat.DateTime;
            Fab = "FAB2";
            SpecfileName = wat.LimitFile;
            TestProgram = wat.TestProgram;
            Operator = wat.TesterID;
            ProbeCard = wat.ProbeCardID;
            FlatOrientation = wat.Notch;
            Owner = wat.UserID;
            foreach (WATSpecData spec in specDatas)
            {
                Etest_Limit limit = new Etest_Limit();
                limit.GetData(spec);
                etest_limits.Add(limit);
            }
            foreach (WATWafer wafer in wat.wafers)
            {
                Etest_Wafer_Run wafer_Run = new Etest_Wafer_Run();
                wafer_Run.GetData(wafer,wat);
                etest_wafers.Add(wafer_Run);
            }

        }
    }
}
