// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ICacheWraper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.Framework.Cache
{
    /// <summary>
    /// 包装器接口
    /// </summary>
    public interface ICacheWraper
    {
        /// <summary>
        /// 绝对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(object key, DateTime absoluteExpiration, Func<T> getDataFunc) ;

        /// <summary>
        /// 绝对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="absoluteExpiration"></param>
        /// <param name="GetDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(DateTime absoluteExpiration, Func<T> GetDataFunc) ;

        /// <summary>
        /// 不过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(object key, Func<T> getDataFunc) ;


        /// <summary>
        /// 不过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(Func<T> getDataFunc) ;


        /// <summary>
        /// 相对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(object key, TimeSpan slidingExpiration, Func<T> getDataFunc) ;

        /// <summary>
        /// 相对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slidingExpiration"></param>
        /// <param name="GetDataFunc"></param>
        /// <returns></returns>
        T SmartyGetPut<T>(TimeSpan slidingExpiration, Func<T> GetDataFunc) ;
    }
}