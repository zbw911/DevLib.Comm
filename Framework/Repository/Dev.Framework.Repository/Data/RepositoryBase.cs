// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：RepositoryBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.ServiceLocation;
using Kt.Framework.Repository.Extensions;
using Kt.Framework.Repository.Specifications;

namespace Kt.Framework.Repository.Data
{
    ///<summary>
    /// A base class for implementors of <see cref="IRepository{TEntity}"/>.
    ///</summary>
    ///<typeparam name="TEntity"></typeparam>
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
    {
        /// <summary>
        /// Gets the <see cref="IQueryable{TEntity}"/> used by the <see cref="RepositoryBase{TEntity}"/> 
        /// to execute Linq queries.
        /// </summary>
        /// <value>A <see cref="IQueryable{TEntity}"/> instance.</value>
        /// <remarks>
        /// Inheritors of this base class should return a valid non-null <see cref="IQueryable{TEntity}"/> instance.
        /// </remarks>
        protected abstract IQueryable<TEntity> RepositoryQuery { get; }

        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        public virtual T UnitOfWork<T>() where T : IUnitOfWork
        {
            var currentScope = UnitOfWorkManager.CurrentUnitOfWork;

            if (currentScope == null) return default(T);

            Guard.Against<InvalidOperationException>(currentScope == null,
                                                     "No compatible UnitOfWork was found. Please start a compatible UnitOfWorkScope before " +
                                                     "using the repository.");

            Guard.TypeOf<T>(currentScope,
                                              "The current UnitOfWork instance is not compatible with the repository. " +
                                              "Please start a compatible unit of work before using the repository.");
            return ((T)currentScope);
        }

        /// <summary>
        /// Adds a transient instance of <typeparamref cref="TEntity"/> to be tracked
        /// and persisted by the repository.
        /// </summary>
        /// <param name="entity"></param>
        public abstract TEntity Add(TEntity entity);

        /// <summary>
        /// Marks the entity instance to be deleted from the store.
        /// </summary>
        /// <param name="entity">An instance of <typeparamref name="TEntity"/> that should be deleted.</param>
        public abstract void Delete(TEntity entity);

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        public abstract void Detach(TEntity entity);

        /// <summary>
        /// Attaches a detached entity, previously detached via the <see cref="IRepository{TEntity}.Detach"/> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        public abstract void Attach(TEntity entity);

        /// <summary>
        /// Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        public abstract void Refresh(TEntity entity);

        /// <summary>
        /// Querries the repository based on the provided specification and returns results that
        /// are only satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TEntity}"/> instnace used to filter results
        /// that only satisfy the specification.</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/> that can be used to enumerate over the results
        /// of the query.</returns>
        public IQueryable<TEntity> Query(ISpecification<TEntity> specification)
        {
            return RepositoryQuery.Where(specification.Predicate).AsQueryable();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> Predicate)
        {
            //RepositoryQuery.Where(Predicate);
            return RepositoryQuery.Where(Predicate);
        }


        public abstract TEntity Get<TA>(TA id);

        public virtual IQueryable<TEntity> Get()
        {
            return RepositoryQuery;
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> where)
        {
            return RepositoryQuery.FirstOrDefault(where);
        }

        public abstract void Delete(IEnumerable<TEntity> entities);

        public abstract void Delete(IQueryable<TEntity> entities);

        public abstract void Delete<TA>(TA id);

        public abstract TEntity Update(TEntity entity);

        public abstract void SaveChanges();
    }
}