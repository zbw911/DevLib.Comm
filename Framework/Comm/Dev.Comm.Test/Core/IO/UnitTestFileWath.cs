using System;
using System.Threading;
using Dev.Comm.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core.IO
{
    [TestClass]
    public class UnitTestFileWath
    {
        [TestMethod]
        public void TestMethod1()
        {
            var file = @"C:\Users\Administrator\Desktop\aaaa.txt";
            Console.Write("asdfasdf");
            //Console.ReadKey();

            FileWatcher fw = new FileWatcher(file);
            fw.OnChanged = (e, o) =>
                                {
                                    Console.WriteLine("file changed");
                                };


            Thread.Sleep(20000);

        }

        [TestMethod]
        public void MyTestMethodFileContentWatcher()
        {
            var file = @"C:\Users\Administrator\Desktop\aaaa.txt";
            //first read
            var content = FileContentWatcher.GetFileCurrentContent(file);
            Console.WriteLine(content);
            //to modify file
            Thread.Sleep(20 * 1000);

            Console.WriteLine(FileContentWatcher.GetFileCurrentContent(file));

        }
    }
}
