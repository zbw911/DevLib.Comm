// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcBadWord.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 不良词语Model
    /// </summary>
    public class UcBadWord : UcItemReceiveBase<UcBadWord>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcBadWord(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcBadWord(XmlNode xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>
        public string Admin { get; set; }

        /// <summary>
        /// 查找字符串
        /// </summary>
        public string Find { get; set; }

        /// <summary>
        /// 替换字符串
        /// </summary>
        public string Replacement { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            Id = Data.GetInt("id");
            Admin = Data.GetString("admin");
            Find = Data.GetString("find");
            Replacement = Data.GetString("replacement");
            CheckForSuccess("id");
        }
    }
}