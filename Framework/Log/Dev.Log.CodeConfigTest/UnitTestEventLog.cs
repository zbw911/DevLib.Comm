using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Log.CodeConfigTest
{
    [TestClass]
    public class UnitTestEventLog
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToEventlog());

            Dev.Log.Loger.Error("afsdfasdf");
        }
    }
}
