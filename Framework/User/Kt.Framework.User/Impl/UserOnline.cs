// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UserOnline.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Framework.User
{
    public class UserOnline : IUserOnline
    {
        private static List<OnlineUserInfo> _onlineUser = new List<OnlineUserInfo>();
        private static readonly object lockobj = new object();

        internal IEnumerable<OnlineUserInfo> OnlineUser
        {
            get
            {
                lock (lockobj)
                {
                    return _onlineUser;
                }
            }
        }

        #region IUserOnline Members

        /// <summary>
        ///     加入一个用户进入在线列表
        /// </summary>
        /// <param name="userinfo"></param>
        void IUserOnline.AddUser(OnlineUserInfo userinfo)
        {
            if (!(_onlineUser.Where(p => p.Uid == userinfo.Uid).Count() > 0))
            {
                _onlineUser.Add(userinfo);
                SortUser();
            }
        }

        /// <summary>
        ///     取得一个人的在线状态
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        bool IUserOnline.IsOnLine(decimal uid)
        {
            if (OnlineUser.Count() == 0)
                return false;

            OnlineUserInfo online = OnlineUser.FirstOrDefault(x => x.Uid == uid);

            if (online == null) return false;

            //if (online.Uid != uid) return false;

            //假设最后一次是活动在20分钟内
            if (online.LASTActive < DateTime.Now.AddMinutes(-20))
            {
                ((IUserOnline) this).RemoveUser(uid);
                return false;
            }

            return true;
        }

        /// <summary>
        ///     返回当前在线的用户列表
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        IEnumerable<OnlineUserInfo> IUserOnline.GetOnlineList(int pagesize, int page)
        {
            if (OnlineUser.Count() == 0)
                return null;
            return OnlineUser.Skip(pagesize*(page - 1)).Take(pagesize);
        }

        /// <summary>
        ///     移除
        /// </summary>
        /// <param name="uid"></param>
        void IUserOnline.RemoveUser(decimal uid)
        {
            if (OnlineUser.Count() == 0)
                return;
            if (OnlineUser == null) return;

            OnlineUserInfo user = OnlineUser.FirstOrDefault(x => x.Uid == uid);
            if (user != null)
            {
                _onlineUser.Remove(user);
            }
        }

        #endregion

        /// <summary>
        ///     更新在线状态
        /// </summary>
        /// <param name="uid"></param>
        internal void FreshUser(decimal uid)
        {
            OnlineUserInfo online = OnlineUser.FirstOrDefault(x => x.Uid == uid);

            if (online == null)
            {
                ((IUserOnline) this).AddUser(new OnlineUserInfo {Uid = uid, LASTActive = DateTime.Now});
                return;
            }
            ;

            online.LASTActive = DateTime.Now;
        }

        /// <summary>
        ///     排序
        /// </summary>
        private void SortUser()
        {
            if (OnlineUser.Count() == 0)
                return;
            _onlineUser = OnlineUser.OrderByDescending(x => x.LASTActive).ToList();
        }


        //IEnumerable<UserInfo> IUserOnline.GetOnlineList(int pagesize, int page)
        //{
        //    throw new NotImplementedException();
        //}

        //bool IUserOnline.IsOnLine(decimal uid)
        //{
        //    throw new NotImplementedException();
        //}

        //void IUserOnline.RemoveUser(decimal uid)
        //{
        //    throw new NotImplementedException();
        //}
    }
}