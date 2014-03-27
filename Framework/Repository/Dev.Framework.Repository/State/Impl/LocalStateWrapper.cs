// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：LocalStateWrapper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;

namespace Kt.Framework.Repository.State.Impl
{
    /// <summary>
    /// Implementation of <see cref="ILocalState"/> that wraps a <see cref="ILocalState"/> instance
    /// from a <see cref="ILocalStateSelector"/>.
    /// </summary>
    public class LocalStateWrapper : ILocalState
    {
        readonly ILocalState _state;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="LocalStateWrapper"/> class.
        /// </summary>
        /// <param name="selector">A <see cref="ILocalStateSelector"/> instance used by the wrapper
        /// to obtain the underlying <see cref="ILocalState"/> instance.</param>
        public LocalStateWrapper(ILocalStateSelector selector)
        {
            _state = selector.Get();
        }

        /// <summary>
        /// Gets state data stored with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <returns>An isntance of <typeparamref name="T"/> or null if not found.</returns>
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
            return _state.Get<T>(key);
        }

        /// <summary>
        /// Puts state data into the local state with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to put.</param>
        public void Put<T>(T instance)
        {
            Put(null, instance);
        }

        /// <summary>
        /// Puts state data into the local state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(object key, T instance)
        {
            _state.Put(key, instance);
        }

        /// <summary>
        /// Removes state data stored in the local state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        public void Remove<T>()
        {
            Remove<T>(null);
        }

        /// <summary>
        /// Removes state data stored in the local state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        public void Remove<T>(object key)
        {
            _state.Remove<T>(key);
        }

        /// <summary>
        /// Clears all state stored in local state.
        /// </summary>
        public void Clear()
        {
            _state.Clear();
        }
    }
}