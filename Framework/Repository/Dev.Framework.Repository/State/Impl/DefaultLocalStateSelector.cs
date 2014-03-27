// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：DefaultLocalStateSelector.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using Kt.Framework.Repository.Context;

namespace Kt.Framework.Repository.State.Impl
{
    /// <summary>
    /// Default implementation of <see cref="ILocalStateSelector"/>.
    /// </summary>
    public class DefaultLocalStateSelector : ILocalStateSelector
    {
        readonly IContext _context;

        /// <summary>
        /// Default Constructor.
        /// Creates an instance of <see cref="DefaultLocalStateSelector"/> class.
        /// </summary>
        /// <param name="context">An instance of <see cref="IContext"/>.</param>
        public DefaultLocalStateSelector(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets the <see cref="ILocalState"/> instance to use.
        /// </summary>
        /// <returns></returns>
        public ILocalState Get()
        {
            if (_context.IsWcfApplication)
                return new WcfLocalState(_context);
            if (_context.IsWebApplication)
                return new HttpLocalState(_context);
            return new ThreadLocalState();
        }
    }
}