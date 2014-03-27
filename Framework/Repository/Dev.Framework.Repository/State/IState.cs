// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IState.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.State
{
    /// <summary>
    /// Base IState interface.
    /// </summary>
    public interface IState
    {
        /// <summary>
        /// Gets the application specific state.
        /// </summary>
        IApplicationState Application { get; }

        /// <summary>
        /// Gets the session specific state.
        /// </summary>
        ISessionState Session { get; }

        /// <summary>
        /// Gets the cache specific state.
        /// </summary>
        ICacheState Cache { get; }

        /// <summary>
        /// Gets the thread local / request local specific state.
        /// </summary>
        ILocalState Local { get; }
    }
}