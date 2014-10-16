using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Log.Test
{
    [TestClass]
    public class UnitTestLog4Net
    {
        [TestMethod]
        public void TestMethod1()
        {
            //由于文件会自动加配置文件，所以会自动附加两个输出，这点要注意
             // 已经自动加载了 Log.config中的配置。
            //Dev.Log.Config.Setting.SetLogSeverity(LogSeverity.Debug);
            Dev.Log.Config.Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());

            Dev.Log.Loger.Debug("debug:dddd");
            Dev.Log.Loger.Error("error:dddd");
        }
    }
}