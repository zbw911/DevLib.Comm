// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：UnitOfWorkAttribute.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Web.Mvc;
using log4net;
 

namespace Kt.Framework.Repository.Data
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        FilterScope _filterScope = FilterScope.Action;
        TransactionMode _transactionMode = TransactionMode.Default;
        readonly static ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static readonly string ContextUnitOfWorkKey = "UnitOfWorkAttribute_Request_UnitOfWork";

        public FilterScope Scope
        {
            get { return _filterScope; }
            set { _filterScope = value; }
        }

        public TransactionMode TransactionMode
        {
            get { return _transactionMode; }
            set { _transactionMode = value; }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Start(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Commits the transaction if the filter scope is action and no errors have occured.
            if (filterContext.Exception != null)
            {
                //Rollback...
                CurrentUnitOfWork(filterContext).Dispose();
                return;
            }

            if (_filterScope != FilterScope.Action) 
                return;

            CurrentUnitOfWork(filterContext).Commit();
            CurrentUnitOfWork(filterContext).Dispose();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (_filterScope != FilterScope.Result)
                return;

            if (filterContext.Exception != null)
            {
                //Rollback
                CurrentUnitOfWork(filterContext).Dispose();
                return;
            }

            //Commits the unit of work if the filter scope is Result and no errors have occured.
            CurrentUnitOfWork(filterContext).Commit();
            CurrentUnitOfWork(filterContext).Dispose();
        }

        public void Start(ControllerContext filterContext)
        {
            var unitOfWork = new UnitOfWorkScope(_transactionMode);
            filterContext.HttpContext.Items[ContextUnitOfWorkKey] = unitOfWork;
        }

        public IUnitOfWorkScope CurrentUnitOfWork(ControllerContext filterContext)
        {
            var currentUnitOfWork = filterContext.HttpContext.Items[ContextUnitOfWorkKey] as IUnitOfWorkScope;
            if (currentUnitOfWork == null)
            {
                throw new InvalidOperationException("No unit of work scope was found for the current action." +
                                                    "This might indicate a possible bug in Kt.Framework.Repository UnitOfWorkAttribute action filter.");
            }
            return currentUnitOfWork;
        }

        /// <summary>
        /// Defines the scope of the unit of work when executing in the context of an Action. Default is
        /// <see cref="Action"/>
        /// </summary>
        public enum FilterScope
        {
            /// <summary>
            /// Specifies that the unit of work scope will be comitted when the action finishes executing.
            /// </summary>
            Action,
            /// <summary>
            /// Specifies that the unit of work scope will be comitted when the view finishes rendering.
            /// </summary>
            Result
        }
    }
}