// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：开关性能.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.DBUtility.Test
{
    /// <summary>
    /// UnitTest1 的摘要说明
    /// </summary>
    [TestClass]
    public class 开关性能
    {
        public 开关性能()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///获取或设置测试上下文，该上下文提供
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
        // 编写测试时，可以使用以下附加特性:
        //
        // 在运行类中的第一个测试之前使用 ClassInitialize 运行代码
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在类中的所有测试都已运行之后使用 ClassCleanup 运行代码
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在运行每个测试之前，使用 TestInitialize 来运行代码
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在每个测试运行完之后，使用 TestCleanup 来运行代码
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void keepOpen()
        {
            //var dao = DataAccessFactory.CreateDataAccess().KeepOpen();

            //for (var i = 0; i < 10000; i++)
            //{
            //    dao.ExecuteNonQuery("select * from mytable");


            //}

            //dao.Close();

            //通过	keepOpen	Dev.DBUtility.Test	00:00:01.7784809		

        }

        [TestMethod]
        public void UnkeepOpen()
        {
            //var dao = DataAccessFactory.CreateDataAccess().KeepOpen(false);

            //for (var i = 0; i < 10000; i++)
            //{
            //    dao.ExecuteNonQuery("select * from mytable");


            //}

            //dao.Close();

            //通过	UnkeepOpen	Dev.DBUtility.Test	00:00:07.5477701		

        }

        [TestMethod]
        public void InUsingUnKeep()
        {
            //using (var dao = DataAccessFactory.CreateDataAccess())
            //{

            //    for (var i = 0; i < 10000; i++)
            //    {
            //        dao.ExecuteNonQuery("select * from mytable");
            //    }
            //}

            //通过	InUsing	Dev.DBUtility.Test	00:00:07.3291380				

        }

        [TestMethod]
        public void InUsingKeep()
        {
            //using (var dao = DataAccessFactory.CreateDataAccess().KeepOpen())
            //{

            //    for (var i = 0; i < 10000; i++)
            //    {
            //        dao.ExecuteNonQuery("select * from mytable");
            //    }
            //}

            //通过	InUsingKeep	Dev.DBUtility.Test	00:00:01.8051868					

        }

        [TestMethod]
        public void InTrans()
        {
            //using (var dao = DataAccessFactory.CreateDataAccess().BeginTransaction())
            //{

            //    for (var i = 0; i < 10000; i++)
            //    {
            //        dao.ExecuteNonQuery("select * from mytable");
            //    }
            //}
            // 这个速度快，只是说明这是一次性连接，但实际上更重要的是使用了一个会话，一个连接而已
            //通过	InTrans	Dev.DBUtility.Test	00:00:01.7789237					

        }
    }
}
