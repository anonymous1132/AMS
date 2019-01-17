using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Caojin.Common
{
    public class FileHelper
    {
        public FileHelper()
        { }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="srcFile">复制的源文件</param>
        /// <param name="destFile">复制后目标文件,允许覆盖同名文件</param>
        public static void Copy(string srcFile, string destFile)
        {
            File.Copy(srcFile, destFile, true);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="srcFile">源文件</param>
        /// <param name="destFile">移动后文件</param>
        public static void Move(string srcFile, string destFile)
        {
            if (!File.Exists(srcFile))
            {
                throw new Exception("源文件:" + srcFile + "不存在。");
            }
            if (File.Exists(destFile))
            {
                File.Delete(destFile);
            }
            File.Move(srcFile, destFile);
        }


        public static List<string> ReadTxtFileToLineList(string FilePath)
        {
            List<string> list = new List<string>();
            using (StreamReader sr = new StreamReader(FilePath))
            {
                while (!sr.EndOfStream)
                {
                    list.Add(sr.ReadLine());
                }
            }
            return list;
        }
    }

    /// <summary>
    /// 文件分割符
    /// </summary>
    public class FileFilter
    {
        public string Description
        {
            get;
            set;
        }

        /// <summary>
        /// 使用*.Name的格式
        /// </summary>
        public string Extension
        {
            get;
            set;
        }

        public string Filter
        {
            get { return Description + "|" + Extension; }
        }
    }
}
