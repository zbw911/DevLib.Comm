// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IStateConfiguration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.Configuration
{
    /// <summary>
    /// Interface that can be implemented by classes that provide state configuration for Kt.Framework.Repository.
    /// </summary>
    public interface IStateConfiguration
    {
        /// <summary>
        /// Called by Kt.Framework.Repository <see cref="Configure"/> to configure state storage.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance that can be
        /// used to register state storage components.</param>
        void Configure(IContainerAdapter containerAdapter);
    }
}