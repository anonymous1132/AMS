using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace SiviewLogLoader.Models
{
    [Table("MMTRX_SUM_Files")]
    public class MMTRX_SUMFilesEntity
    {
        [Key]
        public int EntityID { get; set; }

        public string FileName { get; set; }

    }
}
