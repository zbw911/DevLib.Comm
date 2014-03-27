// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcItemReturnBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;
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
    public abstract class UcItemReturnBase : UcItemBase
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="htmlOn"></param>
        /// <param name="isRoot"></param>
        /// <returns></returns>
        private string serialize(bool htmlOn = true, bool isRoot = true)
        {
            return serialize(Data, htmlOn, isRoot);
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="item">需要序列化的对象</param>
        /// <param name="htmlOn">是否输出HTML</param>
        /// <param name="isRoot">是否为根目录</param>
        /// <returns></returns>
        private string serialize(IDictionary item, bool htmlOn = true, bool isRoot = true)
        {
            var sb = new StringBuilder();
            getHeader(isRoot, sb);

            foreach (DictionaryEntry entry in item)
            {
                if (entry.Value is Hashtable)
                {
                    sb.AppendLine("<item id=\"" + entry.Key + "\">");
                    sb.AppendLine(serialize((Hashtable) entry.Value, htmlOn, false));
                    sb.AppendLine("</item>");
                }
                else
                {
                    sb.AppendFormat(
                        htmlOn ? "<item id=\"{0}\"><![CDATA[{1}]]></item>\r\n" : "<item id=\"{0}\">{1}</item>\r\n",
                        entry.Key, entry.Value);
                }
            }

            getFooter(isRoot, sb);

            return sb.ToString();
        }

        private void getFooter(bool isRoot, StringBuilder sb)
        {
            if (isRoot)
            {
                sb.AppendLine("</root>");
            }
        }

        private void getHeader(bool isRoot, StringBuilder sb)
        {
            if (!isRoot) return;
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sb.AppendLine("<root>");
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected abstract void SetItems();

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
        public virtual string ToString(bool isRoot)
        {
            SetItems();
            return serialize(true, isRoot);
        }

        #endregion
    }
}