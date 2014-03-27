// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年04月16日 18:19
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/UrlHelperExtension.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Web;
using System.Web.Mvc;

namespace Dev.Comm.Web
{
    /// <summary>
    /// </summary>
    public static class UrlHelperExtension
    {
        public static string Absolute(this UrlHelper url, string relativeOrAbsolute)
        {
            var uri = new Uri(relativeOrAbsolute, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                return relativeOrAbsolute;
            }


            // At this point, we know the url is relative.
            return VirtualPathUtility.ToAbsolute(relativeOrAbsolute);
        }
    }
}