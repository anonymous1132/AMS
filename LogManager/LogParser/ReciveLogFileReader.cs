using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class ReciveLogFileReader : LogFileReader<SmtpSslLogEntity, SmtpSslLineEntity>
    {
        public ReciveLogFileReader(string filepath,string top,bool isUpdate=false) 
        {
            TopStr = top;
            IsUpdate = isUpdate;
            FileObj.FilePath = filepath;
            GetData();
            PutDB();
        }


        private string TopStr { get; set; }

        public bool IsUpdate { get; set; } = false;

        public override void GetData()
        {
            var list = ReadTxtFileToLineList(FileObj.FilePath);
            foreach (string line in list)
            {
                string[] arry = line.Split(' ');
                DateTime outDate = new DateTime();
                if (arry.Length >= 3 && DateTime.TryParseExact(arry[0], "yyyy/MM/dd-HH:mm:ss", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out outDate))
                {
                    SmtpSslLineEntity SmtpLine = new SmtpSslLineEntity
                    {
                        NoteTime = outDate,
                        Mask = Convert.ToInt32(arry[1]),
                        Command = arry[2],
                    };
                    SmtpLine.Content = string.Join(" ", arry.Skip(3));
                    FileObj.Entities.Add(SmtpLine);
                }
            }
            GetModelByLine();
        }

        public override void PutDB()
        {
            var db = Public.Mongo;
            foreach (var entity in LogObj.LogEntities)
            {
                var builder = db.GetFilter<SmtpSslLogEntity>();
                var filter = builder.Eq("StartTime", entity.StartTime) & builder.Eq("Mask", entity.Mask);
                if (db.CountQuery<SmtpSslLogEntity>(TopStr, filter) == 0)
                {
                    db.InsertOne<SmtpSslLogEntity>(TopStr, entity);
                }
                else if (IsUpdate)
                {
                    db.DeleteMany<SmtpSslLogEntity>(TopStr,filter);
                    db.InsertOne<SmtpSslLogEntity>(TopStr,entity);
                }
            }
        }

        private void GetModelByLine()
        {
            var obj = FileObj.Entities.GroupBy(p => p.Mask).Select(g => g.Key).ToList();
            foreach (int i in obj)
            {
                //try
                //{
                FillModel(i);
                //}
                //catch (Exception)
                //{
                //    //日志记录一下，或者其他方式告警
                //    //throw;
                //    continue;
                //}
            }
        }

        private void FillModel(int mask)
        {
            var linelist = FileObj.Entities.Where(p => p.Mask == mask).OrderBy(o => o.NoteTime).ToList();
            var fromlist = linelist.FindAll(p => p.Command == "来自");
            for (int i = 0; i < fromlist.Count; i++)
            {
                List<SmtpSslLineEntity> unityList;
                if (i == fromlist.Count - 1)
                {
                    unityList = linelist.FindAll(f => f.NoteTime >= fromlist[i].NoteTime);
                }
                else
                {
                    unityList = linelist.FindAll(f => f.NoteTime >= fromlist[i].NoteTime && f.NoteTime < fromlist[i + 1].NoteTime);
                }
                SetSingleModelByLineList(unityList);
            }
        }

        private void SetSingleModelByLineList(List<SmtpSslLineEntity> linelist)
        {
            var from = linelist.Where(p => p.Command == "来自");
            var sent = linelist.Where(p => p.Command == "发件人");
            var reserve = linelist.Where(p => p.Command == "收件人");
            var maxSize = linelist.Where(p => p.Command == "允许的最大邮件字节数");
            var reserceSize = linelist.Where(p => p.Command == "接收到邮件大小");
            SmtpSslLogEntity smtpModel = new SmtpSslLogEntity
            {
                IPAddress = from.Any() ? from.First().Content.Split(' ')[0] : "",
                StartTime = from.Any() ? from.First().NoteTime : linelist.First().NoteTime,
                EndTime = linelist.Last().NoteTime,
                SendAddress = sent.Any() ? sent.First().Content.Split(' ')[1] : "",
                ReserveAddress = reserve.Any() ? reserve.First().Content.Split(' ')[1] : "",
                MaxSize = maxSize.Any() ? Convert.ToDouble(maxSize.First().Content.Split(' ')[1].Trim()) : 0,
                Mask = linelist.First().Mask,
                Details = string.Join("\n", linelist.Select(s => s.Detail).ToList())
            };
            double.TryParse(reserceSize.Any() ? reserceSize.First().Content.Split(' ')[1] : "0", out double d);
            smtpModel.ReserveSize = d;
            LogObj.LogEntities.Add(smtpModel);
        }
    }
}
