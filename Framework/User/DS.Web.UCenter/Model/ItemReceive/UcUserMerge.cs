// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserMerge.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 用户合并Model
    /// </summary>
    public class UcUserMerge
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserMerge(string xml)
        {
            int result = 0;
            int.TryParse(xml, out result);
            Uid = result;
        }

        /// <summary>
        /// Uid
        /// </summary>
        public decimal Uid { get; private set; }

        /// <summary>
        /// 合并结果
        /// </summary>
        public UserMergeResult Result
        {
            get { return Uid > 0 ? UserMergeResult.Success : (UserMergeResult) Uid; }
        }
    }
}