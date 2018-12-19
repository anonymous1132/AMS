using System;
using System.Collections.Generic;
using System.Linq;
using FirstFloor.ModernUI.App.Model;
using Caojin.Common;

namespace FirstFloor.ModernUI.App.Runner
{
    public class QueueFileReader
    {
        public QueueFileReader(string filePath)
        {
            FilePath = filePath;
            QueueLineList = new List<MailReadLineEntity>();
            QueueModelList = new List<MailQueueViewModel>();
            GetData();
        }

        public string FilePath
        {
            get;
            set;
        }

        public List<MailReadLineEntity> QueueLineList;

        public List<MailQueueViewModel> QueueModelList;

        private void GetData()
        {
            List<string> list = FileHelper.ReadTxtFileToLineList(FilePath);
            foreach (string line in list)
            {
                string[] arry = line.Split(' ');
                DateTime outDate = new DateTime();
                if (DateTime.TryParseExact(arry[0], "yyyy/MM/dd-HH:mm:ss", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out outDate))
                {
                    MailReadLineEntity QueueLine = new MailReadLineEntity
                    {
                        NoteTime = outDate,
                        Mask = Convert.ToInt32(arry[1]),
                        Command = arry[2],
                    };
                    QueueLine.Content = string.Join(" ", arry.Skip(3));
                    QueueLineList.Add(QueueLine);
                }
                else if(QueueLineList.Any())
                {
                    QueueLineList.LastOrDefault().Content += "\n" + string.Join(" ", arry);
                }
            }

            GetModelByLine();


        }

        private void GetModelByLine()
        {
            var obj = QueueLineList.GroupBy(p => p.Mask).Select(g => g.Key).ToList();
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
            var linelist = QueueLineList.Where(p => p.Mask == mask).OrderBy(o => o.NoteTime).ToList();
            var fromlist = linelist.FindAll(p => p.Command == "新邮件递送");
            for (int i = 0; i < fromlist.Count; i++)
            {
                List<MailReadLineEntity> unityList;
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

        private void SetSingleModelByLineList(List<MailReadLineEntity> linelist)
        {
            MailQueueViewModel queueViewModel = new MailQueueViewModel();
            queueViewModel.QueueModel = new MailQueueModel()
            {
                MailID = linelist.Where(p => p.Command == "新邮件递送").Select(p => p.Content).FirstOrDefault().Trim(new char[] { '[', ']' }),
                StartTime = linelist.FirstOrDefault().NoteTime,
                EndTime = linelist.Last().NoteTime,
                SendMailAddress = linelist.Where(p => p.Command == "邮件字节数:").Select(s => s.Content).FirstOrDefault().Split(' ')[2].Trim(new char[] { '<', '>' }),
                Mask = linelist.FirstOrDefault().Mask,
                Details = string.Join("\n", linelist.Select(s => s.Detail).ToList())
            };
            double d = 0;
            double.TryParse(linelist.Where(p => p.Command == "邮件字节数:").Select(s => s.Content).FirstOrDefault().Split(' ')[0], out d);
            queueViewModel.QueueModel.SendSize = d;

            var senderlist = linelist.FindAll(f=>f.Command.Contains("开始递送，"));
            var resaultlist = linelist.FindAll(f=>f.Command=="递送成功:"||f.Command=="递送失败:");
            if (senderlist.Count == resaultlist.Count)
            {
                for (int i = 0; i < senderlist.Count; i++)
                {
                   List<MailReadLineEntity> unityList = linelist.GetRange(linelist.IndexOf(senderlist[i]), linelist.IndexOf(resaultlist[i]) - linelist.IndexOf(senderlist[i]) + 1);
                    MailQueueReserverEntity reserverEntity = new MailQueueReserverEntity()
                    {
                        ReserverAddress = unityList.FirstOrDefault().Content.Split(' ')[0].Trim(new char[] { '<', '>' }),
                        IsSuccessful = unityList.Any(a=>a.Command== "递送成功:")
                    };
                    reserverEntity.SetMailGuid(queueViewModel.Guid);
                    reserverEntity.OutSideServerAddress = unityList.Any(w => w.Command == "递送给远程主机") ? unityList.Where(w => w.Command == "递送给远程主机").FirstOrDefault().Content : "";
                    queueViewModel.ReserverEntities.Add(reserverEntity);
                }
            }

            if (ShareDataEntity.GetSingleton().MailQueueCollection.Where(w =>w.MailID==queueViewModel.MailID&&w.StartTime == queueViewModel.StartTime && w.Mask == queueViewModel.Mask).Count() == 0)
                QueueModelList.Add(queueViewModel);
        }
    }
}
