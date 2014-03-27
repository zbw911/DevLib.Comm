// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserRegister.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 用户注册Model
    /// </summary>
    public class UcUserRegister
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserRegister(string xml)
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
        /// 注册结果
        /// </summary>
        public RegisterResult Result
        {
            get { return Uid > 0 ? RegisterResult.Success : (RegisterResult) Uid; }
        }
    }
}