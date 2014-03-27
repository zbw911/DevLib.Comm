// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：TransactionScopeHelper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************


using System;
using System.Transactions;
using log4net;

namespace Kt.Framework.Repository.Data.Impl
{
    /// <summary>
    /// Helper class to create <see cref="TransactionScope"/> instances.
    /// </summary>
    public static class TransactionScopeHelper
    {
        static readonly ILog Logger = LogManager.GetLogger(typeof(TransactionScopeHelper));


        ///<summary>
        ///</summary>
        ///<param name="isolationLevel"></param>
        ///<param name="txMode"></param>
        ///<returns></returns>
        ///<exception cref="NotImplementedException"></exception>
        public static TransactionScope CreateScope(IsolationLevel isolationLevel, TransactionMode txMode)
        {
            if (txMode == TransactionMode.New)
            {
                Logger.Debug(("Creating a new TransactionScope with TransactionScopeOption.RequiresNew"));
                return new TransactionScope(TransactionScopeOption.RequiresNew, new TransactionOptions { IsolationLevel = isolationLevel });
            }
            if (txMode == TransactionMode.Supress)
            {
                Logger.Debug(("Creating a new TransactionScope with TransactionScopeOption.Supress"));
                return new TransactionScope(TransactionScopeOption.Suppress);
            }
            Logger.Debug(("Creating a new TransactionScope with TransactionScopeOption.Required"));
            return new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = isolationLevel });
        }
    }
}