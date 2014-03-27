// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年02月26日 10:18
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/EmailAddressAttribute.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Text;

namespace Dev.Comm.Web.Mvc.Validate
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    ///   邮件地址
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailAddressAttribute : DataTypeAttribute
    {
        private readonly Regex regex = new Regex(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", RegexOptions.Compiled);

        public EmailAddressAttribute()
            : base(DataType.EmailAddress)
        {
        }

        public override bool IsValid(object value)
        {
            string str = Convert.ToString(value, CultureInfo.CurrentCulture);
            if (string.IsNullOrEmpty(str))
                return true;

            Match match = regex.Match(str);
            return ((match.Success && (match.Index == 0)) && (match.Length == str.Length));
        }
    }
}