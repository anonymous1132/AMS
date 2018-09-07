using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator.Models
{
    public interface ISiffable
    {
        string DataSource
        {
            get;
        }

        string FormatVersion
        {
            get;
        }

        void GetData(string filePath,string specPath);

        /// <summary>
        /// 生成Siff文件
        /// </summary>
        /// <param name="filePath">Siff文件夹路径</param>
        /// <returns>siff文件名</returns>
        string WriteSiff(string filePath);

    }
}
