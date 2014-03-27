// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ReadConfig.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.IO;
using System.Xml.Serialization;

namespace Dev.Framework.FileServer.Config
{
    /// <summary>
    /// read the config file 
    /// added by zbw911
    /// </summary>
    public class ReadConfig
    {
        private static string _configfile = "ImageServer.config";
        private static Configuration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configfile"></param>
        public ReadConfig(string configfile = "ImageServer.config")
        {
            _configfile = configfile;
            if (_configuration == null)
                InitConfig(_configfile);
        }

        /// <summary>
        /// 
        /// </summary>
        public static Configuration Configuration
        {
            get
            {
                if (_configuration == null)
                    InitConfig(_configfile);
                return _configuration;
            }
        }

        private static void InitConfig(string configfile)
        {
            string applicationBaseDirectory = null;
            //try
            //{
            applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //}
            //catch (Exception ex)
            //{
            //    //LogLog.Warn(declaringType, "Exception getting ApplicationBaseDirectory. ConfigFile property path [" + m_configFile + "] will be treated as an absolute path.", ex);
            //}

            //if (applicationBaseDirectory != null)
            //{
            // Just the base dir + the config file
            string fullPath2ConfigFile = Path.Combine(applicationBaseDirectory, configfile);
            //}
            //else
            //{
            //    fullPath2ConfigFile = m_configFile;
            //}

            var mySerializer = new XmlSerializer(typeof(Configuration));
            // To read the file, create a FileStream.
            FileStream myFileStream = File.OpenRead(fullPath2ConfigFile);

            // Call the Deserialize method and cast to the object type.
            _configuration = (Configuration)mySerializer.Deserialize(myFileStream);

            myFileStream.Close();
        }
    }
}