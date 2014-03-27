// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ForbiddenConfig.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    ///     配置项
    /// </summary>
    public static class ForbiddenConfig
    {
        /// <summary>
        ///     最大错误次数
        /// </summary>
        public const int MAXERROR = 5;

        /// <summary>
        ///     在禁用列表中保存时间
        /// </summary>
        public const double KEEPTIME = 15;

        /// <summary>
        ///     最小累计时间间隔
        /// </summary>
        public const double TIMEDENSITY = 10;
    }
}