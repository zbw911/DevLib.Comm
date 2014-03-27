// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/MockUrlCode.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Comm.Utils
{
    /// <summary>
    ///   不使用　System.Web.进行ＵＲＬ编码　，　added by zbw911
    /// </summary>
    public class MockUrlCode
    {

        /// <summary>
        ///   UrlEndcode
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static string UrlEncode(string str)
        {
            return System.Uri.EscapeDataString(str);
        }


        /// <summary>
        /// UrlDecode
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlDecode(string str)
        {
            return System.Uri.UnescapeDataString(str);
        }


    }
}