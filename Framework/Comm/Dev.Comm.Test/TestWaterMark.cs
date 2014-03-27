using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test
{
    [TestClass]
    public class TestWaterMark
    {
        private string from = @"C:\Users\Administrator\Desktop\animatedsample.gif";
        private string to = @"C:\Users\Administrator\Desktop\animatedsample_Small.gif";
        [TestMethod]
        public void TestMethod1()
        {
            Dev.Comm.ImageHelper.GenThumbnail(from, to, 100, 100);
        }
    }
}
