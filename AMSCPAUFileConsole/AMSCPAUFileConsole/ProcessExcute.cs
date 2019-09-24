using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AMSCPAUFileConsole
{
    public class ProcessExcute
    {
        public static bool StartProcess(string runFilePath, string para)
        {
            Process process = new Process();//创建进程对象    
            ProcessStartInfo startInfo = new ProcessStartInfo(runFilePath, para)
            {
                UseShellExecute = false,    //是否使用操作系统shell启动
                RedirectStandardInput = true,//接受来自调用程序的输入信息
                RedirectStandardOutput = true,//由调用程序获取输出信息
                RedirectStandardError = true,//重定向标准错误输出
                CreateNoWindow = true//不显示程序窗口
            }; 
            process.StartInfo = startInfo;
            process.Start();
            return true;
        }
    }
}
