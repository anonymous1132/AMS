using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public delegate void TranslateDelegate(string filePath,string fileSuffix);

    public abstract class SiffFileOperator:FileOperator
    {
        public  string SIFF_PATH;
        public  string SIFF_HISTORY_PATH;
        public  string SPEC_PATH;

        protected string SiffPath
        {
            get { return exeDirctory + SIFF_PATH; }
        }

        protected string SiffHistoryPath
        {
            get { return exeDirctory + SIFF_HISTORY_PATH; }
        }

        protected string SpecPath
        {
            get { return  SPEC_PATH; }
        }

        /// <summary>
        /// Translate操作
        /// </summary>
        protected abstract void TranslateFile();

    }
}
