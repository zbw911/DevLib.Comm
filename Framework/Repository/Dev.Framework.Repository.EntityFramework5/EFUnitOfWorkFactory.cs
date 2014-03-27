// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 16:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFUnitOfWorkFactory.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data.Entity;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    /// <summary>
    ///     Implements the <see cref="IUnitOfWorkFactory" /> interface to provide an implementation of a factory
    ///     that creates <see cref="EFUnitOfWork" /> instances.
    /// </summary>
    public class EFUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly EFSessionResolver _resolver = new EFSessionResolver();

        /// <summary>
        ///     Creates a new instance of <see cref="IUnitOfWork" />.
        /// </summary>
        /// <returns>
        ///     Instances of <see cref="EFUnitOfWork" />.
        /// </returns>
        public IUnitOfWork Create()
        {
            Guard.Against<InvalidOperationException>(
                _resolver.ObjectContextsRegistered == 0,
                "No ObjectContext providers have been registered. You must register ObjectContext providers using " +
                "the RegisterObjectContextProvider method or use Kt.Framework.Repository.Configure class to configure Kt.Framework.Repository.EntityFramework " +
                "using the EFConfiguration class and register ObjectContext instances using the WithObjectContext method.");

            return new EFUnitOfWork(_resolver);
        }

        /// <summary>
        ///     Registers a <see cref="Func{T}" /> of type <see cref="ObjectContext" /> provider that can be used
        ///     to resolve instances of <see cref="ObjectContext" />.
        /// </summary>
        /// <param name="contextProvider">
        ///     A <see cref="Func{T}" /> of type <see cref="ObjectContext" />.
        /// </param>
        public void RegisterObjectContextProvider(Func<DbContext> contextProvider)
        {
            Guard.Against<ArgumentNullException>(contextProvider == null,
                                                 "Invalid object context provider registration. " +
                                                 "Expected a non-null Func<ObjectContext> instance.");
            _resolver.RegisterObjectContextProvider(contextProvider);
        }
    }
}