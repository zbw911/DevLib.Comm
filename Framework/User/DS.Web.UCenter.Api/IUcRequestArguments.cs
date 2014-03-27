// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUcRequestArguments.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Specialized;

namespace DS.Web.UCenter.Api
{
    /// <summary>
    /// Requser参数
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public interface IUcRequestArguments
    {
        /// <summary>
        /// Action
        /// </summary>
        string Action { get; }

        /// <summary>
        /// 时间
        /// </summary>
        long Time { get; }

        /// <summary>
        /// Query参数
        /// </summary>
        NameValueCollection QueryString { get; }

        /// <summary>
        /// Form参数
        /// </summary>
        string FormData { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsInvalidRequest { get; }

        /// <summary>
        /// 
        /// </summary>
        bool IsAuthracationExpiried { get; }
    }
}