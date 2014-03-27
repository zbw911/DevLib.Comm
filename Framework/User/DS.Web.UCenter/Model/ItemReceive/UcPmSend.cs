// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcPmSend.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 短信发送Model
    /// </summary>
    public class UcPmSend
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcPmSend(string xml)
        {
            int result = 0;
            int.TryParse(xml, out result);
            PmId = result;
        }

        /// <summary>
        /// 短信Id
        /// </summary>
        public int PmId { get; private set; }

        /// <summary>
        /// 发送结果
        /// </summary>
        public PmSendResult Result
        {
            get { return PmId > 0 ? PmSendResult.Success : (PmSendResult) PmId; }
        }
    }
}