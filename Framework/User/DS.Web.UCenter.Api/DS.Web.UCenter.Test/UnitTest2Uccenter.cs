using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserCenter.UserCenter;

namespace DS.Web.UCenter.Test
{
    [TestClass]
    public class UnitTest2Uccenter
    {
        [TestMethod]
        public void TestMethod1()
        {
            var username = "340209080@qq.com";
            var password = "12345678";
            UserCenter.UserCenter.UserCenterAPI ui = new UserCenterAPI();
            var r = ui.uc_user_login(username, password);


            foreach (var s in r)
            {
                Console.WriteLine(s);
            }

        }


        [TestMethod]
        public void MyTestMethod()
        {
            
        }






    }
}
