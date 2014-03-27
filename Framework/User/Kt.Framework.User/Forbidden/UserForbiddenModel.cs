// ***********************************************************************************
// Created by zbw911 
// 创建于：2013年02月18日 17:37
// 
// 修改于：2013年02月18日 18:24
// 文件名：UserForbiddenModel.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    using System;

    public class UserForbiddenModel
    {
        #region Public Properties

        public int ErrorCount { get; set; }

        /// <summary>
        ///     最后一次登录错误时间
        /// </summary>
        public DateTime LastTime { get; set; }

        public decimal Uid { get; set; }

        #endregion
    }

    //end UserForbiddenModel
}

//end namespace Forbidden