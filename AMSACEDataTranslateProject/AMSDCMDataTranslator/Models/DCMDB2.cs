using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace AMSDCMDataTranslator.Models
{
   public class DCMDB2
    {
        public DCMDB2(string fileName)
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
                    _guid = System.Guid.NewGuid().ToString();
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

        public string LotID
        {
            get;
            set;
        }

        public string Recipe
        {
            get;
            set;
        }

        public int MeasureDataCount
        {
            get;
            set;
        }

        public string CoordinateArray
        {
            get;
            set;
        }

        public DateTime UpdateTime
        {
            get;
            set;
        }

        public void GetData(DataTable dt)
        {
            bool isfirst = true;
           List<string> Coordinate=new List<string>();
            foreach (DataRowView drv in dt.DefaultView)
                {
                   try
                  {
                    DCM dcm = new DCM(this.FileName);
                    dcm.Getdata(drv);
                    Coordinate.Add(dcm.X/1000.0+","+dcm.Y/1000.0);


                    if (isfirst)
                    {
                        this.CollectionDateTime = dcm.CollectionDateTime;
                        this.EQP_ID = dcm.EQP_ID;
                        this.LotID = dcm.LotID;
                        this.Recipe = dcm.Recipe;
                        UpdateTime = dcm.UpdateTime;
                        isfirst = false;
                    }

                  }
                    catch (Exception e)
                    {
                       AMSDCMDataTranslator.Helper.LogHelper.ErrorLog(e);
                    }
                }

            MeasureDataCount = Coordinate.Count;
            CoordinateArray = string.Join(";",Coordinate);
            }
        
    }
}
