// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IContainerAdapter.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;

namespace Kt.Framework.Repository.Configuration
{
    ///<summary>
    /// Base interface implemented by specific containers that allow registering components to an IoC container.
    ///</summary>
    public interface IContainerAdapter
    {
        /// <summary>
        /// Registers a default implementation type for a service type.
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> type representing the service
        /// for which the implementation type is registered. </typeparam>
        /// <typeparam name="TImplementation">The <typeparamref name="TImplementation"/> type representing
        /// the implementation registered for the <typeparamref name="TService"/></typeparam>
        void Register<TService, TImplementation>() where TImplementation : TService;

        /// <summary>
        /// Registers a named implementation type of a service type.
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> type representing the service
        /// for which the implementation type is registered. </typeparam>
        /// <typeparam name="TImplementation">The <typeparamref name="TImplementation"/> type representing
        /// the implementation registered for the <typeparamref name="TService"/></typeparam>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        void Register<TService, TImplementation>(string named) where TImplementation : TService;

        /// <summary>
        /// Registers a default implementation type for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service for which the
        ///  implementation type is registered.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing the implementation
        /// registered for the service type.</param>
        void Register(Type service, Type implementation) ;

        /// <summary>
        /// Registers a named implementation type for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service for which the
        /// implementation type if registered.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing the implementaton
        /// registered for the service.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        void Register(Type service, Type implementation, string named);

        ///<summary>
        /// Registers a open generic implementation for a generic service type.
        ///</summary>
        ///<param name="service">The type representing the service for which the implementation type is registered.</param>
        ///<param name="implementation">The type representing the implementation registered for the service.</param>
        void RegisterGeneric(Type service, Type implementation);

        ///<summary>
        /// Registers a named open generic implementation for a generic service type.
        ///</summary>
        ///<param name="service">The type representing the service for which the implementation is registered.</param>
        ///<param name="implementation">The type representing the implementation registered for the service.</param>
        ///<param name="named">string. The service name with which the implementation is registerd.</param>
        void RegisterGeneric(Type service, Type implementation, string named);

        /// <summary>
        /// Registers a default implementation type for a service type as a singleton.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</typeparam>
        /// <typeparam name="TImplementation"><typeparamref name="TImplementation"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</typeparam>
        void RegisterSingleton<TService, TImplementation>() where TImplementation : TService;

        /// <summary>
        /// Registers a named implementation type for a service type as a singleton.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</typeparam>
        /// <typeparam name="TImplementation"><typeparamref name="TImplementation"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</typeparam>
        /// <param name="named">string. The service name with which the implementation is registerd.</param>
        void RegisterSingleton<TService, TImplementation>(string named) where TImplementation : TService;

        /// <summary>
        /// Registers a default implementation type for a service type as a singleton.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</param>
        void RegisterSingleton(Type service, Type implementation);

        /// <summary>
        /// Registers a named implementation type for a service type as a singleton.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        void RegisterSingleton(Type service, Type implementation, string named);

        /// <summary>
        /// Registers an instance as an implementation for a service type.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing
        /// the service for which the instance is registered.</typeparam>
        /// <param name="instance">An instance of type <typeparamref name="TService"/> that is
        /// registered as an instance for <typeparamref name="TService"/>.</param>
        void RegisterInstance<TService>(TService instance) where TService : class ;

        /// <summary>
        /// Registers an named instance as an implementation for a service type.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing
        /// the service for which the instance is registered.</typeparam>
        /// <param name="instance">An instance of type <typeparamref name="TService"/> that is
        /// registered as an instance for <typeparamref name="TService"/>.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        void RegisterInstance<TService>(TService instance, string named) where TService : class ;

        /// <summary>
        /// Registers an instance as an implementation for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing
        /// the service for which the instance is registered.</param>
        /// <param name="instance">An instance of <paramref name="service"/> that is
        /// registered as an instance for the service.</param>
        void RegisterInstance(Type service, object instance);

        /// <summary>
        /// Registers a named instance as an implementation for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing
        /// the service for which the instance is registered.</param>
        /// <param name="instance">An instance of <paramref name="service"/> that is
        /// registered as an instance for the service.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        void RegisterInstance(Type service, object instance, string named);
    }
}