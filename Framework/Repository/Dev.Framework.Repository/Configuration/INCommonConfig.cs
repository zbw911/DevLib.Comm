// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：INCommonConfig.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;

namespace Kt.Framework.Repository.Configuration
{
    /// <summary>
    /// Configuration interface exposed by Kt.Framework.Repository to configure different services exposed by Kt.Framework.Repository.
    /// </summary>
    public interface INCommonConfig
    {
        /// <summary>
        /// Configure Kt.Framework.Repository state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed by Kt.Framework.Repository.
        /// </typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureState<T>() where T : IStateConfiguration, new();

        /// <summary>
        /// Configure Kt.Framework.Repository state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed by Kt.Framework.Repository.
        /// </typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IStateConfiguration"/> instance.</param>
        /// <returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureState<T>(Action<T> actions) where T : IStateConfiguration, new();

        /// <summary>
        /// Configure data providers used by Kt.Framework.Repository.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers for Kt.Framework.Repository.</typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureData<T>() where T : IDataConfiguration, new();

        /// <summary>
        /// Configure data providers used by Kt.Framework.Repository.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers for Kt.Framework.Repository.</typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IDataConfiguration"/> instance.</param>
        /// <returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureData<T>(Action<T> actions) where T : IDataConfiguration, new();

        /// <summary>
        /// Configures Kt.Framework.Repository unit of work settings.
        /// </summary>
        /// <typeparam name="T">A <see cref="IUnitOfWorkConfiguration"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureUnitOfWork<T> () where T : IUnitOfWorkConfiguration, new();

        ///<summary>
        /// Configures Kt.Framework.Repository unit of work settings.
        ///</summary>
        /// <typeparam name="T">A <see cref="INCommonConfig"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        ///<param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IUnitOfWorkConfiguration"/> instance.</param>
        ///<returns><see cref="INCommonConfig"/></returns>
        INCommonConfig ConfigureUnitOfWork<T>(Action<T> actions) where T : IUnitOfWorkConfiguration, new();
    }
}