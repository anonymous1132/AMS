using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DefectTranslator
{
    public class EDAWaferEntity
    {
        public string WaferID { get; set; }

        public double SlotID { get; set; }

        public double ADC { get; set; }

        public double DDC { get; set; }
        
        public double RDC { get; set; }

        public DateTime InspectionTime { get; set; }
    }
}
