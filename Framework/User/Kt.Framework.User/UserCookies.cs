// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UserCookies.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using DS.Web.UCenter;

namespace Dev.Framework.User
{
    /// <summary>
    ///     与用户相关的cookies集合
    /// </summary>
    public class UserCookies
    {
        public const string API_UTILITY_USERCOOKIES_SAFEKEY = "加密key";

        public const string AUTHCOOKIE_NAME_USERID = "userId";
        public const string AUTHCOOKIE_NAME_USERNAME = "username";
        public const string AUTHCOOKIE_NAME_BALANCE = "balance";
        public const string AUTHCOOKIE_NAME_ISSAFLECOMPLATE = "isSafleComplate";
        public const string AUTHCOOKIE_NAME_UEISEXIST = "ueisexist";
        public const string AUTHCOOKIE_NAME_ISSAFEPASS = "hassafePass";
        public const string AUTHCOOKIE_NAME_EMAIL = "email";
        public const string AUTHCOOKIE_NAME_NICKNAME = "nickname";

        public const string ACTIVECOOKIE_NAME_USERID = "userId";
        public const string ACTIVECOOKIE_NAME_USERNAME = "username";
        public const string ACTIVECOOKIE_NAME_PASSWORD = "passWord";
        public const string ACTIVECOOKIE_NAME_EMAIL = "eMail";

        /**
         * cookie过期时间，如果设置为0就是浏览器进程cookies
         * @var unknown_type
         */
        public const int COOKIE_EXPIRETIME_2_WEEK = 3600*24*7*2;
        /**
         * 验证cookies名称 
         * @var unknown_type
         */
        public const string AUTHCOOKIENAME = "actweb_auth_";

        public const string SECURELOGIN = "SecureLogin";

        public const string ACTIVECOOKIESNAME = "actActiveCookies";

        public static void setUserLoginidCookies(string logidid = "")
        {
            setcookie("loginuserID", logidid, UcUtility.PhpTimeNow() + 3600*24*7,
                      ConfigurationManager.AppSettings["RootDomain"]);
        }

        public static string getUserLoginidCookies()
        {
            return getcookie("loginuserID");
        }

        /// <summary>
        ///     设置 cookies
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <param name="domain"></param>
        private static void setcookie(string cookieName, string value, long time, string domain)
        {
            var cookies = new HttpCookie(cookieName, value);
            if (!string.IsNullOrEmpty(domain) && domain != "localhost")
                cookies.Domain = domain;
            cookies.Path = "/";
            if (time > 0)
            {
                cookies.Expires = UcUtility.PhpTimeToDateTime(time);
            }
            else
            {
                //cookies.Expires = DateTime.Now.AddDays(14);
            }
            HttpContext.Current.Response.AppendCookie(cookies); //.a.Cookies.Add(cookies);
        }


        private static string getcookie(string cookieName)
        {
            if (!IsExistCookies(cookieName)) return null;

            return HttpContext.Current.Request.Cookies[cookieName].Value;
        }


        /**
         * 与安全相关的cookies设置
         * @param unknown_type $cookieName
         * @param unknown_type $data
         * @param unknown_type $safeKey
         * @param unknown_type $expire
         * @param unknown_type $domain
         */

        private static void _setAuthCookies(string cookieName, IEnumerable<KeyValuePair<string, string>> data,
                                            int expire = 0, string safeKey = API_UTILITY_USERCOOKIES_SAFEKEY)
        {
            long time = expire > 0 ? UcUtility.PhpTimeNow() + expire : 0;
            string val = "";
            foreach (var kv in data)
            {
                val += kv.Key + "=" + kv.Value + "|";
            }
            setcookie(cookieName, UcUtility.AuthCodeEncode(val, safeKey, expire), time,
                      ConfigurationManager.AppSettings["RootDomain"]);
        }


        private static string _getAuthCookies(string cookieName, string cookieKey, int expire = 0,
                                              string safeKey = API_UTILITY_USERCOOKIES_SAFEKEY)
        {
            HttpCookie cookies = HttpContext.Current.Request.Cookies[cookieName];

            if (cookies == null) return null;

            //if (cookies[cookieName] == null) return null;

            if (string.IsNullOrEmpty(cookies.Value)) return null;

            string val = UcUtility.AuthCodeDecode(cookies.Value, safeKey, expire);

            var list = new List<KeyValuePair<string, string>>();

            string[] cookiearr = val.Split('|');
            foreach (var v in cookiearr)
            {
                //list($key, $value) = explode("=", $v);
                string[] lskv = v.Split('=');
                // KeyValuePair<string, string> kv = new KeyValuePair<string,string>( lskv[0],lskv[1]);
                //if( 
                if (lskv[0] == cookieKey)
                {
                    return lskv[1];
                }
            }
            return null;
        }

        /// **
        // * 是否存在
        // * @param unknown_type $cookieName
        // */
        public static bool IsExistCookies(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName] != null;
        }

        /**
         * 清除cookies
         */

        public static void clearCookies(string cookieName)
        {
            // setcookie($cookieName, "", time() - 3600 * 24 * 7, "/");
            var cookie = new HttpCookie(cookieName);
            cookie.Expires = DateTime.Now.AddDays(-7);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /**
         * 设置验证cookie
         *
         * @param array $data
         * @param int $expire
         */

        private static void setAuthCookie(IEnumerable<KeyValuePair<string, string>> data, int expire = 0)
        {
            _setAuthCookies(AUTHCOOKIENAME, data, expire);
        }


        public static DateTime GetExpires(string cookieName)
        {
            if (!IsExistCookies(cookieName))
                return DateTime.MinValue;

            return HttpContext.Current.Request.Cookies[cookieName].Expires;
        }

        /**
         * 通过Cookie获得验证用户信息
         */

        public static string getAuthCookie(string name)
        {
            return _getAuthCookies(AUTHCOOKIENAME, name);
        }

        /**
         * 设置用于激活的key
         * @param unknown_type $userId
         * @param unknown_type $loginId
         * @param unknown_type $passWord
         * @param unknown_type $eMail
         * @param unknown_type $expire
         */

        public static void setActiveCookies(decimal userId, string loginId, string passWord, string eMail,
                                            int expire = 0)
        {
            IEnumerable<KeyValuePair<string, string>> data = new[]
                                                                 {
                                                                     new KeyValuePair<string, string>(
                                                                         ACTIVECOOKIE_NAME_USERID, userId.ToString())
                                                                     ,
                                                                     new KeyValuePair<string, string>(
                                                                         ACTIVECOOKIE_NAME_USERNAME, loginId)
                                                                     ,
                                                                     new KeyValuePair<string, string>(
                                                                         ACTIVECOOKIE_NAME_PASSWORD, passWord)
                                                                     ,
                                                                     new KeyValuePair<string, string>(
                                                                         ACTIVECOOKIE_NAME_EMAIL, eMail)
                                                                 };


            _setAuthCookies(ACTIVECOOKIESNAME, data, expire);
        }

        /**
         * 取得激活参数
         * @param $name
         */

        public static string getActiveCookies(string name)
        {
            return _getAuthCookies(ACTIVECOOKIESNAME, name);
        }


        /// <summary>
        ///     设置登录COOKIES
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="logidId"></param>
        /// <param name="email"></param>
        /// <param name="expire"></param>
        /// <param name="nickname"></param>
        public static void setAuthCookies(decimal userid, string logidId, string email, int expire, string nickname)
        {
            IEnumerable<KeyValuePair<string, string>> udata = new[]
                                                                  {
                                                                      new KeyValuePair<string, string>(
                                                                          AUTHCOOKIE_NAME_USERID, userid.ToString())
                                                                      ,
                                                                      new KeyValuePair<string, string>(
                                                                          AUTHCOOKIE_NAME_USERNAME, logidId)
                                                                      ,
                                                                      new KeyValuePair<string, string>(
                                                                          AUTHCOOKIE_NAME_NICKNAME, nickname)
                                                                      ,
                                                                      new KeyValuePair<string, string>(
                                                                          AUTHCOOKIE_NAME_EMAIL, email)
                                                                  };
            setAuthCookie(udata, expire);
        }
    }
}