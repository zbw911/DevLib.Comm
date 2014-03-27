// ***********************************************************************************
// Created by zbw911 
// 创建于：2013年02月18日 17:37
// 
// 修改于：2013年02月18日 18:24
// 文件名：DBForbidden.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    ///     本方法用于与数据库结合，暂不实现
    /// </summary>
    public class DBForbidden : IForbidden
    {
        #region Public Methods and Operators

        public void ClearForbiddenList(decimal uid)
        {
        }

        /// <summary>
        ///     从列表中排除
        /// </summary>
        /// <param name="UserForbiddenModel">从队列中排除</param>
        public void DeList(UserForbiddenModel UserForbiddenModel)
        {
        }

        public virtual void Dispose()
        {
        }

        /// <summary>
        ///     加入列表
        /// </summary>
        /// <param name="UserForbiddenModel"></param>
        public void EnList(UserForbiddenModel UserForbiddenModel)
        {
        }

        /// <param name="Uid"></param>
        public bool IsForbidden(decimal Uid)
        {
            return false;
        }

        #endregion
    }

    //end DBForbidden
}

//end namespace Forbidden