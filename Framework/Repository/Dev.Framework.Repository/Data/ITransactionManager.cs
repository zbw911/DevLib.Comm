// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：ITransactionManager.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;

namespace Kt.Framework.Repository.Data
{
    /// <summary>
    /// Implemented by a transaction manager that manages unit of work transactions.
    /// </summary>
    public interface ITransactionManager : IDisposable
    {
        /// <summary>
        /// Returns the current <see cref="IUnitOfWork"/>.
        /// </summary>
        IUnitOfWork CurrentUnitOfWork { get;}

        /// <summary>
        /// Enlists a <see cref="UnitOfWorkScope"/> instance with the transaction manager,
        /// with the specified transaction mode.
        /// </summary>
        /// <param name="scope">The <see cref="IUnitOfWorkScope"/> to register.</param>
        /// <param name="mode">A <see cref="TransactionMode"/> enum specifying the transaciton
        /// mode of the unit of work.</param>
        void EnlistScope(IUnitOfWorkScope scope, TransactionMode mode);
    }
}