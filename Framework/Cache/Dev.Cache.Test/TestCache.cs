using System;
using System.Threading;
using Dev.Framework.Cache;
using Dev.Framework.Cache.AppFabric;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Cache.Test
{
    [TestClass]
    public class TestCache
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
        public void TestMethod1()
        {
            var key = "ddddddd";
            var data = this._cacheWraper.SmartyGetPut(key, new TimeSpan(10, 0, 0), () => this.GetData());

            Console.WriteLine(data.DateTime);

            this._cacheState.Remove<datacls>(key);

            Thread.Sleep(1000 * 1);

            data = this._cacheWraper.SmartyGetPut(key, new TimeSpan(10, 0, 0), () => this.GetData());

            Console.WriteLine(data.DateTime);
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

    public class datacls
    {
        #region Instance Properties

        public DateTime DateTime { get; set; }

        #endregion
    }
}