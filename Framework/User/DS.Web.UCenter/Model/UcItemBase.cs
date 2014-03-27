// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcItemBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 项目基类
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public abstract class UcItemBase
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