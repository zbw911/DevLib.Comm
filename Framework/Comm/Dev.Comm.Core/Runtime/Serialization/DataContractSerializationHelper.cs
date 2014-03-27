// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/DataContractSerializationHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Xml;
using Dev.Comm.IO;

namespace Dev.Comm.Runtime.Serialization
{
    /// <summary>
    /// </summary>
    public class DataContractSerializationHelper
    {
        public static void Serialize<T>(T o, string filePath)
        {
            string folderPath = Path.GetDirectoryName(filePath);
            IOUtility.EnsureDirectoryExists(folderPath);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                Serialize(o, stream);
            }
        }

        public static void Serialize<T>(T o, Stream stream)
        {
            var ser = new DataContractSerializer(typeof (T));

            var settings = new XmlWriterSettings()
                               {
                                   Indent = true,
                                   IndentChars = "\t"
                               };
            XmlWriter xmlWriter = XmlWriter.Create(stream, settings);
            using (var writer = XmlWriter.Create(xmlWriter, settings))
            {
                ser.WriteObject(writer, o);
            }
        }

        public static T Deserialize<T>(string filePath)
        {
            var ser = new DataContractSerializer(typeof (T));
            return (T) Deserialize(typeof (T), new[] {typeof (T)}, filePath);
        }

        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Deserialize(type, knownTypes, stream);
            }
        }

        public static object Deserialize(Type type, IEnumerable<Type> knownTypes, Stream stream)
        {
            var ser = new DataContractSerializer(type, knownTypes);
            return ser.ReadObject(stream);
        }

        public static string SerializeAsXml<T>(T o, IEnumerable<Type> knownTypes = null)
        {
            if (knownTypes == null)
            {
                knownTypes = new[] {typeof (T)};
            }
            var sb = new StringBuilder();
            using (XmlWriter xmlWriter = XmlWriter.Create(sb))
            {
                var ser = new DataContractSerializer(typeof (T), knownTypes);
                ser.WriteObject(xmlWriter, o);
            }
            return sb.ToString();
        }

        public static T DeserializeFromXml<T>(string xml, IEnumerable<Type> knownTypes = null)
        {
            if (knownTypes == null)
            {
                knownTypes = new[] {typeof (T)};
            }
            using (XmlReader reader = XmlReader.Create(new StringReader(xml)))
            {
                var ser = new DataContractSerializer(typeof (T), knownTypes);
                return (T) ser.ReadObject(reader);
            }
        }
    }
}