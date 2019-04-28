using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonUtilsLibrary.Utils;
using Newtonsoft.Json;

namespace DefectTranslator
{
    public class Translator
    {
        public Translator()
        {
            Translate();
            FtpSend();
            System.IO.File.WriteAllText(Public.CheckTimePath,JsonConvert.SerializeObject(new InspectionTimeEntity() { InspectionTime=LastDateTime}));
        }
        DateTime LastDateTime;
        private void Translate()
        {
           var list= Public.EdaCatcher.GetEntities().EntityList.Where(w=>w.InspectionTime>Public.LastInspectionTime).OrderBy(o=>o.InspectionTime);
            if (!list.Any()) { throw new Exception("没有新的数据产生"); }
            LastDateTime = list.Max(m=>m.InspectionTime);
            XmlFactory factory = new XmlFactory();
            var list_xml = list.Select(s => new { s.LotID, s.EqpID, s.RecipeID }).Distinct();
            foreach (var xml in list_xml)
            {
                var wafer_list = list.Where(w => w.LotID == xml.LotID && w.EqpID == xml.EqpID && w.RecipeID == xml.RecipeID);
                var eda = new EDAEntity() { EqpID=xml.EqpID,LotID=xml.LotID,RecipeID=xml.RecipeID};
                foreach (var wafer in wafer_list)
                {
                    eda.WaferEntities.Add(new EDAWaferEntity() {WaferID=xml.LotID.Split('.')[0]+".00."+wafer.WaferID.TrimStart('@'),SlotID=wafer.SlotID,ADC=wafer.ADC,DDC=wafer.DDC,RDC=wafer.RDC,InspectionTime=wafer.InspectionTime });
                }
                factory.Eda = eda;
                factory.FormXml();
            }
        }

        private void FtpSend()
        {
            var files = XmlFactory.DonePath.ToArray();
            if (files.Length > 0)
            {
                var ftp = new FtpUtil(Public.FtpConPara);
                ftp.FtpUploadFileForMulti(files);
                LogUtils.InfoLog("Files发送成功："+ string.Join(",",files));
            }
        }

    }
}
