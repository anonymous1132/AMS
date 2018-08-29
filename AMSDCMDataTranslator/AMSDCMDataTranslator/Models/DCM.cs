using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
    public class DCM
    {
        public DCM(string fileName)
        {
            FileName = fileName;
        }

        public string FileName
        {
            get;
            set;
        }

        private string _guid;
        public string Guid
        {
            get
            {
                if (_guid == null)
                {
                    _guid=System.Guid.NewGuid().ToString();
                }
                return _guid;
            }
        }

        public DateTime CollectionDateTime
        {
            get;
            set;
        }

        public string EQP_ID
        {
            get;
            set;
        }

        public string WaferID
        {
            get;
            set;
        }

        public string LotID
        {
            get;
            set;
        }

        public string Cassette
        {
            get;
            set;
        }

        public int SLOT
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public string DataType
        {
            get;
            set;
        }

        public string Recipe
        {
            get;
            set;
        }

        public string RCPCNT
        {
            get;
            set;
        }

        public string MeasSet
        {
            get;
            set;
        }

        public string MateRial
        {
            get;
            set;
        }

        public int Site
        {
            get;
            set;
        }

        public int SiteNo
        {
            get;
            set;
        }

        public float X
        {
            get;
            set;
        }

        public float Y
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }

        public List<DCM_Value> values = new List<DCM_Value>();

        public void Getdata(DataRowView drv)
        {
            CollectionDateTime =Convert.ToDateTime(drv[0]);
            EQP_ID = drv[1].ToString();
            WaferID = drv[2].ToString();
            LotID = drv[3].ToString();
            Cassette = drv[4].ToString();
            SLOT = Convert.ToInt32(drv[5]);
            Status = drv[6].ToString();
            DataType = drv[7].ToString();
            Recipe = drv[8].ToString();
            RCPCNT = drv[9].ToString();
            MeasSet = drv[10].ToString();
            MateRial = drv[11].ToString();
            Site = Convert.ToInt32(drv[12]);
            SiteNo =Convert.ToInt32(drv[13]);
            X = Convert.ToSingle(drv[14]);
            Y = Convert.ToSingle(drv[15]);
            for (int i = 16; i < drv.Row.Table.Columns.Count; i++)
            {
                DCM_Value dCM_Value = new DCM_Value();
                dCM_Value.ValueCategory = drv.Row.Table.Columns[i].ColumnName;
                dCM_Value.MeasureValue = Convert.ToSingle(drv[i]);
                values.Add(dCM_Value);
            }
            UpdateTime = DateTime.Now;
        }


    }
}
