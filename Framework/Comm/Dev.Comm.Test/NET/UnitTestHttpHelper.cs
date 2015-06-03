using System;
using Dev.Comm.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.NET
{
    [TestClass]
    public class UnitTestHttpHelper
    {
        [TestMethod]
        public void TestGet()
        {
            //newwindow=1&q=1212#newwindow=1&q=1212&btnK=Google+%E6%90%9C%E7%B4%A2
            var respone = HttpHelper.Get(new RequestInfo
                {
                    Url = "http://www.google.com",
                    QueryParams = new System.Collections.Generic.Dictionary<string, string>
                    {
                         {"newwindow","1"},
                         {"q","112121"}
                    }
                });


            Console.WriteLine(respone.ResponeText);
            Console.WriteLine(respone.Outcookieheader);

        }
    }
}
