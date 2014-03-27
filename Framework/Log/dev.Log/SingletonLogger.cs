// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：SingletonLogger.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using Dev.Log.Config;

namespace Dev.Log
{
    /// <summary>
    /// Singleton logger class through which all log events are processed.
    /// </summary>
    /// <remarks>
    /// GoF Design Patterns: Singleton, Observer.
    /// </remarks>
    internal sealed class SingletonLogger
    {
        #region Delegates

        /// <summary>
        /// Delegate event handler that hooks up requests.
        /// </summary>
        /// <param name="sender">Sender of the event.</param>
        /// <param name="e">Event arguments.</param>
        /// <remarks>
        /// GoF Design Pattern: Observer, Singleton.
        /// The Observer Design Pattern allows Observer classes to attach itself to 
        /// this Logger class and be notified if certain events occur. 
        /// 
        /// The Singleton Design Pattern allows the application to have just one
        /// place that is aware of the application-wide LogSeverity setting.
        /// </remarks>
        public delegate void LogEventHandler(object sender, LogEventArgs e);

        #endregion

        // These booleans are used strictly to improve performance.
        private bool _isDebug = true;
        private bool _isError = true;
        private bool _isFatal = true;
        private bool _isInfo = true;
        private bool _isWarning = true;
        private LogSeverity _severity = LogSeverity.Debug;

        /// <summary>
        /// Gets and sets the severity level of logging activity.
        /// </summary>
        public LogSeverity Severity
        {
            get { return _severity; }
            set
            {
                _severity = value;

                // Set booleans to help improve performance
                var severity = (int)_severity;

                _isDebug = ((int)LogSeverity.Debug) >= severity ? true : false;
                _isInfo = ((int)LogSeverity.Info) >= severity ? true : false;
                _isWarning = ((int)LogSeverity.Warning) >= severity ? true : false;
                _isError = ((int)LogSeverity.Error) >= severity ? true : false;
                _isFatal = ((int)LogSeverity.Fatal) >= severity ? true : false;
            }
        }

        /// <summary>
        /// The Log event.
        /// </summary>
        public event LogEventHandler Log;

        /// <summary>
        /// Log a message when severity level is "Debug" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Debug(string message)
        {
            if (_isDebug)
                Debug(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Debug" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Debug(string message, Exception exception)
        {
            if (_isDebug)
                OnLog(new LogEventArgs(LogSeverity.Debug, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Info" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Info(string message)
        {
            if (_isInfo)
                Info(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Info" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Info(string message, Exception exception)
        {
            if (_isInfo)
                OnLog(new LogEventArgs(LogSeverity.Info, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Warning" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        public void Warning(string message)
        {
            if (_isWarning)
                Warning(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Warning" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Warning(string message, Exception exception)
        {
            if (_isWarning)
                OnLog(new LogEventArgs(LogSeverity.Warning, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Log a message when severity level is "Error" or higher.
        /// </summary>
        /// <param name="message">Log message</param>
        public void Error(string message)
        {
            if (_isError)
                Error(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Error" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Error(string message, Exception exception)
        {
            if (_isError)
                OnLog(new LogEventArgs(LogSeverity.Error, message, exception, DateTime.Now));
        }

        public void Error(Exception excep)
        {
            Error("", excep);
        }

        /// <summary>
        /// Log a message when severity level is "Fatal"
        /// </summary>
        /// <param name="message">Log message</param>
        public void Fatal(string message)
        {
            if (_isFatal)
                Fatal(message, null);
        }

        /// <summary>
        /// Log a message when severity level is "Fatal"
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public void Fatal(string message, Exception exception)
        {
            if (_isFatal)
                OnLog(new LogEventArgs(LogSeverity.Fatal, message, exception, DateTime.Now));
        }

        /// <summary>
        /// Invoke the Log event.
        /// </summary>
        /// <param name="e">Log event parameters.</param>
        public void OnLog(LogEventArgs e)
        {
            if (Log != null)
            {
                Log(this, e);
            }
        }

        /// <summary>
        /// Attach a listening observer logging device to logger.
        /// </summary>
        /// <param name="observer">Observer (listening device).</param>
        public void Attach(ILog observer)
        {
            Log += observer.Log;
        }

        /// <summary>
        /// Detach a listening observer logging device from logger.
        /// </summary>
        /// <param name="observer">Observer (listening device).</param>
        public void Detach(ILog observer)
        {
            Log -= observer.Log;
        }

        #region The Singleton definition

        /// <summary>
        /// The one and only Singleton Logger instance. 
        /// </summary>
        private static readonly SingletonLogger instance = new SingletonLogger();

        static SingletonLogger()
        {
            try
            {
                new XMLConfig().InitConfig();
            }
            catch (Exception e)
            {
                var s = e.Message;
                throw;
            }

        }

        /// <summary>
        /// Private constructor. Initializes default severity to "Error".
        /// </summary>
        private SingletonLogger()
        {

            // Default severity is Error level
            //Severity = LogSeverity.Error;
        }

        /// <summary>
        /// Gets the instance of the singleton logger object.
        /// </summary>
        public static SingletonLogger Instance
        {
            get
            {

                return instance;
            }
        }

        #endregion
    }
}