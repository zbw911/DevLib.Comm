// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/XmlCompent.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Collections;
using System.Xml;

namespace Dev.Comm.XML
{
    public class XmlCompent
    {
        protected static Hashtable GetChildTable(XmlNode xn) //已知道有子接点
        {
            var ht = new Hashtable();
            foreach (XmlNode nxn in xn.ChildNodes)
            {
                if (nxn.ChildNodes.Count <= 0)
                {
                    ht.Add(nxn.Name, nxn.InnerText);
                }
                else if (nxn.ChildNodes.Count == 1)
                {
                    XmlNode nxn1 = nxn.ChildNodes[0];
                    if (nxn1.NodeType == XmlNodeType.CDATA)
                    {
                        ht.Add(nxn.Name, nxn.InnerText);
                    }
                    else
                    {
                        ht.Add(nxn.Name, GetChildTable(nxn));
                    }
                }
                else
                {
                    ht.Add(nxn.Name, GetChildTable(nxn));
                }
            }
            return ht;
        }

        /// <summary>
        ///   字符串格式XML转换成HASHTABLE
        /// </summary>
        /// <param name="XmlFile"> </param>
        /// <returns> </returns>
        public static Hashtable GetTable(string XmlFile)
        {
            var ht = new Hashtable();
            var XMLDom = new XmlDocument();
            XMLDom.LoadXml(XmlFile);
            XmlNode newXMLNode = XMLDom.SelectSingleNode("root");
            foreach (XmlNode xn in newXMLNode.ChildNodes)
            {
                if (xn.ChildNodes.Count <= 0)
                {
                    ht.Add(xn.Name, xn.InnerText);
                }
                else if (xn.ChildNodes.Count == 1) //这里主要是判断子接点中是否是<![CDATA[0]]>情形
                {
                    XmlNode nxn = xn.ChildNodes[0];
                    if (nxn.NodeType == XmlNodeType.CDATA)
                    {
                        ht.Add(xn.Name, xn.InnerText);
                    }
                    else
                    {
                        ht.Add(xn.Name, GetChildTable(xn));
                    }
                }
                else
                {
                    ht.Add(xn.Name, GetChildTable(xn));
                }
            }

            return ht;
        }
    }

    //固定顺序的HASHTABLE
    public class NoSortHashTable : Hashtable
    {
        private readonly ArrayList list = new ArrayList();

        public override ICollection Keys
        {
            get { return list; }
        }

        public override void Add(object key, object value)
        {
            base.Add(key, value);
            list.Add(key);
        }

        public override void Clear()
        {
            base.Clear();
            list.Clear();
        }

        public override void Remove(object key)
        {
            base.Remove(key);
            list.Remove(key);
        }
    }
}