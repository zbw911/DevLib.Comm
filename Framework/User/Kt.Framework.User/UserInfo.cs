// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UserInfo.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;

namespace Dev.Framework.User
{
    public class UserInfo
    {
        /// <summary>
        ///     用户编号 UID
        /// </summary>
        public static decimal UID
        {
            get { return GetUserIdFromCookie(); }
        }

        /// <summary>
        ///     当前登录用户昵称
        /// </summary>
        public static string NICKNAME
        {
            get { return UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_NICKNAME); }
        }


        public static string USER_EMAIL
        {
            get { return UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_EMAIL); }
        }

        /// <summary>
        ///     当前用户登录名
        /// </summary>
        public static string USER_NAME
        {
            get
            {
                if (UserState.getIsNeedActived())
                {
                    return UserCookies.getActiveCookies(UserCookies.ACTIVECOOKIE_NAME_USERNAME);
                }
                else
                {
                    return UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_USERNAME);
                }
            }
        }

        ///// <summary>
        ///// 最后一次活动
        ///// </summary>
        //public static DateTime LASTActive
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[SessionKeys.LASTActive] == null || HttpContext.Current.Session[SessionKeys.LASTActive].ToString() == "")
        //        {
        //            return System.DateTime.MinValue;
        //        }
        //        else
        //        {
        //            return DateTime.Parse(HttpContext.Current.Session[SessionKeys.LASTActive].ToString());
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[SessionKeys.LASTActive] = value;
        //    }
        //}

        /// <summary>
        ///     判断是否登录,如果未激话，状态为未登录
        /// </summary>
        public static bool IS_LOGIN
        {
            get
            {
                if (UID > 0)
                {
                    var oline = new UserOnline();
                    oline.FreshUser(UID);
                }


                return !UserState.getIsNeedActived() && UID > 0;
            }
        }

        /// <summary>
        ///     未登录用户
        /// </summary>
        public static bool IS_GUEST
        {
            get { return UID == 0; }
        }

        public static void Set_MEMBER_ID(decimal UID)
        {
            IUserOnline oline = new UserOnline();

            oline.AddUser(new OnlineUserInfo {Uid = UID, LASTActive = DateTime.Now});
        }

        private static decimal GetUserIdFromCookie()
        {
            decimal uid;

            if (UserState.getIsNeedActived())
            {
                string strUid = UserCookies.getActiveCookies(UserCookies.ACTIVECOOKIE_NAME_USERID);
                decimal.TryParse(strUid, out uid);
                return uid;
            }

            string authuid = UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_USERID);
            decimal.TryParse(authuid, out uid);
            return uid;
        }

        //public static DateTime LASTPOST
        //{
        //    get
        //    {
        //        if (HttpContext.Current.Session[SessionKeys.LASTPOST] == null || HttpContext.Current.Session[SessionKeys.LASTPOST].ToString() == "")
        //        {
        //            return System.DateTime.MinValue;
        //        }
        //        else
        //        {
        //            return DateTime.Parse(HttpContext.Current.Session[SessionKeys.LASTPOST].ToString());
        //        }
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session[SessionKeys.LASTPOST] = value;
        //    }
        //}
    }
}