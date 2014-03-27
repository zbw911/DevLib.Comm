// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IForbidden.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using Dev.Framework.User.Forbidden;

namespace Dev.Framework.User
{
    public interface IForbidden
    {
        /// <summary>
        ///     从列表中排除
        /// </summary>
        /// <param name="UserForbiddenModel">从队列中排除</param>
        void DeList(UserForbiddenModel UserForbiddenModel);

        /// <summary>
        ///     加入列表
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        void EnList(UserForbiddenModel UserForbiddenModel);

        /// <summary>
        ///     是否被拒绝
        /// </summary>
        /// <param name="Uid"></param>
        bool IsForbidden(decimal Uid);

        void ClearForbiddenList(decimal uid);
    }

//end IForbidden
}

//end namespace Forbidden