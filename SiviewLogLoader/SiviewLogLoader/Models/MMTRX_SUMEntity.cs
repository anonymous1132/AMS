using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SiviewLogLoader.Models
{
    [Table("MMTRX_SUM")]
    public class MMTRX_SUMEntity
    {
        [Key]
        public int EntityID { get; set; }

        public DateTime Log_Time { get; set; }

        public double RESP_max { get; set; }

        public double RESP_mean { get; set; }

        public double RESP_min { get; set; }

        public double RESP_sum { get; set; }

        [Required]
        [MaxLength(128)]
        public string RETN_CODE { get; set; }

        [Required]
        [MaxLength(128)]
        public string TRX_ID { get; set; }

        public int TRX_ID_count { get; set; }
    }
}
