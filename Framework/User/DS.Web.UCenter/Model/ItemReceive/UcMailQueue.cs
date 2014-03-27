// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcMailQueue.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 邮件Model
    /// </summary>
    public class UcMailQueue
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcMailQueue(string xml)
        {
            int result;
            Result = false;
            if (!int.TryParse(xml, out result)) return;
            MailId = result;
            Result = true;
        }

        /// <summary>
        /// 修改结果
        /// </summary>
        public bool Result { get; private set; }

        /// <summary>
        /// 邮件Id
        /// </summary>
        public int MailId { get; private set; }
    }
}