using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Dev.Comm.Web.Mvc.Filter
{
    /// <summary>
    /// 加入 Cache-Control: no-cache
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NoCacheAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            base.OnResultExecuted(filterContext);
        }
    }
    /// <summary>
    /// Http头加入 MaxAge={}
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class CacheMaxAgeAttribute : ActionFilterAttribute
    {

        ///// <summary>
        ///// MaxAge值
        ///// </summary>
        //public TimeSpan MaxAge { get; set; }


        /// <summary>
        /// 过期时间 
        /// </summary>
        public int SecondMaxAge { get; set; }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //filterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            filterContext.HttpContext.Response.Cache.SetMaxAge(TimeSpan.FromSeconds(SecondMaxAge));

            //filterContext.HttpContext.Response.CacheControl = "private";
            base.OnResultExecuted(filterContext);
        }
    }
}
