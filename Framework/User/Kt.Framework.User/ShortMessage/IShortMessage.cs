// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IShortMessage.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.ShortMessage
{
    public interface IShortMessage
    {
        /// <summary>
        ///     从列表中删除
        /// </summary>
        /// <param name="ShortMessageModel"></param>
        void DeList(ShortMessageModel ShortMessageModel);

        /// <summary>
        ///     加入列表
        /// </summary>
        /// <param name="ShortMessageModel"></param>
        void EnList(ShortMessageModel ShortMessageModel);

        /// <summary>
        ///     更新列表
        /// </summary>
        /// <param name="ShortMessageModel"></param>
        void FreshList(ShortMessageModel ShortMessageModel);

        /// <summary>
        ///     是否频繁发送，true表示频繁发送（禁止），false表示正常发送（允许）
        /// </summary>
        /// <param name="phoneNo"></param>
        /// <returns></returns>
        bool IsSendToMany(string Mobile);

        /// <summary>
        ///     根据手机号，获取此手机发送了多少次信息
        /// </summary>
        /// <param name="Mobile"></param>
        /// <returns></returns>
        int GetSendTimes(string Mobile);
    }
}