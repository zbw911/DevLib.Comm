// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：MemForbidden.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    ///     内存实现
    /// </summary>
    public class MemForbidden : IForbidden
    {
        /// <summary>
        ///     限定列表
        /// </summary>
        private static List<UserForbiddenModel> list = new List<UserForbiddenModel>();

        private static readonly object lockobj = new object();


        internal IEnumerable<UserForbiddenModel> UserList
        {
            get
            {
                lock (lockobj)
                {
                    return list;
                }
            }
        }

        #region IForbidden Members

        /// <summary>
        ///     从列表中排除
        /// </summary>
        /// <param name="UserForbiddenModel">从队列中排除</param>
        public void DeList(UserForbiddenModel UserForbiddenModel)
        {
            list.Remove(UserForbiddenModel);
        }

        /// <summary>
        ///     加入列表
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        public void EnList(UserForbiddenModel UserForbiddenModel)
        {
            list.Add(UserForbiddenModel);
            SortList();
        }

        /// <summary>
        ///     输入正确后，清除列表中此用户的记录，并且清除失效的数据
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public void ClearForbiddenList(decimal uid)
        {
            UserForbiddenModel user = list.Where(x => x.Uid == uid).FirstOrDefault();
            DeList(user);
            //以下，如果另外一个人在调用此方法，循环删除的时候，将另外的人的记录删除了，会出现问题
            //foreach (var u in UserList)
            //{
            //    if (DateTime.Now >= u.LastTime.AddMinutes(ForbiddenConfig.KEEPTIME))
            //    {
            //        this.DeList(u);
            //    }
            //}
        }

        /// <summary>
        ///     根据uid判断此用户是否在禁用列表中
        /// </summary>
        /// <param name="Uid"></param>
        /// <returns>false：表示用户不在禁用列表中， true：表示用户在禁用列表中</returns>
        public bool IsForbidden(decimal Uid)
        {
            //判断在禁用列表中是否存在这个对象
            UserForbiddenModel user = UserList.FirstOrDefault(x => x.Uid == Uid);
            if (user != null)
            {
                if (DateTime.Now >= user.LastTime.AddMinutes(ForbiddenConfig.KEEPTIME)) //未超过最小时间间隔
                {
                    FreshList(new UserForbiddenModel {Uid = Uid, LastTime = DateTime.Now, ErrorCount = 1});
                    return false;
                }
                else
                {
                    if (user.ErrorCount >= ForbiddenConfig.MAXERROR)
                    {
                        return true;
                    }
                    else
                    {
                        FreshList(new UserForbiddenModel
                                      {Uid = Uid, LastTime = DateTime.Now, ErrorCount = user.ErrorCount + 1});
                        return false;
                    }
                }
            }
            else
            {
                EnList(new UserForbiddenModel {Uid = Uid, LastTime = DateTime.Now, ErrorCount = 1});
                return false;
            }

            /*首先根据传入的Uid判断在禁用列表中是否存在这个对象
                如果存在：判断此对象的_LastTime和当前时间比较是否超过了KEEPTIME
             *      如果是：更新此用户，_LastTime=当前时间，_ErrorCount=1，返回false
             *          否：
             *              判断此用户的_ErrorCount是否大于等于MAXERROR
             *                  如果是：则直接返回true，表示该用户已经被禁用
             *                  如果不是：更新此用户，_LastTime=当前时间，_ErrorCount=_ErrorCount+1，返回false
             *                  
             *  如不存在：
             *          在列表中新增此对象，_LastTime=当前时间，_ErrorCount=1，返回false
             */
        }

        #endregion

        /// <summary>
        ///     刷新列表
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        private void FreshList(UserForbiddenModel UserForbiddenModel)
        {
            UserForbiddenModel user = list.Where(x => x.Uid == UserForbiddenModel.Uid).FirstOrDefault();
            if (list.Remove(user))
            {
                EnList(UserForbiddenModel);
            }
        }

        /// <summary>
        ///     排序
        /// </summary>
        private void SortList()
        {
            if (UserList.Count() != 0)
            {
                list = UserList.OrderByDescending(x => x.LastTime).ToList();
            }
        }
    }

//end MemForbidden
}

//end namespace Forbidden