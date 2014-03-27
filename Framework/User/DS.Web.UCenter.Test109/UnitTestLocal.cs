using System;
using DS.Web.UCenter.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DS.Web.UCenter.Test
{
    [TestClass]
    public partial class UnitTestLocal
    {
        private UcClient client = new UcClient();

        //string email = "hello@hello.com";
        //string username = "hello";
        //string password = "hello";



        private int questionid = 1;
        private string answer = "aaaaaa";
        [TestMethod]
        public void TestMethod1()
        {
            username = "12121@qq.com";
            password = "12345678";
            var r = client.UserLogin(username, password);
            Assert.IsTrue(r.Result == LoginResult.Success);

        }

        [TestMethod]
        public void testSendMessage()
        {

            var send = client.PmSend(1, 0, "aaaaaaaaa", "aaaaaaafsdf", "hello");

            Console.WriteLine(send.Result);

            Assert.IsTrue(send.Result == PmSendResult.Success);
        }



        [TestMethod]
        public void Register()
        {

            var check = client.UserCheckEmail(email);

            if (check.Result != UserCheckEmailResult.Success)
            {
                Console.WriteLine("Exist ");
                return;

            }


            var userinfo = client.UserInfo(username);
            if (userinfo.Uid > 0)
            {
                Console.WriteLine("exist username");
                return;
            }

            var result = client.UserRegister(username, password, email, questionid, answer);

            Assert.IsTrue(result.Result == RegisterResult.Success);
        }

        [TestMethod]
        public void GetPostMessage()
        {
            var user = client.UserInfo(username);
            if (user.Uid == 0)
                throw new NullReferenceException();

            var news = client.PmCheckNew(user.Uid);



        }


        [TestMethod]
        public void TestOnLs()
        {
            var news = client.PmList(Uid, 1, 15, PmReadFolder.OutBox, PmReadFilter.NewPm, 15);

        }


        private decimal Uid
        {
            get
            {
                var user = client.UserInfo(username);
                if (user.Uid == 0)
                    throw new NullReferenceException();

                return user.Uid;
            }
        }


        [TestMethod]
        public void UserNotExist()
        {
            var user = client.UserInfo("llllllllllllll");
            Assert.IsTrue(user.Uid == 0);
        }


        [TestMethod]
        public void testdeleteMsg()
        {
            //client.PmDelete()
        }



    }
}
