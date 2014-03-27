// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DatabaseType.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.DBUtility
{
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DatabaseType
    {
        /// <summary>
        /// MSSQLServer
        /// </summary>
        MSSQLServer,

        /// <summary>
        /// Oracle
        /// </summary>
        Oracle,

        /// <summary>
        /// Ole
        /// </summary>
        OleDBSupported
    }
}