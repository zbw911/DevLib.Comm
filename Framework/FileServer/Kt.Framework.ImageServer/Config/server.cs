// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：server.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Xml.Serialization;

namespace Dev.Framework.FileServer.Config
{
    /// <summary>
    /// Server Struct Map to Config
    /// added by zbw911
    /// </summary>
    public class Server
    {
        /// <summary>
        /// 服务器编号
        /// </summary>
        [XmlAttribute]
        public int id { get; set; }
        /// <summary>
        /// 是否使用中
        /// </summary>
        [XmlAttribute]
        public bool used { get; set; }
        /// <summary>
        /// 服务器IP
        /// </summary>
        [XmlAttribute]
        public string hostip { get; set; }
        /// <summary>
        /// 起始目录
        /// </summary>
        [XmlAttribute]
        public string startdirname { get; set; }
        /// <summary>
        /// 共享用户名
        /// </summary>
        [XmlAttribute]
        public string username { get; set; }
        /// <summary>
        /// 共享密码
        /// </summary>
        [XmlAttribute]
        public string password { get; set; }
        /// <summary>
        /// 目录对应的URL
        /// </summary>
        [XmlAttribute]
        public string serverurl { get; set; }
    }
}