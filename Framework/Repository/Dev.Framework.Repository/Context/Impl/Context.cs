// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：Context.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System.ServiceModel.Activation;
using System.Web;

namespace Kt.Framework.Repository.Context.Impl
{
    /// <summary>
    /// Default implementation of <see cref="IContext"/>
    /// </summary>
    public class Context : IContext
    {
        /// <summary>
        /// Gets weather the current application is a web based application.
        /// </summary>
        /// <value>True if the application is a web based application, else false.</value>
        public bool IsWebApplication
        {
            get
            {
                return HttpContext != null;
            }
        }

        /// <summary>
        /// Gets weather the current application is a WCF based application.
        /// </summary>
        /// <value>True if the application is a WCF based application, else false.</value>
        public bool IsWcfApplication
        {
            get { return OperationContext != null; }
        }

        /// <summary>
        /// Gets weather ASP.Net compatability is enabled for the current WCF service.
        /// </summary>
        /// <value>True if <see cref="IsWcfApplication"/> is true and ASP.Net compatability is enabled for
        /// the current service, else false.</value>
        public bool IsAspNetCompatEnabled
        {
            get
            {
                if (!IsWcfApplication)
                    return false;
                var aspnetCompat = OperationContext.Host
                    .Description
                    .Behaviors
                    .Find<AspNetCompatibilityRequirementsAttribute>();

                return (aspnetCompat != null &&
                        (aspnetCompat.RequirementsMode == AspNetCompatibilityRequirementsMode.Allowed ||
                         aspnetCompat.RequirementsMode == AspNetCompatibilityRequirementsMode.Required) && IsWebApplication);
            }
        }

        /// <summary>
        /// Gets a <see cref="HttpContextBase"/> that wraps the current <see cref="HttpContext"/>
        /// </summary>
        /// <value>An <see cref="HttpContextBase"/> instnace if <see cref="IsWebApplication"/> is true,
        /// else null.</value>
        public virtual HttpContextBase HttpContext
        {
            get
            {
                if (System.Web.HttpContext.Current == null)
                    return null;
                return new HttpContextWrapper(System.Web.HttpContext.Current);
            }
        }

        /// <summary>
        /// Gets a <see cref="IOperationContext"/> that wraps the current <see cref="OperationContext"/>
        /// for a WCF based application.
        /// </summary>
        /// <value>An  <see cref="IOperationContext"/> instance if <see cref="IsWcfApplication"/> is true,
        /// else null.</value>
        public virtual IOperationContext OperationContext
        {
            get
            {
                if (System.ServiceModel.OperationContext.Current == null)
                    return null;
                return new OperationContextWrapper(System.ServiceModel.OperationContext.Current);
            }
        }
    }
}