// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUnitOfWorkScope.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;

namespace Kt.Framework.Repository.Data
{
    ///<summary>
    ///</summary>
    public interface IUnitOfWorkScope : IDisposable
    {
        /// <summary>
        /// Event fired when the scope is comitting.
        /// </summary>
        event Action<IUnitOfWorkScope> ScopeComitting;

        /// <summary>
        /// Event fired when the scope is rollingback.
        /// </summary>
        event Action<IUnitOfWorkScope> ScopeRollingback;

        /// <summary>
        /// Gets the unique Id of the <see cref="UnitOfWorkScope"/>.
        /// </summary>
        /// <value>A <see cref="Guid"/> representing the unique Id of the scope.</value>
        Guid ScopeId { get; }

        ///<summary>
        /// Commits the current running transaction in the scope.
        ///</summary>
        void Commit();

        /// <summary>
        /// Marks the scope as completed.
        /// Used for internally by Kt.Framework.Repository and should not be used by consumers.
        /// </summary>
        void Complete();
    }
}