// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcApps.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 应用程序集合
    /// </summary>
    public class UcApps : UcCollectionReceiveBase<UcApp, UcApps>
    {
        private IList<UcApp> _items;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        public UcApps(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<UcApp> Items
        {
            get { return _items ?? (_items = new List<UcApp>()); }
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