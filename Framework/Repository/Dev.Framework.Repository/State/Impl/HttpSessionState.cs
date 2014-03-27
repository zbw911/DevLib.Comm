// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：HttpSessionState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using System.Collections;
using log4net;
using Kt.Framework.Repository.Context;

namespace Kt.Framework.Repository.State.Impl
{
    /// <summary>
    /// Implementation of <see cref="ISessionState"/> that uses the current HttpContext's session.
    /// </summary>
    public class HttpSessionState : ISessionState
    {
        readonly IContext _context;
        ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="HttpSessionState"/> class.
        /// </summary>
        /// <param name="context">An instance of <see cref="IContext"/>.</param>
        public HttpSessionState(IContext context)
        {
            _context = context;
        }

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
            var fullKey = key.BuildFullKey<T>();
            _logger.Debug(string.Format("Attempting to get {0} from session state for session {1}", 
                fullKey, _context.HttpContext.Session.SessionID));
            lock (_context.HttpContext.Session.SyncRoot)
                return (T) _context.HttpContext.Session[fullKey];
        }

        /// <summary>
        /// Puts state data into the session state with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(T instance)
        {
            Put(null, instance);
        }

        /// <summary>
        /// Puts state data into the session state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to put.</typeparam>
        /// <param name="key">An object representing the unique key with which the data is stored.</param>
        /// <param name="instance">An instance of <typeparamref name="T"/> to store.</param>
        public void Put<T>(object key, T instance)
        {
            var fullKey = key.BuildFullKey<T>();
            _logger.Debug(string.Format("Attempting to put {0} to session state for session {1}",
                fullKey, _context.HttpContext.Session.SessionID));
            lock (_context.HttpContext.Session.SyncRoot)
                _context.HttpContext.Session[fullKey] = instance;
        }

        /// <summary>
        /// Removes state data stored in the session state with the default key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        public void Remove<T>()
        {
            Remove<T>(null);
        }

        /// <summary>
        /// Removes state data stored in the session state with the specified key.
        /// </summary>
        /// <typeparam name="T">The type of data to remove.</typeparam>
        /// <param name="key">An object representing the unique key with which the data was stored.</param>
        public void Remove<T>(object key)
        {
            var fullKey = key.BuildFullKey<T>();
            _logger.Debug(string.Format("Attempting to remove {0} from session state for session {1}",
                fullKey, _context.HttpContext.Session.SessionID));
            lock (_context.HttpContext.Session.SyncRoot)
                _context.HttpContext.Session.Remove(fullKey);
        }

        /// <summary>
        /// Clears all state data stored in the session.
        /// </summary>
        public void Clear()
        {
            _logger.Debug(string.Format("Attempting to clear session state for session {1}", _context.HttpContext.Session.SessionID));
            _context.HttpContext.Session.Clear();
        }
    }
}