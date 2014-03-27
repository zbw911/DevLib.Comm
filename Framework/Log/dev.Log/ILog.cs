// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ILog.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Log
{
    /// <summary>
    /// Defines a single method to write requested log events to an output device.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Write a log request to a given output device.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        void Log(object sender, LogEventArgs e);
    }
}