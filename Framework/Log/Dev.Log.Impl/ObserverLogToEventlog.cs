// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ObserverLogToEventlog.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Dev.Log.Impl
{
    /// <summary>
    /// Writes log events to the Windows event log.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToEventlog : ILog
    {
        #region ILog Members

        /// <summary>
        /// Write a log request to the event log.
        /// </summary>
        /// <remarks>
        /// Actual event log write entry statements are commented out.
        /// Uncomment if you have the proper privileges.
        /// </remarks>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = "[" + e.Date.ToString() + "] " +

                             GetExceptionMsg(e.Exception, e.SeverityString + ": " + e.Message + "; ");


            var eventLog = new EventLog();
            eventLog.Source = GetSource();// MethodBase.GetCurrentMethod().DeclaringType.Name;// "Patterns In Action";

            // Map severity level to an Windows EventLog entry type
            var type = EventLogEntryType.Error;
            if (e.Severity < LogSeverity.Warning) type = EventLogEntryType.Information;
            if (e.Severity < LogSeverity.Error) type = EventLogEntryType.Warning;

            // In try catch. You will need proper privileges to write to eventlog.
            //try
            //{
            eventLog.WriteEntry(message, type);
            //}
            //catch
            //{
            //    /* do nothing */
            //}
        }

        static string GetExceptionMsg(Exception ex, string backStr)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("****************************异常文本****************************");
            sb.AppendLine("【出现时间】：" + DateTime.Now.ToString());
            if (ex != null)
            {
                sb.AppendLine("【异常类型】：" + ex.GetType().Name);
                sb.AppendLine("【异常信息】：" + ex.Message);
                sb.AppendLine("【堆栈调用】：" + ex.StackTrace);
            }

            sb.AppendLine("" + backStr);
            sb.AppendLine("***************************************************************");
            var error = sb.ToString();

            //Dev.Log.Loger.Error(error);

            return error;
        }


        private static string GetSource()
        {
            string name;

            var frame = new StackFrame(5);

            var method = frame.GetMethod();      //取得调用函数

            if (method != null)
            {
                name = method.DeclaringType + "." + method.Name;
            }
            else
            {
                name = "NULL Frame";
            }

            return name;
        }

        #endregion
    }
}