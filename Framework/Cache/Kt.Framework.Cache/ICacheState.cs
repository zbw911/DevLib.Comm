// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ICacheState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.Framework.Cache
{
    /// <summary>
    /// Interface implemented by cache state providers that store and retrieve state data for the cache.
    /// </summary>
    public interface ICacheState
    {
        /// <summary>
        /// GetObject
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>

        ///<summary>
        /// Gets state data stored with the default key.
        ///</summary>
        ///<typeparam name="T">The type of data to retrieve.</typeparam>
        ///<returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>();
        object GetObject<T>();

        /// <summary>
        /// Gets state data stored with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>(object key);
        object GetObject<T>(object key);
        object GetObjectByKey(string key);
        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        void Put<T>(T instance);
        void PutObject<T>(object instance);

        /// <summary>
        /// Puts state data into the cache state with the specified key with no expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        void Put<T>(object key, T instance);
        void PutObject<T>(object key, object instance);
        void PutObjectByKey(string key, object instance);
        /// <summary>
        /// Puts state data into the cache state with the default key and absolute expiration policy.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="absoluteExpiration">The date and time when the data from the cache will be removed.</param>
        void Put<T>(T instance, DateTime absoluteExpiration);
        void PutObject<T>(object instance, DateTime absoluteExpiration);

        /// <summary>
        /// Puts state data into the cache state with the specified key with the specified absolute expiration.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="absoluteExpiration">The date and time when the data from the cache will be removed.</param>
        void Put<T>(object key, T instance, DateTime absoluteExpiration);
        void PutObject<T>(object key, object instance, DateTime absoluteExpiration);
        void PutObjectByKey(string key, object instance, DateTime absoluteExpiration);

        /// <summary>
        /// Puts state data into the cache state with the default key and sliding expiration policy.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="slidingExpiration">A time span representing the sliding expiration policy.</param>
        void Put<T>(T instance, TimeSpan slidingExpiration);
        void PutObject<T>(object instance, TimeSpan slidingExpiration);

        /// <summary>
        /// Puts state data into the cache state with the specified key with the specified sliding expiration
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        /// <param name="slidingExpiration">A <see cref="TimeSpan"/> specifying the sliding expiration policy.</param>
        void Put<T>(object key, T instance, TimeSpan slidingExpiration);
        void PutObject<T>(object key, object instance, TimeSpan slidingExpiration);
        void PutObjectByKey(string key, object instance, TimeSpan slidingExpiration);
        /// <summary>
        /// Removes state data stored in the cache with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        void Remove<T>();

        /// <summary>
        /// Removes state data stored in the cache state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        void Remove<T>(object key);

        void RemoveByKey(string key);
        /// <summary>
        /// Clears all state stored in the cache.
        /// </summary>
        void Clear();
    }
}