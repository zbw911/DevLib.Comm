// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Exceptions.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;

namespace Dev.Comm.Web
{
    /// <summary>
    /// 不安装的请求异常 added by zbw911 
    /// </summary>
    public class UnSafeRequestException : Exception
    {
        public UnSafeRequestException(string message)
            : base(message)
        {
        }
    }
}