// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：QueryParameterCollectionTest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using Dev.DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace dev.DBUtility.Test
{


    /// <summary>
    ///这是 QueryParameterCollectionTest 的测试类，旨在
    ///包含所有 QueryParameterCollectionTest 单元测试
    ///</summary>
    [TestClass()]
    public class QueryParameterCollectionTest
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

         QueryParameterCollection target = new QueryParameterCollection() { new QueryParameter { ParameterName = "zbw", Value = 1 },
             new QueryParameter { ParameterName = "zbw1", Value =2 },
             new QueryParameter { ParameterName = "zbw2", Value = 3 }};

        /// <summary>
        ///Clear 的测试
        ///</summary>
        [TestMethod()]
        public void CountTest()
        {
            Assert.AreEqual(3, target.Count);
            //target.Clear();
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }
           [TestMethod()]
        public void ValueTest()
        {

            Assert.AreEqual(1, target[0].Value);
            Assert.AreEqual(2, target[1].Value);
            Assert.AreEqual(3, target[2].Value);
            //target.Clear();
            //Assert.Inconclusive("无法验证不返回值的方法。");
        }

           [TestMethod()]
           public void NameValueTest()
           {

               Assert.AreEqual(1, target["zbw"].Value);
               Assert.AreEqual(2, target["zbw1"].Value);
               //Assert.AreEqual(3, target["zbw"].Value);
               //target.Clear();
               //Assert.Inconclusive("无法验证不返回值的方法。");
           }

    }
}
