// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月21日 16:28
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
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Kt.Framework.Repository.State;
using Microsoft.Practices.ServiceLocation;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    /// <summary>
    ///     Inherits from the <see cref="RepositoryBase{TEntity}" /> class to provide an implementation of a
    ///     Repository that uses Entity Framework.
    /// </summary>
    public class EFRepository<TEntity> : RepositoryBase<TEntity> where TEntity : class
    {
        /// <summary>
        ///     当前的自启动单元管理，放于缓存中
        /// </summary>
        private static readonly Func<EFUnitOfWork> Cur_UOWManager = () =>
                                                                        {
                                                                            string LocalTransactionManagerKey =
                                                                                "EFUOW.My.Custom.NoScoap";

                                                                            var state =
                                                                                ServiceLocator.Current
                                                                                              .GetInstance<IState>();
                                                                            var uow =
                                                                                state.Local.Get<EFUnitOfWork>(
                                                                                    LocalTransactionManagerKey);
                                                                            if (uow == null)
                                                                            {
                                                                                // 如果没有启动，那就启动一个默认的
                                                                                var unitOfWorkFactory =
                                                                                    (EFUnitOfWorkFactory)
                                                                                    ServiceLocator.Current
                                                                                                  .GetInstance
                                                                                        <IUnitOfWorkFactory>();
                                                                                uow =
                                                                                    (EFUnitOfWork)
                                                                                    unitOfWorkFactory.Create();
                                                                                state.Local.Put(
                                                                                    LocalTransactionManagerKey, uow);
                                                                            }
                                                                            return uow;
                                                                        };

        private readonly List<string> _includes = new List<string>();

        /// <summary>
        ///     Creates a new instance of the <see cref="EFRepository{TEntity}" /> class.
        /// </summary>
        public EFRepository()
        {
            if (ServiceLocator.Current == null)
                return;
        }

        /// <summary>
        ///     Gets the <see cref="ObjectContext" /> to be used by the repository.
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
                EFUnitOfWork eFUnitOfWork = Cur_UOWManager(); // (EFUnitOfWork)unitOfWorkFactory.Create();
                IEFSession newsession = eFUnitOfWork.GetSession<TEntity>();

                return newsession;
            }
        }


        /// <summary>
        ///     Gets the <see cref="IQueryable{T}" /> used by the <see cref="RepositoryBase{TEntity}" />
        ///     to execute Linq queries.
        /// </summary>
        /// <value>
        ///     A <see cref="IQueryable{TEntity}" /> instance.
        /// </value>
        protected override IQueryable<TEntity> RepositoryQuery
        {
            get
            {
                DbQuery<TEntity> query = Session.CreateQuery<TEntity>();
                if (_includes.Count > 0)
                    _includes.ForEach(x => query = query.Include(x));
                return query;
            }
        }

        /// <summary>
        ///     Adds a transient instance of <typeparamref cref="TEntity" /> to be tracked
        ///     and persisted by the repository.
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
        ///     Marks the entity instance to be deleted from the store.
        /// </summary>
        /// <param name="entity">
        ///     An instance of <typeparamref name="TEntity" /> that should be deleted.
        /// </param>
        public override void Delete(TEntity entity)
        {
            Session.Delete(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        ///     Detaches a instance from the repository.
        /// </summary>
        /// <param name="entity">The entity instance, currently being tracked via the repository, to detach.</param>
        /// <exception cref="NotImplementedException">
        ///     Implentors should throw the NotImplementedException if Detaching
        ///     entities is not supported.
        /// </exception>
        public override void Detach(TEntity entity)
        {
            Session.Detach(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        ///     Attaches a detached entity, previously detached via the <see cref="IRepository{TEntity}.Detach" /> method.
        /// </summary>
        /// <param name="entity">The entity instance to attach back to the repository.</param>
        /// <exception cref="NotImplementedException">
        ///     Implentors should throw the NotImplementedException if Attaching
        ///     entities is not supported.
        /// </exception>
        public override void Attach(TEntity entity)
        {
            Session.Attach(entity);
            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        /// <summary>
        ///     Refreshes a entity instance.
        /// </summary>
        /// <param name="entity">The entity to refresh.</param>
        /// <exception cref="NotImplementedException">
        ///     Implementors should throw the NotImplementedException if Refreshing
        ///     entities is not supported.
        /// </exception>
        public override void Refresh(TEntity entity)
        {
            Session.Refresh(entity);
            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }


        public override TEntity Get<TA>(TA keyValue)
        {

            DbContext _context = Session.Context;
            DbSet<TEntity> _table = _context.Set<TEntity>();

            return _table.Find(keyValue);

            //EntityKey key = GetEntityKey<TEntity>(keyValue);

            //object originalItem;
            //if (((IObjectContextAdapter) Session.Context).ObjectContext.TryGetObjectByKey(key, out originalItem))
            //{
            //    return (TEntity) originalItem;
            //}
            //return default(TEntity);
        }

        public override void Delete(IEnumerable<TEntity> entities)
        {
            DbContext _context = Session.Context;
            DbSet<TEntity> _table = _context.Set<TEntity>();
            foreach (var entity in entities)
            {
                _table.Remove(entity);
            }

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        public override void Delete(IQueryable<TEntity> entities)
        {
            DbContext _context = Session.Context;
            DbSet<TEntity> _table = _context.Set<TEntity>();
            foreach (var entity in entities)
            {
                _table.Remove(entity);
            }

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        public override void Delete<TA>(TA id)
        {
            DbContext _context = Session.Context;
            DbSet<TEntity> _table = _context.Set<TEntity>();
            TEntity entity = Get(id);
            //_table.(entity);

            _table.Remove(entity);

            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();
        }

        public override TEntity Update(TEntity entity)
        {
            DbContext _context = Session.Context;
            DbSet<TEntity> _table = _context.Set<TEntity>();

            _context.Entry(entity).State = EntityState.Modified;
            var unitOfWork = base.UnitOfWork<EFUnitOfWork>();
            if (unitOfWork == null)
                Session.SaveChanges();

            return entity;
        }

        public override void SaveChanges()
        {
            Session.SaveChanges();
        }

        //
        private object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            ObjectStateEntry objectStateEntry =
                ((IObjectContextAdapter)this).ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        private EntityKey GetEntityKey<TEntity>(object keyValue) where TEntity : class
        {
            string entitySetName = GetEntityName<TEntity>();
            ObjectSet<TEntity> objectSet =
                ((IObjectContextAdapter)Session.Context).ObjectContext.CreateObjectSet<TEntity>();
            string keyPropertyName = objectSet.EntitySet.ElementType.KeyMembers[0].ToString();
            var entityKey = new EntityKey(entitySetName, new[] { new EntityKeyMember(keyPropertyName, keyValue) });
            return entityKey;
        }

        private string GetEntityName<TEntity>() where TEntity : class
        {
            // PluralizationService pluralizer = PluralizationService.CreateService(CultureInfo.GetCultureInfo("en"));
            // return string.Format("{0}.{1}", ((IObjectContextAdapter)DbContext).ObjectContext.DefaultContainerName, pluralizer.Pluralize(typeof(TEntity).Name));

            // Thanks to Kamyar Paykhan -  http://huyrua.wordpress.com/2011/04/13/entity-framework-4-poco-repository-and-specification-pattern-upgraded-to-ef-4-1/#comment-688
            string entitySetName = ((IObjectContextAdapter)Session.Context).ObjectContext
                                                                            .MetadataWorkspace
                                                                            .GetEntityContainer(
                                                                                ((IObjectContextAdapter)Session.Context)
                                                                                    .ObjectContext.DefaultContainerName,
                                                                                DataSpace.CSpace)
                                                                            .BaseEntitySets.First(
                                                                                bes =>
                                                                                bes.ElementType.Name ==
                                                                                typeof(TEntity).Name).Name;
            return string.Format("{0}.{1}", ((IObjectContextAdapter)Session.Context).ObjectContext.DefaultContainerName,
                                 entitySetName);
        }
    }

    /*
    internal static class exends
    {
        public static IEnumerable<string> PrimayKeysFor(
          this DbContext context,
          object entity)
        {
            Contract.Requires(context != null);
            Contract.Requires(entity != null);

            return context.PrimayKeysFor(entity.GetType());
        }

        public static IEnumerable<string> PrimayKeysFor(
            this DbContext context,
            Type entityType)
        {
            Contract.Requires(context != null);
            Contract.Requires(entityType != null);

            var metadataWorkspace =
                ((IObjectContextAdapter)context).ObjectContext.MetadataWorkspace;
            var objectItemCollection =
                (ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace);
            var ospaceTypes = metadataWorkspace.GetItems<EntityType>(DataSpace.OSpace);

            var ospaceType = ospaceTypes
                .FirstOrDefault(t => objectItemCollection.GetClrType(t) == entityType);

            if (ospaceType == null)
            {
                throw new ArgumentException(
                    string.Format(
                        "The type '{0}' is not mapped as an entity type.",
                        entityType.Name),
                    "entityType");
            }

            return ospaceType.KeyMembers.Select(k => k.Name);
        }
     
    }
* */
}