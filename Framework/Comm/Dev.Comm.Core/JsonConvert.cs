// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/JsonConvert.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Dev.Comm.Json;

namespace Dev.Comm
{
    /********  comm 中引用System.web.Extends，这个问题值得考虑一下，以后是否应该改用json.net 去除web的依赖， ***********/

    /// <summary>
    /// </summary>
    public static class JsonConvert
    {
        /// <summary>
        ///   将对象转成Json格式
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static string ToJsonStr<T>(T t)
        {
            var ser = new DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            //替换Json的Date字符串 
            string p = @"\\/Date\((\d+)\+\d+\)\\/";
            MatchEvaluator matchEvaluator = ConvertJsonDateToDateString;
            var reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            return jsonString;
        }

        /// <summary>
        ///   将动态对象转化为Json 字符串
        /// </summary>
        /// <param name="obj"> </param>
        /// <returns> </returns>
        public static string ToJsonStrDyn(dynamic obj)
        {
            //var o = ObjectDynamicConvert.ToExpando(obj);
            //var serializer = new JavaScriptSerializer();

            //StringBuilder sb = new StringBuilder();
            //serializer.Serialize(o, sb);

            JavaScriptSerializer serializer = new JavaScriptSerializer();

            //serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            //if (MaxJsonLength.HasValue)
            //{
            //    serializer.MaxJsonLength = MaxJsonLength.Value;
            //}
            //if (RecursionLimit.HasValue)
            //{
            //    serializer.RecursionLimit = RecursionLimit.Value;
            //}
            return (serializer.Serialize(obj));

            //return Newtonsoft.Json.JsonConvert.SerializeObject(o);
            //return sb.ToString();
        }

        /// <summary>
        ///   将JSON 字符转换为对象
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static T ToJsonObject<T>(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式  
            //或 将"yyyy-MM-ddTHH:mm:ss.xxx"格式的字符串转为"\/Date(1294499956278+0800)\/"格式  
            //string p = @"\d{4}-\d{2}-\d{2}\s\d{1,2}:\d{1,2}:\d{1,2}";
            string p = @"\d{4}-\d{2}-\d{2}[T\s]\d{1,2}:\d{1,2}:\d{1,2}[\.]?\d{0,3}";
            MatchEvaluator matchEvaluator = ConvertDateStringToJsonDate;
            var reg = new Regex(p);
            jsonString = reg.Replace(jsonString, matchEvaluator);
            var ser = new DataContractJsonSerializer(typeof (T));
            var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            var obj = (T) ser.ReadObject(ms);
            return obj;
        }


        /// <summary>
        ///   动态json
        /// </summary>
        /// <param name="jsonString"> </param>
        /// <returns> </returns>
        public static dynamic ToJsonObject(string jsonString)
        {
            //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式  
            //string p = @"\d{4}-\d{2}-\d{2}\s\d{1,2}:\d{1,2}:\d{1,2}";
            //MatchEvaluator matchEvaluator = ConvertDateStringToJsonDate;
            //var reg = new Regex(p);
            //jsonString = reg.Replace(jsonString, matchEvaluator);
            //var ser = new DataContractJsonSerializer();
            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            //var obj = (T)ser.ReadObject(ms);
            //return obj;


            //string p = @"\d{4}-\d{2}-\d{2}\s\d{1,2}:\d{1,2}:\d{1,2}";
            //MatchEvaluator matchEvaluator = ConvertDateStringToJsonDate;
            //var reg = new Regex(p);
            //jsonString = reg.Replace(jsonString, matchEvaluator);
            //var ser = new DataContractJsonSerializer(typeof(DynamicJsonConverter.DynamicJsonObject));


            //var ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            //dynamic obj = ser.ReadObject(ms);
            //return obj;

            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] {new DynamicJsonConverter()});

            dynamic obj = serializer.Deserialize(jsonString, typeof (object));

            return obj;

            //return Newtonsoft.Json.JsonConvert.DeserializeObject(jsonString);
        }


        /// <summary>
        ///   原生生成Json对像
        /// </summary>
        /// <param name="jsonString"> </param>
        /// <returns> </returns>
        public static dynamic ToJsonDynamic(string jsonString)
        {
            var jss = new JavaScriptSerializer();

            dynamic obj = jss.Deserialize<dynamic>(jsonString);

            return obj;
        }


        ///// <summary> 
        ///// /// JSON序列化  
        ///// /// </summary>  
        //public static string JsonSerializer<T>(T t)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    MemoryStream ms = new MemoryStream();
        //    ser.WriteObject(ms, t);
        //    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //    ms.Close();
        //    //替换Json的Date字符串 
        //    string p = @"\\/Date\((\d+)\+\d+\)\\/";
        //    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertJsonDateToDateString);
        //    Regex reg = new Regex(p);
        //    jsonString = reg.Replace(jsonString, matchEvaluator);
        //    return jsonString;
        //}
        ///// <summary>  
        ///// /// JSON反序列化 
        ///// /// </summary>  
        //public static T JsonDeserialize<T>(string jsonString)
        //{
        //    //将"yyyy-MM-dd HH:mm:ss"格式的字符串转为"\/Date(1294499956278+0800)\/"格式  
        //    string p = @"\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}";
        //    MatchEvaluator matchEvaluator = new MatchEvaluator(ConvertDateStringToJsonDate);
        //    Regex reg = new Regex(p);
        //    jsonString = reg.Replace(jsonString, matchEvaluator);
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        //    T obj = (T)ser.ReadObject(ms);
        //    return obj;
        //}
        /// <summary>
        ///   将Json序列化的时间由/Date(1294499956278+0800)转为字符串
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;

            var dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        ///   将时间字符串转为Json时间
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
    }
}