// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：AppFabricCache.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using Microsoft.ApplicationServer.Caching;

namespace Dev.Framework.Cache.AppFabric
{
    /// <summary>
    ///   使用 AppFabric 缓存服务器的缓存
    /// </summary>
    public class AppFabricCache : ICacheState
    {
        #region Readonly & Static Fields

        private readonly DataCache Cache = CacheUtil.GetCache();

        #endregion

        #region ICacheState Members

        public object GetObject<T>(object key)
        {
            var cachekey = key.BuildFullKey<T>();

            return this.GetObjectByKey(cachekey);
        }

        public object GetObjectByKey(string key)
        {
            var cachedobj = this.Cache.Get(key);

            return cachedobj;
        }

        ///<summary>
        ///  Gets state data stored with the default key.
        ///</summary>
        ///<typeparam name="T"> The type of data to retrieve. </typeparam>
        ///<returns> An instance of <typeparamref name="T" /> or null if not found. </returns>
        public T Get<T>()
        {
            return this.Get<T>(null);
        }

        public object GetObject<T>()
        {
            return this.GetObject<T>(null);
        }


        /// <summary>
        ///   通过KEY取得缓存内容
        /// </summary>
        /// <typeparam name="T"> The type of data to retrieve. </typeparam>
        /// <param name="key"> An object representing the unique key with which the data was stored. </param>
        /// <returns> An instance of <typeparamref name="T" /> or null if not found. </returns>
        public T Get<T>(object key)
        {
            var cachedobj = this.GetObject<T>(key);
            if (cachedobj == null)
                return default(T);
            return (T)cachedobj;
        }

        /// <summary>
        ///   Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        public void Put<T>(T instance)
        {
            this.Put(null, instance);
        }

        public void PutObject<T>(object instance)
        {
            this.PutObject<T>(null, instance);
        }

        /// <summary>
        ///   Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="key"> An object representing the unique key with which the data is stored. </param>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        public void Put<T>(object key, T instance)
        {
            this.PutObject<T>(key, instance);
        }

        public void PutObject<T>(object key, object instance)
        {
            var cachekey = key.BuildFullKey<T>();
            this.PutObjectByKey(cachekey, instance);
        }

        public void PutObjectByKey(string key, object instance)
        {
            this.Cache.Put(key, instance);
        }

        /// <summary>
        ///   Puts state data into the cache state with the default key and absolute expiration policy.
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        /// <param name="absoluteExpiration"> The date and time when the data from the cache will be removed. </param>
        public void Put<T>(T instance, DateTime absoluteExpiration)
        {
            this.Put(null, instance, absoluteExpiration);
        }

        public void PutObject<T>(object instance, DateTime absoluteExpiration)
        {
            this.PutObject<T>(null, instance, absoluteExpiration);
        }

        /// <summary>
        ///   Puts state data into the cache state with the specified key with the specified absolute expiration.
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="key"> An object representing the unique key with which the data is stored. </param>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        /// <param name="absoluteExpiration"> The date and time when the absolute data from the cache will be removed. </param>
        public void Put<T>(object key, T instance, DateTime absoluteExpiration)
        {
            PutObject<T>(key, instance, absoluteExpiration);
        }

        public void PutObject<T>(object key, object instance, DateTime absoluteExpiration)
        {
            var cachekey = key.BuildFullKey<T>();
            this.PutObjectByKey(cachekey, instance, absoluteExpiration);
        }

        public void PutObjectByKey(string key, object instance, DateTime absoluteExpiration)
        {
            this.Cache.Put(key, instance, absoluteExpiration - DateTime.Now);
        }

        /// <summary>
        ///   Puts state data into the cache state with the default key and sliding expiration policy.
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        /// <param name="slidingExpiration"> A time span representing the sliding expiration policy. </param>
        public void Put<T>(T instance, TimeSpan slidingExpiration)
        {
            this.Put(null, instance, slidingExpiration);
        }

        public void PutObject<T>(object instance, TimeSpan slidingExpiration)
        {
            PutObject<T>(null, instance, slidingExpiration);
        }

        /// <summary>
        ///   Puts state data into the cache state with the specified key with the specified sliding expiration
        /// </summary>
        /// <typeparam name="T"> The type of data to put. </typeparam>
        /// <param name="key"> An object representing the unique key with which the data is stored. </param>
        /// <param name="instance"> An instance of <typeparamref name="T" /> to store. </param>
        /// <param name="slidingExpiration"> A <see cref="TimeSpan" /> specifying the sliding expiration policy. </param>
        public void Put<T>(object key, T instance, TimeSpan slidingExpiration)
        {
            this.PutObject<T>(key, instance, slidingExpiration);
        }

        public void PutObject<T>(object key, object instance, TimeSpan slidingExpiration)
        {
            var cachekey = key.BuildFullKey<T>();

            this.PutObjectByKey(cachekey, instance, slidingExpiration);
        }

        public void PutObjectByKey(string key, object instance, TimeSpan slidingExpiration)
        {
            this.Cache.Put(key, instance, slidingExpiration);
        }

        /// <summary>
        ///   Removes state data stored in the cache with the default key.
        /// </summary>
        /// <typeparam name="T"> The type of data to remove. </typeparam>
        public void Remove<T>()
        {
            this.Remove<T>(null);
        }

        /// <summary>
        ///   Removes state data stored in the cache state with the specified key.
        /// </summary>
        /// <typeparam name="T"> The type of data to remove. </typeparam>
        /// <param name="key"> An object representing the unique key with which the data was stored. </param>
        public void Remove<T>(object key)
        {
            var cachekey = key.BuildFullKey<T>();
            this.RemoveByKey(cachekey);

        }

        public void RemoveByKey(string key)
        {
            var cache = this.Cache.Remove(key);
        }

        /// <summary>
        ///   Clears all state stored in the cache.
        /// </summary>
        public void Clear()
        {
            //There's no elegant way to clear the HttpRuntime cache yet... So we ignore this call. Noop.
        }

        #endregion
    }
}