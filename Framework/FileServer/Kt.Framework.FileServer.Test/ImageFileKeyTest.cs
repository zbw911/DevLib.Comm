// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ImageFileKeyTest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Kt.Framework.FileServer.Test
{


    /// <summary>
    ///这是 ImageFileKeyTest 的测试类，旨在
    ///包含所有 ImageFileKeyTest 单元测试
    ///</summary>
    [TestClass()]
    public class ImageFileKeyTest
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
        ///GetFileAbsPath 的测试
        ///</summary>
        [TestMethod()]
        public void GetFileAbsPathTest()
        {
            //ReadConfig config = new ReadConfig();

            //ShareFileKey target = new ShareFileKey(); // TODO: 初始化为适当的值
            //string fileKey = "2-2011-04-26-adf96d2c6be8dfade2a049f1ee4b8d7d.jpg"; // TODO: 初始化为适当的值
            //object[] param = null; // TODO: 初始化为适当的值
            //string expected = string.Empty; // TODO: 初始化为适当的值

            //var actual = target.GetFileSavePath(fileKey);

            //Assert.AreEqual(".jpg", actual.extname);

            //Assert.AreEqual(2, actual.FileServer.id);

            //Assert.AreEqual("adf96d2c6be8dfade2a049f1ee4b8d7d.jpg", actual.savefilename);
            //Assert.AreEqual(@"2011\04\26\ad\f\9", actual.dirname);
        }


        
    }
}
