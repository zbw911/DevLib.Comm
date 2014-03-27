// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：LogSeverity.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Log
{
    /// <summary>
    /// Enumeration of log severity levels.
    /// </summary>
    public enum LogSeverity
    {
        /// <summary>
        /// Severity level of "Debug"
        /// </summary>
        Debug = 1,

        /// <summary>
        /// Severity level of "Info"
        /// </summary>
        Info = 2,

        /// <summary>
        /// Severity level of "Warning"
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Severity level of "Error"
        /// </summary>
        Error = 4,

        /// <summary>
        /// Severity level of "Fatal"
        /// </summary>
        Fatal = 5
    }
}