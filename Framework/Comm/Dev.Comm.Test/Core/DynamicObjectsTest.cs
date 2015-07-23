using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core
{
    [TestClass]
    public class DynamicObjectsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new[] { 1, 2, 3, 4 };

            var obj = list.Select(x => new
            {
                x
            });

            dynamic d = new DynamicObjects(obj);

            var z = d[0].x;

            Assert.AreEqual(1, z);

        }


        [TestMethod]
        public void GroupbyTest()
        {
            var list = new[] { 100, 2, 3, 4 };

            var obj = list.Select(x => new
            {
                l = new { a = x, b = x, c = 9 }
            }).GroupBy(x => new { x.l.a, x.l.b });

            dynamic d = new DynamicObjects(obj);


            var key = d[0];
            var v = key[0];
            var w = v.l;
            var l = w.a;


        }
        [TestMethod]
        public void TestMethod12()
        {
            var list = new[] { 100, 2, 3, 4 };

            var obj = list.Select(x => new
            {
                l = new { a = x, b = x }
            });

            dynamic d = new DynamicObjects(obj);

            var z = d[0].l.a;

            Assert.AreEqual(100, z);

        }
        [TestMethod]
        public void TestObject()
        {
            var obj = new { a = 1, b = "2" };
            dynamic d = new DynamicObjects(obj);

            var z = d.a;

            Assert.AreEqual(1, z);
        }


        [TestMethod]
        public void TestArray()
        {
            string[] a = new string[] { "a", "b", "c" };

            dynamic d = new DynamicObjects(a);


            Assert.AreEqual("a", d[0]);
        }





        [TestMethod]
        public void TestIDic()
        {
            var dic = new Dictionary<string, object> { { "a", 1 }, { "b", 2 } };

            dynamic d = new DynamicObjects(dic);

            Assert.AreEqual(1, d.a);
        }
    }
}
