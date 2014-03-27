// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DBShortMessage.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Framework.User.ShortMessage
{
    public class DBShortMessage : IShortMessage
    {
        #region IShortMessage Members

        public void DeList(ShortMessageModel ShortMessageModel)
        {
            return;
        }

        public void EnList(ShortMessageModel ShortMessageModel)
        {
            return;
        }

        public void FreshList(ShortMessageModel ShortMessageModel)
        {
            return;
        }

        public bool IsSendToMany(string Mobile)
        {
            return true;
        }

        public int GetSendTimes(string Mobile)
        {
            return 0;
        }

        #endregion
    }
}