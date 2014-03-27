// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CookieFun.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Web;

namespace Dev.Comm.Web
{
    public class CookieFun
    {
        /// <summary>
        /// 设置 cookies
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="time"></param>
        /// <param name="domain"></param>
        public static void SetCookie(string cookieName, string value, TimeSpan? timespan = null, string domain = "", bool crossDomainCookie = false, string path = "")
        {
            var cookies = new HttpCookie(cookieName, value);

            if (!string.IsNullOrEmpty(domain) && domain != "localhost")
                cookies.Domain = domain;

            if (!string.IsNullOrEmpty(path))
                cookies.Path = path;


            if (timespan != null)
                cookies.Expires = DateTime.Now.Add((TimeSpan)timespan);

            if (crossDomainCookie)
                HttpContext.Current.Response.AddHeader("P3P",
                                    "CP=\"CURa ADMa DEVa PSAo PSDo OUR BUS UNI PUR INT DEM STA PRE COM NAV OTC NOI DSP COR\"");

            HttpContext.Current.Response.AppendCookie(cookies); //.a.Cookies.Add(cookies);
        }

        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="value"></param>
        /// <param name="timespan"></param>
        /// <param name="crossDomainCookie"></param>
        public static void SetCookie(string cookieName, string value, TimeSpan? timespan = null, bool crossDomainCookie = false)
        {
            SetCookie(cookieName, value, timespan, null, crossDomainCookie);
        }

        public static void SetCookie(string cookieName, string value)
        {
            SetCookie(cookieName, value, null, null, false);
        }


        /// <summary>
        /// 移除cookies
        /// </summary>
        /// <param name="cookieName"></param>
        /// <param name="crossDomainCookie"></param>
        public static void RemoveCookie(string cookieName, bool crossDomainCookie = false)
        {
            SetCookie(cookieName, "", new TimeSpan(365 * 24, 0, 0), crossDomainCookie);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public static void Clear()
        {
            HttpContext.Current.Response.Cookies.Clear();
        }

        /// <summary>
        /// 取得
        /// </summary>
        /// <param name="cookieName"></param>
        /// <returns></returns>

        public static string GetCookie(string cookieName)
        {
            if (!IsExistCookies(cookieName)) return null;

            return HttpContext.Current.Request.Cookies[cookieName].Value;
        }

        ///**
        // * 是否存在
        // * @param unknown_type $cookieName
        // */
        public static bool IsExistCookies(string cookieName)
        {
            return HttpContext.Current.Request.Cookies[cookieName] != null;
        }
    }
}