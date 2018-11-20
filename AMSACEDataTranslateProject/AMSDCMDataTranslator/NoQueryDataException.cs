using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMSDCMDataTranslator
{
    public class NoQueryDataException:ApplicationException
    {
        private string error;
        private Exception innerException;

        public NoQueryDataException()
        { }

        public NoQueryDataException(string msg) : base(msg)
        {
            error = msg;
        }

        public string GetError()
        {
            return error;
        }

        //带有一个字符串参数和一个内部异常信息参数的构造函数
        public NoQueryDataException(string msg, Exception exception) : base(msg)
        {
            error = msg;
            innerException = exception;
        }
}
}
