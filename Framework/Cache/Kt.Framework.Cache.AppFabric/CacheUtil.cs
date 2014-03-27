// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CacheUtil.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Diagnostics;
using Microsoft.ApplicationServer.Caching;

namespace Dev.Framework.Cache.AppFabric
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheUtil
    {
        private static DataCacheFactory _factory;
        private static DataCache _cache;

        public static DataCache GetCache()
        {
            if (_cache != null)
                return _cache;

            //-------------------------
            // Configure Cache Client 
            //-------------------------
            /*
            //Define Array for 1 Cache Host
            List<DataCacheServerEndpoint> servers = new List<DataCacheServerEndpoint>(1);

            //Specify Cache Host Details 
            //  Parameter 1 = host name
            //  Parameter 2 = cache port number
            servers.Add(new DataCacheServerEndpoint("WINAPP1", 22233));
            //servers.Add(new DataCacheServerEndpoint("WINAPP2", 22233));
            //servers.Add(new DataCacheServerEndpoint("WINAPP4", 22233));
            //DataCacheSecurity dcs = new DataCacheSecurity(

            //Create cache configuration
            DataCacheFactoryConfiguration configuration = new DataCacheFactoryConfiguration();

            //Set the cache host(s)
            configuration.Servers = servers;

            //Set default properties for local cache (local cache disabled)
            //configuration.LocalCacheProperties = new DataCacheLocalCacheProperties();
            */

            //Disable tracing to avoid informational/verbose messages on the web page
            DataCacheClientLogManager.ChangeLogLevel(TraceLevel.Info);

            //Pass configuration settings to cacheFactory constructor
            //_factory = new DataCacheFactory(configuration);
            _factory = new DataCacheFactory();
            //Get reference to named cache called "default"
            _cache = _factory.GetCache("default");

            return _cache;
        }
    }
}