// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 16:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFConfiguration.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Data.Entity;
using Kt.Framework.Repository.Configuration;
 

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    /// <summary>
    ///     Implementation of <see cref="IDataConfiguration" /> for Entity Framework.
    /// </summary>
    public class EFConfiguration : IDataConfiguration
    {
        private readonly EFUnitOfWorkFactory _factory = new EFUnitOfWorkFactory();

        /// <summary>
        ///     Called by Kt.Framework.Repository <see cref="Configure" /> to configure data providers.
        /// </summary>
        /// <param name="containerAdapter">
        ///     The <see cref="IContainerAdapter" /> instance that allows
        ///     registering components.
        /// </param>
        public void Configure(IContainerAdapter containerAdapter)
        {
            containerAdapter.RegisterInstance<IUnitOfWorkFactory>(_factory);
            containerAdapter.RegisterGeneric(typeof (IRepository<>), typeof (EFRepository<>));
        }

        /// <summary>
        ///     Configures unit of work instances to use the specified <see cref="DbContext" />.
        /// </summary>
        /// <param name="DbContextProvider">
        ///     A <see cref="Func{T}" /> of type <see cref="DbContext" />
        ///     that can be used to construct <see cref="DbContext" /> instances.
        /// </param>
        /// <returns>
        ///     <see cref="EFConfiguration" />
        /// </returns>
        public EFConfiguration WithObjectContext(Func<DbContext> objectContextProvider)
        {
            Guard.Against<ArgumentNullException>(objectContextProvider == null,
                                                 "Expected a non-null Func<ObjectContext> instance.");
            _factory.RegisterObjectContextProvider(objectContextProvider);
            return this;
        }
    }
}