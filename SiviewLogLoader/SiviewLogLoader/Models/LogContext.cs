using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SiviewLogLoader.Models
{
    public class LogContext:DbContext
    {
        public LogContext() : base("SiviewLogDB")
        { }

        public DbSet<MMTRX_SUMEntity> MMTRX_SUMEntities { get; set; }

        public DbSet<MMTRX_SUMFilesEntity> MMTRX_SUMFiles { get; set; }
    }
}
