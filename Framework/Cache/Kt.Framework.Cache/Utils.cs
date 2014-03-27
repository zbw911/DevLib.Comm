// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Utils.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.Cache
{
    ///<summary>
    ///  
    ///</summary>
    public static class Utils
    {
        ///<summary>
        /// 为一个类型创建命名
        ///</summary>
        ///<param name="userKey">用户提供的类型</param>
        ///<typeparam name="T">用于创建KEY的类型</typeparam>
        ///<returns>返回KEY</returns>
        public static string BuildFullKey<T>(this object userKey)
        {
            if (userKey == null)
                return typeof(T).FullName;
            return typeof(T).FullName + userKey.ToString();
        }
    }
}