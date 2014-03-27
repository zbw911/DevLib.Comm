// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：HttpServerInfo.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Dev.Comm.Web
{
    /// <summary>
    ///  http服务器信息
    /// </summary>
    public class HttpServerInfo
    {
        #region Class Properties

        /// <summary>
        ///   服务器地址
        /// </summary>
        public static string BaseUrl
        {
            get { return GetRequestType() + "://" + GetHost() + (GetPort() == 80 ? "" : ":" + GetPort()) + ""; }
        }

        /// <summary>
        ///   系统根路径
        /// </summary>
        // ReSharper disable InconsistentNaming
        public static string RELATIVE_ROOT_PATH
        // ReSharper restore InconsistentNaming
        {
            get
            {
                if (HttpContextCurrent == null)
                    throw new NullReferenceException("HttpContext.Current");

                if (HttpContextCurrent.Server == null)
                    throw new NullReferenceException("HttpContext.Current.Server");

                var path = HttpContextCurrent.Server.MapPath("~");

                return path;
            }
        }

        private static HttpContext HttpContextCurrent
        {
            get
            {
                if (HttpContext.Current == null)
                    throw new NullReferenceException("HttpContext.Current");
                return HttpContext.Current;
            }
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///   增加参数
        /// </summary>
        /// <param name="sk"> </param>
        /// <param name="sv"> </param>
        /// <returns> </returns>
        public static string AddParmToUrl(string sk, string sv)
        {
            var queryString = HttpContextCurrent.Request.QueryString;

            var u = HttpContextCurrent.Request.Url;

            var url = GetRequestType() + "://" + u.Authority + "" + u.AbsolutePath;

            var addParms = new NameValueCollection { { sk, sv } };

            return RebuildUrlParms(addParms, null, queryString, url);
        }

        /// <summary>
        ///   全地址包括？后的
        /// </summary>
        /// <returns> </returns>
        public static string FullRequestPath()
        {
            return BaseUrl + HttpContextCurrent.Request.Url.PathAndQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCurrAbsolutePath()
        {
            var url = HttpContextCurrent.Request.Url.AbsolutePath;
            return url;
        }

        /// <summary>
        ///   当前系统网点地址
        /// </summary>
        /// <returns> </returns>
        public static string GetHost()
        {
            //HttpContextCurrent.Request.
            return HttpContextCurrent.Request.Url.Host;
        }

        /// <summary>
        ///   如源地址 : http://www.google.com/aaaa.html?fffffff.com
        ///   返回: http://www.google.com/aaaa.html
        /// </summary>
        /// <returns> </returns>
        public static string GetLeftPath()
        {
            return HttpContextCurrent.Request.Url.GetLeftPart(UriPartial.Path);
        }


        /// <summary>
        ///   请求端口
        /// </summary>
        /// <returns> </returns>
        public static int GetPort()
        {
            return HttpContextCurrent.Request.Url.Port;
        }

        /// <summary>
        ///   Agent
        /// </summary>
        /// <returns> </returns>
        public static string GetRequestAgent()
        {
            return HttpContextCurrent.Request.UserAgent;
        }

        /// <summary>
        ///   请求类型
        /// </summary>
        /// <returns> </returns>
        public static string GetRequestType()
        {
            return HttpContextCurrent.Request.Url.Scheme;
        }

        /// <summary>
        ///   返回上一个页面的地址
        /// </summary>
        /// <returns> 上一个页面的地址 </returns>
        public static string GetUrlReferrer()
        {
            string retVal = null;


            if (HttpContextCurrent.Request.UrlReferrer != null)
                retVal = HttpContextCurrent.Request.UrlReferrer.ToString();


            if (retVal == null)
                return "";

            return retVal;
        }

        /// <summary>
        ///   重建
        /// </summary>
        /// <param name="AddParms"> </param>
        /// <param name="RemoveKeys"> </param>
        /// <returns> </returns>
        public static string RebuildUrl(NameValueCollection AddParms, string[] RemoveKeys)
        {
            var queryString = HttpUtility.ParseQueryString(HttpContextCurrent.Request.Url.Query);

            var u = HttpContextCurrent.Request.Url;

            var url = GetRequestType() + "://" + u.Authority + "" + u.AbsolutePath;

            return RebuildUrlParms(AddParms, RemoveKeys, queryString, url);
        }


        /// <summary>
        ///   重建
        /// </summary>
        /// <param name="addkey"> </param>
        /// <param name="addvalue"> </param>
        /// <param name="removeKey"> </param>
        /// <returns> </returns>
        public static string RebuildUrl(string addkey, string addvalue, string removeKey)
        {
            var queryString = HttpUtility.ParseQueryString(HttpContextCurrent.Request.Url.Query);
            //NameValueCollection queryString = HttpContextCurrent.Request.QueryString;

            var u = HttpContextCurrent.Request.Url;

            var url = GetRequestType() + "://" + u.Authority + "" + u.AbsolutePath;


            var addParms = new NameValueCollection { { addkey, addvalue } };
            var removeKeys = new[] { removeKey };
            return RebuildUrlParms(addParms, removeKeys, queryString, url);
        }

        /// <summary>
        ///   从URL中移除一个参数
        /// </summary>
        /// <param name="sk"> </param>
        /// <returns> </returns>
        public static string RemoveParmToUrl(string sk)
        {
            var queryString = HttpContextCurrent.Request.QueryString;

            var u = HttpContextCurrent.Request.Url;

            var url = GetRequestType() + "://" + u.Authority + "" + u.AbsolutePath;


            return RebuildUrlParms(null, new[] { sk }, queryString, url);
        }

        /// <summary>
        ///   将本地URL,包装包完整URL
        /// </summary>
        /// <param name="url"> </param>
        /// <returns> </returns>
        public static string WapperFullUrl(string url)
        {
            url = string.IsNullOrEmpty(url) ? "" : url;
            return url.IndexOf(HttpServerInfo.BaseUrl, System.StringComparison.Ordinal) == 0
                       ? url
                       : HttpServerInfo.BaseUrl + url;
        }

        /// <summary>
        ///   重建URL
        /// </summary>
        /// <param name="addParms"> </param>
        /// <param name="removeKeys"> </param>
        /// <param name="queryString"> </param>
        /// <param name="url"> </param>
        /// <returns> </returns>
        private static string RebuildUrlParms(
            NameValueCollection addParms, IEnumerable<string> removeKeys, NameValueCollection queryString, string url)
        {
            url += "?";


            if (removeKeys != null)
            {
                queryString = new NameValueCollection(queryString);
                foreach (var removeKey in removeKeys)
                {
                    var key = removeKey;
                    var localkeys = queryString.AllKeys.FirstOrDefault(x => x.ToLower() == key.ToLower());

                    queryString.Remove(localkeys);
                }
            }

            if (queryString != null)
            {
                foreach (var item in queryString.AllKeys)
                {
                    var val = queryString[item];

                    var parmkey = "";
                    if (
                        !string.IsNullOrEmpty(
                            parmkey = addParms.AllKeys.FirstOrDefault(x => x.ToLower() == item.ToLower())))
                    {
                        val = addParms[parmkey];
                    }

                    url += item + "=" + val + "&";
                }


                // 1,2,3,4,5,6
                //         5,6,7

                var queryResult = from p in addParms.AllKeys
                                  where queryString.AllKeys.All(x => x.ToLower() != p.ToLower())
                                  select p;

                foreach (var key in queryResult)
                {
                    url += key + "=" + addParms[key] + "&";
                }
            }
            url = url.TrimEnd('&').TrimEnd('?');

            return url;
        }

        /// <summary>
        /// 返回物理路径 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }

        #endregion
    }
}