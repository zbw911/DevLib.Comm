// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IDataConfiguration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.Configuration
{
    /// <summary>
    /// Base interface implemented by specific data configurators that configure Kt.Framework.Repository data providers.
    /// </summary>
    public interface IDataConfiguration
    {
        /// <summary>
        /// Called by Kt.Framework.Repository <see cref="Configure"/> to configure data providers.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance that allows
        /// registering components.</param>
        void Configure(IContainerAdapter containerAdapter);
    }
}