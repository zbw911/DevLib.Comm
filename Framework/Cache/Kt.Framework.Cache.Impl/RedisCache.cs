using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Framework.Cache.Impl
{
    /// <summary>
    /// Redis 支持方案
    /// </summary>
    class RedisCache : ICacheState
    {
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

        public object GetObject<T>(object key)
        {
            throw new NotImplementedException();
        }

        public object GetObjectByKey(string key)
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
    }
}
