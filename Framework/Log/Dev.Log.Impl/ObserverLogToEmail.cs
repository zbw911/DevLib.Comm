// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ObserverLogToEmail.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Net.Mail;

namespace Dev.Log.Impl
{
    /// <summary>
    /// Sends log events via email.
    /// </summary>
    /// <remarks>
    /// GoF Design Pattern: Observer.
    /// The Observer Design Pattern allows this class to attach itself to an
    /// the logger and 'listen' to certain events and be notified of the event. 
    /// </remarks>
    public class ObserverLogToEmail : ILog
    {
        private string _body;
        private string _from;
        private SmtpClient _smtpClient;
        private string _subject;
        private string _to;

        /// <summary>
        /// Constructor for the ObserverlogToEmail class
        /// </summary>
        /// <param name="from">From email address.</param>
        /// <param name="to">To email address.</param>
        /// <param name="subject">Email subject.</param>
        /// <param name="smtpClient">Smtp email client.</param>
        public ObserverLogToEmail(string from, string to, string subject, string body, SmtpClient smtpClient)
        {
            _from = from;
            _to = to;
            _subject = subject;
            _body = body;
            _smtpClient = smtpClient;
        }

        #region ILog Members

        /// <summary>
        /// Sends a log request via email.
        /// </summary>
        /// <remarks>
        /// Actual email 'Send' calls are commented out.
        /// Uncomment if you have the proper email privileges.
        /// </remarks>
        /// <param name="sender">Sender of the log request.</param>
        /// <param name="e">Parameters of the log request.</param>
        public void Log(object sender, LogEventArgs e)
        {
            string message = "[" + e.Date.ToString() + "] " +
                             e.SeverityString + ": " + e.Message;

            // Commented out for now. You need privileges to send email.
            // _smtpClient.Send(_from, _to, _subject, body);
        }

        #endregion
    }
}