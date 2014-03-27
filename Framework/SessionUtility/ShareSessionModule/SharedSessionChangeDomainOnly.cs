// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：SharedSessionChangeDomainOnly.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Configuration;
using System.Reflection;
using System.Web;

namespace ShareSessionModule
{
    /// <summary>
    ///  added by zbw911
    ///  Session
    /// </summary>
    public class SharedSessionChangeDomainOnly : IHttpModule
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
        }

        /// <summary>
        /// Disposes of the resources (other than memory) used by the module
        /// that implements.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion

        ///// <summary>
        ///// Before sending response content to client, change the Cookie to Root Domain
        ///// and store current Session Id.
        ///// </summary>
        ///// <param name="sender">
        ///// An instance of System.Web.HttpApplication that provides access to
        ///// the methods, properties, and events common to all application
        ///// objects within an ASP.NET application.
        ///// </param>
        //void context_PostRequestHandlerExecute(object sender, EventArgs e)
        //{
        //    HttpApplication context = (HttpApplication)sender;

        //    // ASP.NET store a Session Id in cookie to specify current Session.
        //    HttpCookie cookie = context.Response.Cookies["ASP.NET_SessionId"];

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
    }
}