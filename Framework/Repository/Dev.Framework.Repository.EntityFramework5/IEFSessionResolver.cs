// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 16:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IEFSessionResolver.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    /// <summary>
    ///     Interface implemented by a custom resolver for Entity Framework that resolves <see cref="ObjectContext" />
    ///     instances for a type.
    /// </summary>
    public interface IEFSessionResolver
    {
        /// <summary>
        ///     Gets the count of <see cref="ObjectContext" /> providers registered with the resolver.
        /// </summary>
        int ObjectContextsRegistered { get; }

        /// <summary>
        ///     Gets the unique <see cref="IEFSession" /> key for a type.
        /// </summary>
        /// <typeparam name="T">The type for which the ObjectContext key should be retrieved.</typeparam>
        /// <returns>
        ///     A <see cref="Guid" /> representing the unique object context key.
        /// </returns>
        Guid GetSessionKeyFor<T>();

        /// <summary>
        ///     Opens a <see cref="IEFSession" /> instance for a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type for which an <see cref="IEFSession" /> is returned.
        /// </typeparam>
        /// <returns>
        ///     An instance of <see cref="IEFSession" />.
        /// </returns>
        IEFSession OpenSessionFor<T>();

        /// <summary>
        ///     Gets the <see cref="ObjectContext" /> that can be used to query and update a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type for which an <see cref="ObjectContext" /> is returned.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ObjectContext" /> that can be used to query and update the given type.
        /// </returns>
        DbContext GetObjectContextFor<T>();

        /// <summary>
        ///     Registers an <see cref="ObjectContext" /> provider with the resolver.
        /// </summary>
        /// <param name="contextProvider">
        ///     A <see cref="Func{T}" /> of type <see cref="ObjectContext" />.
        /// </param>
        void RegisterObjectContextProvider(Func<DbContext> contextProvider);
    }
}