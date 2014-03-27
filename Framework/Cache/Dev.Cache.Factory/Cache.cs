// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:43
// 
// 修改于：2013年02月18日 18:24
// 文件名：Cache.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using Dev.Framework.Cache;
using Dev.Framework.Cache.AppFabric;

namespace Dev.Cache.Factory
{
    /// <summary>
    ///   sington 模式
    /// </summary>
    public class Cache
    {
        #region Readonly & Static Fields

        private static ICacheState iCacheState;
        private static ICacheWraper iCacheWraper;

        #endregion

        #region Class Properties

        /// <summary>
        ///   普通方式
        /// </summary>
        public static ICacheState CacheState
        {
            get
            {
                if (iCacheState == null)
                {
                    //从这里硬编码方式切换缓存模式，未来可以考虑使用反射方式进行配置文件配置
                    iCacheState = new AppFabricCache();

                    //切换为普通缓存                    
                    // iCacheState = new HttpRuntimeCache();
                }

                return iCacheState;
            }
        }

        /// <summary>
        ///   智能化方式
        /// </summary>
        public static ICacheWraper CacheWraper
        {
            get
            {
                if (iCacheWraper == null)
                {
                    //从这里硬编码方式切换缓存模式，未来可以考虑使用反射方式进行配置文件配置
                    iCacheWraper = new CacheWraper(CacheState);
                }
                return iCacheWraper;
            }
        }

        #endregion
    }
}