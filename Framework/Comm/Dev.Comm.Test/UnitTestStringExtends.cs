using System;
using Dev.Comm.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test
{
    [TestClass]
    public class UnitTestStringExtends
    {
        [TestMethod]
        public void TestMethod1()
        {
            string a = "1";

            var i = a.AS<int>();

            Assert.AreEqual(1, i);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            string a = "1";

            var i = a.AsInt();

            var j = a.AS<int>();

            Assert.AreEqual(i, j);

        }
    }
}
