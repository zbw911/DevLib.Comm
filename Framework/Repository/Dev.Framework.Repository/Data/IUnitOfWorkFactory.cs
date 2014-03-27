// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUnitOfWorkFactory.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.Data
{
    /// <summary>
    /// Factory interface that the <see cref="UnitOfWorkScope"/> uses to create instances of
    /// <see cref="IUnitOfWork"/>
    /// </summary>
    public interface IUnitOfWorkFactory
    {
        /// <summary>
        /// Creates a new instance of <see cref="IUnitOfWork"/>.
        /// </summary>
        /// <returns></returns>
        IUnitOfWork Create();
    }
}