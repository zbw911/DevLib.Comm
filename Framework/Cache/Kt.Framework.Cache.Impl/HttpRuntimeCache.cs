// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：HttpRuntimeCache.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;

namespace Dev.Framework.Cache.Impl
{
    /// <summary>
    /// Implementation of <see cref="ICacheState"/> that uses the ASP.Net runtime cache.
    /// </summary>
    public class HttpRuntimeCache : ICacheState
    {
        #region ICacheState Members

        public object GetObject<T>(object key)
        {
            return GetObjectByKey(key.BuildFullKey<T>());
        }

        public object GetObjectByKey(string key)
        {
            return HttpRuntime.Cache.Get(key);
        }

        ///<summary>
        /// Gets state data stored with the default key.
        ///</summary>
        ///<typeparam name="T">The type of data to retrieve.</typeparam>
        ///<returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        public T Get<T>()
        {
            return this.Get<T>(null);
        }

        public object GetObject<T>()
        {
            return GetObject<T>(null);
        }

        /// <summary>
        /// 通过KEY取得缓存内容
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        public T Get<T>(object key)
        {
            return (T)HttpRuntime.Cache.Get(key.BuildFullKey<T>());
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(T instance)
        {
            this.Put(null, instance);
        }

        public void PutObject<T>(object instance)
        {
            PutObject<T>(null, instance);
        }

        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(object key, T instance)
        {
            PutObject<T>(key, instance);
        }

        public void PutObject<T>(object key, object instance)
        {
            this.PutObjectByKey(key.BuildFullKey<T>(), instance);
        }

        public void PutObjectByKey(string key, object instance)
        {
            HttpRuntime.Cache.Insert(key, instance);
        }

        /// <summary>
        /// Puts state data into the cache state with the default key and absolute expiration policy.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="absoluteExpiration">The date and time when the data from the cache will be removed.</param>
        public void Put<T>(T instance, DateTime absoluteExpiration)
        {
            this.Put(null, instance, absoluteExpiration);
        }

        public void PutObject<T>(object instance, DateTime absoluteExpiration)
        {
            this.PutObject<T>(null, instance, absoluteExpiration);
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
            PutObject<T>(key, instance, absoluteExpiration);
        }

        public void PutObject<T>(object key, object instance, DateTime absoluteExpiration)
        {
            PutObjectByKey(key.BuildFullKey<T>(), instance, absoluteExpiration);
        }

        public void PutObjectByKey(string key, object instance, DateTime absoluteExpiration)
        {
            HttpRuntime.Cache.Insert(key, instance, null, absoluteExpiration,
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
            this.Put(null, instance, slidingExpiration);
        }

        public void PutObject<T>(object instance, TimeSpan slidingExpiration)
        {
            this.PutObject<T>(instance, slidingExpiration);
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
            //HttpRuntime.Cache.Insert(key.BuildFullKey<T>(), instance, null,
            //                         System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);

            PutObject<T>(key, instance, slidingExpiration);
        }

        public void PutObject<T>(object key, object instance, TimeSpan slidingExpiration)
        {
            this.PutObjectByKey(key.BuildFullKey<T>(), instance, slidingExpiration);
        }

        public void PutObjectByKey(string key, object instance, TimeSpan slidingExpiration)
        {
            HttpRuntime.Cache.Insert(key, instance, null,
                                     System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiration);
        }

        /// <summary>
        /// Removes state data stored in the cache with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        public void Remove<T>()
        {
            this.Remove<T>(null);
        }

        /// <summary>
        /// Removes state data stored in the cache state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        public void Remove<T>(object key)
        {
            RemoveByKey(key.BuildFullKey<T>());
        }

        public void RemoveByKey(string key)
        {
            HttpRuntime.Cache.Remove(key);
        }

        /// <summary>
        /// Clears all state stored in the cache.
        /// </summary>
        public void Clear()
        {
            //There's no elegant way to clear the HttpRuntime cache yet... So we ignore this call. Noop.
        }

        #endregion
    }
}