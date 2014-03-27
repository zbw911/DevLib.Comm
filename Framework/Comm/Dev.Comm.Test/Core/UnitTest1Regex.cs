using System;
using System.Text.RegularExpressions;
using Dev.Comm.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core
{
    [TestClass]
    public class UnitTest1Regex
    {
        [TestMethod]
        public void TestMethod1()
        {
            var mat =
                @"\[url(=((https?|ftp|gopher|news|telnet|rtsp|mms|callto|bctp|ed2k|thunder|synacast){1}:\/\/|www\.)([^\[\""']+?))?\](.+?)\[\/url\]";
            string message = "[url=http://www.google.com]22222[/url]";
            var ms = Dev.Comm.RegexHelper.MatchesGroups(message, mat, 0);

            Assert.IsTrue(ms.Count > 0);

            for (int i = 0; i < ms.Count; i++)
            {
                Console.WriteLine(i + "=>" + ms[i]);
            }
        }


        [TestMethod]
        public void TestBat()
        {
            var mat =
              @"\[url(=((https?|ftp|gopher|news|telnet|rtsp|mms|callto|bctp|ed2k|thunder|synacast){1}:\/\/|www\.)([^\[\""']+?))?\](.+?)\[\/url\]";
            string message = "[url=http://www.google.com]22222[/url][url=http://www.aaaaa.com]aaaaaaaa[/url]";
            var ms = Dev.Comm.RegexHelper.Matches(message, mat);

            Assert.IsTrue(ms.Count > 0);

            for (int i = 0; i < ms.Count; i++)
            {
                var s = ms[i].Success;
                if (!s)
                    continue;
                var group = ms[i].Groups;

                for (int j = 0; j < group.Count; j++)
                {
                    Console.WriteLine(group[j]);
                }
            }
        }


        [TestMethod]
        public void TestBatRep()
        {
            var mat =
              @"\[url(=((https?|ftp|gopher|news|telnet|rtsp|mms|callto|bctp|ed2k|thunder|synacast){1}:\/\/|www\.)([^\[\""']+?))?\](.+?)\[\/url\]";
            string message = "fff[url=http://www.google.com]22222[/url]asdfasdfasdf[url=http://www.aaaaa.com]aaaaaaaa[/url]sdfasdfasdf";



            var s = Dev.Comm.RegexHelper.MatchAndHandler(message, mat, (x, y) =>
                                 {
                                     return x.Replace(y[0].Value, string.Format("<a href={0}{1}>{2}</a>", y[2], y[4], y[5]));
                                 });

            Console.WriteLine(s);

        }

        [TestMethod]
        public void TestMail()
        {
            var mat = @"\[email(=([a-z0-9\-_.+]+)@([a-z0-9\-_]+[.][a-z0-9\-_.]+))?\](.+?)\[\/email\]";
            string message = "[email=zbw911@qq.com]zbw911name[/email]fff[url=http://www.google.com]22222[/url]asdfasdfasdf[url=http://www.aaaaa.com]aaaaaaaa[/url]sdfasdfasdf";



            var s = Dev.Comm.RegexHelper.MatchAndHandler(message, mat, (x, y) =>
                                 {
                                     for (int j = 0; j < y.Count; j++)
                                     {
                                         Console.WriteLine(y[j]);
                                     }


                                     return x.Replace(y[0].Value, string.Format("<a mailto=\"{0}@{1}\">{2}</a>", y[2], y[3], y[4]));
                                 });

            Console.WriteLine(s);

        }


        [TestMethod]
        public void testCode()
        {
            var message = "[color=red]aaa[/color] adsasdf[size=9999]sizeaa[/size]";
            message = Dev.Comm.RegexHelper.PregReplace(message, new[] {   
                       @"\[color=([#\w]+?)\]",
                   @"\[size=(\d+?)\]",
                   @"\[size=(\d+(\.\d+)?(px|pt|in|cm|mm|pc|em|ex|%)+?)\]",
                   @"\[font=([^\[\<]+?)\]",
                   @"\[align=(left|center|right)\]",
                   @"\[float=(left|right)\]"},
               new[] {   
                       "<font color=\"$1\">",
                   "<font size=\"$1\">",
                   "<font style=\"font-size: $1\">",
                   "<font face=\"$1 \">",
                   "<p align=\"$1\">",
                   "<span style=\"float: $1;\">" });

            Console.WriteLine(message);

        }

        [TestMethod]
        public void MyTestMethod()
        {
            var message = "[color=red]aaa[/color] adsasdf[size=9999]sizeaa[/size]";
            var mat = @"\[color=([#\w]+?)\]";

            var rep = "<font color=\"$1\">";

            message = Dev.Comm.RegexHelper.PregReplace(message, mat, rep);

            Console.WriteLine(message);
        }

        [TestMethod]
        public void Quteses()
        {
            string message = "asdf[quote]ccccccccccccccccccccccccccccc[/quote]";
            if (StringUtil.strpos(message, "[/quote]") >= 0)
            {
                message = Dev.Comm.RegexHelper.PregReplace(message, @"\s*\[quote\][\n\r]*(.+?)[\n\r]*\[\/quote\]\s*", this.tpl_quote());
            }

            Console.WriteLine(message);
        }

        private string tpl_quote()
        {
            return "<div class=\"quote\"><blockquote>$1</blockquote></div>";
        }


        [TestMethod]
        public void TestImage()
        {
            var mats = new[] { @"\[img\]\s*([^\[\<\r\n]+?)\s*\[\/img\]",
                            @"\[img=(\d{1,4})[x|\,](\d{1,4})\]\s*([^\[\<\r\n]+?)\s*\[\/img\]"};


            //var message = "[img]http://www.google.com[/img]";

            var message = " [img=100,1]http://wwwasdfasdf.com[/img] asdfasdf";

            message = Dev.Comm.RegexHelper.MatchAndHandler(message, mats[0], (x, y) =>
            {
                return x.Replace(y[0].Value,
                                 string.Format(
                                     "<img src=\"{0}\" border=\"0\" alt=\"\" />",
                                     y[1]));

            });

            message = Dev.Comm.RegexHelper.MatchAndHandler(message, mats[1], (x, y) =>
            {
                return x.Replace(y[0].Value,
                                 string.Format(
                                     "<img width=\"{0}\" height=\"{1}\" src=\"{2}\" border=\"0\" alt=\"\" />",
                                     y[1], y[2], y[3]));

            });

            Console.WriteLine(message);
        }



    }
}
