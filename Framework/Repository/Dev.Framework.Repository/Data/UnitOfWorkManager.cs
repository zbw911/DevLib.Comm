// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：UnitOfWorkManager.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************


using System;

using Microsoft.Practices.ServiceLocation;
using Kt.Framework.Repository.Data.Impl;
using Kt.Framework.Repository.State;
using log4net;

namespace Kt.Framework.Repository.Data
{
    ///<summary>
    /// Gets an instances of <see cref="ITransactionManager"/>.
    ///</summary>
    public static class UnitOfWorkManager
    {
        static Func<ITransactionManager> _provider;
        static readonly ILog Logger = LogManager.GetLogger(typeof(UnitOfWorkManager));
        private const string LocalTransactionManagerKey = "UnitOfWorkManager.LocalTransactionManager";
        private static IState state = ServiceLocator.Current.GetInstance<IState>();
        static readonly Func<ITransactionManager> DefaultTransactionManager = () =>
        {
            Logger.Debug(string.Format("Using default UnitOfWorkManager provider to resolve current transaction manager."));

            var transactionManager = state.Local.Get<ITransactionManager>(LocalTransactionManagerKey);

            //for (var i = 0; i < 1000; i++)
            //{
            //    //var state1 = ServiceLocator.Current.GetInstance<IState>();
            //    var transactionManager1 = state.Local.Get<ITransactionManager>(LocalTransactionManagerKey);
            //}

            if (transactionManager == null)
            {
                Logger.Debug(string.Format("No valid ITransactionManager found in Local state. Creating a new TransactionManager."));
                transactionManager = new TransactionManager();
                state.Local.Put(LocalTransactionManagerKey, transactionManager);
            }
            return transactionManager;
        };

        /// <summary>
        /// Default Constructor.
        /// Creates a new instance of the <see cref="UnitOfWorkManager"/>.
        /// </summary>
        static UnitOfWorkManager()
        {
            _provider = DefaultTransactionManager;
        }

        ///<summary>
        /// Sets a <see cref="Func{T}"/> of <see cref="ITransactionManager"/> that the 
        /// <see cref="UnitOfWorkManager"/> uses to get an instance of <see cref="ITransactionManager"/>
        ///</summary>
        ///<param name="provider"></param>
        public static void SetTransactionManagerProvider(Func<ITransactionManager> provider)
        {
            if (provider == null)
            {
                Logger.Debug(string.Format("The transaction manager provide is being set to null. Using " +
                                    " the transaction manager to the default transaction manager provider."));
                _provider = DefaultTransactionManager;
                return;
            }
            Logger.Debug(string.Format("The transaction manager provider is being overriden. Using supplied" +
                                " trasaction manager provider."));
            _provider = provider;
        }

        /// <summary>
        /// Gets the current <see cref="ITransactionManager"/>.
        /// </summary>
        public static ITransactionManager CurrentTransactionManager
        {
            get
            {
                return _provider();
            }
        }

        /// <summary>
        /// Gets the current <see cref="IUnitOfWork"/> instance.
        /// </summary>
        public static IUnitOfWork CurrentUnitOfWork
        {
            get
            {
                return _provider().CurrentUnitOfWork;
            }
        }
    }
}