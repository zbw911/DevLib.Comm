// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年04月16日 15:30
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/RouteInfo.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;
using System.Web.Routing;

namespace Dev.Comm.Web.Mvc.Routes
{
    ///// Here it is...
    /// <summary>
    ///   Uri uri = new Uri("http://www.yoursite.com/somepage");
    ///   RouteInfo routeInfo = new RouteInfo(uri, HttpContext.Current.Request.ApplicationPath);
    ///   RouteData routeData = routeInfo.RouteData;
    /// </summary>
    public class RouteInfo
    {
        public RouteData RouteData { get; private set; }

        public RouteInfo(RouteData data)
        {
            RouteData = data;
        }

        public RouteInfo(Uri uri, string applicationPath)
        {
            RouteData = RouteTable.Routes.GetRouteData(new InternalHttpContext(uri, applicationPath));
        }

        private class InternalHttpContext : HttpContextBase
        {
            private readonly HttpRequestBase _request;

            public InternalHttpContext(Uri uri, string applicationPath)
            {
                _request = new InternalRequestContext(uri, applicationPath);
            }

            public override HttpRequestBase Request
            {
                get { return _request; }
            }
        }

        private class InternalRequestContext : HttpRequestBase
        {
            private readonly string _appRelativePath;
            private readonly string _pathInfo;

            public InternalRequestContext(Uri uri, string applicationPath)
            {
                _pathInfo = uri.Query;

                if (String.IsNullOrEmpty(applicationPath) ||
                    !uri.AbsolutePath.StartsWith(applicationPath, StringComparison.OrdinalIgnoreCase))
                    _appRelativePath = uri.AbsolutePath.Substring(applicationPath.Length);
                else
                    _appRelativePath = uri.AbsolutePath;
            }

            public override string AppRelativeCurrentExecutionFilePath
            {
                get { return String.Concat("~", _appRelativePath); }
            }

            public override string PathInfo
            {
                get { return _pathInfo; }
            }
        }
    }
}