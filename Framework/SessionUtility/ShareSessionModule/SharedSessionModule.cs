// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：SharedSessionModule.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using Dev.Log;

namespace ShareSessionModule
{
    /// <summary>
    /// 
    /// A HttpModule used for sharing the session between Applications in 
    /// sub domains.
    /// 
    /// added by zbw911 from : msdn
    /// </summary>
    public class SharedSessionModule : IHttpModule
    {
        // Cache settings on memory.
        protected static string applicationName = ConfigurationManager.AppSettings["ApplicationName"];
        protected static string rootDomain = ConfigurationManager.AppSettings["RootDomain"];

        #region IHttpModule Members

        /// <summary>
        /// Initializes a module and prepares it to handle requests.
        /// </summary>
        /// <param name="context">
        /// An System.Web.HttpApplication
        /// that provides access to the methods,
        /// properties, and events common to all application objects within 
        /// an ASP.NET application.
        /// </param>
        public void Init(HttpApplication context)
        {
            // This module requires both Application Name and Root Domain to work.
            if (string.IsNullOrEmpty(applicationName) ||
                string.IsNullOrEmpty(rootDomain))
            {
                return;
            }

            // Change the Application Name in runtime.
            FieldInfo runtimeInfo = typeof (HttpRuntime).GetField("_theRuntime",
                                                                  BindingFlags.Static | BindingFlags.NonPublic);
            var theRuntime = (HttpRuntime) runtimeInfo.GetValue(null);
            FieldInfo appNameInfo = typeof (HttpRuntime).GetField("_appDomainAppId",
                                                                  BindingFlags.Instance | BindingFlags.NonPublic);

            appNameInfo.SetValue(theRuntime, applicationName);

            // Subscribe Events.
            //context.PostRequestHandlerExecute += new EventHandler(context_PostRequestHandlerExecute);

            //context.EndRequest += new EventHandler(context_EndRequest);
        }

        //void context_EndRequest(object sender, EventArgs e)
        //{
        //    HttpApplication context = (HttpApplication)sender;

        //    // ASP.NET store a Session Id in cookie to specify current Session.
        //    HttpCookie cookie = context.Response.Cookies["ASP.NET_SessionId"];

        //    var url = context.Request.RawUrl;

        //    if (context.Session != null &&
        //        !string.IsNullOrEmpty(context.Session.SessionID))
        //    {
        //        // Need to store current Session Id during every request.
        //        cookie.Value = context.Session.SessionID;

        //        // All Applications use one root domain to store this Cookie
        //        // So that it can be shared.
        //        if (rootDomain != "localhost")
        //        {
        //            cookie.Domain = rootDomain;
        //        }

        //        // All Virtual Applications and Folders share this Cookie too.
        //        cookie.Path = "/";
        //    }
        //}

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module
        /// that implements.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        /// <summary>
        /// Before sending response content to client, change the Cookie to Root Domain
        /// and store current Session Id.
        /// </summary>
        /// <param name="sender">
        /// An instance of System.Web.HttpApplication that provides access to
        /// the methods, properties, and events common to all application
        /// objects within an ASP.NET application.
        /// </param>
        private void context_PostRequestHandlerExecute(object sender, EventArgs e)
        {
            string url = "NOT URL";

            try
            {
                var context = (HttpApplication) sender;

                // ASP.NET store a Session Id in cookie to specify current Session.
                HttpCookie cookie = context.Response.Cookies["ASP.NET_SessionId"];


                url = context.Request.RawUrl;
                //对静态文件不处理,此方法应用于集成模式下，
                if (IgnorePath(url))
                    return;


                if (context.Session != null &&
                    !string.IsNullOrEmpty(context.Session.SessionID))
                {
                    // Need to store current Session Id during every request.
                    cookie.Value = context.Session.SessionID;
                    //Kt.Framework.Common.Logger.Info("使用了" + url);
                    // All Applications use one root domain to store this Cookie
                    // So that it can be shared.
                    if (rootDomain != "localhost")
                    {
                        cookie.Domain = rootDomain;
                    }

                    // All Virtual Applications and Folders share this Cookie too.
                    cookie.Path = "/";
                }
                //else
                //{
                //    Kt.Framework.Common.Logger.Info("cookies is null" + url);
                //}
            }
            catch (Exception excep)
            {
                //Kt.Framework.Common.Logger.Error(excep);
                Loger.Error("the Error Url is " + url, excep);
                throw;
            }
        }


        private bool IgnorePath(string path)
        {
            var extnames = new[] {".js", ".gif", ".css", ".jpg", ".jpeg", ".png", ".tif", ".html", ".htm"};
            foreach (var extname in extnames)
            {
                if (path.EndsWith(extname, StringComparison.OrdinalIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}