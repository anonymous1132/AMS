using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    /// <summary>
    /// 为Inline提供获取坐标数据
    /// </summary>
    public class InlineDCMEntity
    {
        public string LotID
        {
            get;
            set;
        }

        public string EQPID
        {
            get;
            set;
        }

        public int MeasureDataCount
        {
            get;
            set;
        }

        public string Recipe
        {
            get;
            set;
        }

        public string Coordinate
        {
            get;
            set;
        }

        public DateTime NewDate
        {
            get;
            set;
        }

    }
}
