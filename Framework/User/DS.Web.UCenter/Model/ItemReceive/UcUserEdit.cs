// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserEdit.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 用户修改Model
    /// </summary>
    public class UcUserEdit
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserEdit(string xml)
        {
            int result = 0;
            int.TryParse(xml, out result);
            Result = (UserEditResult) result;
        }

        /// <summary>
        /// 修改结果
        /// </summary>
        public UserEditResult Result { get; private set; }
    }
}