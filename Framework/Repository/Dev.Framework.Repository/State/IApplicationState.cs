// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IApplicationState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.State
{
    /// <summary>
    /// Interface implemented by application state providers that store and retrieve application state data.
    /// </summary>
    public interface IApplicationState
    {
        ///<summary>
        /// Gets state data stored with the default key.
        ///</summary>
        ///<typeparam name="T">The type of data to retrieve.</typeparam>
        ///<returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>();

        /// <summary>
        /// Gets state data stored with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to retrieve.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        /// <returns>An instance of <typeparamref name="T"/> or null if not found.</returns>
        T Get<T>(object key);

        ///<summary>
        /// Puts state data into the application state using the type's name as the default key.
        ///</summary>
        ///<param name="instance">The instance of <typeparamref name="T"/></param>
        ///<typeparam name="T">The type of data to put.</typeparam>
        void Put<T>(T instance);

        /// <summary>
        /// Puts state data into the application state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        void Put<T>(object key, T instance);

        /// <summary>
        /// Removes state data stored in the application state with the default key.
        /// </summary>
        /// <typeparam name="T">The tyoe of data to remove.</typeparam>
        void Remove<T>();

        /// <summary>
        /// Removes state data stored in the application state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        void Remove<T>(object key);

        /// <summary>
        /// Clears all state data stored in the application state.
        /// </summary>
        void Clear();
    }
}