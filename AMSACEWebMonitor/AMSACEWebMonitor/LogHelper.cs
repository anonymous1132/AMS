using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AegisImplicitMail;
using System.ComponentModel;

namespace AMSACEWebMonitor
{
    public class LogHelper
    {
        private static string LogFilePath
        {
            get { return Setting.exeDir + "App\\log.txt"; }
        }

        public static void LogWrite(string message)
        {
            using (StreamWriter file = new StreamWriter(LogFilePath, true))
            {
                file.WriteLine(message+"\t"+DateTime.Now.ToString());
            }
        }

        public static void SendEmail(string subject,string body)
        {
            //Generate Message
            var mailMessage = new MimeMailMessage();
            mailMessage.From = new MimeMailAddress(Setting.emailParameter.From);
            mailMessage.To.Add(Setting.emailParameter.To);
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            

            //Create Smtp Client
            var mailer = new MimeMailer(Setting.emailParameter.SMTPServer, Setting.emailParameter.Port);
            mailer.User = Setting.emailParameter.From;
            mailer.Password = Setting.emailParameter.Password;
            mailer.SslType = SslMode.Ssl;
            mailer.AuthenticationMode = AuthenticationType.Base64;

            //Set a delegate function for call back
            mailer.SendCompleted += compEvent;
            mailer.SendMailAsync(mailMessage);
        }

        //Call back function
        private static void compEvent(object sender, AsyncCompletedEventArgs e)
        {
            if (e.UserState != null)
                LogWrite("MailInfo:"+ e.UserState.ToString());
            if (e.Error != null)
                LogWrite("MailError:"+e.Error.Message);
        }
    }
}
