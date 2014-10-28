using System;
using Dev.Comm.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.NET
{
    [TestClass]
    public class HtmlTextHelperTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var test = @"<form name='form' action='Radioivote.asp' method='Post'>
<input type='Hidden' name='Voeid' id='VoTeid' value='49a57a54a49' />
<input type='Hidden' name='VotId' id='VotId' value='3779' /></form>";


            var v = HtmlTextHelper.FindValueByName(test.Replace('\'', '"'), "Voeid");

            Assert.AreEqual("49a57a54a49", v);
        }
    }
}
