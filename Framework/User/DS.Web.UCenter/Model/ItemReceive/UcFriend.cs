// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcFriend.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 好友Model
    /// </summary>
    public class UcFriend : UcItemReceiveBase<UcFriend>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcFriend(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcFriend(XmlNode xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 用户 ID
        /// </summary>
        public decimal Uid { get; set; }

        /// <summary>
        /// 好友用户 ID
        /// </summary>
        public int FriendId { get; set; }

        /// <summary>
        /// 方向
        /// </summary>
        public FriendDirection Direction { get; set; }

        /// <summary>
        /// 好友用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            Uid = Data.GetInt("uid");
            FriendId = Data.GetInt("friendid");
            Direction = (FriendDirection) Data.GetInt("direction");
            UserName = Data.GetString("username");
            CheckForSuccess("uid", "friendid");
        }
    }
}