using System;
using Dev.Comm.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.WEB
{
    [TestClass]
    public class UnitTestUrlHelper
    {
        [TestMethod]
        public void TestMethod1()
        {
            var url = "http://www.google.com/?a=1&b=2";
            var a = UrlHelper.GetParam(url, "a");

            Assert.AreEqual(a, "1");

            var c = UrlHelper.GetParam(url, "c");

            Assert.AreEqual(c, null);

        }
    }
}
