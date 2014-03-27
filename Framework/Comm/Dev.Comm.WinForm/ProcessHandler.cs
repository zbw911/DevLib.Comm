// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年11月13日 16:47
//  
//  修改于：2013年11月13日 16:49
//  文件名：Dev.Libs/Dev.Comm.WinForm/ProcessHandler.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Diagnostics;

namespace Dev.Comm.WinForm
{
    /// <summary>
    /// 进程处理
    /// </summary>
    public static class ProcessHandler
    {
        public static void Kill(string processname)
        {
            Process[] processes = Process.GetProcessesByName(processname);
            foreach (Process p in processes)
            {
                p.Kill();
                p.Close();
            }
        }


        public static void Kill(string[] processes)
        {
            foreach (var process in processes)
            {
                Kill(process);
            }
        }
    }
}