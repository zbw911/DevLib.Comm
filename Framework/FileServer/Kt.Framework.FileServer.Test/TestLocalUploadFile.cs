using System;
using System.IO;
using Dev.Framework.FileServer.Config;
using Dev.Framework.FileServer.LocalUploaderFileImpl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.FileServer.Test
{
    [TestClass]
    public class TestLocalUploadFile
    {
        [TestMethod]
        public void TestMethod1()
        {
            var filepath =
                @"E:\Github\zbw911\Dev.All\DevLibs\Framework\FileServer\Kt.Framework.FileServer.Test\TestLocalUploadFile.cs";
            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            IUploadFile upload = new LocalUploadFile(key);

            var filekey = "1-2013-12-02-05e89032842c22cc4cb13f07e1173333.cs";
            //var filekey = key.CreateFileKey(filepath);


            Console.WriteLine(filekey);


            var s = key.GetFileSavePath(filekey);


            Console.WriteLine(s);


            Console.WriteLine(Dev.Comm.JsonConvert.ToJsonStr(s));


            var uploadedkey = upload.SaveFile(File.OpenRead(filepath), filekey);

        }

        [TestMethod]
        public void TestFileUrl()
        {
            var skey = "1-2013-12-02-05e89032842c22cc4cb13f07e1173333.cs";
            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            IUploadFile upload = new LocalUploadFile(key);

            var url = key.GetFileUrl(skey);

        }


        [TestMethod]
        public void TestDeleteFile()
        {

            var skey = "1-2013-12-02-05e89032842c22cc4cb13f07e1173333.cs";
            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            IUploadFile upload = new LocalUploadFile(key);

            upload.DeleteFile(skey);
        }


        [TestMethod]
        public void TestDeletePath()
        {
            var skey = "1-2013-12-02-05e89032842c22cc4cb13f07e1173333.cs";
            var x = new ReadConfig("TestLocalUploadFile.config");
            IKey key = new LocalFileKey();
            IUploadFile upload = new LocalUploadFile(key);

            upload.DeltePath(skey);
        }


        [TestMethod]
        public void MyTestMethod()
        {
            var x = System.AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
