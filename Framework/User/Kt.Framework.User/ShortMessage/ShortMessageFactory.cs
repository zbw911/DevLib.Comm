// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ShortMessageFactory.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.ShortMessage
{
    public class ShortMessageFactory
    {
        public static IShortMessage FactoryMethod()
        {
            return new MemShortMessage();
        }
    }
}