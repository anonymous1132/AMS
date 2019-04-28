using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonUtilsLibrary.Utils;
using CommonUtilsLibrary.Models;
using Newtonsoft.Json;

namespace DefectTranslator
{
    public static class Public
    {
        public static readonly string ExePath = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;

        public static readonly string ExeDir = Path.GetDirectoryName(ExePath);

        public static readonly string DataPath = Path.Combine(ExeDir, "App\\data");

        public static OracleConPara OracleConPara { get; set; } = JsonConvert.DeserializeObject<OracleConPara>(File.ReadAllText(Path.Combine(ExeDir, "App\\config\\oracle.json")));

        public static string CheckTimePath{get;set;} = Path.Combine(ExeDir, "App\\config\\ins_time.json");

        public static  DateTime LastInspectionTime
        {
            get
            {
                if (File.Exists(CheckTimePath))
                {
                    return JsonConvert.DeserializeObject<InspectionTimeEntity>(File.ReadAllText(CheckTimePath)).InspectionTime;
                }
                else
                {
                    return DateTime.Parse("2019/4/10");
                }
            }
        }

        public static FtpConPara FtpConPara { get; set; }= JsonConvert.DeserializeObject<FtpConPara>(File.ReadAllText(Path.Combine(ExeDir, "App\\config\\ftp.json")));

        public static OracleDataCatcher<EDAORM> EdaCatcher { get; set; } = new OracleDataCatcher<EDAORM>("",OracleConPara,string.Format(@"select s.inspect_equip_id EqpID,s.lot_id LotID,r.recipe_id RecipeID,s.adder_defects ADC,s.defective_die DDC,s.repeaters RDC,s.inspection_time InspectionTime,s.slot_id SlotID,s.wafer_id WaferID
from UDB.INSP_WAFER_SUMMARY s left join udb.insp_recipe r on s.recipe_key = r.recipe_key where s.inspection_time >to_date('{0}','yyyy-mm-dd hh24:mi:ss')",LastInspectionTime.ToString("yyyy-MM-dd HH:mm:ss")));
    }
}
