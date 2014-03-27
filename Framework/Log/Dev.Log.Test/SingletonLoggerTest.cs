// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：SingletonLoggerTest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;
using Dev.Log;
using Dev.Log.Config;
using Dev.Log.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;
using Dev.Comm;

namespace Dev.Log.Test
{


    /// <summary>
    ///这是 SingletonLoggerTest 的测试类，旨在
    ///包含所有 SingletonLoggerTest 单元测试
    ///</summary>
    [TestClass()]
    public class SingletonLoggerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，上下文提供
        ///有关当前测试运行及其功能的信息。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 附加测试特性
        // 
        //编写测试时，还可使用以下特性:
        //
        //使用 ClassInitialize 在运行类中的第一个测试前先运行代码
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //使用 ClassCleanup 在运行完类中的所有测试后再运行代码
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //使用 TestInitialize 在运行每个测试前先运行代码
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //使用 TestCleanup 在运行完每个测试后运行代码
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion




        [TestMethod()]
        public void LogSeverity_convert_Test()
        {
            // 
            Dev.Log.LogSeverity v;
            if (Enum.TryParse<Dev.Log.LogSeverity>("debug", true, out v))
            {
                //
                Console.WriteLine(v.ToString());
            }
            else
            {
                Console.WriteLine("faild" + v.ToString());
            }
        }

        [TestMethod()]
        public void LogTest2()
        {

            //  Dev.Log.Loger.Error("this is the error");
            Setting.AttachLog(new ObserverLogToLog4net());

            Dev.Log.Loger.Error("aaaaaaa");

        }

        [TestMethod]
        public void MyTestMethod()
        {
            Setting.AttachLog(new Dev.Log.Impl.ObserverLogToLog4net());
            Dev.Log.Loger.Error("aaaaaaaaa");
        }


        [TestMethod]
        public void MyTestMethod_Auto()
        {
            Dev.Log.Loger.Error("aaaaaaaaa");
        }


        [TestMethod]
        public void MyTestMethod_Config()
        {
            new XMLConfig().InitConfig();
        }



        [TestMethod]
        public void MyTestMethod_IsRooted()
        {
            string log4NetConfig = @"c:\windows\aasdf.txt";
            var root = Path.IsPathRooted(log4NetConfig);

            Assert.IsTrue(root);
        }


        [TestMethod]
        public void MyTestMethod_IsNotRooted()
        {
            string log4NetConfig = @"aasdf.txt";
            var root = Path.IsPathRooted(log4NetConfig);

            Assert.IsFalse(root);
        }


        [TestMethod]
        public void MyTestMethod_FindFile()
        {
            var file = Directory.GetFiles(@"C:\Users\Administrator\Source\Repos\Dev.All\DevLibs\Framework\Log\Dev.Log.Test\", @"Log.config", SearchOption.AllDirectories);

            Assert.IsTrue(file.Length > 0);
        }

    }
}
