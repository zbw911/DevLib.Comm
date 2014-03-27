// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Loger.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;

namespace Dev.Log
{
    public class Loger
    {
        /// <summary>
        /// Log a message when severity level is "Debug" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Debug(string message, Exception exception = null)
        {
            SingletonLogger.Instance.Debug(message, exception);
        }


        /// <summary>
        /// Log a message when severity level is "Info" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Info(string message, Exception exception = null)
        {
            SingletonLogger.Instance.Info(message, exception);
        }


        /// <summary>
        /// Log a message when severity level is "Warning" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Warning(string message, Exception exception = null)
        {
            SingletonLogger.Instance.Warning(message, exception);
        }


        /// <summary>
        /// Log a message when severity level is "Error" or higher.
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Error(string message, Exception exception = null)
        {
            var instance = SingletonLogger.Instance;
            instance.Error(message, exception);
        }

        public static void Error(Exception excep)
        {
            Error("", excep);
        }


        /// <summary>
        /// Log a message when severity level is "Fatal"
        /// </summary>
        /// <param name="message">Log message.</param>
        /// <param name="exception">Inner exception.</param>
        public static void Fatal(string message, Exception exception)
        {
            SingletonLogger.Instance.Fatal(message, exception);
        }
    }
}