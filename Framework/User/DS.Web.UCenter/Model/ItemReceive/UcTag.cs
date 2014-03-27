// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcTag.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// TagModel
    /// </summary>
    public class UcTag : UcItemReceiveBase<UcTag>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcTag(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcTag(XmlNode xml)
            : base(xml)
        {
        }

        /// <summary>
        /// URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 扩展数据
        /// </summary>
        public IDictionary Extra { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            Url = Data.GetString("url");
            Subject = Data.GetString("subject");
            Extra = Data.GetHashtable("extra");
            CheckForSuccess("url");
        }
    }
}