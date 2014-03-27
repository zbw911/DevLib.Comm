// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年02月21日 17:13
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/WebApiAllowCrossSiteJsonAttribute.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Comm.Web.Mvc.Filter
{
    using System.Web.Http.Filters;

    public class WebApiAllowCrossSiteJsonAttribute : ActionFilterAttribute
    {
        private const string Origin = "Origin";
        private const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";

        private const string originHeaderdefault = "*";

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //if (actionExecutedContext.Request.Headers.Contains(Origin))
            //{
            //    string originHeader = actionExecutedContext.Request.Headers.GetValues(Origin).FirstOrDefault();
            //    if (!string.IsNullOrEmpty(originHeader))
            //    {
            actionExecutedContext.Response.Headers.Add(AccessControlAllowOrigin, originHeaderdefault);
            //actionExecutedContext.Result.Headers.Add(AccessControlAllowOrigin, originHeader);
            //}
        }
    }
}