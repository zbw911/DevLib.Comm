// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ObserverLogToConsole.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Diagnostics;

namespace Dev.Log.Impl
{
    /// <summary>
    /// Writes log events to the diagnostic trace.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToConsole : ILog
    {
        /// <summary>
        /// 
        /// </summary>
        public string Prex { get; set; }

        #region ILog Members

        /// <summary>
        /// Writes a log request to the diagnostic trace on the page.
        /// </summary>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            // Example code of entering a log event to output console
            string message = "[" + e.Date.ToString() + "] " +
                             e.SeverityString + ": " + e.Message;

            // Writes message to debug output window
            Debugger.Log(0, null, Prex + message + "\r\n\r\n");
            //System.Console.WriteLine(Prex + message);
        }

        #endregion
    }
}