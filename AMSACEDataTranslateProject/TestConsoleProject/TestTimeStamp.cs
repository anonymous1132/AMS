using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Helper;

namespace TestConsoleProject
{
    public class TestTimeStamp
    {
        public static void Test()
        {
            //DateTime dt = DateTime.Now;
            //Console.WriteLine(dt.ToString("yyyy-MM-dd-HH.mm.ss.ffffff"));

            string sql = "select timestamp(claim_time) from fvace_inline_sqlbase fetch first 1 rows only";
            DB2Helper db2 = new DB2Helper();
            db2.GetSomeData(sql);
            DateTime dt = (DateTime)db2.dt.Rows[0][0];
            Console.WriteLine(dt.ToString("yyyy-MM-dd-HH.mm.ss.ffffff"));
        }
    }
}
