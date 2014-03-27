// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：MemcacheCache.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;

namespace Dev.Framework.Cache.Impl
{
    /// <summary>
    /// 使用memcache,
    /// </summary>
    internal class MemcacheCache : ICacheState
    {
        #region ICacheState Members

        public object GetObject<T>(object key)
        {
            throw new NotImplementedException();
        }

        public object GetObjectByKey(string key)
        {
            throw new NotImplementedException();
        }

        public T Get<T>() 
        {
            throw new NotImplementedException();
        }

        public object GetObject<T>()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(object key) 
        {
            throw new NotImplementedException();
        }

        public void Put<T>(T instance) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object instance)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(object key, T instance) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object key, object instance)
        {
            throw new NotImplementedException();
        }

        public void PutObjectByKey(string key, object instance)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(T instance, DateTime absoluteExpiration) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object instance, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(object key, T instance, DateTime absoluteExpiration) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object key, object instance, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void PutObjectByKey(string key, object instance, DateTime absoluteExpiration)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(T instance, TimeSpan slidingExpiration) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object instance, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Put<T>(object key, T instance, TimeSpan slidingExpiration) 
        {
            throw new NotImplementedException();
        }

        public void PutObject<T>(object key, object instance, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void PutObjectByKey(string key, object instance, TimeSpan slidingExpiration)
        {
            throw new NotImplementedException();
        }

        public void Remove<T>() 
        {
            throw new NotImplementedException();
        }

        public void Remove<T>(object key) 
        {
            throw new NotImplementedException();
        }

        public void RemoveByKey(string key)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}