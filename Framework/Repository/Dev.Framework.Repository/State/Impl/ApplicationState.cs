// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：ApplicationState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using System.Collections;

namespace Kt.Framework.Repository.State.Impl
{
    ///<summary>
    /// Default implementation of <see cref="IApplicationState"/>
    ///</summary>
    public class ApplicationState : IApplicationState
    {
        readonly static object _syncRoot = new object();
        readonly Hashtable _applicationState = new Hashtable();

        /// <summary>
        /// Gets state data stored with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
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
            lock(_syncRoot)
                return (T) _applicationState[key.BuildFullKey<T>()];
        }

        ///<summary>
        /// Puts state data into the application state using the type's name as the default key.
        ///</summary>
        ///<param name="instance">The instance of <typeparamref name="T"/></param>
        ///<typeparam name="T"></typeparam>
        public void Put<T>(T instance)
        {
            Put(null, instance);
        }

        /// <summary>
        /// Puts state data into the application state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(object key, T instance)
        {
            lock (_syncRoot)
                _applicationState[key.BuildFullKey<T>()] = instance;
        }

        /// <summary>
        /// Removes state data stored in the application state with the default key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Remove<T>()
        {
            Remove<T>(null);
        }

        /// <summary>
        /// Removes state data stored in the application state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        public void Remove<T>(object key)
        {
            lock(_syncRoot)
                _applicationState.Remove(key.BuildFullKey<T>());
        }

        /// <summary>
        /// Clears all state data stored in the application state.
        /// </summary>
        public void Clear()
        {
            lock(_syncRoot)
                _applicationState.Clear();
        }
    }
}