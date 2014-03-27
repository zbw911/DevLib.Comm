// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：EFSessionResolver.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************


using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using Kt.Framework.Repository.Extensions;

namespace Kt.Framework.Repository.Data.EntityFramework
{
    /// <summary>
    /// Implementation of <see cref="IEFSessionResolver"/> that resolves <see cref="IEFSession"/> instances.
    /// </summary>
    public class EFSessionResolver : IEFSessionResolver
    {
        readonly IDictionary<string, Guid> _objectContextTypeCache = new Dictionary<string, Guid>();
        readonly IDictionary<Guid, Func<ObjectContext>> _objectContexts = new Dictionary<Guid, Func<ObjectContext>>();

        /// <summary>
        /// Gets the number of <see cref="ObjectContext"/> instances registered with the session resolver.
        /// </summary>
        public int ObjectContextsRegistered
        {
            get { return _objectContexts.Count; }
        }

        /// <summary>
        /// Gets the unique ObjectContext key for a type. 
        /// </summary>
        /// <typeparam name="T">The type for which the ObjectContext key should be retrieved.</typeparam>
        /// <returns>A <see cref="Guid"/> representing the unique object context key.</returns>
        public Guid GetSessionKeyFor<T>()
        {
            var typeName = typeof(T).Name;
            Guid key;
            if (!_objectContextTypeCache.TryGetValue(typeName, out key))
                throw new ArgumentException("No ObjectContext has been registered for the specified type.");
            return key;
        }

        /// <summary>
        /// Opens a <see cref="IEFSession"/> instance for a given type.
        /// </summary>
        /// <typeparam name="T">The type for which an <see cref="IEFSession"/> is returned.</typeparam>
        /// <returns>An instance of <see cref="IEFSession"/>.</returns>
        public IEFSession OpenSessionFor<T>()
        {
            var context = GetObjectContextFor<T>();
            return new EFSession(context);
        }

        /// <summary>
        /// Gets the <see cref="ObjectContext"/> that can be used to query and update a given type.
        /// </summary>
        /// <typeparam name="T">The type for which an <see cref="ObjectContext"/> is returned.</typeparam>
        /// <returns>An <see cref="ObjectContext"/> that can be used to query and update the given type.</returns>
        public ObjectContext GetObjectContextFor<T>()
        {
            var typeName = typeof(T).Name;
            Guid key;
            if (!_objectContextTypeCache.TryGetValue(typeName, out key))
                throw new ArgumentException("No ObjectContext has been registered for the specified type.");
            return _objectContexts[key]();
        }

        /// <summary>
        /// Registers an <see cref="ObjectContext"/> provider with the resolver.
        /// </summary>
        /// <param name="contextProvider">A <see cref="Func{T}"/> of type <see cref="ObjectContext"/>.</param>
        public void RegisterObjectContextProvider(Func<ObjectContext> contextProvider)
        {
            var key = Guid.NewGuid();
            _objectContexts.Add(key, contextProvider);
            //Getting the object context and populating the _objectContextTypeCache.
            var context = contextProvider();
            var entities = context.MetadataWorkspace.GetItems<EntityType>(DataSpace.CSpace);

            //跳过
            entities.ForEach(entity => { if ("sysdiagrams" == entity.Name) return; _objectContextTypeCache.Add(entity.Name, key); });
        }
    }
}