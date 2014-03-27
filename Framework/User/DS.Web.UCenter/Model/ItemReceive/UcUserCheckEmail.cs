// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserCheckEmail.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 检查右键Model
    /// </summary>
    public class UcUserCheckEmail
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserCheckEmail(string xml)
        {
            int result = 0;
            int.TryParse(xml, out result);
            Result = (UserCheckEmailResult) result;
        }

        /// <summary>
        /// 检查结果
        /// </summary>
        public UserCheckEmailResult Result { get; private set; }
    }
}