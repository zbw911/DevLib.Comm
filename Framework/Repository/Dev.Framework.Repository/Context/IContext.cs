// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IContext.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System.Web;

namespace Kt.Framework.Repository.Context
{
    /// <summary>
    /// 
    /// </summary>
    public interface IContext
    {
        /// <summary>
        /// Gets weather the current application is a web based application.
        /// </summary>
        /// <value>True if the application is a web based application, else false.</value>
        bool IsWebApplication { get; }
        /// <summary>
        /// Gets weather the current application is a WCF based application.
        /// </summary>
        /// <value>True if the application is a WCF based application, else false.</value>
        bool IsWcfApplication { get; }
        /// <summary>
        /// Gets weather ASP.Net compatability is enabled for the current WCF service.
        /// </summary>
        /// <value>True if <see cref="IsWcfApplication"/> is true and ASP.Net compatability is enabled for
        /// the current service, else false.</value>
        bool IsAspNetCompatEnabled { get; }
        /// <summary>
        /// Gets a <see cref="HttpContextBase"/> that wraps the current <see cref="HttpContext"/>
        /// </summary>
        /// <value>An <see cref="HttpContextBase"/> instnace if <see cref="IsWebApplication"/> is true,
        /// else null.</value>
        HttpContextBase HttpContext { get; }
        /// <summary>
        /// Gets a <see cref="IOperationContext"/> that wraps the current <see cref="OperationContext"/>
        /// for a WCF based application.
        /// </summary>
        /// <value>An  <see cref="IOperationContext"/> instance if <see cref="IsWcfApplication"/> is true,
        /// else null.</value>
        IOperationContext OperationContext { get; }
    }
}