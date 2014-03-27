// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年03月18日 11:46
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/JsonpFilterAttribute.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Dev.Comm.Web.Mvc.ActionResult;

namespace Dev.Comm.Web.Mvc.Filter
{
    public class JsonpFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext == null)
                throw new ArgumentNullException("filterContext");

            //
            // see if this request included a "callback" querystring parameter
            //
            string callback = filterContext.HttpContext.Request.QueryString["callback"];
            if (callback != null && callback.Length > 0)
            {
                //
                // ensure that the result is a "JsonResult"
                //
                JsonResult result = filterContext.Result as JsonResult;
                if (result == null)
                {
                    throw new InvalidOperationException("JsonpFilterAttribute must be applied only " +
                                                        "on controllers and actions that return a JsonResult object.");
                }

                filterContext.Result = new JsonpResult
                                           {
                                               ContentEncoding = result.ContentEncoding,
                                               ContentType = result.ContentType,
                                               Data = result.Data,
                                               Callback = callback
                                           };
            }
        }
    }
}