using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;

namespace Commons
{
    public class XmlCompent
    {
        protected static Hashtable GetChildTable(XmlNode xn)//已知道有子接点
        {
            Hashtable ht = new Hashtable();
            foreach (XmlNode nxn in xn.ChildNodes)
            {
                if (nxn.ChildNodes.Count <= 0)
                {
                    ht.Add(getNodeName(nxn), nxn.InnerText);
                }
                else if (nxn.ChildNodes.Count == 1)
                {
                    XmlNode nxn1 = nxn.ChildNodes[0];
                    if (nxn1.NodeType == XmlNodeType.CDATA)
                    {
                        ht.Add(getNodeName(nxn), nxn.InnerText);
                    }
                    else
                    {
                        ht.Add(getNodeName(nxn), GetChildTable(nxn));
                    }
                }
                else
                {
                    ht.Add(getNodeName(nxn), GetChildTable(nxn));
                }
            }
            return ht;
        }
        /// <summary>
        /// 字符串格式XML转换成HASHTABLE
        /// </summary>
        /// <param name="XmlFile"></param>
        /// <returns></returns>
        public static Hashtable GetTable(string XmlFile)
        {
            try
            {
                Hashtable ht = new Hashtable();
                XmlDocument XMLDom = new XmlDocument();
                XMLDom.LoadXml(XmlFile);
                XmlNode newXMLNode = XMLDom.SelectSingleNode("root");
                foreach (XmlNode xn in newXMLNode.ChildNodes)
                {
                    if (xn.ChildNodes.Count <= 0)
                    {
                        ht.Add(getNodeName(xn), xn.InnerText);
                    }
                    else if (xn.ChildNodes.Count == 1)//这里主要是判断子接点中是否是<![CDATA[0]]>情形
                    {
                        XmlNode nxn = xn.ChildNodes[0];
                        if (nxn.NodeType == XmlNodeType.CDATA)
                        {
                            ht.Add(getNodeName(xn), xn.InnerText);
                        }
                        else
                        {
                            ht.Add(getNodeName(xn), GetChildTable(xn));
                        }
                    }
                    else
                    {
                        ht.Add(getNodeName(xn), GetChildTable(xn));
                    }
                }
                //foreach (XmlNode xn in newXMLNode.ChildNodes)
                //{
                //    int k= xn.ChildNodes.Count;

                //    ht.Add(xn.Name.ToString(), xn.InnerText);
                //}
                return ht;
            }
            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write(XmlFile);
                //HttpContext.Current.Response.End();

                throw new Exception();
                return null;
            }
            ////Stream s = new MemoryStream(ASCIIEncoding.Default.GetBytes(XmlFile)); 
            //XmlReader reader = null;
            //string key = "";
            //string value = "";
            //Hashtable ht = new Hashtable();
            //try
            //{
            //    reader = XmlReader.Create(new StringReader(XmlFile));
            //    while (reader.Read())
            //    {
            //        if (reader.NodeType == XmlNodeType.Element)
            //        {
            //            //读取key属性
            //            if (reader.Name.ToString().IndexOf("item") > -1)
            //            {
            //                key = reader.Name == null ? "" : reader.Name.ToString();
            //            }

            //        }
            //        if (reader.NodeType == XmlNodeType.Text && key.Length > 0)
            //        {
            //            if (key != "" && ht.ContainsKey(key) == false)
            //            {
            //                //取得value值
            //                value = reader.Value == null ? "" : reader.Value.ToString();
            //                ht.Add(key, value);
            //            }
            //            key = "";
            //            value = "";
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    return null;
            //}
            //finally
            //{
            //    if (reader!=null)
            //    {//如果reader不为null，关闭reader
            //        reader.Close();
            //    }
            //}
            //return ht;
        }


        private static string getNodeName(XmlNode xmlNode)
        {
            var name = xmlNode.Name + ((xmlNode.Attributes["id"] != null) ? ("_" + xmlNode.Attributes["id"].Value) : "");
            return name;
        }

        /// <summary>
        /// 导航菜单
        /// </summary>
        /// <returns></returns>
        //public static string ShowConfigMenu(string Path)
        //{
        //    string s = "";
        //    string temp = "";
        //    string name = "";
        //    string url = "";
        //    bool start = false;
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(HttpContext.Current.Request.PhysicalApplicationPath + "Config.xml");
        //    XmlReader reader = new XmlNodeReader(doc);
        //    while (reader.Read())
        //    {
        //        //判断当前读取得节点类型
        //        switch (reader.NodeType)
        //        {
        //            case XmlNodeType.Element:
        //                s = reader.Name;
        //                if (reader.Name == "AddMenu") start = true;

        //                break;
        //            case XmlNodeType.Text:
        //                if (!s.Equals("Name"))
        //                {
        //                    if (start)
        //                    {
        //                        if (s == "name")
        //                            name = reader.Value;
        //                        if (s == "url")
        //                            url = reader.Value;
        //                        if (name != "" && url != "")
        //                        {
        //                            temp += "					<li><a href=\"" + Path + url + "\"  target=\"_blank\">" + name + "</a></li>\n";
        //                            if (url.IndexOf("bbs") < 0)
        //                            {
        //                                temp += "					<li><div class=\"main_nav_divline\"></div></li>\n";
        //                            }
        //                            name = "";
        //                            url = "";
        //                        }
        //                    }
        //                }
        //                break;
        //        }
        //    }
        //    return temp;
        //}
    }
    //固定顺序的HASHTABLE
    public class NoSortHashTable : Hashtable
    {
        private ArrayList list = new ArrayList();
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
        public override ICollection Keys
        {
            get
            {
                return list;
            }
        }
    }
}
