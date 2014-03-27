using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;

namespace Dev.Comm.Web
{
    /// <summary>
    /// Http状态处理
    /// </summary>
    public class HttpStateHandler
    {
        /// <summary>
        /// 处理Http 304
        /// </summary>
        /// <param name="checkIsModifyed"></param>
        /// <param name="modifyDate"></param>
        /// <returns></returns>
        public static bool Http304(Func<DateTime, bool> checkIsModifyed, Func<DateTime> modifyDate)
        {
            var request = HttpContext.Current.Request;//.RequestContext;
            var response = HttpContext.Current.Response;

            if (!String.IsNullOrEmpty(request.Headers["If-Modified-Since"]))
            {
                CultureInfo provider = CultureInfo.InvariantCulture;
                var lastMod = DateTime.ParseExact(request.Headers["If-Modified-Since"], "r", provider).ToLocalTime();

                var checkModifyed = checkIsModifyed(lastMod);

                if (!checkModifyed)
                {
                    response.StatusCode = 304;
                    response.StatusDescription = "Not Modified";

                    return false;
                }
            }

            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetLastModified(modifyDate());

            return true;
        }
    }
}
