// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UserState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Web;

namespace Dev.Framework.User
{
    /// <summary>
    ///     用户状态方法
    /// </summary>
    public class UserState
    {
        /**
             * 是否已经完善安全资料
             */

        public static string getIsSafeComplate()
        {
            return UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_ISSAFLECOMPLATE);
        }

        /**
         * 是否已经激活
         */

        public static bool getIsNeedActived()
        {
            return !string.IsNullOrEmpty(UserCookies.getActiveCookies(UserCookies.ACTIVECOOKIE_NAME_USERID));
        }

        /**
         * 是否已经登录UC
         */

        public static bool getIsUcLogin()
        {
            return UserCookies.getAuthCookie(UserCookies.AUTHCOOKIE_NAME_USERID) != null;
        }

        /**
         * 判断是否已经安全平台登录，这个XXX最重要的判断，与SESSION结合 
         */

        public static bool getIsWebLogin()
        {
            return UserInfo.IS_LOGIN;
        }

        /// <summary>
        ///     退出登录
        /// </summary>
        public static void AllLoginOut()
        {
            //清除SESSION
            LoginOut();

            //清除cookies
            UserCookies.clearCookies(UserCookies.ACTIVECOOKIESNAME);
            UserCookies.clearCookies(UserCookies.AUTHCOOKIENAME);
        }

        /// <summary>
        ///     退出
        /// </summary>
        internal static void LoginOut()
        {
            IUserOnline oline = new UserOnline();

            // oline.RemoveUser(UserInfo.UID);

            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
        }
    }
}