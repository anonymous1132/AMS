using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstFloor.ModernUI.App.Model;
using Caojin.Common;

namespace FirstFloor.ModernUI.App.Runner
{
    public class SmtpFileReader
    {
        public SmtpFileReader(string filePath)
        {
            FilePath = filePath;
            SmtpLineList = new List<MailReadLineEntity>();
            SmtpModelList = new List<MailSmtpModel>();
            GetData();
        }

        public string FilePath
        {
            get;
            set;
        }

        public List<MailReadLineEntity> SmtpLineList;

        public List<MailSmtpModel> SmtpModelList;

        private void GetData()
        {
            List<string> list = FileHelper.ReadTxtFileToLineList(FilePath);
            foreach (string line in list)
            {
                string[] arry = line.Split(' ');
                DateTime outDate = new DateTime();
                if (arry.Length >= 3&&DateTime.TryParseExact(arry[0], "yyyy/MM/dd-HH:mm:ss", new System.Globalization.CultureInfo("en-US"), System.Globalization.DateTimeStyles.None, out outDate))
                {
                    MailReadLineEntity SmtpLine = new MailReadLineEntity
                    {
                        NoteTime = outDate,
                        Mask = Convert.ToInt32(arry[1]),
                        Command = arry[2],
                    };
                    SmtpLine.Content=string.Join(" ",arry.Skip(3));
                    SmtpLineList.Add(SmtpLine);
                }
            }
            GetModelByLine();
        }

        private void GetModelByLine()
        {
            var obj = SmtpLineList.GroupBy(p => p.Mask).Select(g => g.Key).ToList();
            foreach (int i in obj)
            {
                try
                {
                    FillModel(i);
                }
                catch (Exception)
                {
                    //日志记录一下，或者其他方式告警
                    //throw;
                    continue;
                }
            }
        }

        private void FillModel(int mask)
        {
            var linelist = SmtpLineList.Where(p => p.Mask == mask).OrderBy(o => o.NoteTime).ToList();
            var fromlist= linelist.FindAll(p => p.Command == "来自");
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

        private void SetSingleModelByLineList(List<MailReadLineEntity>linelist)
        {
            MailSmtpModel smtpModel = new MailSmtpModel
            {
                IPAddress = linelist.Where(p => p.Command == "来自").Select(s => s.Content).FirstOrDefault().Split(' ')[0],
                StartTime = linelist.Where(p => p.Command == "来自").Select(s => s.NoteTime).FirstOrDefault(),
                EndTime = linelist.Last().NoteTime,
                SendMailAddress = linelist.Where(p => p.Command == "发件人").Select(s => s.Content).FirstOrDefault().Split(' ')[1],
                ReserveMailAddress = linelist.Where(p => p.Command == "收件人").Select(s => s.Content).FirstOrDefault().Split(' ')[1],
                MaxSize = Convert.ToDouble(linelist.Where(p => p.Command == "允许的最大邮件字节数").Select(s => s.Content).FirstOrDefault().Split(' ')[1].Trim()),
                Mask = linelist.FirstOrDefault().Mask,
                Details = string.Join("\n", linelist.Select(s => s.Detail).ToList())
            };
            double d = 0;
            double.TryParse(linelist.Where(p => p.Command == "接收到邮件大小").Select(s => s.Content).FirstOrDefault().Split(' ')[1], out d);
            smtpModel.ReserveSize = d;
            if(ShareDataEntity.GetSingleton().MailSmtpCollection.Where(w=>w.StartTime==smtpModel.StartTime&&w.Mask==smtpModel.Mask).Count()==0)
            SmtpModelList.Add(smtpModel);
        }
    }
}
