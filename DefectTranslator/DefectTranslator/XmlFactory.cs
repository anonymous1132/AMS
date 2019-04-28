using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CommonUtilsLibrary.Utils;

namespace DefectTranslator
{
    public class XmlFactory
    {
        public static List<string> DonePath { get; set; } = new List<string>();

        public EDAEntity Eda { get; set; }

        string FilePath { get { return Path.Combine(Public.DataPath,Eda.FileName); } }

        public void FormXml()
        {
            try
            {
                if (File.Exists(FilePath))
                {
                    UpdateXml();
                }
                else
                {
                    NewXml();
                }
                DonePath.Add(FilePath);
            }
            catch (Exception ex)
            {
                LogUtils.ErrorLog(ex);
            }
        }

        private void NewXml()
        {
            List<string> wafers = new List<string>();
            string head = "<?xml version=\"1.0\"?>";
            head += "\n<EDA>\n";
            foreach (var wafer in Eda.WaferEntities)
            {
                if (wafers.IndexOf(wafer.WaferID) >=0) continue;
                head += "<DataItems>\n";
                head += string.Format("<DataItem WaferID=\"{0}\" SlotNo=\"{1}\">\n",wafer.WaferID,wafer.SlotID);
                head += string.Format("<Item ItemName=\"ADC\">{0}</Item>\n",wafer.ADC);
                head += string.Format("<Item ItemName=\"DDC\">{0}</Item>\n", wafer.DDC);
                head += string.Format("<Item ItemName=\"RDC\">{0}</Item>\n", wafer.RDC);
                head += "</DataItem>\n</DataItems>\n";
                wafers.Add(wafer.WaferID);
            }
            head += "</EDA>";
            File.WriteAllText(FilePath,head);
        }

        private void UpdateXml()
        {
            List<string> list = File.ReadAllLines(FilePath).ToList();
            foreach (var wafer in Eda.WaferEntities)
            {
                int pos = list.FindIndex(f=>f.Contains(string.Format("<DataItem waferID=\"{0}\"", wafer.WaferID)));

                //如果没有该wafer
                if (pos==-1)
                {
                    list.Insert(2,"</DataItems>");//插入新的Node
                    list.Insert(2, "</DataItem>");
                    list.Insert(2, string.Format("<Item Itemname=\"RDC\">{0}</Item>", wafer.RDC));
                    list.Insert(2, string.Format("<Item Itemname=\"DDC\">{0}</Item>", wafer.DDC));
                    list.Insert(2, string.Format("<Item Itemname=\"ADC\">{0}</Item>", wafer.ADC));
                    list.Insert(2, string.Format("<DataItem waferID=\"{0}\" waferSlotNo=\"{1}\">", wafer.WaferID, wafer.SlotID));
                    list.Insert(2, "<DataItems>");
                }
                else
                {
                    try
                    {
                        list[pos] = string.Format("<DataItem waferID=\"{0}\" waferSlotNo=\"{1}\">", wafer.WaferID, wafer.SlotID);
                        list[pos + 1] = string.Format("<Item Itemname=\"ADC\">{0}</Item>", wafer.ADC);
                        list[pos + 2] = string.Format("<Item Itemname=\"DDC\">{0}</Item>", wafer.DDC);
                        list[pos + 3] = string.Format("<Item Itemname=\"RDC\">{0}</Item>", wafer.RDC);
                    }
                    catch (Exception ex)
                    {
                        LogUtils.ErrorLog("XmlFactory=>UpdateXml=>"+FilePath+"=>Wafer:"+wafer.WaferID,ex);
                        throw ex;
                    }
                }

            }
            File.WriteAllLines(FilePath,list);
        }

       
    }
}
