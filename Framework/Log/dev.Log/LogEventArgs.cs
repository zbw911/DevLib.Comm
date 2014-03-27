// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：LogEventArgs.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;

namespace Dev.Log
{
    /// <summary>
    /// Contains log specific event data for log events.
    /// </summary>
    public class LogEventArgs : EventArgs
    {
        /// <summary>
        /// Constructor of LogEventArgs.
        /// </summary>
        /// <param name="severity">Log severity.</param>
        /// <param name="message">Log message</param>
        /// <param name="exception">Inner exception.</param>
        /// <param name="date">Log date.</param>
        public LogEventArgs(LogSeverity severity, string message, Exception exception, DateTime date)
        {
            Severity = severity;
            Message = message;
            Exception = exception;
            Date = date;
        }

        /// <summary>
        /// Gets and sets the log severity.
        /// </summary>        
        public LogSeverity Severity { get; private set; }

        /// <summary>
        /// Gets and sets the log message.
        /// </summary>        
        public string Message { get; private set; }

        /// <summary>
        /// Gets and sets the optional inner exception.
        /// </summary>        
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets and sets the log date and time.
        /// </summary>        
        public DateTime Date { get; private set; }

        /// <summary>
        /// Friendly string that represents the severity.
        /// </summary>
        public String SeverityString
        {
            get { return Severity.ToString("G"); }
        }

        /// <summary>
        /// LogEventArgs as a string representation.
        /// </summary>
        /// <returns>String representation of the LogEventArgs.</returns>
        public override String ToString()
        {
            return "" + Date
                   + " - " + SeverityString
                   + " - " + Message
                   + " - " + Exception;
        }
    }
}