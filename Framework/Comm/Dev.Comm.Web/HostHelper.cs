// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：HostHelper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Management;
using System.Net;
using System.Web;
using Dev.Comm.Utils;

namespace Dev.Comm.Web
{
    public class HostHelper
    {
        #region 获取主机名称

        /// <summary>
        /// 获取本地机器名称
        /// </summary>
        /// <returns>机器名称</returns>
        public static string GetHostName()
        {
            string hostName = "";

            hostName = Dns.GetHostName();
            return hostName;
        }

        #endregion

        #region 穿过代理服务器获得Ip地址,如果有多个IP，则第一个是用户的真实IP，其余全是代理的IP，用逗号隔开

        public static string getRealIp()
        {
            string UserIP;
            if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
            {
                UserIP = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            }
            else if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_VIA"])) //得到穿过代理服务器的ip地址
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    UserIP = StringUtil.GetFirstIp(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]);
                else
                    UserIP = HttpContext.Current.Request.ServerVariables["HTTP_VIA"];
            }
            else
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                    UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                else
                    UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                UserIP = UserIP.Replace("；", ",");
                UserIP = UserIP.Replace(";", ",");
                UserIP = UserIP.Replace("，", ",");

                UserIP = StringUtil.GetFirstIp(UserIP);
            }
            return UserIP;
        }

        /// <summary>
        /// 快网CDN专用取IP方法 ygj 2011-6-15 团卡前台用的快网CDN，必须掉这个方法
        /// </summary>
        /// <returns></returns>
        public static string getRealIp_byKW()
        {
            string UserIP;

            if (!string.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                UserIP = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            else
                UserIP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            UserIP = UserIP.Replace("；", ",");
            UserIP = UserIP.Replace(";", ",");
            UserIP = UserIP.Replace("，", ",");

            UserIP = StringUtil.GetFirstIp(UserIP);
            return UserIP;
        }

        public static string getAllIp()
        {
            string UserIP;
            if (HttpContext.Current.Request.Headers["Cdn-Src-Ip"] != null)
            {
                UserIP = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];
            }
            else if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null) //得到穿过代理服务器的ip地址
            {
                UserIP = string.Format("{0},{1}",
                                       HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                                       HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            }
            else
            {
                UserIP = HttpContext.Current.Request.UserHostAddress;
            }
            return UserIP;
        }

        #endregion

        #region 获取主机MAC地址

        /// <summary>
        /// 获取本机MAC地址
        /// </summary>
        /// <returns>string null</returns>
        public static string GetHostMac()
        {
            string mac = "";

            ManagementClass mc;
            mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection moc = mc.GetInstances();
            foreach (ManagementObject mo in moc)
            {
                if (mo["IPEnabled"].ToString() == "True")
                    mac = mo["MacAddress"].ToString();
            }
            return mac;
        }

        #endregion
    }
}