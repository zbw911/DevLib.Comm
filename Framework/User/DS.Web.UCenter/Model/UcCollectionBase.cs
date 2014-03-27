// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcCollectionBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 集合基类
    /// </summary>
    public abstract class UcCollectionBase
    {
        private IDictionary _data;

        /// <summary>
        /// 数据
        /// </summary>
        protected IDictionary Data
        {
            get { return _data ?? (_data = new Hashtable()); }
            set { _data = value; }
        }
    }
}