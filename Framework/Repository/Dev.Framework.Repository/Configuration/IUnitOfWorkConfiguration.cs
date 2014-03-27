// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUnitOfWorkConfiguration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System.Transactions;
using Kt.Framework.Repository.Data;

namespace Kt.Framework.Repository.Configuration
{
    /// <summary>
    /// Configuration settings for <see cref="UnitOfWorkScope"/> instances in Kt.Framework.Repository.
    /// </summary>
    public interface IUnitOfWorkConfiguration
    {
        /// <summary>
        /// Configures <see cref="UnitOfWorkScope"/> settings.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance.</param>
        void Configure(IContainerAdapter containerAdapter);

        /// <summary>
        /// Sets <see cref="UnitOfWorkScope"/> instances to auto complete when disposed.
        /// </summary>
        IUnitOfWorkConfiguration AutoCompleteScope();

        /// <summary>
        /// Sets the default isolation level used by <see cref="UnitOfWorkScope"/>.
        /// </summary>
        /// <param name="isolationLevel"></param>
        IUnitOfWorkConfiguration WithDefaultIsolation(IsolationLevel isolationLevel);
    }
}