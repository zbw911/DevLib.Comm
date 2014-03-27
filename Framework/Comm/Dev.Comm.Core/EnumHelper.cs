// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/EnumHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.Comm
{
    /// <summary>
    ///   枚举类型帮助方法
    ///   added by zbw911
    /// </summary>
    public class EnumHelper
    {
        public static int GetNumberByEnum<TEnum>(TEnum e) where TEnum : struct
        {
            return Convert.ToInt32(Enum.Parse(typeof (TEnum), e.ToString()));
        }


        public static TEnum GetEnumbyNumber<TEnum>(string number) where TEnum : struct
        {
            return (TEnum) Enum.Parse(typeof (TEnum), number);
        }


        public static TEnum GetEnumbyNumber<TEnum>(int number) where TEnum : struct
        {
            return (TEnum) Enum.Parse(typeof (TEnum), number.ToString());
        }
    }
}