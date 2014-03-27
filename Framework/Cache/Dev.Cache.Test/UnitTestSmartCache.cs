using System;
using System.Threading;
using Dev.Framework.Cache;
using Dev.Framework.Cache.AppFabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Cache.Test
{
    [TestClass]
    public class UnitTestSmartCache
    {
        #region Fields

        private ICacheState _cacheState;
        private ICacheWraper _cacheWraper;

        #endregion
        [TestInitialize]
        public void init()
        {
            this._cacheState = new AppFabricCache();
            this._cacheWraper = new CacheWraper(this._cacheState);
        }
        #region Instance Methods

        public datacls GetData()
        {
            return new datacls
            {
                DateTime = System.DateTime.Now
            };
        }

        [TestMethod]
        public void TestRemoveSlidingCache()
        {
            var key = "ddddddd";
            var data = this._cacheWraper.SmartyGetPut(key, new TimeSpan(10, 0, 0), this.GetData);

            Console.WriteLine(data.DateTime);

            this._cacheState.Remove<datacls>(key);

            Thread.Sleep(1000 * 1);

            var data2 = this._cacheWraper.SmartyGetPut(key, new TimeSpan(10, 0, 0), this.GetData);

            Assert.AreNotEqual(data.DateTime, data2.DateTime);

            Console.WriteLine(data2.DateTime);
        }
        [TestMethod]
        public void TestRemoveAbsoluteCache()
        {
            var key = "ddddddd";
            var data = this._cacheWraper.SmartyGetPut(key, System.DateTime.Now.AddSeconds(1), this.GetData);

            Console.WriteLine(data.DateTime);

            //this._cacheState.Remove<datacls>(key);



            var data2 = this._cacheState.Get<datacls>(key);

            Assert.IsNotNull(data2);

            Thread.Sleep(5 * 1000);

            data2 = this._cacheState.Get<datacls>(key);
            Assert.IsNull(data2);

            //Thread.Sleep(1000 * 10);

            //var data2 = this._cacheWraper.SmartyGetPut(key, new TimeSpan(10, 0, 0), this.GetData);

            //Assert.AreNotEqual(data.DateTime, data2.DateTime);

            //Console.WriteLine(data2.DateTime);
        }

        [TestMethod]
        public void TestNoExpritionCache()
        {
            var key = "ddddddd";
            var data = this._cacheWraper.SmartyGetPut(key, this.GetData);

            Console.WriteLine(data.DateTime);

            //this._cacheState.Remove<datacls>(key);



            var data2 = this._cacheState.Get<datacls>(key);

            Assert.IsNotNull(data2);
            // 实际上 AppFabric的过期时间默认是20分钟，所以 。。。。
            Thread.Sleep(10 * 60 * 1000 + 1);

            data2 = this._cacheState.Get<datacls>(key);
            Assert.IsNotNull(data2);

        }








        [TestMethod]
        public void MyTestMethod()
        {

            var key = "llllll";
            this._cacheState.Remove<datacls>(key);
            var data = this._cacheWraper.SmartyGetPut(key, () => System.DateTime.Now);

            Console.WriteLine(data);
        }

        #endregion
    }
}
