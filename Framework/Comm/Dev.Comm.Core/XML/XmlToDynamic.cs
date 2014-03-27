// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月16日 13:40
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/XmlToDynamic.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Xml.Linq;

namespace Dev.Comm.XML
{
    /// <summary>
    ///   From :http://www.itdevspace.com/2012/07/parse-xml-to-dynamic-object-in-c.html
    /// 
    ///   xml 转化为 Dynamic
    /// </summary>
    public class XmlToDynamic
    {
        #region Class Methods

        public static dynamic Parse(string xml)
        {
            var xDoc = XDocument.Parse(xml); //XDocument.Load(../xml.xml)
            dynamic root = new ExpandoObject();

            return Parse(xDoc.Elements().First());
        }


        public static dynamic Parse(XElement node)
        {
            dynamic root = new ExpandoObject();
            return Parse(root, node);
        }

        public static void Parse(dynamic parent, XElement node)
        {
            if (node.HasElements)
            {
                if (node.Elements(node.Elements().First().Name.LocalName).Count() > 1)
                {
                    //list

                    var item = new ExpandoObject();

                    var list = new List<dynamic>();

                    foreach (var element in node.Elements())
                    {
                        Parse(list, element);
                    }


                    AddProperty(item, node.Elements().First().Name.LocalName, list);

                    AddProperty(parent, node.Name.ToString(), item);
                }

                else
                {
                    var item = new ExpandoObject();


                    foreach (var attribute in node.Attributes())
                    {
                        AddProperty(item, attribute.Name.ToString(), attribute.Value.Trim());
                    }


                    //element

                    foreach (var element in node.Elements())
                    {
                        Parse(item, element);
                    }


                    AddProperty(parent, node.Name.ToString(), item);
                }
            }

            else
            {
                AddProperty(parent, node.Name.ToString(), node.Value.Trim());
            }
        }


        private static void AddProperty(dynamic parent, string name, object value)
        {
            if (parent is List<dynamic>)
            {
                (parent as List<dynamic>).Add(value);
            }

            else
            {
                (parent as IDictionary<String, object>)[name] = value;
            }
        }

        #endregion
    }
}