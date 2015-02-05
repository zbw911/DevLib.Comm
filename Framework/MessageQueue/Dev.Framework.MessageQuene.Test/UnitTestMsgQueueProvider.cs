using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.MessageQuene.Test
{
    [TestClass]
    public class UnitTestMsgQueueProvider
    {
        [TestMethod]
        public void TestMethod1()
        {
            MsgQueueProvider<Data>.Provider.Send(new Data
                                                     {
                                                         Id = 10
                                                     });
        }

        [TestMethod]
        public void TestMethodClear()
        {
            MsgQueueProvider<Data>.Provider.Clear();
        }

        [TestMethod]
        public void TestMethodGet()
        {
            var da = MsgQueueProvider<Data>.Provider.Receive();

            Console.WriteLine(da.Id);
        }


        [TestMethod]
        public void GetCount()
        {
            var count = MsgQueueProvider<Data>.Provider.GetMessageCount();


            Console.WriteLine(count);
        }
    }
}
