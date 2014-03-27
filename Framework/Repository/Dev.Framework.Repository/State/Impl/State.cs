// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：State.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.State.Impl
{
    /// <summary>
    /// Default implementation of <see cref="IState"/>.
    /// </summary>
    public class State : IState
    {
        readonly IApplicationState _applicationState;
        readonly ILocalState _localState;
        readonly ISessionState _sessionState;
        readonly ICacheState _cacheState;

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of <see cref="IState"/> class.
        /// </summary>
        /// <param name="applicationState">An instance of <see cref="IApplicationState"/> that is used to store
        /// application state data.</param>
        /// <param name="localState">An instance of <see cref="ILocalState"/> that is used to store local
        /// state data.</param>
        /// <param name="sessionState">An instance of <see cref="ISessionState"/> that is used to store session
        /// state data.</param>
        /// <param name="cacheState">An instance of <see cref="ICacheState"/> that is used to store cache
        /// state data.</param>
        public State(
            IApplicationState applicationState, 
            ILocalState localState, 
            ISessionState sessionState, 
            ICacheState cacheState)
        {
            _applicationState = applicationState;
            _localState = localState;
            _sessionState = sessionState;
            _cacheState = cacheState;
        }

        /// <summary>
        /// Gets the application specific state.
        /// </summary>
        public IApplicationState Application
        {
            get { return _applicationState; }
        }

        /// <summary>
        /// Gets the thread local / request local specific state.
        /// </summary>
        public ILocalState Local
        {
            get { return _localState; }
        }

        /// <summary>
        /// Gets the session specific state.
        /// </summary>
        public ISessionState Session
        {
            get { return _sessionState; }
        }

        /// <summary>
        /// Gets the cache specific state.
        /// </summary>
        public ICacheState Cache
        {
            get { return _cacheState; }
        }
    }
}