// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/StringExtensions.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using Dev.Comm.Utils;

namespace Dev.Comm.Extensions
{
    /// <summary>
    /// </summary>
    public static class StringExtensions
    {
        public static T AS<T>(this string source)
        {
            return AS<T>(source, default(T));
        }


        public static T AS<T>(this string source, T defaultvalue)
        {
            T result = TypeConverter.ConvertType(source, defaultvalue);
            return result;
        }

        public static int AsInt(this string source, int defaultvalue)
        {
            return AS<int>(source, defaultvalue);
        }

        public static int AsInt(this string source)
        {
            return AS<int>(source);
        }

        public static float AsFloat(this string source, float defaultvalue)
        {
            return AS<float>(source, defaultvalue);
        }

        public static float AsFloat(this string source)
        {
            return AS<float>(source);
        }
    }
}