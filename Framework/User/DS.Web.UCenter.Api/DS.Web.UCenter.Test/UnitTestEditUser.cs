using System;
using DS.Web.UCenter.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserCenter.UserCenter;

namespace DS.Web.UCenter.Test
{
    [TestClass]
    public class UnitTestEditUser
    {
        private UcClient client = new UcClient();
        [TestMethod]
        public void TestMethod1()
        {
            var r = client.UserEdit("hello1", "hello1", "hello1", "");

            Console.WriteLine(r);
        }

        [TestMethod]
        public void EditeUserForce()
        {
            var r = client.UserEdit("hello1", "", "hello1", "", true);
        }


        [TestMethod]
        public void LoginAgin()
        {
            var r = client.UserLogin("hello1", "hello1");
            Assert.AreEqual(r.Result, LoginResult.Success);
        }

        [TestMethod]
        public void LoginAfterChangePwd()
        {
            client.UserEdit("hello1", "", "hello2", "", true);

            var r = client.UserLogin("hello1", "hello1");

            Assert.AreEqual(r.Result, LoginResult.PassWordError);

            client.UserEdit("hello1", "", "hello1", "", true);
        }

        [TestMethod]
        public void ChangeQA()
        {
            client.UserEdit("hello1", "", "hello1", "", true, 1, "zbw911");

        }

        [TestMethod]
        public void LoginFailByQA()
        {
            var r = client.UserLogin("hello1", "hello1", LoginMethod.UserName, true, 1, "zbw911_Worng");

            Assert.AreEqual(r.Result, LoginResult.QuestionError);
        }


        [TestMethod]
        public void LoginSucessByQA()
        {
            var r = client.UserLogin("hello1", "hello1", LoginMethod.UserName, true, 1, "zbw911");

            Assert.AreEqual(r.Result, LoginResult.Success);
        }

        [TestMethod]
        public void ChangeQA_Then_Login()
        {
            client.UserEdit("hello1", "", "hello1", "", true, 1, "zbw911_After");

            var r = client.UserLogin("hello1", "hello1", LoginMethod.UserName, true, 1, "zbw911");

            Assert.AreEqual(r.Result, LoginResult.QuestionError);

            r = client.UserLogin("hello1", "hello1", LoginMethod.UserName, true, 1, "zbw911_After");

            Assert.AreEqual(r.Result, LoginResult.Success);
        }


        [TestMethod]
        public void CleanQA()
        {
            client.UserEdit("hello1", "", "hello1", "", true);

            var r = client.UserLogin("hello1", "hello1", LoginMethod.UserName, true, 1, "zbw911");

            Assert.AreEqual(LoginResult.Success, r.Result);
        }


        [TestMethod]
        public void TestLogin()
        {
            var username = "450141191@qq.com";
            var password = "961317";
            client.UserEdit("450141191@qq.com", "", "961317", "", true);

            var r = client.UserLogin("450141191@qq.com", "961317");

            Assert.AreEqual(LoginResult.Success, r.Result);


            UserCenter.UserCenter.UserCenterAPI ui = new UserCenterAPI();
            var t = ui.uc_user_login(username, password);



            foreach (var s in t)
            {
                Console.WriteLine(s);
            }

            Assert.IsTrue(decimal.Parse(t[0]) > 0);
        }


        [TestMethod]
        public void TestLogin2()
        {
            //var username = "450141191@qq.com";
            //var password = "9613171";

            var username = "15369156134";
            var password = "123456";

            UserCenter.UserCenter.UserCenterAPI ui = new UserCenterAPI();

            var result = ui.uc_user_edit(username, "", password, "", 1);

            Assert.IsTrue(result > 0);

            var t = ui.uc_user_login(username, password);

          
            Assert.IsTrue(decimal.Parse(t[0]) > 0);


            var r = client.UserLogin(username, password);

            Assert.AreEqual(LoginResult.Success, r.Result);

        }


    }
}
