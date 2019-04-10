using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogParser
{
    public class Pop3LogFileReader:Pop3SslLogFileReader
    {
        public Pop3LogFileReader(string filepath) : base(filepath)
        { }

        public override void PutDB()
        {
            var db = Public.Mongo;
            foreach (var entity in LogObj.LogEntities)
            {
                var builder = db.GetFilter<SmtpSslLogEntity>();
                var filter = builder.Eq("StartTime", entity.StartTime) & builder.Eq("Mask", entity.Mask);
                if (db.CountQuery<SmtpSslLogEntity>(Public.Config.Pop3Top, filter) == 0)
                {
                    db.InsertOne<SmtpSslLogEntity>(Public.Config.Pop3Top, entity);
                }
            }
        }
    }
}
