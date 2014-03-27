using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Dev.Comm.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class UrlHelper
    {
        /// <summary>
        ///  是否为本地Url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrlLocalToHost(string url)
        {
            return !String.IsNullOrEmpty(url) && ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        public static bool IsCurrentDomainUrl(string url)
        {
            return !String.IsNullOrEmpty(url) && url.ToLower().IndexOf(HttpServerInfo.BaseUrl.ToLower(), StringComparison.Ordinal) == 0;
        }

        /// <summary>
        /// 取得URL中的参数
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static string GetParam(string url, string param)
        {
            Uri myUri = new Uri(url);
            string value = HttpUtility.ParseQueryString(myUri.Query).Get(param);

            return value;
        }

        /// <summary>
        ///  取得包含 http的host
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetHost(string url)
        {
            Uri myUri = new Uri(url);
            return myUri.Scheme + "://" + myUri.Host + (myUri.Port == 80 ? "" : ":" + myUri.Port) + "";
        }



    }
}
