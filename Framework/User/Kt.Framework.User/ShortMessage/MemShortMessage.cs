// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：MemShortMessage.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Framework.User.ShortMessage
{
    public class MemShortMessage : IShortMessage
    {
        private static List<ShortMessageModel> list = new List<ShortMessageModel>();
        private static readonly object lockobj = new object();

        internal IEnumerable<ShortMessageModel> MessageList
        {
            get
            {
                lock (lockobj)
                {
                    return list;
                }
            }
        }

        #region IShortMessage Members

        public void DeList(ShortMessageModel ShortMessageModel)
        {
            list.Remove(ShortMessageModel);
        }

        public void EnList(ShortMessageModel ShortMessageModel)
        {
            list.Add(ShortMessageModel);
        }

        public void FreshList(ShortMessageModel ShortMessageModel)
        {
            ShortMessageModel message = list.Where(x => x.Mobile == ShortMessageModel.Mobile).FirstOrDefault();
            if (list.Remove(message))
            {
                EnList(ShortMessageModel);
            }
        }

        /// <summary>
        ///     判断这个手机号是否是频繁发布的
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns>true:表示频繁发布，不应该继续发送  false:非频繁发布，可以发送短信</returns>
        public bool IsSendToMany(string Mobile)
        {
            ShortMessageModel message = MessageList.FirstOrDefault(x => x.Mobile == Mobile);
            if (message != null)
            {
                if (DateTime.Now >= message.LastTime.AddSeconds(ShortMessageConfig.TimeInterval))
                {
                    FreshList(new ShortMessageModel
                                  {Mobile = message.Mobile, LastTime = DateTime.Now, SendTimes = message.SendTimes + 1});
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                EnList(new ShortMessageModel {Mobile = Mobile, LastTime = DateTime.Now, SendTimes = 1});
                return false;
            }

            /*首先判断该手机号码是否在短信发送列表中
             * 如果是：判断当前时间是否已经超过LastTime+TimeInterval
             *      如果是：列表中更新这个记录，返回false
             *          否：直接返回true
             *  否：在列表中插入这个记录，返回false
             *  
             * */
        }

        /// <summary>
        ///     根据手机号获取此手机发送过多少次短信
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        public int GetSendTimes(string Mobile)
        {
            ShortMessageModel message = MessageList.FirstOrDefault(x => x.Mobile == Mobile);
            if (message != null)
            {
                return message.SendTimes;
            }
            else
            {
                return 0;
            }
        }

        #endregion

        private void SortList()
        {
            if (MessageList.Count() != 0)
            {
                list = MessageList.OrderByDescending(x => x.LastTime).ToList();
            }
        }

        public void ClearShortMessage(string Mobile)
        {
            ShortMessageModel message = list.Where(x => x.Mobile == Mobile).FirstOrDefault();
            DeList(message);
            //以下，如果另外一个人在调用此方法，循环删除的时候，将另外的人的记录删除了，会出现问题
            //foreach (var m in MessageList)
            //{
            //    if (DateTime.Now >= m.LastTime.AddSeconds(ShortMessageConfig.TimeInterval))
            //    {
            //        this.DeList(m);
            //    }
            //}
        }
    }
}