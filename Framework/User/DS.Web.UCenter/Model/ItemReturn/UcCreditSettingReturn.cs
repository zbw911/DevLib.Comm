// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcCreditSettingReturn.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 积分设置Model
    /// </summary>
    public class UcCreditSettingReturn : UcItemReturnBase
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="creditName">积分名字</param>
        /// <param name="creditUnit">积分单位</param>
        public UcCreditSettingReturn(string creditName, string creditUnit)
        {
            CreditName = creditName;
            CreditUnit = creditUnit;
        }

        /// <summary>
        /// 积分名字
        /// </summary>
        public string CreditName { get; set; }

        /// <summary>
        /// 积分单位
        /// </summary>
        public string CreditUnit { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetItems()
        {
            Data.Add("0", CreditName);
            Data.Add("1", CreditUnit);
        }
    }
}