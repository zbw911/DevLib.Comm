using System;
using System.Collections.Generic;
using System.Linq;
using Dev.Comm.DataStructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core.DataStructure
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class MyTestClass
    {
        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void MyTestMethod()
        {
            var array = new[] { "A" };
            CreateSequence cs = new CreateSequence(5, array);

            var result = cs.BeginCreate();


            System.Console.WriteLine(result.Count());

            foreach (var str in result)
            {
                System.Console.WriteLine(str);
            }

            Assert.AreEqual(result.Count(), (int)Math.Pow(array.Length, 5));


        }

        [Microsoft.VisualStudio.TestTools.UnitTesting.TestMethod]
        public void MyTestMethod2()
        {
            var array = new[] { "A", "B" };
            CreateSequence cs = new CreateSequence(4, array);

            var result = cs.BeginCreate();


            System.Console.WriteLine(result.Count());

            foreach (var str in result)
            {
                System.Console.WriteLine(str);
            }

            Assert.AreEqual(result.Count(), (int)Math.Pow(array.Length, 4));


        }
    }


}
