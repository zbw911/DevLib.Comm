// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFRepository.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************


using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using Kt.Framework.Repository.State;

namespace Kt.Framework.Repository.Data.EntityFramework
{
    /// <summary>
    /// Inherits from the <see cref="RepositoryBase{TEntity}"/> class to provide an implementation of a
    /// Repository that uses Entity Framework.
    /// </summary>
    public class EFRepository<TEntity> : RepositoryBase<TEntity> where TEntity : EntityObject
    {


        readonly List<string> _includes = new List<string>();

        /// <summary>
        /// 当前的自启动单元管理，放于缓存中
        /// </summary>
        static readonly Func<EFUnitOfWork> Cur_UOWManager = () =>
        {
            var LocalTransactionManagerKey = "EFUOW.My.Custom.NoScoap";

            var state = ServiceLocator.Current.GetInstance<IState>();
            var uow = state.Local.Get<EFUnitOfWork>(LocalTransactionManagerKey);
            if (uow == null)
            {
                // 如果没有启动，那就启动一个默认的
                var unitOfWorkFactory = (EFUnitOfWorkFactory)ServiceLocator.Current.GetInstance<IUnitOfWorkFactory>();
                uow = (EFUnitOfWork)unitOfWorkFactory.Create();
                state.Local.Put(LocalTransactionManagerKey, uow);
            }
            return uow;
        };

        /// <summary>
        /// Creates a new instance of the <see cref="EFRepository{TEntity}"/> class.
        /// </summary>
        public EFRepository()
        {
            if (ServiceLocator.Current == null)
                return;
        }

        /// <summary>
        /// Gets the <see cref="ObjectContext"/> to be used by the repository.
        /// </summary>
        private IEFSession Session
        {
            get
            {
                //if (_privateSession != null)
                //    return _privateSession;
                var unitOfWork = base.UnitOfWork<EFUnitOfWork>();

                if (unitOfWork != null)
                    return unitOfWork.GetSession<TEntity>();

                // 如果没有启动，那就启动一个默认的
                //var unitOfWorkFactory = (EFUnitOfWorkFactory)ServiceLocator.Current.GetInstance<IUnitOfWorkFactory>();
                var eFUnitOfWork = Cur_UOWManager();// (EFUnitOfWork)unitOfWorkFactory.Create();
                var newsession = eFUnitOfWork.GetSession<TEntity>();

                return newsession;


            }
        }



        /// <summary>
        /// Gets the <see cref="IQueryable{TEntity}"/> used by the <see cref="RepositoryBase{TEntity}"/> 
        /// to execute Linq queries.
        /// </summary>
        /// <value>A <see cref="IQueryable{TEntity}"/> instance.</value>
        protected override IQueryable<TEntity> RepositoryQuery
        {
            get
            {
                var query = Session.CreateQuery<TEntity>();
                if (_includes.Count > 0)
                    _includes.ForEach(x => query = query.Include(x));
                return query;
            }
        }

        /// <summary>
        /// Adds a transient instance of <typeparamref cref="TEntity"/> to be tracked
        /// and persisted by the repository.
        /// </summary>
        /// <param name="entity"></param>
        public override TEntity Add(TEntity entity)
        {
            Session.Add(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();

            return entity;
        }



        /// <summary>
        /// Marks the entity instance to be deleted from the store.
        /// </summary>
        /// <param name="entity">An instance of <typeparamref name="TEntity"/> that should be deleted.</param>
        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        /// Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        /// <exception cref="NotImplementedException">Implentors should throw the NotImplementedException if Detaching
        /// entities is not supported.</exception>
        public override void Detach(TEntity entity)
        {
            Session.Detach(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        /// Attaches a detached entity, previously detached via the <see cref="IRepository{TEntity}.Detach"/> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        /// <exception cref="NotImplementedException">Implentors should throw the NotImplementedException if Attaching
        /// entities is not supported.</exception>
        public override void Attach(TEntity entity)
        {
            Session.Attach(entity);
            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        /// Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        /// <exception cref="NotImplementedException">Implementors should throw the NotImplementedException if Refreshing
        /// entities is not supported.</exception>
        public override void Refresh(TEntity entity)
        {
            Session.Refresh(entity);
            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }



        public override TEntity Get<TA>(TA id)
        {
            var _context = Session.Context;
            var _table = _context.CreateObjectSet<TEntity>();
            string entitySetName = _context.DefaultContainerName + "." + _table.EntitySet.Name;
            string keyName = _table.EntitySet.ElementType.KeyMembers[0].ToString();
            var key = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyName, id) });

            object found;
            if (_context.TryGetObjectByKey(key, out found))
                return (TEntity)found;
            else
                return null;
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
            var _context = Session.Context;
            var _table = _context.CreateObjectSet<TEntity>();
            foreach (var entity in entities)
            {
                _table.DeleteObject(entity);
            }

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();


        }

        public override void Delete(IQueryable<TEntity> entities)
        {
            var _context = Session.Context;
            var _table = _context.CreateObjectSet<TEntity>();
            foreach (var entity in entities)
            {
                _table.DeleteObject(entity);
            }

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        public override void Delete<TA>(TA id)
        {
            var _context = Session.Context;
            var _table = _context.CreateObjectSet<TEntity>();
            var entity = Get<TA>(id);
            _table.DeleteObject(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        public override TEntity Update(TEntity entity)
        {
            var _context = Session.Context;
            var _table = _context.CreateObjectSet<TEntity>();

            if (entity.EntityState == EntityState.Detached)
            {
                _table.Attach(entity);
            }

            _table.Context.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();

            return entity;


        }

        public override void SaveChanges()
        {
            Session.SaveChanges();
        }


    }
}
