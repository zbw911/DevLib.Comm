// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月16日 15:40
//  
//  修改于：2013年07月20日 15:13
//  文件名：Dev.Libs/Dev.Comm.Web/WindowNameData.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Comm.Web
{
    /// <summary>
    ///   生成 为 window.name跨域的数据
    /// </summary>
    public class WindowNameData
    {
        public static string StringData(string data)
        {
            return GenStr(data);
        }


        /// <summary>
        ///   Json数据
        /// </summary>
        /// <param name="data"> </param>
        /// <typeparam name="T"> </typeparam>
        /// <returns> </returns>
        public static string StringData<T>(T data)
        {
            var str = Dev.Comm.JsonConvert.ToJsonStr(data);
            return GenStr(str);
        }


        private static string GenStr(string data)
        {
            return string.Format(@"<script>window.name = '{0}';</script>", data);
        }
    }
}