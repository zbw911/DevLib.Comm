// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：MemberAccessPropertyInfoVisitor.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Kt.Framework.Repository.Expressions
{
    /// <summary>
    /// Inherits from the <see cref="ExpressionVisitor"/> base class and implements a expression visitor
    /// that gets a <see cref="PropertyInfo"/> that represents the property representd by the expresion.
    /// </summary>
    public class MemberAccessPropertyInfoVisitor : ExpressionVisitor
    {
        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> that the expression represents.
        /// </summary>
        public PropertyInfo Property { get; private set; }

        /// <summary>
        /// Overriden. Overrides all MemberAccess to build a path string.
        /// </summary>
        /// <param name="methodExp"></param>
        /// <returns></returns>
        protected override Expression VisitMemberAccess(MemberExpression methodExp)
        {
            if (methodExp.Member.MemberType != MemberTypes.Property)
                throw new NotSupportedException("MemberAccessPathVisitor does not support a member access of type " +
                                                methodExp.Member.MemberType);
            this.Property = (PropertyInfo) methodExp.Member;
            return base.VisitMemberAccess(methodExp);
        }
    }
}