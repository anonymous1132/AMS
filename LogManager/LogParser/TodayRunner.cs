using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using System.IO;
using CommonUtilsLibrary.Utils;

namespace LogParser
{
    public class TodayRunner
    {
        public TodayRunner()
        {
            DoWork();
        }

        DirectoryInfo directory = new DirectoryInfo(Public.Config.DirectoryPath);


        //干活
        void DoWork()
        {
            var smtpfile = directory.GetFiles("smtp.log");
            if (smtpfile.Any())
            {
                new ReciveLogFileReader(smtpfile.First().FullName, Public.Config.SmtpTop);
            }
            else
            {
                LogUtils.ErrorLog("smtp.log文件没有找到");
            }

            var smtpsslfile = directory.GetFiles("smtpssl.log");
            if (smtpsslfile.Any())
            {
                new SmtpSslLogFileReader(smtpsslfile.First().FullName);
            }
            else { LogUtils.ErrorLog("smtpssl.log文件没有找到"); }

            var smtpmsafile = directory.GetFiles("smtpmsa.log");
            if (smtpmsafile.Any())
            {
                new SmtpMsaLogFileReader(smtpmsafile.First().FullName);
            }
            else { LogUtils.ErrorLog("smtpmsa.log文件没有找到"); }

            var pop3file = directory.GetFiles("pop3.log");
            if (pop3file.Any())
            {
                new Pop3LogFileReader(pop3file.First().FullName);
            }
            else { LogUtils.ErrorLog("pop3.log文件没有找到"); }

            var pop3sslfile = directory.GetFiles("pop3ssl.log");
            if (pop3sslfile.Any())
            {
                new Pop3SslLogFileReader(pop3sslfile.First().FullName);
            }
            else { LogUtils.ErrorLog("pop3ssl.log文件没有找到"); }

            var queuefile = directory.GetFiles("queue.log");
            if (queuefile.Any())
            {
                new QueueLogFileReader(queuefile.First().FullName);
            }
            else { LogUtils.ErrorLog("queue.log文件没有找到"); }

            var imapfile = directory.GetFiles("imap.log");
            if (imapfile.Any())
            {
                new ImapLogFileReader(imapfile.First().FullName);
            }
            else { LogUtils.ErrorLog("imap.log文件没有找到"); }

            var imapsslfile = directory.GetFiles("imapssl.log");
            if (imapsslfile.Any())
            {
                new ImapSslLogFileReader(imapsslfile.First().FullName);
            }
            else
            {
                LogUtils.ErrorLog("imapssl.log文件没有找到");
            }
        }


    }
}
