// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/XmlHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;

namespace Dev.Comm.XML
{
    ///<summary>
    ///  This class attempts to wrap up some common things
    ///  we need to do when dealing with Xml and C# classes:
    ///  Load, Save, Add/Remove Attributes/Elements, et al.
    ///</summary>
    public class XmlHelper
    {
        #region Delegates

        public delegate bool Save(string sTargetXML);

        #endregion

        #region LoadType enum

        public enum LoadType
        {
            FromString,
            FromLocalFile,
            FromURL
        }

        #endregion

        private readonly XmlDocument m_xmlDocument;
        private XPathNavigator m_nav;
        private string m_sLastErrorMessage;

        // Constructor
        public XmlHelper()
        {
            m_sLastErrorMessage = "";
            m_xmlDocument = new XmlDocument();
        }

        #region 属性

        public string LastErrorMessage
        {
            get { return m_sLastErrorMessage; }
            set { m_sLastErrorMessage = value; }
        }

        public XmlNode RootNode
        {
            get { return m_xmlDocument.DocumentElement; }
        }

        public XmlDocument Document
        {
            get { return m_xmlDocument; }
        }

        public XPathNavigator Navigator
        {
            get { return m_nav; }
        }

        #endregion

        // 定义委托 delegates - more complex save operations can do it themselves...

        ///<summary>
        ///  Save the XML to a target file.
        ///</summary>
        public bool SaveToFile(string sTargetFileName)
        {
            bool bResult = false;

            try
            {
                m_xmlDocument.Save(sTargetFileName);
                bResult = true;
            }
            catch (XmlException e)
            {
                HandleException(e);
            }

            return bResult;
        }


        ///<summary>
        ///  返回全部的XML字符串
        ///  Easy way to get the entire Xml string
        ///</summary>
        public override string ToString()
        {
            return m_xmlDocument.OuterXml;
        }

        private void DoPostLoadCreateInit()
        {
            m_nav = m_xmlDocument.CreateNavigator();
            MoveToRoot();
        }


        public bool LoadXML(string sourceXMLOrFile, LoadType loadType)
        {
            return LoadXML(sourceXMLOrFile, loadType, Encoding.UTF8);
        }

        ///<summary>
        ///  从文件或者URL加载XML
        ///  Easy way to load XML from a file or URL
        ///</summary>
        public bool LoadXML(string sourceXMLOrFile, LoadType loadType, Encoding encoding)
        {
            bool bLoadResult = false;

            try
            {
                switch (loadType)
                {
                    case LoadType.FromString:
                        m_xmlDocument.LoadXml(sourceXMLOrFile); // loading from source XML text
                        break;

                    case LoadType.FromLocalFile:
                        m_xmlDocument.Load(sourceXMLOrFile); // loading from a file
                        break;

                    case LoadType.FromURL:
                        {
                            string sURLContent = GetURLContent(sourceXMLOrFile, encoding);
                            m_xmlDocument.LoadXml(sURLContent);
                            break;
                        }

                    default:
                        string sErr = "Developer note:  No LoadType case supported for " + loadType.ToString();
                        throw (new Exception(sErr));
                }

                DoPostLoadCreateInit();

                bLoadResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bLoadResult;
        }

        ///<summary>
        ///  Helper method to get string content from a URL - not necessarily XML, but probably
        ///</summary>
        public string GetURLContent(string sURL, Encoding encoding)
        {
            string s = "";

            try
            {
                var webreq = (HttpWebRequest) WebRequest.Create(sURL);
                var webresp = (HttpWebResponse) webreq.GetResponse();

                var stream = new StreamReader(webresp.GetResponseStream(), encoding);
                s = stream.ReadToEnd();
                stream.Close();
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return s;
        }

        ///<summary>
        ///  Helper function if navigation is used to ensure we're at the root node.
        ///</summary>
        public bool MoveToRoot()
        {
            bool bResult = false;
            try
            {
                m_nav.MoveToRoot(); // go to root node!
                bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  Gets an ArrayList of XmlNode children using an xPath expression
        ///</summary>
        public IList<XmlNode> GetChildNodesFromCriteria(string xPathExpression)
        {
            var al = new List<XmlNode>();
            try
            {
                XmlNodeList nl = m_xmlDocument.SelectNodes(xPathExpression);

                if (nl != null)
                {
                    for (int i = 0; i < nl.Count; i++)
                        al.Add(nl.Item(i));
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return al;
        }

        ///<summary>
        ///  Gets an ArrayList of XmlNode children using an xPath expression
        ///</summary>
        public static IList<XmlNode> GetChildNodesFromCriteria(XmlNode XmlNode, string xPathExpression)
        {
            var al = new List<XmlNode>();
            try
            {
                XmlNodeList nl = XmlNode.SelectNodes(xPathExpression);

                if (nl != null)
                {
                    for (int i = 0; i < nl.Count; i++)
                        al.Add(nl.Item(i));
                }
            }
            catch (Exception e)
            {
                throw;
                //HandleException(e);
            }


            return al;
        }

        ///<summary>
        ///  Get first child node given an XPath expression
        ///</summary>
        public XmlNode GetFirstChildNodeFromCriteria(string xPathExpression)
        {
            XmlNode node = null;

            try
            {
                node = m_xmlDocument.SelectSingleNode(xPathExpression);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return node;
        }

        ///<summary>
        ///  Get the Attribute value from a given XmlNode
        ///</summary>
        public string GetAttributeValue(XmlNode node, string sAttributeName)
        {
            string sVal = "";
            try
            {
                XmlAttributeCollection attribColl = node.Attributes;
                XmlAttribute attrib = attribColl[sAttributeName, ""];
                sVal = Decode(attrib.Value);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return sVal;
        }

        ///<summary>
        ///  Get the Attribute int32 (int) value from a given XmlNode
        ///</summary>
        public int GetAttributeInt32Value(XmlNode node, string sAttributeName)
        {
            string sVal = GetAttributeValue(node, sAttributeName);

            return sVal != "" ? Convert.ToInt32(sVal) : 0;
        }

        ///<summary>
        ///  Get the Attribute floating point/Single value from a given XmlNode
        ///</summary>
        public float GetAttributeFloatValue(XmlNode node, string sAttributeName)
        {
            string sVal = GetAttributeValue(node, sAttributeName);
            return sVal != "" ? Convert.ToSingle(sVal) : 0;
        }

        ///<summary>
        ///  Get the Attribute double value from a given XmlNode
        ///</summary>
        public double GetAttributeDoubleValue(XmlNode node, string sAttributeName)
        {
            string sVal = GetAttributeValue(node, sAttributeName);
            return sVal != "" ? Convert.ToDouble(sVal) : 0.00;
        }

        ///<summary>
        ///  Get the Attribute boolean value from a given XmlNode
        ///</summary>
        public bool GetAttributeBooleanValue(XmlNode node, string sAttributeName)
        {
            string sVal = GetAttributeValue(node, sAttributeName);
            return sVal != "" ? Convert.ToBoolean(sVal) : false;
        }

        ///<summary>
        ///  Get the Element value from a given XmlNode
        ///</summary>
        public string GetElementValue(XmlNode xmlNode)
        {
            string sVal = "";

            try
            {
                sVal = Decode(xmlNode.InnerXml);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return sVal;
        }

        ///<summary>
        ///  Get the Element Int32 value from a given XmlNode
        ///</summary>
        public int GetElementInt32Value(XmlNode xmlNode)
        {
            string sVal = GetElementValue(xmlNode);
            return sVal != "" ? Convert.ToInt32(sVal) : 0;
        }

        ///<summary>
        ///  Get the Element float/single floating point value from a given XmlNode
        ///</summary>
        public float GetElementFloatValue(XmlNode xmlNode)
        {
            string sVal = GetElementValue(xmlNode);
            return sVal != "" ? Convert.ToSingle(sVal) : 0;
        }

        ///<summary>
        ///  Get the Element Double value from a given XmlNode
        ///</summary>
        public double GetElementDoubleValue(XmlNode xmlNode)
        {
            string sVal = GetElementValue(xmlNode);
            return sVal != "" ? Convert.ToDouble(sVal) : 0.00;
        }

        ///<summary>
        ///  Get the Element Boolean value from a given XmlNode
        ///</summary>
        public bool GetElementBooleanValue(XmlNode xmlNode)
        {
            string sVal = GetElementValue(xmlNode);
            return sVal != "" ? Convert.ToBoolean(sVal) : false;
        }

        ///<summary>
        ///  Get the first Child Element value from a given XmlNode
        ///</summary>
        public string GetChildElementValue(XmlNode parentNode, string sElementName)
        {
            string sVal = "";
            try
            {
                XmlNodeList childNodes = parentNode.ChildNodes;
                foreach (XmlNode childNode in childNodes)
                {
                    if (childNode.Name == sElementName)
                    {
                        sVal = GetElementValue(childNode);
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return sVal;
        }

        ///<summary>
        ///  Get the Child Element int32 value from a given XmlNode and ElementName
        ///</summary>
        public int GetChildElementInt32Value(XmlNode parentNode, string sElementName)
        {
            string sVal = GetChildElementValue(parentNode, sElementName);
            return sVal != "" ? Convert.ToInt32(sVal) : 0;
        }

        ///<summary>
        ///  Get the Child Element floating point/single value from a given XmlNode and ElementName
        ///</summary>
        public float GetChildElementFloatValue(XmlNode parentNode, string sElementName)
        {
            string sVal = GetChildElementValue(parentNode, sElementName);
            return sVal != "" ? Convert.ToSingle(sVal) : 0;
        }

        ///<summary>
        ///  Get the Child Element double value from a given XmlNode and ElementName
        ///</summary>
        public double GetChildElementDoubleValue(XmlNode parentNode, string sElementName)
        {
            string sVal = GetChildElementValue(parentNode, sElementName);
            return sVal != "" ? Convert.ToDouble(sVal) : 0.00;
        }

        ///<summary>
        ///  Get the Child Element boolean value from a given XmlNode and ElementName
        ///</summary>
        public bool GetChildElementBooleanValue(XmlNode parentNode, string sElementName)
        {
            string sVal = GetChildElementValue(parentNode, sElementName);
            return sVal != "" ? Convert.ToBoolean(sVal) : false;
        }

        ///<summary>
        ///  Returns the first XmlNode object matching this element name
        ///  <seealso cref='GetFirstChildXmlNode' />
        ///</summary>
        public XmlNode GetFirstChildXmlNodeFromRoot(string sElementName)
        {
            // TODO:  isn't there a better/faster/more effiecient way to do this?  couldn't find it sifting through documentation!
            XmlNodeList nodeList = GetChildNodesFromRoot(sElementName);
            if (nodeList.Count > 0)
                return nodeList[0];

            return null;
        }

        ///<summary>
        ///  Returns the first XmlNode object matching this element name 
        ///  NOTE:  this doesn't seem to work if parent is Root!  Use GetFirstChildXmlNodeFromRoot
        ///  <seealso cref='GetFirstChildXmlNodeFromRoot' />
        ///</summary>
        public XmlNode GetFirstChildXmlNode(XmlNode parentNode, string sElementName)
        {
            // NOTE:  this doesn't seem to work if parent is Root!  Use GetFirstChildXmlNodeFromRoot
            XmlNode foundChildNode = null;
            try
            {
                XmlNodeList childNodes = parentNode.ChildNodes;
                foreach (XmlNode childNode in childNodes)
                {
                    if (childNode.Name == sElementName)
                    {
                        foundChildNode = childNode;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return foundChildNode;
        }

        ///<summary>
        ///  Returns an XmlNodeList of child nodes matching this element name
        ///</summary>
        public XmlNodeList GetChildNodesFromRoot(string sElementName)
        {
            return m_xmlDocument.GetElementsByTagName(sElementName);
        }

        ///<summary>
        ///  Returns an ArrayList (boxed XmlNode objects) of child nodes matching this element name
        ///  This function is recursive in that it will find ALL the children, even if their in 
        ///  sub folders (sub child nodes)
        ///</summary>
        public ArrayList GetRecursiveChildNodesFromParent(XmlNode parentNode, string sElementName)
        {
            var elementList = new ArrayList();

            try
            {
                XmlNodeList children = parentNode.ChildNodes;
                foreach (XmlNode child in children)
                {
                    if (child.Name == sElementName)
                        elementList.Add(child);

                    if (child.HasChildNodes)
                    {
                        ArrayList childrenList = GetRecursiveChildNodesFromParent(child, sElementName);
                        if (childrenList.Count > 0)
                        {
                            foreach (XmlNode subChild in childrenList)
                                elementList.Add(subChild);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return elementList;
        }

        ///<summary>
        ///  Create an Element under the given parent based on the name and value pair.
        ///</summary>
        public XmlElement CreateNodeElement(XmlNode parentNode, string sElementName, string sElementValue)
        {
            XmlElement newElem = null;

            try
            {
                newElem = m_xmlDocument.CreateElement(sElementName);
                //newElem.InnerXml = Encode(sElementValue);
                newElem.InnerXml = sElementValue;
                XmlDocument ownerDoc = parentNode.OwnerDocument;

                if (ownerDoc != null)
                {
                    parentNode.AppendChild(newElem);
                }
                else
                {
                    XmlElement root = m_xmlDocument.DocumentElement;
                    root.AppendChild(newElem);
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return newElem;
        }

        ///<summary>
        ///  Create an Element under the given parent based on the name and value pair.
        ///</summary>
        public XmlElement CreateNodeElement(XmlNode parentNode, string sElementName, string sElementValue,
                                            string sAttributeName, string sAttributeValue)
        {
            XmlElement newElem = null;

            try
            {
                newElem = m_xmlDocument.CreateElement(sElementName);
                //newElem.InnerXml = Encode(sElementValue);
                newElem.InnerXml = sElementValue;
                XmlDocument ownerDoc = parentNode.OwnerDocument;


                XmlAttribute newAttr = null;
                newAttr = m_xmlDocument.CreateAttribute(sAttributeName);
                newElem.SetAttributeNode(newAttr);
                newElem.SetAttribute(sAttributeName, "", sAttributeValue);


                if (ownerDoc != null)
                {
                    parentNode.AppendChild(newElem);
                }
                else
                {
                    XmlElement root = m_xmlDocument.DocumentElement;
                    root.AppendChild(newElem);
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return newElem;
        }

        ///<summary>
        ///  Creates and adds a comment before the given node.  If root node, or null, 
        ///  the comment node is Appended to the tree.
        ///</summary>
        public XmlNode CreateComment(XmlNode insertAfterThisNode, string sVal)
        {
            if (insertAfterThisNode == null)
                return null;

            XmlNode createdNode = null;
            try
            {
                XmlComment commentNode = m_xmlDocument.CreateComment(Encode(sVal));
                createdNode = insertAfterThisNode.AppendChild(commentNode);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return createdNode;
        }


        public XmlNode CreateXmlDeclaration(string version, string encoding, string standalone)
        {
            XmlNode createdNode = null;
            try
            {
                XmlDeclaration dec = m_xmlDocument.CreateXmlDeclaration(version, encoding, standalone);
                createdNode = m_xmlDocument.PrependChild(dec);
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return createdNode;
        }


        ///<summary>
        ///  Delete an XmlNode from the tree
        ///</summary>
        public bool DeleteNodeElement(XmlNode targetNode)
        {
            bool bResult = false;

            try
            {
                XmlNode xmlNode = RootNode.RemoveChild(targetNode);
                if (xmlNode != null)
                    bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  Modify an XmlNode elment with a new value.
        ///</summary>
        public bool ModifyNodeElementValue(XmlNode targetNode, string sNewElementValue)
        {
            bool bResult = false;

            try
            {
                //targetNode.InnerXml = Encode(sNewElementValue);
                targetNode.InnerXml = sNewElementValue;
                bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  Create a new attribute given an XmlElement (XmlNode) target
        ///</summary>
        public XmlAttribute CreateNodeAttribute(XmlElement targetElement, string sAttributeName, string sAttributeValue)
        {
            XmlAttribute newAttr = null;

            try
            {
                newAttr = m_xmlDocument.CreateAttribute(sAttributeName);
                targetElement.SetAttributeNode(newAttr);
                targetElement.SetAttribute(sAttributeName, "", Encode(sAttributeValue));
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return newAttr;
        }


        ///<summary>
        ///  Delete an attribute from the given target node.
        ///</summary>
        public bool DeleteNodeAttribute(XmlNode targetNode, string sAttributeName)
        {
            bool bResult = false;

            try
            {
                XmlAttributeCollection attrColl = targetNode.Attributes;
                XmlAttribute xmlAttribute = attrColl.Remove(attrColl[sAttributeName, ""]);
                if (xmlAttribute != null)
                    bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  GenerateSchema a schema file from a given target file
        ///</summary>
        public bool GenerateSchema(string sTargetFile)
        {
            bool bResult = false;
            try
            {
                var data = new DataSet();
                data.ReadXml(new XmlNodeReader(RootNode), XmlReadMode.Auto);
                data.WriteXmlSchema(sTargetFile);
                bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  GenerateSchemaAsString based on the currently loaded Xml
        ///</summary>
        public string GenerateSchemaAsString()
        {
            string sSchemaXmlString = "";
            try
            {
                var data = new DataSet();
                data.ReadXml(new XmlNodeReader(RootNode), XmlReadMode.Auto);

                string sTempFile = Path.GetTempFileName();

                data.WriteXmlSchema(sTempFile);

                // read the data into a string
                var sr = new StreamReader(sTempFile);
                sSchemaXmlString = sr.ReadToEnd();
                sr.Close();

                if (File.Exists(sTempFile))
                    File.Delete(sTempFile);
            }
            catch (Exception e)
            {
                HandleException(e);
                sSchemaXmlString = "<root><error>" + LastErrorMessage + "</error></root>";
            }

            return sSchemaXmlString;
        }

        ///<summary>
        ///  Modify an attribute value to a new value
        ///</summary>
        public bool ModifyNodeAttributeValue(XmlNode targetNode, string sAttributeName, string sNewAttributeValue)
        {
            bool bResult = false;

            try
            {
                XmlAttributeCollection attrColl = targetNode.Attributes;
                XmlAttribute xmlAttribute = attrColl[sAttributeName, ""];
                xmlAttribute.Value = Encode(sNewAttributeValue);
                bResult = true;
            }
            catch (Exception e)
            {
                HandleException(e);
            }

            return bResult;
        }

        ///<summary>
        ///  Internal method used to ensure that HTML and XML tags are encoded within their values
        ///</summary>
        private string Encode(string input)
        {
            string output = input;
            output = Regex.Replace(output, "&", "&amp;");
            output = Regex.Replace(output, "<", "&lt;");
            output = Regex.Replace(output, ">", "&gt;");
            output = Regex.Replace(output, "\"", "&quot;");

            return output;
        }

        ///<summary>
        ///  Internal method used to ensure that HTML and XML tags are decoded for display in other systems
        ///</summary>
        private string Decode(string input)
        {
            string output = input;
            output = Regex.Replace(output, "&amp;", "&");
            output = Regex.Replace(output, "&lt;", "<");
            output = Regex.Replace(output, "&gt;", ">");
            output = Regex.Replace(output, "&quot;", "\"");
            return output;
        }

        ///<summary>
        ///  Internal method used to process errors and exception handling
        ///</summary>
        private void HandleException(Exception e)
        {
            m_sLastErrorMessage = e.Message;

            Console.WriteLine(m_sLastErrorMessage + " Stack Trace:  " + e.StackTrace + " Source: " + e.Source);

            throw e;
        }
    }
}