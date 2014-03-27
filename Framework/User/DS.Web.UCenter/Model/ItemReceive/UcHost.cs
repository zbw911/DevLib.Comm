// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcHost.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// HostModel
    /// </summary>
    public class UcHost : UcItemReceiveBase<UcHost>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcHost(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcHost(XmlNode xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 域名
        /// </summary>
        public string Domain { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            Id = Data.GetInt("id");
            Domain = Data.GetString("domain");
            Ip = Data.GetString("ip");
            CheckForSuccess("id");
        }
    }
}