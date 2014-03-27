using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dev.Comm.Utils;

namespace Dev.Comm.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DictionaryExtension
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
        /// 取得任意类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <param name="defValue"></param>
        /// <returns></returns>
        public static T Get<T>(this IDictionary data, object key, T defValue)
        {
            if (data == null || !data.Contains(key))
                return defValue;


            var val = data[key];

            return TypeConverter.ConvertType(val, defValue);
        }
        /// <summary>
        /// 取得Decimal类型
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetDecimal(this   IDictionary data, string key)
        {
            return Get<decimal>(data, key, default(decimal));
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
            if (data != null) if (data.Contains(key)) result = data[key] as string;
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


        ///// <summary>
        ///// 得到DateTime
        ///// </summary>
        ///// <param name="data"></param>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static DateTime GetDateTime(this IDictionary data, string key)
        //{
        //    DateTime result = default(DateTime);
        //    if (data != null)
        //        if (data.Contains(key)) result = UcUtility.PhpTimeToDateTime(long.Parse(data[key].ToString()));
        //    return result;
        //}


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
            if (data != null) if (data.Contains(key)) if (data[key] is Hashtable) result = (Hashtable)data[key];
            return result;
        }
    }
}
