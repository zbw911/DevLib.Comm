// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CrossDomainCookie.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Web.SessionState;

namespace ShareSessionModule
{
    /**
     * added by zbw911
     * 修改respone cookies
     * */

    public class CrossDomainCookie : IHttpModule
    {
        private string m_RootDomain = string.Empty;

        #region IHttpModule Members

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            m_RootDomain = ConfigurationManager.AppSettings["RootDomain"];

            Type stateServerSessionProvider =
                typeof (HttpSessionState).Assembly.GetType("System.Web.SessionState.OutOfProcSessionStateStore");
            FieldInfo uriField = stateServerSessionProvider.GetField("s_uribase",
                                                                     BindingFlags.Static | BindingFlags.NonPublic);

            if (uriField == null)
                throw new ArgumentException("UriField was not found");

            uriField.SetValue(null, m_RootDomain);

            context.EndRequest += context_EndRequest;
        }

        #endregion

        private void context_EndRequest(object sender, EventArgs e)
        {
            var app = sender as HttpApplication;

            for (int i = 0; i < app.Context.Response.Cookies.Count; i++)
            {
                app.Context.Response.Cookies[i].Domain = m_RootDomain;
            }
        }
    }
}