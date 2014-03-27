// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：SpecificationExtensions.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Linq;
using System.Linq.Expressions;
using Kt.Framework.Repository.Specifications;

namespace Kt.Framework.Repository.Extensions
{
    ///<summary>
    /// Extension methods for <see cref="ISpecification{T}"/>.
    ///</summary>
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Retuns a new specification adding this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns></returns>
        public static ISpecification<T> And<T>(this ISpecification<T> rightHand, ISpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                leftHand.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.AndAlso, leftHand.Predicate.Body,
                                                      rightInvoke);
            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }

        /// <summary>
        /// Retuns a new specification or'ing this one with the passed one.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="rightHand">The right hand.</param>
        /// <param name="leftHand">The left hand.</param>
        /// <returns></returns>
        public static ISpecification<T> Or<T>(this ISpecification<T> rightHand, ISpecification<T> leftHand)
        {
            var rightInvoke = Expression.Invoke(rightHand.Predicate,
                                                leftHand.Predicate.Parameters.Cast<Expression>());
            var newExpression = Expression.MakeBinary(ExpressionType.OrElse, leftHand.Predicate.Body,
                                                      rightInvoke);
            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(newExpression, leftHand.Predicate.Parameters)
                );
        }
    }
}