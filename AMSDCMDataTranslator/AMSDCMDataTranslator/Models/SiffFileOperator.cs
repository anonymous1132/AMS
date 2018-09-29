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
        /// <param name="filePath">数据文件全路径</param>
        /// <param name="fileSuffix">数据文件指定后缀</param>  
        protected abstract void TranslateFile(string filePath,string fileSuffix);


        protected virtual void OperateFiles(TranslateDelegate translateDelegate,string fileSuffix)
        {
        }
    }
}
