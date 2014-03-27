// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcTagReturns.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace DS.Web.UCenter
{
    /// <summary>
    /// Tag集合
    /// </summary>
    public class UcTagReturns : UcCollectionReturnBase<UcTagReturn>
    {
        private IList<UcTagReturn> _items;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tagName">Tag名字</param>
        /// <param name="ucTagReturn">集合</param>
        public UcTagReturns(string tagName, params UcTagReturn[] ucTagReturn)
        {
            AddToList(Items, ucTagReturn);
            TagName = tagName;
        }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<UcTagReturn> Items
        {
            get { return _items ?? (_items = new List<UcTagReturn>()); }
        }

        /// <summary>
        /// Tag名字
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetItems()
        {
            Data.Add("0", TagName);
            int index = 1;
            foreach (var item in Items)
            {
                Data.Add(index++, item);
            }
        }
    }
}