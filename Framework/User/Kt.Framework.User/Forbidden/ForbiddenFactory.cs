// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ForbiddenFactory.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.Forbidden
{
    /// <summary>
    /// </summary>
    /// <example>
    ///     <![CDATA[
    /// var forbidden = ForbiddenFactory.FactoryMethod();
    /// forbidden.is.....
    /// forbidden.En....
    /// forbidden.De....
    /// ]]>
    /// </example>
    public class ForbiddenFactory
    {
        public static IForbidden FactoryMethod()
        {
            return new MemForbidden();
        }
    }
}