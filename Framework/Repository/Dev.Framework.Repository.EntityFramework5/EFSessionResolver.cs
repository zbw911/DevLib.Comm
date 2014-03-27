// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月22日 15:34
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFSessionResolver.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using Kt.Framework.Repository.Extensions;

namespace Kt.Framework.Repository.Data.EntityFramework5
{
    /// <summary>
    ///     Implementation of <see cref="IEFSessionResolver" /> that resolves <see cref="IEFSession" /> instances.
    /// </summary>
    public class EFSessionResolver : IEFSessionResolver
    {
        private readonly IDictionary<string, Guid> _objectContextTypeCache = new Dictionary<string, Guid>();
        private readonly IDictionary<Guid, Func<DbContext>> _objectContexts = new Dictionary<Guid, Func<DbContext>>();

        /// <summary>
        ///     Gets the number of <see cref="ObjectContext" /> instances registered with the session resolver.
        /// </summary>
        public int ObjectContextsRegistered
        {
            get { return _objectContexts.Count; }
        }

        /// <summary>
        ///     Gets the unique ObjectContext key for a type.
        /// </summary>
        /// <typeparam name="T">The type for which the ObjectContext key should be retrieved.</typeparam>
        /// <returns>
        ///     A <see cref="Guid" /> representing the unique object context key.
        /// </returns>
        public Guid GetSessionKeyFor<T>()
        {
            string typeName = typeof (T).FullName.ToLower();
            Guid key;
            if (!_objectContextTypeCache.TryGetValue(typeName, out key))
                throw new ArgumentException("No ObjectContext has been registered for the specified type.");
            return key;
        }

        /// <summary>
        ///     Opens a <see cref="IEFSession" /> instance for a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type for which an <see cref="IEFSession" /> is returned.
        /// </typeparam>
        /// <returns>
        ///     An instance of <see cref="IEFSession" />.
        /// </returns>
        public IEFSession OpenSessionFor<T>()
        {
            DbContext context = GetObjectContextFor<T>();
            return new EFSession(context);
        }

        /// <summary>
        ///     Gets the <see cref="ObjectContext" /> that can be used to query and update a given type.
        /// </summary>
        /// <typeparam name="T">
        ///     The type for which an <see cref="ObjectContext" /> is returned.
        /// </typeparam>
        /// <returns>
        ///     An <see cref="ObjectContext" /> that can be used to query and update the given type.
        /// </returns>
        public DbContext GetObjectContextFor<T>()
        {
            string typeName = typeof (T).FullName.ToLower();
            Guid key;
            if (!_objectContextTypeCache.TryGetValue(typeName, out key))
                throw new ArgumentException("No ObjectContext has been registered for the specified type.");
            return _objectContexts[key]();
        }

        /// <summary>
        ///     Registers an <see cref="ObjectContext" /> provider with the resolver.
        /// </summary>
        /// <param name="contextProvider">
        ///     A <see cref="Func{T}" /> of type <see cref="ObjectContext" />.
        /// </param>
        public void RegisterObjectContextProvider(Func<DbContext> contextProvider)
        {
            Guid key = Guid.NewGuid();
            _objectContexts.Add(key, contextProvider);
            //Getting the object context and populating the _objectContextTypeCache.
            DbContext context = contextProvider();

            IList<Type> entities = GetDbContextGetGenericType(context);

            //跳过
            entities.ForEach(entity => { _objectContextTypeCache.Add(entity.FullName.ToLower(), key); });
        }


        /// <summary>
        ///     取得dbcontext中的 DbSet 中的类型，Added by zbw911
        ///     2012-12-22
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private IList<Type> GetDbContextGetGenericType(DbContext context)
        {
            var listtype = new List<Type>();
            Type type = context.GetType();

            PropertyInfo[] listpropertyies = type.GetProperties();

            foreach (var property in listpropertyies)
            {
                Type t = property.PropertyType;
                if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof (DbSet<>))
                {
                    Type[] args = t.GetGenericArguments();
                    listtype.AddRange(args);
                }
            }

            return listtype;
        }
    }
}