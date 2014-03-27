// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/TypeConverter.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Text.RegularExpressions;

namespace Dev.Comm.Utils
{
    public class TypeConverter
    {
        /// <summary>
        ///   string型转换为bool型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的bool类型结果 </returns>
        public static bool StrToBool(object expression, bool defValue)
        {
            if (expression != null)
                return StrToBool(expression, defValue);

            return defValue;
        }

        /// <summary>
        ///   string型转换为bool型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的bool类型结果 </returns>
        public static bool StrToBool(string expression, bool defValue)
        {
            if (expression != null)
            {
                if (string.Compare(expression, "true", true) == 0)
                    return true;
                else if (string.Compare(expression, "false", true) == 0)
                    return false;
            }
            return defValue;
        }

        /// <summary>
        ///   将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int ObjectToInt(object expression)
        {
            return ObjectToInt(expression, 0);
        }

        /// <summary>
        ///   将对象转换为Int32类型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int ObjectToInt(object expression, int defValue)
        {
            if (expression != null)
                return StrToInt(expression.ToString(), defValue);

            return defValue;
        }

        /// <summary>
        ///   将对象转换为Int32类型,转换失败返回0
        /// </summary>
        /// <param name="str"> 要转换的字符串 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int StrToInt(string str)
        {
            return StrToInt(str, 0);
        }

        /// <summary>
        ///   将对象转换为Int32类型
        /// </summary>
        /// <param name="str"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int StrToInt(string str, int defValue)
        {
            if (string.IsNullOrEmpty(str) || str.Trim().Length >= 11 ||
                !Regex.IsMatch(str.Trim(), @"^([-]|[0-9])[0-9]*(\.\w*)?$"))
                return defValue;

            int rv;
            if (Int32.TryParse(str, out rv))
                return rv;

            return Convert.ToInt32(StrToFloat(str, defValue));
        }

        /// <summary>
        ///   string型转换为float型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static float StrToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        ///   string型转换为float型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static float ObjectToFloat(object strValue, float defValue)
        {
            if ((strValue == null))
                return defValue;

            return StrToFloat(strValue.ToString(), defValue);
        }

        /// <summary>
        ///   string型转换为float型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static float ObjectToFloat(object strValue)
        {
            return ObjectToFloat(strValue.ToString(), 0);
        }

        /// <summary>
        ///   string型转换为float型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static float StrToFloat(string strValue)
        {
            if ((strValue == null))
                return 0;

            return StrToFloat(strValue, 0);
        }

        /// <summary>
        ///   string型转换为float型
        /// </summary>
        /// <param name="strValue"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static float StrToFloat(string strValue, float defValue)
        {
            if ((strValue == null) || (strValue.Length > 10))
                return defValue;

            float intValue = defValue;
            if (strValue != null)
            {
                bool IsFloat = Regex.IsMatch(strValue, @"^([-]|[0-9])[0-9]*(\.\w*)?$");
                if (IsFloat)
                    float.TryParse(strValue, out intValue);
            }
            return intValue;
        }

        /// <summary>
        ///   将对象转换为日期时间类型
        /// </summary>
        /// <param name="str"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static DateTime StrToDateTime(string str, DateTime defValue)
        {
            if (!string.IsNullOrEmpty(str))
            {
                DateTime dateTime;
                if (DateTime.TryParse(str, out dateTime))
                    return dateTime;
            }
            return defValue;
        }

        /// <summary>
        ///   将对象转换为日期时间类型
        /// </summary>
        /// <param name="str"> 要转换的字符串 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static DateTime StrToDateTime(string str)
        {
            return StrToDateTime(str, DateTime.Now);
        }

        /// <summary>
        ///   将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj"> 要转换的对象 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static DateTime ObjectToDateTime(object obj)
        {
            return StrToDateTime(obj.ToString());
        }

        /// <summary>
        ///   将对象转换为日期时间类型
        /// </summary>
        /// <param name="obj"> 要转换的对象 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static DateTime ObjectToDateTime(object obj, DateTime defValue)
        {
            return StrToDateTime(obj.ToString(), defValue);
        }

        /// <summary>
        ///   字符串转成整型数组
        /// </summary>
        /// <param name="idList"> 要转换的字符串 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int[] StringToIntArray(string idList)
        {
            return StringToIntArray(idList, -1);
        }

        /// <summary>
        ///   字符串转成整型数组
        /// </summary>
        /// <param name="idList"> 要转换的字符串 </param>
        /// <param name="defValue"> 缺省值 </param>
        /// <returns> 转换后的int类型结果 </returns>
        public static int[] StringToIntArray(string idList, int defValue)
        {
            if (string.IsNullOrEmpty(idList))
                return null;
            string[] strArr = idList.Split(",".ToCharArray());
            var intArr = new int[strArr.Length];
            for (int i = 0; i < strArr.Length; i++)
                intArr[i] = StrToInt(strArr[i], defValue);

            return intArr;
        }

        #region 泛型数据类型转换

        /**/

        /// <summary>
        ///   泛型数据类型转换
        /// </summary>
        /// <typeparam name="T"> 自定义数据类型 </typeparam>
        /// <param name="value"> 传入需要转换的值 </param>
        /// <param name="defaultValue"> 默认值 </param>
        /// <returns> </returns>
        public static T ConvertType<T>(object value, T defaultValue)
        {
            return (T)ConvertToT(value, defaultValue);
        }

        /**/

        /// <summary>
        ///   转换数据类型
        /// </summary>
        /// <typeparam name="T"> 自定义数据类型 </typeparam>
        /// <param name="myvalue"> 传入需要转换的值 </param>
        /// <param name="defaultValue"> 默认值 </param>
        /// <returns> </returns>
        public static object ConvertToT<T>(object myvalue, T defaultValue)
        {
            //TypeCode typeCode = Type.GetTypeCode(typeof(T));
            //if (myvalue == null)
            //    return defaultValue;
            //string value = myvalue.ToString();
            //switch (typeCode)
            //{
            //    case TypeCode.Boolean:
            //        bool flag = false;
            //        if (bool.TryParse(value, out flag))
            //            return flag;
            //        return defaultValue;
            //    case TypeCode.Char:
            //        char c;
            //        if (Char.TryParse(value, out c))
            //            return c;
            //        return defaultValue;
            //    case TypeCode.SByte:
            //        sbyte s = 0;
            //        if (SByte.TryParse(value, out s))
            //            return s;
            //        return defaultValue;
            //    case TypeCode.Byte:
            //        byte b = 0;
            //        if (Byte.TryParse(value, out b))
            //            return b;
            //        return defaultValue;
            //    case TypeCode.Int16:
            //        Int16 i16 = 0;
            //        if (Int16.TryParse(value, out i16))
            //            return i16;
            //        return defaultValue;
            //    case TypeCode.UInt16:
            //        UInt16 ui16 = 0;
            //        if (UInt16.TryParse(value, out ui16))
            //            return ui16;
            //        return defaultValue;
            //    case TypeCode.Int32:
            //        int i = 0;
            //        if (Int32.TryParse(value, out i))
            //            return i;
            //        return defaultValue;
            //    case TypeCode.UInt32:
            //        UInt32 ui32 = 0;
            //        if (UInt32.TryParse(value, out ui32))
            //            return ui32;
            //        return defaultValue;
            //    case TypeCode.Int64:
            //        Int64 i64 = 0;
            //        if (Int64.TryParse(value, out i64))
            //            return i64;
            //        return defaultValue;
            //    case TypeCode.UInt64:
            //        UInt64 ui64 = 0;
            //        if (UInt64.TryParse(value, out ui64))
            //            return ui64;
            //        return defaultValue;
            //    case TypeCode.Single:
            //        Single single = 0;
            //        if (Single.TryParse(value, out single))
            //            return single;
            //        return defaultValue;
            //    case TypeCode.Double:
            //        double d = 0;
            //        if (Double.TryParse(value, out d))
            //            return d;
            //        return defaultValue;
            //    case TypeCode.Decimal:
            //        decimal de = 0;
            //        if (Decimal.TryParse(value, out de))
            //            return de;
            //        return defaultValue;
            //    case TypeCode.DateTime:
            //        DateTime dt;
            //        if (DateTime.TryParse(value, out dt))
            //            return dt;
            //        return defaultValue;
            //    case TypeCode.String:
            //        if (value.Length == 0)
            //            return "";
            //        return value;
            //}

            return ConvertToT(myvalue, typeof(T), defaultValue);

            throw new ArgumentNullException("defaultValue", "不能为Empty,Object,DBNull");
        }


        public static object ConvertToT(object myvalue, Type type, object defaultValue = null)
        {

            TypeCode typeCode = Type.GetTypeCode(type);
            if (myvalue == null)
                return defaultValue;
            string value = myvalue.ToString();
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    bool flag = false;
                    if (bool.TryParse(value, out flag))
                        return flag;
                    return defaultValue ?? flag;
                case TypeCode.Char:
                    char c;
                    if (Char.TryParse(value, out c))
                        return c;
                    return defaultValue ?? c;
                case TypeCode.SByte:
                    sbyte s = 0;
                    if (SByte.TryParse(value, out s))
                        return s;
                    return defaultValue ?? s;
                case TypeCode.Byte:
                    byte b = 0;
                    if (Byte.TryParse(value, out b))
                        return b;
                    return defaultValue ?? b;
                case TypeCode.Int16:
                    Int16 i16 = 0;
                    if (Int16.TryParse(value, out i16))
                        return i16;
                    return defaultValue ?? i16;
                case TypeCode.UInt16:
                    UInt16 ui16 = 0;
                    if (UInt16.TryParse(value, out ui16))
                        return ui16;
                    return defaultValue ?? ui16;
                case TypeCode.Int32:
                    int i = 0;
                    if (Int32.TryParse(value, out i))
                        return i;
                    return defaultValue ?? i;
                case TypeCode.UInt32:
                    UInt32 ui32 = 0;
                    if (UInt32.TryParse(value, out ui32))
                        return ui32;
                    return defaultValue ?? ui32;
                case TypeCode.Int64:
                    Int64 i64 = 0;
                    if (Int64.TryParse(value, out i64))
                        return i64;
                    return defaultValue ?? i64;
                case TypeCode.UInt64:
                    UInt64 ui64 = 0;
                    if (UInt64.TryParse(value, out ui64))
                        return ui64;
                    return defaultValue ?? ui64;
                case TypeCode.Single:
                    Single single = 0;
                    if (Single.TryParse(value, out single))
                        return single;
                    return defaultValue ?? single;
                case TypeCode.Double:
                    double d = 0;
                    if (Double.TryParse(value, out d))
                        return d;
                    return defaultValue ?? d;
                case TypeCode.Decimal:
                    decimal de = 0;
                    if (Decimal.TryParse(value, out de))
                        return de;
                    return defaultValue ?? de;
                case TypeCode.DateTime:
                    DateTime dt;
                    if (DateTime.TryParse(value, out dt))
                        return dt;
                    return defaultValue ?? dt;
                case TypeCode.String:
                    if (value.Length == 0)
                        return "";
                    return value;
            }
            throw new ArgumentNullException("defaultValue", "不能为Empty,Object,DBNull");
        }

        #endregion
    }
}