using System;
using Dev.Comm.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Dev.Comm.Test
{
    [TestClass]
    public class MyTestClass
    {
        [TestMethod]
        public void MyTestMethod()
        {
            
            //var matches = Dev.Comm.RegexHelper.MatchesGroupsString(context, pattern, 0);

            //foreach (var match in matches)
            //{
            //    Console.WriteLine(match);
            //}
var context = "<img src=http://www.google.com/aaaaa/image_1 /><img src=\"http://www.google.com/aaaaa/image_1\" /><img src='http://www.google.com/aaaaa/image_1' /><img src='aaaaa' /><img src='aaaaa' />";
            var pattern = "<img[^>]*src\\s*=\\s*['\"]?([\\w/\\-\\.\\:_]*)['\"]?[^>]*";
            var matches = Dev.Comm.RegexHelper.MatchesGroupsString(context, pattern, 1);

            //var local = matches.Where(x => Dev.Comm.Web.UrlHelper.IsUrlLocalToHost(x));
            foreach (var match in matches)
            {
                Console.WriteLine(match);
            }


        }
    }
}