// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CacheWraper.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.Framework.Cache
{


    /// <summary>
    /// 事实上的空
    /// </summary>
    [Serializable]
    public class FactNull
    {
    }

    /// <summary>
    /// 包装器
    /// </summary>
    public class CacheWraper : ICacheWraper
    {
        public ICacheState CacheState;

        public CacheWraper(ICacheState CacheState)
        {
            this.CacheState = CacheState;
        }

        #region ICacheWraper Members

        /// <summary>
        /// 绝对过期的智能方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="absoluteExpiration"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(object key, DateTime absoluteExpiration, Func<T> getDataFunc)
        {
            return InnerCache(key, (cachekey, data) => this.CacheState.PutObjectByKey(cachekey, data, absoluteExpiration), getDataFunc);


            //var cachekey = key.BuildFullKey<T>();

            //var instance = this.CacheState.GetObjectByKey(cachekey);


            //if (instance == null)
            //    instance = GetDataFunc();
            ////取回的数据还是空的
            //if (instance == null)
            //{
            //    this.CacheState.PutObjectByKey(cachekey, new FactNull(), absoluteExpiration);
            //    return default(T);
            //}

            //if (instance is FactNull)
            //    return default(T);

            //if (instance is T)
            //{
            //    this.CacheState.Put(key, (T)instance, absoluteExpiration);
            //    return (T)instance;
            //}

            //throw new Exception("Error");
        }

        /// <summary>
        /// 绝对过期的智能方式
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="absoluteExpiration"></param>
        /// <param name="GetDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(DateTime absoluteExpiration, Func<T> GetDataFunc)
        {
            return this.SmartyGetPut(null, absoluteExpiration, GetDataFunc);
        }

        /// <summary>
        /// 相对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="slidingExpiration"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(object key, TimeSpan slidingExpiration, Func<T> getDataFunc)
        {

            return InnerCache(key, (cachekey, data) => this.CacheState.PutObjectByKey(cachekey, data, slidingExpiration), getDataFunc);

            //var cachekey = key.BuildFullKey<T>();

            //var instance = this.CacheState.GetObjectByKey(cachekey);


            //if (instance == null)
            //    instance = GetDataFunc();
            ////取回的数据还是空的
            //if (instance == null)
            //{
            //    this.CacheState.PutObjectByKey(cachekey, new FactNull(), slidingExpiration);
            //    return default(T);
            //}

            //if (instance is FactNull)
            //    return default(T);

            //if (instance is T)
            //{
            //    this.CacheState.Put(key, (T)instance, slidingExpiration);
            //    return (T)instance;
            //}

            //throw new Exception("Error");
        }


        private T InnerCache<T>(object key, Action<string, object> innerAction, Func<T> getDataFunc)
        {
            var cachekey = key.BuildFullKey<T>();

            var instance = this.CacheState.GetObjectByKey(cachekey);


            if (instance == null)
                instance = getDataFunc();
            //取回的数据还是空的
            if (instance == null)
            {
                innerAction(cachekey, new FactNull());
                //this.CacheState.PutObjectByKey(cachekey, new FactNull(), slidingExpiration);
                return default(T);
            }

            if (instance is FactNull)
                return default(T);

            if (instance is T)
            {
                //this.CacheState.PutObjectByKey(cachekey, instance, slidingExpiration);
                innerAction(cachekey, instance);
                return (T)instance;
            }

            throw new Exception("Error");
        }

        /// <summary>
        /// 相对过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="slidingExpiration"></param>
        /// <param name="GetDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(TimeSpan slidingExpiration, Func<T> GetDataFunc)
        {
            return this.SmartyGetPut(null, slidingExpiration, GetDataFunc);
        }

        /// <summary>
        /// 永远不过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(object key, Func<T> getDataFunc)
        {
            return InnerCache(key, (cachekey, data) => this.CacheState.PutObjectByKey(cachekey, data), getDataFunc);

            //var cachekey = key.BuildFullKey<T>();

            //var instance = this.CacheState.GetObjectByKey(cachekey);


            //if (instance == null)
            //    instance = GetDataFunc();
            ////取回的数据还是空的
            //if (instance == null)
            //{
            //    this.CacheState.PutObjectByKey(cachekey, new FactNull());
            //    return default(T);
            //}

            //if (instance is FactNull)
            //    return default(T);

            //if (instance is T)
            //{
            //    this.CacheState.Put(key, (T)instance);
            //    return (T)instance;
            //}

            //throw new Exception("Error");
        }

        /// <summary>
        /// 永远不过期
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="getDataFunc"></param>
        /// <returns></returns>
        public T SmartyGetPut<T>(Func<T> getDataFunc)
        {
            return this.SmartyGetPut(null, getDataFunc);
        }

        #endregion
    }
}