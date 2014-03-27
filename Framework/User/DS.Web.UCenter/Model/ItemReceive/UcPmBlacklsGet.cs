// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcPmBlacklsGet.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter
{
    /// <summary>
    /// 黑名单Model
    /// </summary>
    public class UcPmBlacklsGet
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcPmBlacklsGet(string xml)
        {
            DeleteNumber = xml.Split(',');
        }

        /// <summary>
        /// 黑名单
        /// </summary>
        public string[] DeleteNumber { get; private set; }
    }
}