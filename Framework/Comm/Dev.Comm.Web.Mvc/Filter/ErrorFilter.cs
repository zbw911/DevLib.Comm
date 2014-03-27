// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年03月12日 13:02
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/ErrorFilter.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web.Mvc;
using System.Web;

namespace Dev.Comm.Web.Mvc.Filter
{
    /// <summary>
    /// </summary>
    public class ErrorFilter : HandleErrorAttribute
    {
        public override void OnException(System.Web.Mvc.ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.IsCustomErrorEnabled)
            {
                filterContext.ExceptionHandled = true;
            }

            Dev.Log.Loger.Error(filterContext.Exception);

            base.OnException(filterContext);

            //OVERRIDE THE 500 ERROR  

            //filterContext.HttpContext.Response.StatusCode = 200;
        }


        private static void RaiseErrorSignal(Exception e)
        {
            var context = HttpContext.Current;

            // using.Elmah.ErrorSignal.FromContext(context).Raise(e, context);
        }
    }
}