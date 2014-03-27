using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Dev.Comm.XML;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Comm.Test.Core
{

    [TestClass]
    public class XmlToDynamicTest
    {
        private string xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>

<contacts>

  <contact id='1'>

    <firstName>Michael</firstName>

    <lastName>Jordan</lastName>

    <age>40</age>

    <dob>1965</dob>

    <salary>100.35</salary>

  </contact>

  <contact id='2'>

    <firstName>Scottie</firstName>

    <lastName>Pippen</lastName>

    <age>38</age>

    <dob>1967</dob>

    <salary>55.28</salary>

  </contact>

</contacts>
";
        [TestMethod]
        public void TestMethod1()
        {
            //read from text

            //var xDoc = XDocument.Parse(txt);



            //read from url

            //var request = WebRequest.Create(@"http://...") as HttpWebRequest;

            //request.Credentials = CredentialCache.DefaultNetworkCredentials;

            //var xDoc = XDocument.Load(request.GetResponse().GetResponseStream());



            //read from file



            var xDoc = XDocument.Parse(xml);//XDocument.Load(../xml.xml)
            dynamic root = new ExpandoObject();



            XmlToDynamic.Parse(root, xDoc.Elements().First());



            Console.WriteLine(root.contacts.contact.Count);

            Console.WriteLine(root.contacts.contact[0].firstName);

            Console.WriteLine(root.contacts.contact[0].id);


        }
    }
}
