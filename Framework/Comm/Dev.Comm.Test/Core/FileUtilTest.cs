using System;
using Dev.Comm.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core
{
    [TestClass]
    public class FileUtilTest
    {
        [TestMethod]
        public void TestMethod1()
        {
        }

        [TestMethod]
        public void Md5File()
        {
            var filepath = @"E:\AutoDesk\Revit\Autodesk Revit2014完美完整包（族库、样板、注册机、序列号密、匙、安装视频及说明）.zip";
            var key1 = FileUtil.Md5File(filepath);
            var key2 = FileUtil.Md5Stream(filepath);

            Console.WriteLine(key1);

            Assert.AreEqual(key1, key2);
        }

        [TestMethod]
        public void Md5File2()
        {
            var filepath = @"C:\Users\Administrator\Desktop\changelog.txt";
            var filepath2 = @"C:\Users\Administrator\Desktop\changelog.txt";

            var key1 = FileUtil.Md5File(filepath);
            var key2 = FileUtil.Md5Stream(filepath2);

            Console.WriteLine(key1);

            Assert.AreEqual(key1, key2);
        }


        [TestMethod]
        public void MyTestMethod()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();

            var filepath = @"E:\AutoDesk\Revit\Autodesk Revit2014完美完整包（族库、样板、注册机、序列号密、匙、安装视频及说明）.zip";
            var key1 = FileUtil.Md5File(filepath);
            //var key2 = FileUtil.MD5Stream(filepath);
            sw.Stop();

            Console.WriteLine(sw.ElapsedTicks);


        }

        [TestMethod]
        public void MyTestMethod2()
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            var filepath = @"E:\AutoDesk\Revit\Autodesk Revit2014完美完整包（族库、样板、注册机、序列号密、匙、安装视频及说明）.zip";
            //var key1 = FileUtil.MD5File(filepath);
            var key2 = FileUtil.Md5Stream(filepath);

            sw.Stop();

            Console.WriteLine(sw.ElapsedTicks);


        }
    }
}
