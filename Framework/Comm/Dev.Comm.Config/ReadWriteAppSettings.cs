// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ReadWriteAppSettings.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Xml;

namespace Dev.Comm
{
    ///  <summary> 
    /// Summary description for ReadWriteConfig. 
    /// .config appsetting readwrite  
    /// added by zbw911
    ///  </summary> 
    public class ReadWriteAppSettings
    {
        private XmlDocument cfgDoc;
        public string docName = String.Empty;
        public string message;
        private XmlNode node;

        #region GetValue

        public string GetValue(string key)
        {
            // retrieve the appSettings node   
            node = cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }
            // XPath select setting "add" element that contains this key to remove       
            var addElem = (XmlElement) node.SelectSingleNode("//add[@key='" + key + "']");


            if (addElem == null)
            {
                throw new ArgumentNullException("");
            }

            string value = addElem.GetAttribute("value");

            return value;
        }

        #endregion

        #region SetValue

        public bool SetValue(string key, string value)
        {
            //增加 

            // retrieve the appSettings node    
            node = cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }

            // XPath select setting "add" element that contains this key        
            var addElem = (XmlElement) node.SelectSingleNode("//add[@key='" + key + "']");
            if (addElem != null)
            {
                message = "此key已经存在！";
                return false;
            }
                // not found, so we need to add the element, key and value    
            else
            {
                XmlElement entry = cfgDoc.CreateElement("add");
                entry.SetAttribute("key", key);
                entry.SetAttribute("value", value);
                node.AppendChild(entry);
            }
            //save it    
            saveConfigDoc(cfgDoc, docName);
            message = "添加成功！";
            return true;
        }

        #endregion

        #region saveConfigDoc

        private void saveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
        {
            try
            {
                var writer = new XmlTextWriter(cfgDocPath, null);
                writer.Formatting = Formatting.Indented;
                cfgDoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
                return;
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region removeElement

        public string removeElement(string elementKey)
        {
            // 删除 


            // retrieve the appSettings node   
            node = cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }

            var addElem = (XmlElement) node.SelectSingleNode("//add[@key='" + elementKey + "']");
            if (addElem == null)
            {
                message = "此key不存在！";
                return message;
            }
            // XPath select setting "add" element that contains this key to remove       
            node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
            saveConfigDoc(cfgDoc, docName);
            message = "删除成功！";
            return message;
        }

        #endregion

        #region modifyElement

        public string modifyElement(string elementKey, string elementValue)
        {
            // retrieve the appSettings node   
            node = cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }
            // XPath select setting "add" element that contains this key to remove       
            var addElem = (XmlElement) node.SelectSingleNode("//add[@key='" + elementKey + "']");
            if (addElem == null)
            {
                message = "此key不存在！";
                return message;
            }

            addElem.SetAttribute("value", elementValue);
            //save it    
            saveConfigDoc(cfgDoc, docName);
            message = "修改成功！";
            return message;
        }

        #endregion

        #region loadConfigDoc

        public XmlDocument loadConfigDoc(string docName)
        {
            this.docName = docName;
            cfgDoc = new XmlDocument();

            cfgDoc.Load(docName);
            return cfgDoc;
        }

        #endregion

        public int ConfigType { get; set; }
    }
}