// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IRepository.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Kt.Framework.Repository.Specifications;

namespace Kt.Framework.Repository.Data
{
    /// <summary>
    /// The <see cref="IRepository{TEntity}"/> interface defines a standard contract that repository
    /// components should implement.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity that the repository encapsulates.</typeparam>
    public interface IRepository<TEntity> //: IQueryable<TEntity>
    {
        /// <summary>
        /// Gets the a <see cref="IUnitOfWork"/> of <typeparamref name="T"/> that
        /// the repository will use to query the underlying store.
        /// </summary>
        /// <typeparam name="T">The type of <see cref="IUnitOfWork"/> implementation to retrieve.</typeparam>
        /// <returns>The <see cref="IUnitOfWork"/> implementation.</returns>
        T UnitOfWork<T>() where T : IUnitOfWork;

        /// <summary>
        /// Adds a transient instance of <typeparamref cref="TEntity"/> to be tracked
        /// and persisted by the repository.
        /// </summary>
        /// <param name="entity"></param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Marks the changes of an existing entity to be saved to the store.
        /// </summary>
        /// <param name="entity">An instance of <typeparamref name="TEntity"/> that should be
        /// updated in the database.</param>
        /// <remarks>Implementors of this method must handle the Update scneario. </remarks>
        void Delete(TEntity entity);

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        /// <exception cref="NotSupportedException">Implentors should throw the NotImplementedException if Detaching
        /// entities is not supported.</exception>
        void Detach(TEntity entity);

        /// <summary>
        /// Attaches a detached entity, previously detached via the <see cref="Detach"/> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        /// <exception cref="NotSupportedException">Implentors should throw the NotImplementedException if Attaching
        /// entities is not supported.</exception>
        void Attach(TEntity entity);

        /// <summary>
        /// Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        /// <exception cref="NotSupportedException">Implementors should throw the NotImplementedException if Refreshing
        /// entities is not supported.</exception>
        void Refresh(TEntity entity);

        /// <summary>
        /// Querries the repository based on the provided specification and returns results that
        /// are only satisfied by the specification.
        /// </summary>
        /// <param name="specification">A <see cref="ISpecification{TEntity}"/> instnace used to filter results
        /// that only satisfy the specification.</param>
        /// <returns>A <see cref="IEnumerable{TEntity}"/> that can be used to enumerate over the results
        /// of the query.</returns>
        IQueryable<TEntity> Query(ISpecification<TEntity> specification);


        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> Predicate);


        /// <summary>
        /// 取得类型
        /// </summary>
        /// <typeparam name="TA"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get<TA>(TA id);
        /// <summary>
        /// Returns all the entities in the table.
        /// </summary>
        IQueryable<TEntity> Get();

        /// <summary>
        /// Returns the first entity that matches a specific condition.
        /// </summary>
        /// <param name="where">Predicate function to test each entity.</param>
        TEntity First(Expression<Func<TEntity, bool>> where);


        /// <summary>
        /// Deletes a list of entites from the database.
        /// </summary>
        /// <param name="entities">List of entities to delete.</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// Deletes a list of entites from the database.
        /// </summary>
        /// <param name="entities">List of entities to delete.</param>
        void Delete(IQueryable<TEntity> entities);

        /// <summary>
        /// 根据ID进行删除，ID要指定类型
        /// </summary>
        /// <param name="id">模板类型</param>
        void Delete<TA>(TA id);

        /// <summary>
        /// Updates an entity. Attaches the entity to the data context if it is not attached.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Saves changes made to the data context through this repository.
        /// </summary>
        void SaveChanges();
    }
}