using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Cache.Test
{
    [TestClass]
    public class UnitTestDataType
    {
        [TestMethod]
        public void TestMethod1()
        {
            var i = default(int);

            if (i.Equals(default(int)))
            {
                Console.WriteLine("=");
            }
            else
            {
                Console.WriteLine("!=");
            }
        }


        [TestMethod]
        public void MyTestMethod()
        {
            Console.WriteLine(Eq(""));
        }


        [TestMethod]
        public void MyTestMethodType()
        {
            var type = typeof(string);
            Assert.IsTrue(type.IsClass);

            type = typeof(DateTime);
            Assert.IsFalse(type.IsClass);
        }


        [TestMethod]
        public void MyTestMethodTypes()
        {
            var types = new[] { typeof(int), typeof(DateTime), typeof(decimal), typeof(string), };

            foreach (var type in types)
            {
                Console.WriteLine(type.IsValueType);
            }
        }



        public bool Eq<T>(T a) where T : class
        {
            return a.Equals(default(T));
        }
    }
}
