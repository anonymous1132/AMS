using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMSDCMDataTranslator.Models;
using System.IO;

namespace AMSDCMDataTranslator
{
   public class WATFileUtil
    {
        public WATFileUtil(string filePath)
        {
            this.FilePath = filePath;
            GetData();
        }
        public WAT Wat
        {
            get;
            set;
        }

        private string _filePath;
        public string FilePath
        {
            get { return _filePath; }
            set
            {
                if (File.Exists(value))
                {
                    _filePath = value;
                }
                else { _filePath = null; return; }
            }
        }

        private void GetData()
        {
            if (string.IsNullOrEmpty(FilePath))
            {
                Wat = null;
                return;
            }

             Wat = new WAT();
             StreamReader sr = new StreamReader(FilePath, Encoding.Default);
            try
            {

                String line = sr.ReadLine();
                //如果文件第一行不包含5个“=”，则断定为格式错误。
                if (!line.Contains("=====")) return;
                while (!(line = sr.ReadLine()).Contains("====="))
                {
                    GetData_Overview(line);
                }
                sr.ReadLine();
                while (!(line = sr.ReadLine()).Contains("="))
                {
                    GetData_SiteCoordinate(line);
                }
                WATWafer wafer = new WATWafer();

                while ((line = sr.ReadLine()) != null)
                {
                    string[] array = line.Split(',');

                    if (array[0] == "SYS_WAFERID")
                    {
                        if (!string.IsNullOrEmpty(wafer.WaferID))
                        {
                            Wat.wafers.Add(wafer);
                        }
                        wafer.WaferID = array[1];
                    }
                    if (array.Length > 3)
                    {
                        WATParameter parameter = new WATParameter
                        {
                            ItemNo = array[0],
                            ParameterName = array[1],
                            unit = array[2]
                        };
                        for (int i = 3; i < array.Length; i++)
                        {
                            parameter.ValueList.Add(array[i]);
                        }
                        wafer.parameters.Add(parameter);
                    }
                }

                if (!string.IsNullOrEmpty(wafer.WaferID))
                {
                    Wat.wafers.Add(wafer);
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                sr.Close();
            }
            
        }

        private Dictionary<string, string> dic = new Dictionary<string, string> { { "Recipe ID", "RecipeID" },{ "Lot ID", "LotID"},{ "Foup ID", "FoupID"},{ "Tester ID", "TesterID"},{ "ProbeCard ID", "ProbeCardID" },{ "User ID", "UserID" },{ "Testprogram", "TestProgram" },
            { "Limit file","LimitFile"},{ "Date","DateTime"},{ "Test wafer","TestWafer"},{ "Test site","TestSite"},{ "Test item", "TestItem" },{ "Notch","Notch"} };

        private void GetData_Overview(string line)
        {
            foreach (var item in dic)
            {
                if (line.Contains(item.Key))
                {
                    var property = Wat.GetType().GetProperty(item.Value);
                    property.SetValue(Wat,line.Substring(line.IndexOf(':')+1),null);
                    break;
                }
            }
        }

        private void GetData_SiteCoordinate(string line)
        {
            string[] array = line.Split(',');
            if (array.Length == 3)
            {
                string[,] strtemp = new string[,] { { array[0], array[1] } };
                Wat.site_coordinates.Add(strtemp);
            }

        }
    }
}
