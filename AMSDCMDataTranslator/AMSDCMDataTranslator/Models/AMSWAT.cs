using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AMSDCMDataTranslator.Models
{
   public class AMSWAT:Etest
    {
        /// <summary>
        /// 读取数据文件
        /// </summary>
        /// <param name="filePath">数据文件路径</param>
        public override void GetData()
        {
            WATFileUtil util = new WATFileUtil(FilePath);
            SpecDataFileUtil specUtil = new SpecDataFileUtil(specPath + "\\" + util.Wat.LimitFile);
            lot_run = new Etest_Lot_Run();
            lot_run.GetData(util.Wat, specUtil.datalist);
        }


        public string specPath
        {
            get;
            set;
        }

    }
}
