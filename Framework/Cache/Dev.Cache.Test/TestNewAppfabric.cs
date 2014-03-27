using System;
using System.Threading;
using Dev.Framework.Cache;
using Dev.Framework.Cache.AppFabric;
using Dev.Framework.Cache.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Cache.Test
{
    [TestClass]
    public class TestNewAppfabric
    {
        #region Fields

        private ICacheState _cacheState;
        private ICacheWraper _cacheWraper;

        #endregion
        [TestInitialize]
        public void init()
        {
            this._cacheState = new AppFabricCache();
            //this._cacheState = new HttpRuntimeCache();
            this._cacheWraper = new CacheWraper(this._cacheState);
        }


        [TestMethod]
        public void MyTestMethod()
        {
            var key = "key";
            var cachekey = key.BuildFullKey<datacls>();

            var fromcache = new datacls
                                {
                                    DateTime = System.DateTime.Now
                                };

            this._cacheState.PutObjectByKey(cachekey, fromcache);


            var cachefrom = this._cacheState.Get<datacls>(key);
            Assert.AreEqual(fromcache.DateTime, cachefrom.DateTime);



        }

        [TestMethod]
        public void MyTestMethod_Get_IsNull()
        {
            var cachefrom = this._cacheState.Get<datacls>();
            Assert.IsNull(cachefrom);
        }


        [TestMethod]
        public void MyTestMethod_GetByKey()
        {
            MyTestMethod();
            var key = "key";
            var cachekey = key.BuildFullKey<datacls>();
            var cachefrom = this._cacheState.GetObjectByKey(cachekey);

            Assert.IsNotNull(cachefrom);
        }


        [TestMethod]
        public void MyTestMethod_GetType_abs()
        {
            var mykey = "adfasdf";
            var val = new datacls
                          {
                              DateTime = System.DateTime.Now
                          };

            this._cacheState.PutObjectByKey(mykey, val, new System.TimeSpan(0, 0, 1));

            var cacheval = this._cacheState.GetObjectByKey(mykey);

            Assert.IsNotNull(cacheval);

            Thread.Sleep(1 * 500);




            Thread.Sleep(1 * 500);
            cacheval = this._cacheState.GetObjectByKey(mykey);
            Assert.IsNull(cacheval);
        }

        [TestMethod]
        public void MyTestMethod_GetType_111()
        {
            var mykey = "adfasdf";
            var val = new datacls
            {
                DateTime = System.DateTime.Now
            };

            this._cacheState.PutObjectByKey(mykey, val, System.DateTime.Now.AddSeconds(1));

            var cacheval = this._cacheState.GetObjectByKey(mykey);

            Assert.IsNotNull(cacheval);

            Thread.Sleep(1 * 1000);


            cacheval = this._cacheState.GetObjectByKey(mykey);

            Assert.IsNull(cacheval);
        }



        [TestMethod]
        public void MyTestMethodSmartPutGet()
        {
            var key = "aaaaa";
            var obj = this._cacheWraper.SmartyGetPut<int>(key, System.DateTime.Now.AddSeconds(1000), () => 1);
            var getobj = this._cacheState.GetObjectByKey(key.BuildFullKey<int>());
            Assert.IsNotNull(getobj);
            Console.WriteLine(obj);
        }


        [TestMethod]
        public void MyTestMethod_Smartputget_Class()
        {

            var key = "aaaaaN";


            var obj = this._cacheWraper.SmartyGetPut(key, System.DateTime.Now.AddSeconds(1000), () => data());
            var getobj = this._cacheState.GetObjectByKey(key.BuildFullKey<datacls>());
            Assert.IsNotNull(getobj);
            Console.WriteLine(obj);

        }


        [TestMethod]
        public void MyTestMethod_smartputgetclassnull()
        {
            var key = "aaaaaNclass";


            var obj = this._cacheWraper.SmartyGetPut<datacls>(key, System.DateTime.Now.AddSeconds(1000), () => null);
            var getobj = this._cacheState.GetObjectByKey(key.BuildFullKey<datacls>());
            Assert.IsTrue(getobj is FactNull);
            Console.WriteLine(obj);

        }

        private datacls data()
        {
            return new datacls
                       {
                           DateTime = System.DateTime.Now
                       };
        }

    }
}