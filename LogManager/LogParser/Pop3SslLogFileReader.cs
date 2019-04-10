using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class Pop3SslLogFileReader : LogFileReader<SmtpSslLogEntity, SmtpSslLineEntity>
    {
        public Pop3SslLogFileReader(string filepath) : base(filepath)
        { }


        public override void PutDB()
        {
            var db = Public.Mongo;
            foreach (var entity in LogObj.LogEntities)
            {
                var builder = db.GetFilter<SmtpSslLogEntity>();
                var filter = builder.Eq("StartTime", entity.StartTime) & builder.Eq("Mask", entity.Mask);
                if (db.CountQuery<SmtpSslLogEntity>(Public.Config.Pop3SslTop, filter) == 0)
                {
                    db.InsertOne<SmtpSslLogEntity>(Public.Config.Pop3SslTop, entity);
                }
            }
        }

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
           // var sent = linelist.Where(p => p.Command == "发件人");
            var reserve = linelist.Where(p => p.Command == "命令"&&p.Content.Contains("= USER"));
            SmtpSslLogEntity smtpModel = new SmtpSslLogEntity
            {
                IPAddress = from.Any() ? from.First().Content.Split(' ')[0] : "",
                StartTime = from.Any() ? from.First().NoteTime : linelist.First().NoteTime,
                EndTime = linelist.Last().NoteTime,
                SendAddress = "",
                ReserveAddress = reserve.Any() ? reserve.First().Content.Split(' ')[2] : "",
                MaxSize =0,
                Mask = linelist.First().Mask,
                Details = string.Join("\n", linelist.Select(s => s.Detail).ToList()),
                ReserveSize=0
            };
            LogObj.LogEntities.Add(smtpModel);
        }

    }
}
