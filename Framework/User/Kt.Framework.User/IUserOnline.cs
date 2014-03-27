// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUserOnline.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace Dev.Framework.User
{
    /// <summary>
    ///     用户在线接口
    /// </summary>
    public interface IUserOnline
    {
        void AddUser(OnlineUserInfo userinfo);
        IEnumerable<OnlineUserInfo> GetOnlineList(int pagesize = 12, int page = 1);
        bool IsOnLine(decimal uid);
        void RemoveUser(decimal uid);
    }
}