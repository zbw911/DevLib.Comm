// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IDictionaryExtension.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections;

namespace DS.Web.UCenter
{
    /// <summary>   
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    internal static class DictionaryExtension
    {
        /// <summary>
        /// 得到Int
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetInt(this IDictionary data, string key)
        {
            int result = default(int);
            if (data != null) if (data.Contains(key)) int.TryParse(data[key].ToString(), out result);
            return result;
        }


        /// <summary>
        /// 得到String
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetString(this IDictionary data, string key)
        {
            string result = string.Empty;
            if (data != null) if (data.Contains(key)) result = data[key].ToString();
            return result;
        }


        /// <summary>
        /// 得到Bool
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="compare"></param>
        /// <returns></returns>
        public static bool GetBool(this IDictionary data, string key, string compare = "1")
        {
            bool result = default(bool);
            if (data != null) if (data.Contains(key)) result = (data[key].ToString() == compare);
            return result;
        }


        /// <summary>
        /// 得到DateTime
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(this IDictionary data, string key)
        {
            DateTime result = default(DateTime);
            if (data != null)
                if (data.Contains(key)) result = UcUtility.PhpTimeToDateTime(long.Parse(data[key].ToString()));
            return result;
        }


        /// <summary>
        /// 得到Double
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static double GetDouble(this IDictionary data, string key)
        {
            double result = default(double);
            if (data != null) if (data.Contains(key)) double.TryParse(data[key].ToString(), out result);
            return result;
        }


        /// <summary>
        /// 得到Hashtable
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Hashtable GetHashtable(this IDictionary data, string key)
        {
            Hashtable result = default(Hashtable);
            if (data != null) if (data.Contains(key)) if (data[key] is Hashtable) result = (Hashtable) data[key];
            return result;
        }
    }
}