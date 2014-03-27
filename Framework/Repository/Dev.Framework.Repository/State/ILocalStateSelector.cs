// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：ILocalStateSelector.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

namespace Kt.Framework.Repository.State
{
    /// <summary>
    /// Interface that is implemented by a custom selector that creates instances of <see cref="ILocalState"/>.
    /// </summary>
    public interface ILocalStateSelector
    {
        /// <summary>
        /// Gets the implementation of <see cref="ILocalState"/> to use.
        /// </summary>
        /// <returns></returns>
        ILocalState Get();
    }
}