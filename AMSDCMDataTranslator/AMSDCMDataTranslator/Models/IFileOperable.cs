using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public interface IFileOperable
    {
        void OperateFiles(string fileSuffix);
    }
}
