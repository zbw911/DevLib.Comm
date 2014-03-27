// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CheckSQLTest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using Dev.DBUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Dev.DBUtility.Test
{


    /// <summary>
    ///这是 CheckSQLTest 的测试类，旨在
    ///包含所有 CheckSQLTest 单元测试
    ///</summary>
    [TestClass()]
    public class CheckSQLTest
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
        ///CheckSQLText 的测试
        ///</summary>
        [TestMethod()]
        public void CheckSQLTextTest_indexOfMach()
        {
            //通过	CheckSQLTextTest_indexOfMach	Dev.DBUtility.Test	00:00:10.9211235		
            string sql = "select * from zbw911 and where 1=1"; // TODO: 初始化为适当的值
            string notallowstr = string.Empty; // TODO: 初始化为适当的值
            string notallowstrExpected = string.Empty; // TODO: 初始化为适当的值
            int expected = 1; // TODO: 初始化为适当的值
            int actual = 0;



            for (int i = 0; i < 100000; i++)
            {
                actual = CheckSQL.CheckSQLText(sql, out notallowstr);
            }
            Assert.AreEqual(notallowstrExpected, notallowstr);
            Assert.AreEqual(expected, actual);

        }



        string strSQL = @"select * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sdselect * from table where aaaabb sd
select * from table where aaaabb sd";

        /// <summary>
        ///CheckSQLText 的测试
        ///</summary>
        [TestMethod()]
        public void CheckSQLTextTest_indexOfMach_bigStr()
        {
            //通过	CheckSQLTextTest_indexOfMach_bigStr	Dev.DBUtility.Test	00:00:07.1101321			
            string sql = "select * from zbw911 and where 1=1"; // TODO: 初始化为适当的值
            string notallowstr = string.Empty; // TODO: 初始化为适当的值
            string notallowstrExpected = string.Empty; // TODO: 初始化为适当的值
            int expected = 1; // TODO: 初始化为适当的值
            int actual = 0;



            for (int i = 0; i < 1000; i++)
            {
                actual = CheckSQL.CheckSQLText(strSQL, out notallowstr);
            }
            Assert.AreEqual(notallowstrExpected, notallowstr);
            Assert.AreEqual(expected, actual);

        }


        /// <summary>
        ///CheckSQLText 的测试
        ///</summary>
        [TestMethod()]
        public void CheckSQLTextTest_注入()
        {
            //通过	CheckSQLTextTest_indexOfMach_bigStr	Dev.DBUtility.Test	00:00:07.1101321			
            string sql = "select * from zbw911 and where 1=1 '   "; // TODO: 初始化为适当的值
            string notallowstr = string.Empty; // TODO: 初始化为适当的值
            string notallowstrExpected = "'"; // TODO: 初始化为适当的值
            int expected = -1; // TODO: 初始化为适当的值
            int actual = 0;


            Assert.IsTrue(sql.IndexOf("'") > 0);



            actual = CheckSQL.CheckSQLText(sql, out notallowstr);

            Assert.AreEqual(notallowstrExpected, notallowstr);
            Assert.AreEqual(expected, actual);

        }








        /// <summary>
        ///CheckSQLText 的测试
        ///</summary>
        [TestMethod()]
        public void CheckSQLTextTest_params_null()
        {
            QueryParameterCollection commandParameters = null; // TODO: 初始化为适当的值
            string notallow = string.Empty; // TODO: 初始化为适当的值
            string notallowExpected = string.Empty; // TODO: 初始化为适当的值
            int expected = 1; // TODO: 初始化为适当的值
            int actual;
            actual = CheckSQL.CheckSQLText(commandParameters, out notallow);
            Assert.AreEqual(notallowExpected, notallow);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }


        /// <summary>
        ///CheckSQLText 的测试
        ///</summary>
        [TestMethod()]
        public void CheckSQLTextTest_params_有注入()
        {
            var commandParameters = new QueryParameterCollection { new QueryParameter { Value = "'" } }; // TODO: 初始化为适当的值
            string notallow = string.Empty; // TODO: 初始化为适当的值
            string notallowExpected = "'"; // TODO: 初始化为适当的值
            int expected = -1; // TODO: 初始化为适当的值
            int actual;
            actual = CheckSQL.CheckSQLText(commandParameters, out notallow);
            Assert.AreEqual(notallowExpected, notallow);
            Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }

        [TestMethod()]
        public void CheckSQLTextTest_params_to_Array()
        {
            var commandParameters = new QueryParameterCollection { new QueryParameter { Value = "'" } }; // TODO: 初始化为适当的值

            var casted = commandParameters.Cast<QueryParameter>();

            foreach (var item in casted)
            {
                var s = item.Value.ToString();
            }
            //Assert.Inconclusive("验证此测试方法的正确性。");
        }
    }
}
