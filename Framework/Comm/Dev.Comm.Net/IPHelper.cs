// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IPHelper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Net;

namespace Dev.Comm.Net
{
    public class IPHelper
    {
        /// <summary>
        /// 将IP地址转换为数字[long型] 杨栋添加
        /// </summary>
        /// <param name="strIP">IP地址</param>
        /// <returns>返回long型数字</returns>
        public static long IPToLong(string strIP)
        {
            try
            {
                long Ip = 0;
                string[] addressIP = strIP.Split('.');
                Ip = Convert.ToUInt32(addressIP[3]) + Convert.ToUInt32(addressIP[2])*256 +
                     Convert.ToUInt32(addressIP[1])*256*256 + Convert.ToUInt32(addressIP[0])*256*256*256;
                return Ip;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将[long型]数字转换为IP地址 杨栋添加
        /// </summary>
        /// <param name="strNum">数字</param>
        /// <returns>返回IP地址</returns>
        public static string LongToIP(long strNum)
        {
            var numip = new IPAddress(strNum);
            string[] addressIP = numip.ToString().Split('.');
            string IP = addressIP[3] + "." + addressIP[2] + "." + addressIP[1] + "." + addressIP[0];
            return IP;
        }
    }
}