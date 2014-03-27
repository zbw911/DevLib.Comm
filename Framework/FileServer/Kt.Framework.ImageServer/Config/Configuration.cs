// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Configuration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Xml.Serialization;

namespace Dev.Framework.FileServer.Config
{
    /// <summary>
    /// Configs
    /// added by zbw911
    /// </summary>
    [XmlRoot("configuration")]
    public class Configuration
    {
        [XmlArray("servers")] [XmlArrayItem("server")] public Server[] Servers;
    }
}