using System;
using Dev.Comm.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string aa = "aaa{你好";

            var bb = Dev.Comm.Utils.MockUrlCode.UrlEncode(aa);

            //var xx = 
 

            Console.WriteLine(bb);

            var cc = Dev.Comm.Utils.MockUrlCode.UrlDecode(bb);

            Console.WriteLine(cc);

            Assert.AreEqual(aa, cc);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            //转换。
            string ss = System.Uri.EscapeDataString("$%^&*(中文)!@#$%$#Test");
            //转回来
            string sss = System.Uri.UnescapeDataString(ss);
            //显示转换后的字串
            Console.WriteLine(ss);
            //显示转回来的字串
            Console.WriteLine(sss);

        }
    }
}
