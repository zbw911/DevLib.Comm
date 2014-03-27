// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcUserProtecteds.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 受保护用户集合
    /// </summary>
    public class UcUserProtecteds : UcCollectionReceiveBase<UcUserProtected, UcUserProtecteds>
    {
        private IList<UcUserProtected> _items;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcUserProtecteds(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<UcUserProtected> Items
        {
            get { return _items ?? (_items = new List<UcUserProtected>()); }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetProperty()
        {
            SetItems(Items);
        }
    }
}