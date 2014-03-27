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
        private XmlDocument _cfgDoc;
        private string docName = String.Empty;
        //private string message;
        //private XmlNode node;

        #region GetValue

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public string GetValue(string key)
        {
            // retrieve the appSettings node   
            var node = _cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }
            // XPath select setting "add" element that contains this key to remove       
            var addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");


            if (addElem == null)
            {
                throw new ArgumentNullException("");
            }

            string value = addElem.GetAttribute("value");

            return value;
        }

        #endregion

        #region SetValue

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public bool SetValue(string key, string value)
        {
            //增加 

            // retrieve the appSettings node    
            var node = _cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }

            // XPath select setting "add" element that contains this key        
            var addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + key + "']");
            if (addElem != null)
            {
                var message = "此key已经存在！";

                throw new Exception(message);
                return false;
            }
            // not found, so we need to add the element, key and value    
            else
            {
                XmlElement entry = _cfgDoc.CreateElement("add");
                entry.SetAttribute("key", key);
                entry.SetAttribute("value", value);
                node.AppendChild(entry);
            }
            //save it    
            SaveConfigDoc(_cfgDoc, docName);

            return true;
        }

        #endregion

        #region saveConfigDoc

        internal void SaveConfigDoc(XmlDocument cfgDoc, string cfgDocPath)
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementKey"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public bool RemoveElement(string elementKey)
        {
            // 删除 


            // retrieve the appSettings node   
            var node = _cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }

            var addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + elementKey + "']");
            if (addElem == null)
            {
                var message = "此key不存在！";
                throw new Exception(message);
            }
            // XPath select setting "add" element that contains this key to remove       
            node.RemoveChild(node.SelectSingleNode("//add[@key='" + elementKey + "']"));
            SaveConfigDoc(_cfgDoc, docName);

            return true;
        }

        #endregion

        #region modifyElement

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementKey"></param>
        /// <param name="elementValue"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public bool ModifyElement(string elementKey, string elementValue)
        {
            // retrieve the appSettings node   
            var node = _cfgDoc.SelectSingleNode("//appSettings");
            if (node == null)
            {
                throw new InvalidOperationException("appSettings section not found");
            }
            // XPath select setting "add" element that contains this key to remove       
            var addElem = (XmlElement)node.SelectSingleNode("//add[@key='" + elementKey + "']");
            if (addElem == null)
            {
                var message = "此key不存在！";
                throw new Exception(message);
            }

            addElem.SetAttribute("value", elementValue);
            //save it    
            SaveConfigDoc(_cfgDoc, docName);

            return true;
        }

        #endregion

        #region loadConfigDoc

        /// <summary>
        /// 
        /// </summary>
        /// <param name="docName"></param>
        /// <returns></returns>
        public XmlDocument LoadConfigDoc(string docName)
        {
            this.docName = docName;
            _cfgDoc = new XmlDocument();

            _cfgDoc.Load(docName);
            return _cfgDoc;
        }

        #endregion

        //public int ConfigType { get; set; }
    }
}