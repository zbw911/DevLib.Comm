// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：DefaultStateConfiguration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using Kt.Framework.Repository.State;
using Kt.Framework.Repository.State.Impl;

namespace Kt.Framework.Repository.Configuration
{
    /// <summary>
    /// Default implementation of <see cref="IStateConfiguration"/> that allows configuring
    /// state storage in Kt.Framework.Repository.
    /// </summary>
    public class DefaultStateConfiguration : IStateConfiguration
    {
        Type _customCacheType;
        Type _customSessionType;
        Type _customLocalStateType;
        Type _customApplicationStateType;

        /// <summary>
        /// Instructs Kt.Framework.Repository to use the custom <see cref="ICacheState"/> type as the cache storage.
        /// </summary>
        /// <typeparam name="T">A type that implements the <see cref="ICacheState"/> interface.</typeparam>
        /// <returns>The <see cref="DefaultStateConfiguration"/> instance.</returns>
        public DefaultStateConfiguration UseCustomCacheOf<T>() where T : ICacheState
        {
            _customCacheType = typeof (T);
            return this;
        }

        /// <summary>
        /// Instructs Kt.Framework.Repository to use a custom <see cref="ISessionState"/> type as the session state storage.
        /// </summary>
        /// <typeparam name="T">A type that implements the <see cref="ISessionState"/> interface.</typeparam>
        /// <returns>The <see cref="DefaultStateConfiguration"/> instance</returns>
        public DefaultStateConfiguration UseCustomSessionStateOf<T>() where T : ISessionState
        {
            _customSessionType = typeof (T);
            return this;
        }

        /// <summary>
        /// Instructs Kt.Framework.Repository to use a custom <see cref="ILocalState"/> type as the local state storage.
        /// </summary>
        /// <typeparam name="T">A type that implements the <see cref="ILocalState"/> interface.</typeparam>
        /// <returns>The <see cref="DefaultStateConfiguration"/> instance.</returns>
        public DefaultStateConfiguration UseCustomLocalStateOf<T>() where T : ILocalState
        {
            _customLocalStateType = typeof (T);
            return this;
        }

        /// <summary>
        /// Instructs Kt.Framework.Repository to use a custom <see cref="IApplicationState"/> type as the application stage storage.
        /// </summary>
        /// <typeparam name="T">A type that implements the <see cref="IApplicationState"/> interface.</typeparam>
        /// <returns>The <see cref="DefaultStateConfiguration"/> instance.</returns>
        public DefaultStateConfiguration UseCustomApplicationStateOf<T>() where T : IApplicationState
        {
            _customApplicationStateType = typeof (T);
            return this;
        }

        /// <summary>
        /// Called by Kt.Framework.Repository <see cref="Configure"/> to configure state storage.
        /// </summary>
        /// <param name="containerAdapter">The <see cref="IContainerAdapter"/> instance that can be
        /// used to register state storage components.</param>
        public void Configure(IContainerAdapter containerAdapter)
        {
            if (_customSessionType != null)
                containerAdapter.Register(typeof(ISessionState), _customSessionType);
            else
            {
                containerAdapter.Register<ISessionStateSelector, DefaultSessionStateSelector>();
                containerAdapter.Register<ISessionState, SessionStateWrapper>();
            }

            if (_customLocalStateType != null)
                containerAdapter.Register(typeof(ILocalState), _customLocalStateType);
            else
            {
                containerAdapter.Register<ILocalStateSelector, DefaultLocalStateSelector>();
                containerAdapter.Register<ILocalState, LocalStateWrapper>();
            }
            if (_customCacheType != null)
                containerAdapter.Register(typeof(ICacheState), _customCacheType);
            else
                containerAdapter.Register<ICacheState, HttpRuntimeCache>();
            if (_customApplicationStateType != null)
                containerAdapter.RegisterSingleton(typeof(IApplicationState), _customApplicationStateType);
            else
                containerAdapter.RegisterSingleton<IApplicationState, ApplicationState>();

            containerAdapter.Register<IState, State.Impl.State>();
        }
    }
}