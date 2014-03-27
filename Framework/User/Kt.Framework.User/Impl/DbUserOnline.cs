// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DbUserOnline.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;

namespace Dev.Framework.User.Impl
{
    /// <summary>
    ///     本方法用于其它扩展,
    ///     这里再一次强调的是（给未来实现的人，我相信你很想当然的就在这里实现了，然后就开始耦合什么数据库了，什么业务了，哈哈），
    ///     这个类不应该，是在这里实现的，应该去另一个程序集中实现，然后再与接口进行绑定。
    /// </summary>
    internal class DbUserOnline : IUserOnline
    {
        #region IUserOnline Members

        public void AddUser(OnlineUserInfo userinfo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OnlineUserInfo> GetOnlineList(int pagesize = 12, int page = 1)
        {
            throw new NotImplementedException();
        }

        public bool IsOnLine(decimal uid)
        {
            throw new NotImplementedException();
        }

        public void RemoveUser(decimal uid)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}