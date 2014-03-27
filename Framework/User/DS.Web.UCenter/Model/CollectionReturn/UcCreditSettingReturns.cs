// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcCreditSettingReturns.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 积分设置集合
    /// </summary>
    public class UcCreditSettingReturns : UcCollectionReturnBase<UcCreditSettingReturn>
    {
        private IList<UcCreditSettingReturn> _items;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ucCreditSetting">集合</param>
        public UcCreditSettingReturns(params UcCreditSettingReturn[] ucCreditSetting)
        {
            AddToList(Items, ucCreditSetting);
        }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<UcCreditSettingReturn> Items
        {
            get { return _items ?? (_items = new List<UcCreditSettingReturn>()); }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected override void SetItems()
        {
            int index = 0;
            foreach (var item in Items)
            {
                Data.Add(index++, item);
            }
        }
    }
}