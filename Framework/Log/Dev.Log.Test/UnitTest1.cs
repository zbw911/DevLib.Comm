using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Log.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Dev.Log.Config.Setting.SetLogSeverity(LogSeverity.Debug);
            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToFile("./aaaa.txt"));

            Dev.Log.Loger.Debug("debug:dddd");
            Dev.Log.Loger.Error("error:dddd");
        }
    }
}
