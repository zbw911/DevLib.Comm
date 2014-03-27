// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcCollectionReturnBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 集合基类
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    /// <typeparam name="T">项目</typeparam>
    public abstract class UcCollectionReturnBase<T> : UcCollectionBase
        where T : UcItemReturnBase
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="htmlOn">是否输出HTML</param>
        /// <param name="isRoot">是否为根目录</param>
        /// <returns></returns>
        private string serialize(bool htmlOn = true, bool isRoot = true)
        {
            var sb = new StringBuilder();
            if (isRoot)
            {
                sb.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
                sb.AppendLine("<root>");
            }
            foreach (DictionaryEntry entry in Data)
            {
                sb.AppendFormat(
                    htmlOn ? "<item id=\"{0}\"><![CDATA[{1}]]></item>\r\n" : "<item id=\"{0}\">{1}</item>\r\n",
                    entry.Key, ((T) entry.Value).ToString(false));
            }
            if (isRoot)
            {
                sb.AppendLine("</root>");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected abstract void SetItems();

        /// <summary>
        /// 加入List
        /// </summary>
        /// <param name="list"></param>
        /// <param name="array"></param>
        protected void AddToList(IList<T> list, IEnumerable<T> array)
        {
            foreach (var item in array)
            {
                list.Add(item);
            }
        }

        #region 输出

        /// <summary>
        /// 序列化输出
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToString(true);
        }

        /// <summary>
        /// 序列化输出
        /// </summary>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        public string ToString(bool isRoot)
        {
            SetItems();
            return serialize(false, isRoot);
        }

        #endregion
    }
}