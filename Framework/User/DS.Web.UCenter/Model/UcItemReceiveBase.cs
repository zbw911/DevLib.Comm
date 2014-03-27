// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcItemReceiveBase.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Linq;
using System.Xml;

namespace DS.Web.UCenter
{
    /// <summary>
    /// 项目基类
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class UcItemReceiveBase<T> : UcItemBase
        where T : UcItemReceiveBase<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        protected UcItemReceiveBase(string xml)
        {
            Success = true;
            initialize(xml);
        }


        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xml">数据</param>
        protected UcItemReceiveBase(XmlNode xml)
        {
            Success = true;
            initialize(xml);
        }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; private set; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="xml">参数</param>
        private void initialize(string xml)
        {
            try
            {
                unSerialize(xml);
            }
            catch
            {
                Success = false;
            }
            finally
            {
                SetProperty();
            }
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="xml">数据</param>
        private void initialize(XmlNode xml)
        {
            try
            {
                getItems(xml);
            }
            finally
            {
                SetProperty();
            }
        }

        /// <summary>
        /// 得到UCenter项目
        /// </summary>
        /// <param name="node">节点</param>
        private void getItems(XmlNode node)
        {
            foreach (XmlNode xn in node.ChildNodes)
            {
                if (xn.Attributes != null) Data.Add(xn.Attributes["id"].Value, xn.InnerText);
            }
        }

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="xml">XML</param>
        /// <param name="rootNodeName">根目录</param>
        /// <returns></returns>
        private void unSerialize(string xml, string rootNodeName = "root")
        {
            var document = new XmlDocument();
            document.LoadXml(xml);
            XmlNode node = document.SelectSingleNode(rootNodeName);
            getItems(node);
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        protected abstract void SetProperty();

        /// <summary>
        /// 检查是否成功
        /// </summary>
        /// <param name="keys">必要的参数</param>
        protected void CheckForSuccess(params string[] keys)
        {
            Success = true;
            if (keys.Any(key => !Data.Contains(key))) Success = false;
        }
    }
}