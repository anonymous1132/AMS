using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectTranslator
{
    public class EDAEntity
    {
        public string EqpID { get; set; }

        public string LotID { get; set; }

        public string RecipeID { get; set; }

        public List<EDAWaferEntity> WaferEntities { get; set; } = new List<EDAWaferEntity>(); 

        public string FileName { get { return string.Format("{0}_{1}_{2}.xml",EqpID,LotID,RecipeID); } }
    }
}
