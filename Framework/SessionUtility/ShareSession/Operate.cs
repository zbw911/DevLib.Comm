// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Operate.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.ComponentModel;
using System.Web;
using System.Linq;

namespace ShareSession
{
    enum MyEnum
    {

    }
    /// <summary>
    /// 会话操作 added by zbw911
    /// </summary>
    public abstract class SessionOperateBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SessionName"></param>
        /// <returns></returns>
        public static T Get<T>(string SessionName)
        {

            if (HttpContext.Current == null)
            {
                throw new Exception("会话无效");
            }

            object sessionvalue = HttpContext.Current.Session[SessionName];

            if (sessionvalue != null)
            {
                var t = sessionvalue.GetType();
            }
            if (sessionvalue == null)
            {
                return default(T);
            }

            return (T)HttpContext.Current.Session[SessionName.ToString()];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SessionName"></param>
        /// <param name="SessionValue"></param>
        public static void Set(string SessionName, object SessionValue)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("会话无效");
            }


            var attributes = SessionValue.GetType().Attributes;

            if (!attributes.HasFlag(System.Reflection.TypeAttributes.Serializable))
            {
                throw new Exception("没有 [Serializable] 属性");
            }

            //Todo: 应该加入一个功能，如果SessionValue没有  [Serializable] 属性 ，可以考虑注入属性
            // 但是这样做，同样会另一个问题，人总是懒惰的，程序员们都会依赖这个方法， 先放在这里考虑一下再说吧。。 added by zbw911

            HttpContext.Current.Session[SessionName.ToString()] = SessionValue;
        }

        public static void Remove(string SessionName)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("会话无效");
            }

            HttpContext.Current.Session.Remove(SessionName);
        }

        public static void Clear()
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("会话无效");
            }
            HttpContext.Current.Session.Clear();
        }
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public  override string ToString()
        //{
        //    return base.ToString();
        //}
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public override bool Equals(object obj)
        //{
        //    return base.Equals(obj);
        //}
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public override int GetHashCode()
        //{
        //    return base.GetHashCode();
        //}
    }
}