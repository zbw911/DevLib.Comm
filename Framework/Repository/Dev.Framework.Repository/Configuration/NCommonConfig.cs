// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：NCommonConfig.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using Kt.Framework.Repository.Context;

namespace Kt.Framework.Repository.Configuration
{
    ///<summary>
    /// Default implementation of <see cref="INCommonConfig"/> class.
    ///</summary>
    public class NCommonConfig : INCommonConfig
    {
        readonly IContainerAdapter _containerAdapter;

        ///<summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="NCommonConfig"/>  class.
        ///</summary>
        ///<param name="containerAdapter">An instance of <see cref="IContainerAdapter"/> that can be
        /// used to register components.</param>
        public NCommonConfig(IContainerAdapter containerAdapter)
        {
            _containerAdapter = containerAdapter;
            InitializeDefaults();
        }

        /// <summary>
        /// Registers default components for Kt.Framework.Repository.
        /// </summary>
        void InitializeDefaults()
        {
            _containerAdapter.Register<IContext, Context.Impl.Context>();
        }

        /// <summary>
        /// Configure Kt.Framework.Repository state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed by Kt.Framework.Repository.
        /// </typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureState<T>() where T : IStateConfiguration, new()
        {
            var configuration = (T) Activator.CreateInstance(typeof (T));
            configuration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure Kt.Framework.Repository state storage using a <see cref="IStateConfiguration"/> instance.
        /// </summary>
        /// <typeparam name="T">A <see cref="IStateConfiguration"/> type that can be used to configure
        /// state storage services exposed by Kt.Framework.Repository.
        /// </typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IStateConfiguration"/> instance.</param>
        /// <returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureState<T>(Action<T> actions) where T : IStateConfiguration, new()
        {
            var configuration = (T) Activator.CreateInstance(typeof (T));
            actions(configuration);
            configuration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure data providers used by Kt.Framework.Repository.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers for Kt.Framework.Repository.</typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureData<T>() where T : IDataConfiguration, new()
        {
            var datConfiguration = (T) Activator.CreateInstance(typeof (T));
            datConfiguration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configure data providers used by Kt.Framework.Repository.
        /// </summary>
        /// <typeparam name="T">A <see cref="IDataConfiguration"/> type that can be used to configure
        /// data providers for Kt.Framework.Repository.</typeparam>
        /// <param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IDataConfiguration"/> instance.</param>
        /// <returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureData<T>(Action<T> actions) where T : IDataConfiguration, new()
        {
            var dataConfiguration = (T) Activator.CreateInstance(typeof (T));
            actions(dataConfiguration);
            dataConfiguration.Configure(_containerAdapter);
            return this;
        }

        /// <summary>
        /// Configures Kt.Framework.Repository unit of work settings.
        /// </summary>
        /// <typeparam name="T">A <see cref="IUnitOfWorkConfiguration"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        /// <returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureUnitOfWork<T> () where T : IUnitOfWorkConfiguration, new()
        {
            var uowConfiguration = (T) Activator.CreateInstance(typeof (T));
            uowConfiguration.Configure(_containerAdapter);
            return this;
        }

        ///<summary>
        /// Configures Kt.Framework.Repository unit of work settings.
        ///</summary>
        /// <typeparam name="T">A <see cref="INCommonConfig"/> type that can be used to configure
        /// unit of work settings.</typeparam>
        ///<param name="actions">An <see cref="Action{T}"/> delegate that can be used to perform
        /// custom actions on the <see cref="IUnitOfWorkConfiguration"/> instance.</param>
        ///<returns><see cref="INCommonConfig"/></returns>
        public INCommonConfig ConfigureUnitOfWork<T>(Action<T> actions) where T : IUnitOfWorkConfiguration, new()
        {
            var uowConfiguration = (T) Activator.CreateInstance(typeof (T));
            actions(uowConfiguration);
            uowConfiguration.Configure(_containerAdapter);
            return this;
        }
    }
}