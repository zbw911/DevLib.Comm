// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DBLogTest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using Dev.DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data;

namespace Dev.DBUtility.Test
{


    /// <summary>
    ///这是 DBLogTest 的测试类，旨在
    ///包含所有 DBLogTest 单元测试
    ///</summary>
    [TestClass()]
    public class DBLogTest
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


        /// <summary>
        ///AddLog 的测试
        ///</summary>
        [TestMethod()]
        public void AddLogTest()
        {

            for (var i = 0; i < 10000; i++)
            {
                // Dev.Log.Loger.Severity = Dev.Log.LogSeverity.Debug;
                //Dev.Log.SingletonLogger.Instance.Attach(new Dev.Log.ObserverLogToConsole());
                string method = string.Empty; // TODO: 初始化为适当的值
                var cmdType = CommandType.Text; // TODO: 初始化为适当的值
                string cmdText = "select * from datab1ase"; // TODO: 初始化为适当的值
                var cmdParms = new QueryParameterCollection(); // TODO: 初始化为适当的值


                cmdParms.Add("@asdf", "asdfasdf");
               cmdParms.Add("@bbb","aaaaaa-1");

                
            }

        }



    }
}
