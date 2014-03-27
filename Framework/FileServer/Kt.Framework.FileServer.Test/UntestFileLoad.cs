using System;
using System.IO;
using Dev.Framework.FileServer.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.FileServer.Test
{
    [TestClass]
    public class UntestFileLoad
    {
        [TestMethod]
        public void TestMethod1()
        {
            ReadConfig config = new ReadConfig();

            IKey key = new ShareImpl.ShareFileKey();

            IUploadFile uploadfile = new ShareImpl.ShareUploadFile(key);

            //var fkey = key.CreateFileKey("filename.cs");
            var fkey = "1-2013-12-02-0a499429af636838f06bbc2af31b65e8.cs";// key.CreateFileKey("filename.cs");
            var filepath =
                @"E:\Github\zbw911\Dev.All\DevLibs\Framework\FileServer\Kt.Framework.FileServer.Test\TestLocalUploadFile.cs";
            var retkey = uploadfile.UpdateFile(File.OpenRead(filepath), fkey);
        }

        [TestMethod]
        public void DelteTest()
        {
            var skey = "1-2013-12-02-0a499429af636838f06bbc2af31b65e8.cs";

            ReadConfig config = new ReadConfig();

            IKey key = new ShareImpl.ShareFileKey();

            IUploadFile uploadfile = new ShareImpl.ShareUploadFile(key);

            uploadfile.DeleteFile(skey);
        }


        [TestMethod]
        public void DeleteDir()
        {
            var skey = "1-2013-12-02-0a499429af636838f06bbc2af31b65e8.cs";

            ReadConfig config = new ReadConfig();

            IKey key = new ShareImpl.ShareFileKey();

            IUploadFile uploadfile = new ShareImpl.ShareUploadFile(key);


            uploadfile.DeltePath(skey);
        }


        //
    }
}
