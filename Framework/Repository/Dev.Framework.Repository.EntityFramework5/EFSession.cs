// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 16:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFSession.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    internal class EFSession : IEFSession
    {
        private readonly DbContext _context;

        private readonly Dictionary<Type, object> _objectSets = new Dictionary<Type, object>();

        /// <summary>
        ///     Internal implementation of the <see cref="IEFSession" /> interface.
        /// </summary>
        private bool _disposed;

        /// <summary>
        ///     Default Constructor.
        ///     Creates a new instance of the <see cref="EFSession" /> class.
        /// </summary>
        /// <param name="context"></param>
        public EFSession(DbContext context)
        {
            Guard.Against<ArgumentNullException>(context == null, "Expected a non-null DbContext instance.");
            _context = context;
        }

        /// <summary>
        ///     Gets the underlying <see cref="DbContext" />
        /// </summary>
        public DbContext Context
        {
            get { return _context; }
        }

        /// <summary>
        ///     Gets the Connection used by the <see cref="DbContext" />
        /// </summary>
        public IDbConnection Connection
        {
            get { return _context.Database.Connection; }
        }


        /// <summary>
        ///     Adds a transient instance to the context associated with the session.
        /// </summary>
        /// <param name="entity"></param>
        public void Add<T>(T entity) where T : class
        {
            GetObjectSet<T>().Add(entity);
        }

        /// <summary>
        ///     Deletes an entity instance from the context.
        /// </summary>
        /// <param name="entity"></param>
        public void Delete<T>(T entity) where T : class
        {
            GetObjectSet<T>().Remove(entity);
        }

        /// <summary>
        ///     Attaches an entity to the context. Changes to the entityt will be tracked by the underlying
        ///     <see
        ///         cref="DbContext" />
        /// </summary>
        /// <param name="entity"></param>
        public void Attach<T>(T entity) where T : class
        {
            //If the entity implementes the IEntityWithKey interface then we should use Context's Attach metho
            //instead of the set's Attach. Getting an exception 
            //"Mapping and metadata information could not be found for EntityType 'System.Data.Objects.DataClasses.IEntityWithKey"
            //when using set's Attach.
            //var entityWithKey = entity as IEntityWithKey;
            //if (entityWithKey != null)
            //    _context.Attach(entityWithKey);
            //else
            //    GetObjectSet<T>().Attach(entity);

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        ///     Detaches an entity from the context. Changes to the entity will not be tracked by the underlying
        ///     <see
        ///         cref="DbContext" />
        ///     .
        /// </summary>
        /// <param name="entity"></param>
        public void Detach<T>(T entity) where T : class
        {
            GetObjectSet<T>().Remove(entity);
        }

        /// <summary>
        ///     Refreshes an entity.
        /// </summary>
        /// <param name="entity"></param>
        public void Refresh<T>(T entity) where T : class
        {
            //_context.Set<T>().Refresh(RefreshMode.StoreWins, entity);
        }

        /// <summary>
        ///     Creates an <see cref="ObjectQuery" /> of <typeparamref name="T" /> that can be used
        ///     to query the entity.
        /// </summary>
        /// <typeparam name="T">The entityt type to query.</typeparam>
        /// <returns>
        ///     A <see cref="ObjectQuery{T}" /> instance.
        /// </returns>
        public DbQuery<T> CreateQuery<T>() where T : class
        {
            return _context.Set<T>();
        }

        /// <summary>
        ///     Saves changes made to the object context to the database.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private DbSet<T> GetObjectSet<T>() where T : class
        {
            object set = null;
            if (!_objectSets.TryGetValue(typeof (T), out set))
            {
                set = _context.Set<T>();
                _objectSets.Add(typeof (T), set);
            }
            return (DbSet<T>) set;
        }

        /// <summary>
        ///     Disposes off the managed and un-managed resources used.
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (!disposing)
                return;
            if (_disposed)
                return;

            _context.Dispose();
            _disposed = true;
        }
    }
}