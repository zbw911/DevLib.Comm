using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core.Util
{
    [TestClass]
    public class UnitTestFtpClient
    {
        private Dev.Comm.Utils.FtpUtil client = new Utils.FtpUtil("ftp://192.168.2.8", "zbw", "zbw");
        [TestMethod]
        public void TestMethod1()
        {
            client.ChangeDirectory("/");


            client.CreateDirectory("aaaaa");
            client.CreateDirectory("bbbb");

            client.CreateDirectory("/a/b/c");

            var dir = client.ListDirectory();


            foreach (var ftpDirectoryEntry in dir)
            {
                Console.WriteLine(ftpDirectoryEntry.Name + "=>" + (ftpDirectoryEntry.IsDirectory ? "dir" : "file"));
            }

        }


        [TestMethod]
        public void UploadFile()
        {

            client.UploadFiles("/a/aaa.docx", @"C:\Users\Administrator\Desktop\2013-2014.docx");
        }

        [TestMethod]
        public void UploadFileStream()
        {
            var stream = System.IO.File.OpenRead(@"C:\Users\Administrator\Desktop\2013-2014.docx");

            client.UploadFiles("/a/bbb.docx", stream);
        }
        [TestMethod]
        public void UploadFileBytes()
        {
            using (var stream = System.IO.File.OpenRead(@"C:\Users\Administrator\Desktop\新建文本文档.txt"))
            {

                var bytes = Dev.Comm.Utils.FileUtil.StreamToBytes(stream);

                client.UploadFiles("/a/a.txt", bytes);

                stream.Close();
            }
        }


        [TestMethod]
        public void DownloadFile()
        {
            client.DownloadFile("/a/a.txt", @"C:\Users\Administrator\Desktop\1.txt");
        }



        [TestMethod]
        public void DownloadFileStream()
        {
            var stream = client.DownloadFileStream("/a/a.txt");

            Dev.Comm.Utils.FileUtil.StreamToFile(stream, @"C:\Users\Administrator\Desktop\2.txt");
        }

        [TestMethod]
        public void DownloadFileBytes()
        {
            var stream = client.DownloadFileBytes("/a/a.txt");

            Dev.Comm.Utils.FileUtil.BytesToFile(stream, @"C:\Users\Administrator\Desktop\3.txt");

        }


       







    }
}
