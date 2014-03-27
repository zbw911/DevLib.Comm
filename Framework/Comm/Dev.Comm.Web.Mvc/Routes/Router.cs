// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年04月16日 15:30
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/Router.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;

namespace Dev.Comm.Web.Mvc.Routes
{
    public static class Router
    {
        public static bool IsRouteMatch(this Uri uri, string controllerName, string actionName)
        {
            RouteInfo routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return (routeInfo.RouteData.Values["controller"].ToString() == controllerName &&
                    routeInfo.RouteData.Values["action"].ToString() == actionName);
        }

        public static string GetRouteParameterValue(this Uri uri, string paramaterName)
        {
            RouteInfo routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
            return routeInfo.RouteData.Values[paramaterName] != null
                       ? routeInfo.RouteData.Values[paramaterName].ToString()
                       : null;
        }
    }
}