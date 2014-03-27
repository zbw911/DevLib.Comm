// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/W3wpUtil.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Collections.Generic;
using System.DirectoryServices;
using System.Management;
using System.Text.RegularExpressions;

namespace Dev.Comm.Utils
{
    /// <summary>
    ///   W3wp 的摘要说明
    ///   集合IIS容器的相关操作
    /// </summary>
    public class W3wpUtil
    {
        #region AppPollControlOption enum

        public enum AppPollControlOption
        {
            /// <summary>
            ///   启动
            /// </summary>
            Start,

            /// <summary>
            ///   回收
            /// </summary>
            Recycle,

            /// <summary>
            ///   停止
            /// </summary>
            Stop
        }

        #endregion

        private W3wpUtil()
        {
        }

        /// <summary>
        ///   得到所有IIS应用程序池名字
        /// </summary>
        /// <param name="input"> </param>
        /// <returns> </returns>
        public static IList<string> GetAllW3wp(string input)
        {
            var oQuery = new ObjectQuery("select * from Win32_Process where Name='w3wp.exe'");

            var oSearcher = new ManagementObjectSearcher(oQuery);

            ManagementObjectCollection oReturnCollection = oSearcher.Get();

            string pid;

            string cmdLine;

            IList<string> sb = new List<string>();

            foreach (ManagementObject oReturn in oReturnCollection)
            {
                pid = oReturn.GetPropertyValue("ProcessId").ToString();

                cmdLine = (string) oReturn.GetPropertyValue("CommandLine");

                string pattern = "-ap \"(.*)\"";

                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                Match match = regex.Match(cmdLine);

                if (match == null || match.Groups.Count < 2) continue;

                string appPoolName = match.Groups[1].ToString();

                //sb.AppendFormat("W3WP.exe PID:{0} AppPoolId:{1}", pid, appPoolName);
                sb.Add(appPoolName);
            }

            return sb;
        }

        /// <summary>
        ///   得到应用程序池的名字 进程ID可以使用此方法获取 System.Diagnostics.Process.GetCurrentProcess().Id
        /// </summary>
        /// <param name="ProcessId"> </param>
        /// <returns> </returns>
        public static string GetAppPoolNameByPId(object ProcessId)
        {
            var oQuery = new ObjectQuery(string.Format("select * from Win32_Process where ProcessId={0}", ProcessId));

            var oSearcher = new ManagementObjectSearcher(oQuery);

            ManagementObjectCollection oReturnCollection = oSearcher.Get();

            string pid;

            string cmdLine;

            if (oReturnCollection.Count == 0)
                return "";

            string result = string.Empty;

            foreach (ManagementObject oReturn in oReturnCollection)
            {
                string pname = oReturn.GetPropertyValue("Name").ToString();

                if (pname.ToLower() != "w3wp.exe") continue;

                pid = oReturn.GetPropertyValue("ProcessId").ToString();

                cmdLine = (string) oReturn.GetPropertyValue("CommandLine");

                string pattern = "-ap \"(.*)\"";

                var regex = new Regex(pattern, RegexOptions.IgnoreCase);

                Match match = regex.Match(cmdLine);

                if (match == null || match.Groups.Count < 2) continue;

                string appPoolName = match.Groups[1].ToString();

                result = appPoolName;
            }

            return result;
        }

        /// <summary>
        ///   控制应用程序池开始，停止和回收
        /// </summary>
        /// <param name="AppPoolName"> </param>
        /// <param name="Option"> </param>
        public static void AppPoolControl(string AppPoolName, AppPollControlOption Option)
        {
            string method = Option.ToString("g");

            var appPool = new DirectoryEntry("IIS://localhost/W3SVC/AppPools");
            DirectoryEntry findPool = appPool.Children.Find(AppPoolName, "IIsApplicationPool");
            findPool.Invoke(method, null);
            appPool.CommitChanges();
            appPool.Close();
        }
    }
}