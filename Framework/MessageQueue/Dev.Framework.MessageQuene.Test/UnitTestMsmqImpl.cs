using System;
using Dev.Framewrok.MessageQuene;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.MessageQuene.Test
{
    public class Data
    {

        public int Id { get; set; }
    }
    [TestClass]
    public class UnitTestMsmqImpl
    {
        
      
        private string path = @".\Private$\test";
        private IMsgQuene<Data> d;
        [TestInitialize]
        public void Init()
        {
            d = new MsmqQueneImpl<Data>(path,true);
        }

        [TestMethod]
        public void TestSend()
        {
            for (var i = 0; i < 1000; i++)
            {
                d.Send(new Data
                           {
                               Id = i,
                           });
            }
        }


        [TestMethod]
        public void TestPeek()
        {

            for (var i = 0; i < 1000; i++)
            {
                var data = d.Peek();

                Console.WriteLine(data.Id);
            }
        }


        [TestMethod]
        public void TestRecive()
        {
            Data data;

            while ((data = d.Receive()) != null)
            {
                Console.WriteLine(data.Id);
            }

        }

        [TestMethod]
        public void TestGetAllMessage()
        {
            var all = d.GetAllMessages();

            foreach (var data in all)
            {
                Console.WriteLine(data.Id);
            }
        }


        





    }
}
