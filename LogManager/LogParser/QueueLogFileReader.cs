using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LogParser
{
    public class QueueLogFileReader:LogFileReader<QueueLogEntity,QueueLineEntity>
    {
        public QueueLogFileReader(string filepath):base(filepath)
        {

        }

        public override void GetData()
        {
            var list = ReadTxtFileToLineList(FileObj.FilePath);
            foreach (string line in list)
            {
                string[] arry = line.Split(' ');
                DateTime outDate = new DateTime();
                if (DateTime.TryParseExact(arry[0], "yyyy/MM/dd-HH:mm:ss", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out outDate))
                {
                    QueueLineEntity QueueLine = new QueueLineEntity
                    {
                        NoteTime = outDate,
                        Mask = Convert.ToInt32(arry[1]),
                        Command = arry[2],
                    };
                    QueueLine.Content = string.Join(" ", arry.Skip(3));
                    FileObj.Entities.Add(QueueLine);
                }
                else if (FileObj.Entities.Any())
                {
                    FileObj.Entities.Last().Content += "\n" + string.Join(" ", arry);
                }
            }

            GetModelByLine();
        }

        public override void PutDB()
        {
            var db = Public.Mongo;
            foreach (var entity in LogObj.LogEntities)
            {
                var builder = db.GetFilter<QueueLogEntity>();
                var filter = builder.Eq("MailID", entity.MailID) & builder.Eq("Mask", entity.Mask) & builder.Eq("StartTime",entity.StartTime);
                if (db.CountQuery<QueueLogEntity>(Public.Config.QueueTop, filter) == 0)
                {
                    db.InsertOne<QueueLogEntity>(Public.Config.QueueTop,entity);
                }
            }
        }

        private void GetModelByLine()
        {
            var obj = FileObj.Entities.GroupBy(p => p.Mask).Select(g => g.Key).ToList();
            foreach (int i in obj)
            {
                try
                {
                    FillModel(i);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }

        private void FillModel(int mask)
        {
            var linelist = FileObj.Entities.Where(p => p.Mask == mask).OrderBy(o => o.NoteTime).ToList();
            var fromlist = linelist.FindAll(p => p.Command == "新邮件递送");
            for (int i = 0; i < fromlist.Count; i++)
            {
                List<QueueLineEntity> unityList;
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

        private void SetSingleModelByLineList(List<QueueLineEntity> linelist)
        {
            QueueLogEntity queueLogEntity = new QueueLogEntity
            {
                MailID = linelist.Where(p => p.Command == "新邮件递送").Select(p => p.Content).FirstOrDefault().Trim(new char[] { '[', ']' }),
                StartTime = linelist.FirstOrDefault().NoteTime,
                EndTime = linelist.Last().NoteTime,
                SendAddress = linelist.Where(p => p.Command == "邮件字节数:").Select(s => s.Content).FirstOrDefault().Split(' ')[2].Trim(new char[] { '<', '>' }),
                Mask = linelist.FirstOrDefault().Mask,
                Details = string.Join("\n", linelist.Select(s => s.Detail).ToList())
            };
            double.TryParse(linelist.Where(p => p.Command == "邮件字节数:").Select(s => s.Content).FirstOrDefault().Split(' ')[0], out double d);
            queueLogEntity.ReserveSize = d;
            var senderlist = linelist.FindAll(f => f.Command.Contains("开始递送，"));
            var resaultlist = linelist.FindAll(f => f.Command == "递送成功:" || f.Command == "递送失败:");
            if (senderlist.Count == resaultlist.Count)
            {
                for (int i = 0; i < senderlist.Count; i++)
                {
                    List<QueueLineEntity> unityList = linelist.GetRange(linelist.IndexOf(senderlist[i]), linelist.IndexOf(resaultlist[i]) - linelist.IndexOf(senderlist[i]) + 1);
                    QueueReserveEntity reserverEntity = new QueueReserveEntity
                    {
                        ReserveAddress = unityList.FirstOrDefault().Content.Split(' ')[0].Trim(new char[] { '<', '>' }),
                        IsSuccessful = unityList.Any(a => a.Command == "递送成功:")
                    };
                    reserverEntity.OutSideServerAddress = unityList.Any(w => w.Command == "递送给远程主机") ? unityList.Where(w => w.Command == "递送给远程主机").FirstOrDefault().Content : "";
                    queueLogEntity.Reservers.Add(reserverEntity);
                }
            }
            LogObj.LogEntities.Add(queueLogEntity);
        }
    }
}
