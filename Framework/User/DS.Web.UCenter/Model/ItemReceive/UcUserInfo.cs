// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserInfo.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 用户信息Model
    /// </summary>
    public class UcUserInfo : UcItemReceiveBase<UcUserInfo>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserInfo(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserInfo(XmlNode xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Uid
        /// </summary>
        public decimal Uid { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            Uid = Data.GetInt("0");
            UserName = Data.GetString("1");
            Mail = Data.GetString("2");
            CheckForSuccess("0");
        }
    }
}