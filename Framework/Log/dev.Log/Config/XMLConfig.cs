// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：XMLConfig.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;
using Dev.Comm;
using Dev.Comm.Utils;
using Dev.Comm.XML;

namespace Dev.Log.Config
{
    /// <summary>
    /// 使用  Log.config 配置日志 
    /// </summary>
    public class XMLConfig
    {
        public bool InitConfig(string configfile = "Log.config")
        {



            string applicationBaseDirectory = null;
            applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string fullPath2ConfigFile = Path.Combine(applicationBaseDirectory, configfile);
            //如果存在配置文件就进行配置，否则就跳过
            if (!File.Exists(fullPath2ConfigFile))
                return false;


            var xml = new XmlHelper();
            xml.LoadXML(fullPath2ConfigFile, XmlHelper.LoadType.FromLocalFile);

            InitLogApender(xml);

            InitLogSeverity(xml);

            return true;
        }

        private static void InitLogSeverity(XmlHelper xml)
        {
            XmlNode node = xml.GetFirstChildNodeFromCriteria("//logseverity");

            if (node == null) return;

            string strSeverity = node.Attributes["value"].Value;

            if (string.IsNullOrEmpty(strSeverity))
                return;

            LogSeverity severity;
            if (Enum.TryParse(strSeverity, true, out severity))
            {
                SingletonLogger.Instance.Severity = severity;
                //
                //Console.WriteLine(severity.ToString());
            }
            else
            {
                throw new Exception("日志安全级别配置错误");
            }
        }

        private static void InitLogApender(XmlHelper xml)
        {
            IList<XmlNode> list = xml.GetChildNodesFromCriteria("//log");

            foreach (XmlNode item in list)
            {
                string nodename = item.Attributes["name"].Value;
                string type = item.Attributes["type"].Value;

                string asmfile = "";
                if (item.Attributes["asmfile"] != null && !string.IsNullOrWhiteSpace(item.Attributes["asmfile"].Value))
                    asmfile = item.Attributes["asmfile"].Value;


                Type logtype;
                if (!string.IsNullOrEmpty(asmfile))
                {
                    logtype = CreateType(type, asmfile);
                }
                else
                {
                    logtype = Type.GetType(type);
                }
                if (logtype == null)
                    throw new Exception("logtype=" + type + "不存在");

                object logObj = Activator.CreateInstance(logtype);

                IList<XmlNode> paramlist = XmlHelper.GetChildNodesFromCriteria(item,
                                                                               "//log[@name='" + nodename + "']//param");
                //Console.WriteLine(value);

                //AppDomain.CurrentDomain.BaseDirectory


                foreach (XmlNode param in paramlist)
                {

                    string propertyName = param.Attributes["name"].Value;
                    string propertyValue = param.Attributes["value"].Value;

                    if (AsmUtil.ExistPropertyName(logObj, propertyName))
                        AsmUtil.SetPropertyValue(logObj, propertyName, propertyValue, null);

                    //var pros = logObj.GetType().GetProperties();
                }

                SingletonLogger.Instance.Attach((ILog)logObj);
            }
        }

        public static Type CreateType(string typeName, string AsmPath)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AsmPath);

            path = Path.GetFullPath(path);

            Assembly asm = AsmUtil.GetAssemblyFromCurrentDomain(path);

            Type type = asm.GetType(typeName);




            return type;
        }
    }
}