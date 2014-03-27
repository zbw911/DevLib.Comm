using System;
using System.IO;
using Dev.Framework.FileServer.Config;
using Dev.Framework.FileServer.LocalUploaderFileImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.FileServer.Test
{
    [TestClass]
    public class UnitTestUrlToKey
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReadConfig config = new ReadConfig();

            IKey key = new ShareImpl.ShareFileKey();

            var args = new object[] { "-", "11", "_", "99" };

            var fkey = key.CreateFileKey("zbw911.aspx", "-", "11", "_", "99");

            var url = key.GetFileUrl(fkey, "-", "11", "_", "99");



            var inverseKey = key.GetFileKeyFromFileUrl(url);


            Assert.AreEqual(fkey.Substring(1), inverseKey.Substring(1));
        }

        private static void TestValue(IKey key, string fkey)
        {
            var url = key.GetFileUrl(fkey);


            var inverseKey = key.GetFileKeyFromFileUrl(url);
            //Assert.AreEqual(fkey, inverseKey);
            Assert.AreEqual(fkey.Substring(1), inverseKey.Substring(1));

            Assert.AreEqual(key.GetFileUrl(fkey), key.GetFileUrl(inverseKey));
        }

        [TestMethod]
        public void TestLocalKey()
        {

            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            //var fkey = key.CreateFileKey("filename.cs");
            var fkey = "1-2013-12-02-0a499429af636838f06bbc2af31b65e8.cs";// key.CreateFileKey("filename.cs");


            TestValue(key, fkey);




        }


        [TestMethod]
        public void CreateNoExtKey()
        {
            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            //var fkey = key.CreateFileKey("filename.cs");
            var fkey = "1-2013-12-02-0a499429af636838f06bbc2af31b65e8";// key.CreateFileKey("filename.cs");

            var strkey = key.CreateFileKey(fkey);
            Console.WriteLine(strkey);
        }
    }
}
