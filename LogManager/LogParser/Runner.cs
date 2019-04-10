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
    public class Runner
    {
        public Runner()
        {
            CheckRecord();
            GetFiles();
            DoWork();
            ResetRecordTime();
        }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        List<string> ImapFiles { get; set; } = new List<string>();
        List<string> ImapSslFiles { get; set; } = new List<string>();
        List<string> Pop3Files { get; set; } = new List<string>();
        List<string> Pop3SslFiles { get; set; } = new List<string>();
        List<string> QueueFiles { get; set; } = new List<string>();
        List<string> SmtpMsaFile { get; set; } = new List<string>();
        List<string> SmtpSslFile { get; set; } = new List<string>();
        List<string> SmtpFile { get; set; } = new List<string>();
        DirectoryInfo directory = new DirectoryInfo(Public.Config.DirectoryPath);
        //检查记录
        void CheckRecord()
        {
            try
            {
              StartTime=  Public.Mongo.GetOne<UpdateRecord>("record", null).RecordTime;
              StartTime = (DateTime.Now - StartTime).TotalDays > 31 ? DateTime.Now.AddDays(-31) : StartTime;
            }
            catch (Exception)
            {
                StartTime = DateTime.Now.AddDays(-31);
            }
            EndTime = StartTime;
        }

        //获取需要导入的文件
        void GetFiles()
        {
            GetFileList(ImapFiles, Public.Config.ImapTop);
            GetFileList(ImapSslFiles,Public.Config.ImapSslTop);
            GetFileList(Pop3Files,Public.Config.Pop3Top);
            GetFileList(Pop3SslFiles,Public.Config.Pop3SslTop);
            GetFileList(QueueFiles, Public.Config.QueueTop);
            GetFileList(SmtpMsaFile, Public.Config.SmtpMsaTop);
            GetFileList(SmtpSslFile, Public.Config.SmtpSslTop);
            GetFileList(SmtpFile,Public.Config.SmtpTop);
        }
        
        //干活
        void DoWork()
        {
            try
            {
                ImapFiles.ForEach(f => new ImapLogFileReader(f));
                ImapSslFiles.ForEach(f => new ImapSslLogFileReader(f));
                Pop3Files.ForEach(f => new Pop3LogFileReader(f));
                Pop3SslFiles.ForEach(f => new Pop3SslLogFileReader(f));
                QueueFiles.ForEach(f => new QueueLogFileReader(f));
                SmtpMsaFile.ForEach(f => new SmtpMsaLogFileReader(f));
                SmtpSslFile.ForEach(f => new SmtpSslLogFileReader(f));
                SmtpFile.ForEach(f => new ReciveLogFileReader(f, Public.Config.SmtpTop));
                LogUtils.InfoLog("执行完成");
            }
            catch(Exception ex)
            {
                LogUtils.ErrorLog(ex);
            }

        }

        void GetFileList(List<string> fileList,string type)
        {
          var list=  directory.GetFiles(string.Format("{0}.????????.????.log", type)).Where(w => w.LastWriteTime.ToUniversalTime() > StartTime);
            if (list.Any()) {
                EndTime = EndTime > list.Max(m => m.LastWriteTime) ? EndTime : list.Max(m => m.LastWriteTime);
            }
            foreach (var file in list)
            {
                fileList.Add(file.FullName);
            }
        }

        void ResetRecordTime()
        {
            Public.Mongo.DeleteMany<UpdateRecord>("record",null);
            Public.Mongo.InsertOne<UpdateRecord>("record", new UpdateRecord() { RecordTime = EndTime});
        }
    }

    public class UpdateRecord
    {
        public ObjectId _id { get; set; }
        public DateTime RecordTime { get; set; }
    }

    
}
