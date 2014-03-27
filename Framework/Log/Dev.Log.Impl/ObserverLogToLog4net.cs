// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ObserverLogToLog4net.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using log4net;

namespace Dev.Log.Impl
{
    /// <summary>
    /// 使用Log4NET
    /// </summary>
    // ReSharper disable InconsistentNaming
    public class ObserverLogToLog4net : ILog
    // ReSharper restore InconsistentNaming
    {

        /// <summary>
        /// 默认使用 "log4net.config"
        /// </summary>
        public ObserverLogToLog4net()
            : this("log4net.config")
        {

        }

        /// <summary>
        /// 指定配置文件
        /// </summary>
        /// <param name="log4NetConfig"></param>
        /// <exception cref="Exception"></exception>
        public ObserverLogToLog4net(string log4NetConfig)
        {

            //如果是绝对路径
            if (Path.IsPathRooted(log4NetConfig))
            {

                if (File.Exists(log4NetConfig))
                    log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(log4NetConfig));
                else
                    throw new FileNotFoundException(log4NetConfig);

                return;
            }


            //对于文件名形式的
            var dirstart = AppDomain.CurrentDomain.BaseDirectory;


            //从根查找
            string[] file = Directory.GetFiles(dirstart, log4NetConfig);

            //未找到，找全部
            if (file.Length == 0)
            {
                throw new FileNotFoundException(log4NetConfig);
                //file = Directory.GetFiles(dirstart, log4NetConfig, SearchOption.AllDirectories);
            }


            //if (file == null)
            //{
            //    throw new Exception(log4NetConfig + " not Exist ");
            //}

            //if (file.Length > 1)
            //{
            //    throw new Exception("存在多个" + log4NetConfig + "=>" +
            //                        file.Aggregate("", (current, fly) => current + (fly + ",")));
            //}




            log4net.Config.XmlConfigurator.ConfigureAndWatch(new System.IO.FileInfo(file[0]));


        }

        #region ILog Members

        public void Log(object sender, LogEventArgs e)
        {
            switch (e.Severity)
            {
                case LogSeverity.Debug:
                    getLog().Debug(e.Message, e.Exception);
                    break;
                case LogSeverity.Error:
                    getLog().Error(e.Message, e.Exception);
                    break;
                case LogSeverity.Fatal:
                    getLog().Fatal(e.Message, e.Exception);
                    break;
                case LogSeverity.Info:
                    getLog().Info(e.Message, e.Exception);
                    break;
                case LogSeverity.Warning:
                    getLog().Warn(e.Message, e.Exception);
                    break;
            }
        }



        #endregion

        private static log4net.ILog getLog()
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
            log4net.ILog log = log4net.LogManager.GetLogger(name);
            return log;
        }
    }
}