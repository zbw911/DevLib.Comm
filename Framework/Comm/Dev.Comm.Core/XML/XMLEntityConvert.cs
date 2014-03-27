// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/XMLEntityConvert.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Dev.Comm.XML
{
    /// <summary>
    ///   实体转换XML，XML转换实体
    /// </summary>
    /// <typeparam name="T"> </typeparam>
    public class XMLEntityConvert<T> where T : new()
    {
        #region 实体转换成XML

        /// <summary>
        ///   对象实体转换成xml
        /// </summary>
        /// <param name="item"> 对象实例 </param>
        /// <returns> </returns>
        public static string EntityToXml(T item)
        {
            IList<T> items = new List<T>();
            items.Add(item);
            return EntityToXml(items);
        }

        /// <summary>
        ///   对象实体转换成xml
        /// </summary>
        /// <param name="items"> 对象实例集 </param>
        /// <returns> </returns>
        public static string EntityToXml(IList<T> items)
        {
            //创建XmlDocument文档
            var doc = new XmlDocument();
            //创建根元素
            XmlElement root = doc.CreateElement(typeof (T).Name + "s");
            //添加根元素的子元素集
            foreach (var item in items)
            {
                EntityToXml(doc, root, item);
            }
            //向XmlDocument文档添加根元素
            doc.AppendChild(root);

            return doc.InnerXml;
        }

        private static void EntityToXml(XmlDocument doc, XmlElement root, T item)
        {
            //对象的属性集
            PropertyInfo[] propertyInfo =
                typeof (T).GetProperties(BindingFlags.Public |
                                         BindingFlags.Instance);

            foreach (var pinfo in propertyInfo)
            {
                if (pinfo != null)
                {
                    //对象属性名称
                    string name = pinfo.Name;
                    //对象属性值
                    string value = String.Empty;

                    //创建元素
                    XmlElement xmlItem = doc.CreateElement(name);

                    if (pinfo.GetValue(item, null) != null)
                    {
                        value = pinfo.GetValue(item, null).ToString(); //获取对象属性值
                    }
                    //设置元素的属性值
                    //xmlItem.SetAttribute(name, value);
                    xmlItem.InnerXml = value;
                    root.AppendChild(xmlItem);
                }
            }
            //向根添加子元素            
        }

        #endregion

        #region XML转成实体类

        /// <summary>
        ///   XML转成对象实例
        /// </summary>
        /// <param name="xml"> xml </param>
        /// <returns> </returns>
        public static T XmlToEntity(string xml)
        {
            IList<T> items = XmlToEntityList(xml);
            if (items != null && items.Count > 0)
            {
                return items[0];
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        ///   XML转成对象实例集
        /// </summary>
        /// <param name="xml"> xml </param>
        /// <returns> </returns>
        public static IList<T> XmlToEntityList(string xml)
        {
            var doc = new XmlDocument();
            try
            {
                doc.LoadXml(xml);
            }
            catch
            {
                return null;
            }
            if (doc.ChildNodes.Count != 1)
            {
                return null;
            }
            if (doc.ChildNodes[0].Name.ToLower() != typeof (T).Name.ToLower() + "s")
            {
                return null;
            }
            XmlNode node = doc.ChildNodes[0];

            IList<T> items = new List<T>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name.ToLower() == typeof (T).Name.ToLower())
                {
                    items.Add(XmlNodeToEntity(child));
                }
            }
            return items;
        }

        private static T XmlNodeToEntity(XmlNode node)
        {
            var item = new T();
            if (node.NodeType == XmlNodeType.Element)
            {
                var element = (XmlElement) node;
                PropertyInfo[] propertyInfo =
                    typeof (T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (XmlAttribute attr in element.Attributes)
                {
                    string attrName = attr.Name.ToLower();
                    string attrValue = attr.Value;
                    foreach (var pinfo in propertyInfo)
                    {
                        if (pinfo != null)
                        {
                            string name = pinfo.Name.ToLower();
                            Type dbType = pinfo.PropertyType;
                            if (name == attrName)
                            {
                                if (String.IsNullOrEmpty(attrValue))
                                {
                                    continue;
                                }
                                switch (dbType.ToString())
                                {
                                    case "System.Int32":
                                        pinfo.SetValue(item, Convert.ToInt32(attrValue), null);
                                        break;
                                    case "System.Boolean":
                                        pinfo.SetValue(item, Convert.ToBoolean(attrValue), null);
                                        break;
                                    case "System.DateTime":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Decimal":
                                        pinfo.SetValue(item, Convert.ToDecimal(attrValue), null);
                                        break;
                                    case "System.Double":
                                        pinfo.SetValue(item, Convert.ToDouble(attrValue), null);
                                        break;
                                    default:
                                        pinfo.SetValue(item, attrValue, null);
                                        break;
                                }
                                continue;
                            }
                        }
                    }
                }
            }
            return item;
        }

        #endregion
    }
}