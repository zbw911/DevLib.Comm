// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：HttpRuntimeCache.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using System.Web;

namespace Kt.Framework.Repository.State.Impl
{
    /// <summary>
    /// Implementation of <see cref="ICacheState"/> that uses the ASP.Net runtime cache.
    /// </summary>
    public class HttpRuntimeCache : ICacheState
    {
        ///<summary>
        /// Gets state data stored with the default key.
        ///</summary>
        ///<typeparam name="T">The type of data to retrieve.</typeparam>
        ///<returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        public T Get<T>()
        {
            return Get<T>(null);
        }

        /// <summary>
        /// Gets state data stored with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        public T Get<T>(object key)
        {
            return (T) HttpRuntime.Cache.Get(key.BuildFullKey<T>());
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(T instance)
        {
            Put(null, instance);
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(object key, T instance)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance);
        }

        /// <summary>
        /// Puts state data into the cache state with the default key and absolute expiration policy.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="absoluteExpiration">The date and time when the data from the cache will be removed.</param>
        public void Put<T>(T instance, DateTime absoluteExpiration)
        {
            Put(null, instance, absoluteExpiration);
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with the specified absolute expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="absoluteExpiration">The date and time when the absolute data from the cache will be removed.</param>
        public void Put<T>(object key, T instance, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance, null, absoluteExpiration,
                                     System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Puts state data into the cache state with the default key and sliding expiration policy.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="slidingExpiration">A time span representing the sliding expiration policy.</param>
        public void Put<T>(T instance, TimeSpan slidingExpiration)
        {
            Put(null, instance, slidingExpiration);
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with the specified sliding expiration
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="slidingExpiration">A <see cref="TimeSpan"/> specifying the sliding expiration policy.</param>
        public void Put<T>(object key, T instance, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance, null,
                                     System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// Removes state data stored in the cache with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        public void Remove<T>()
        {
            Remove<T>(null);
        }

        /// <summary>
        /// Removes state data stored in the cache state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        public void Remove<T>(object key)
        {
            HttpRuntime.Cache.Remove(key.BuildFullKey<T>());
        }

        /// <summary>
        /// Clears all state stored in the cache.
        /// </summary>
        public void Clear()
        {
            //There's no elegant way to clear the HttpRuntime cache yet... So we ignore this call. Noop.
        }
    }
}