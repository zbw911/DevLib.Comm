// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：HashImageServer.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dev.Framework.FileServer.Config;

namespace Dev.Framework.FileServer.HashServer
{
    internal class HashFileServer
    {
        /// <summary>
        /// HASH 取得配置，并返回配置文件
        /// </summary>
        /// <param name="unqid"></param>
        /// <returns></returns>
        internal static Server HashConfig(string unqid)
        {
            IEnumerable<Server> usedserver = ServerList.Where(x => x.used);
            if (usedserver == null || usedserver.Count() == 0)
            {
                throw new Exception("不存在有效的服务器");
            }

            char first = unqid.Substring(0, 1).ToArray()[0];

            var firstNo = (int)first;

            int index = firstNo % usedserver.Count();

            return usedserver.ElementAt(index);
        }

        /// <summary>
        /// 根据ID取得服务器
        /// </summary>
        /// <param name="ServerId"></param>
        /// <returns></returns>
        internal static Server GetServer(int ServerId)
        {
            Server usedserver = ServerList.FirstOrDefault(x => x.id == ServerId);
            if (usedserver == null)
            {
                throw new Exception("不存在有效的服务器");
            }

            return usedserver;
        }
        /// <summary>
        /// 全部服务器列表
        /// </summary>
        internal static List<Server> ServerList
        {
            get
            {
                return ReadConfig.Configuration.Servers.ToList().Select(x => new Server
                    {
                        hostip = x.hostip,
                        id = x.id,
                        password = x.password,
                        serverurl = ToAbsloutUrl(x.serverurl),
                        startdirname = x.startdirname,
                        used = x.used,
                        username = x.username
                    }).ToList();
            }
        }


        /// <summary>
        /// 将 ~/ 类型的Url转化为将前的绝对地址
        /// </summary>
        /// <param name="contentPath"></param>
        /// <returns></returns>
        private static string ToAbsloutUrl(string contentPath)
        {
            if (String.IsNullOrEmpty(contentPath))
            {
                return contentPath;
            }

            if (contentPath.StartsWith("~") && HttpContext.Current == null)
            {
                throw new Exception("在非WEB环境下无法使用~开头的路径设置");
            }

            if (contentPath.StartsWith("~"))
                contentPath = VirtualPathUtility.ToAbsolute(contentPath, System.Web.HttpContext.Current.Request.ApplicationPath);
            return contentPath;
        }


    }
}