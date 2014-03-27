// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：Configure.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using Kt.Framework.Repository.Configuration;

namespace Kt.Framework.Repository
{
    /// <summary>
    /// Static configuration class that allows configuration of Kt.Framework.Repository services.
    /// </summary>
    public static class Configure
    {
        /// <summary>
        /// Entry point to Kt.Framework.Repository configuration.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance to use
        /// for component registration.</param>
        /// <returns>An instance of <see cref="INCommonConfig"/> that can be used to configure
        /// Kt.Framework.Repository configuration.</returns> 
        public static INCommonConfig Using(IContainerAdapter containerAdapter)
        {
            Guard.Against<ArgumentNullException>(containerAdapter == null,
                                                 "Expected a non-null IContainerAdapter implementation.");
            return new NCommonConfig(containerAdapter);
        }
    }
}