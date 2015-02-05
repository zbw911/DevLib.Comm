using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.MessageQuene.Test
{
    [TestClass]
    public class UnitTestSyncMessageQ
    {
        static ManualResetEvent signal = new ManualResetEvent(false);

        [TestMethod]
        public void TestMethod1()
        {

            var md = MsgQueueProvider<Mydata>.Provider;

            for (int i = 0; i < 1000; i++)
            {
                md.Send(new Mydata()
                {
                    DateTime = System.DateTime.Now,
                    Mydataint = i
                });
            }

            //md.BeginReceive();

            //signal.WaitOne();
        }




        [TestMethod]
        public void TestSend()
        {
            var md = MsgQueueProvider<Mydata>.Provider;

            md.Receive();
        }


        [TestMethod]
        public void TestRevice()
        {
            Action<Mydata> funcd = (x) =>
            {
                Console.WriteLine(x.Mydataint);
                signal.Set();
            };
            var md = MsgQueueProvider<Mydata>.Provider;
            md.ConfigReceiveCompleted(funcd);



            md.BeginReceive();

            signal.WaitOne();
        }

        [TestMethod]
        public void TestReviceBat()
        {
            var count = 0;

            var md = MsgQueueProvider<Mydata>.Provider;

            Action<Mydata> funcd = (x) =>
            {
                Console.WriteLine("count:" + count + "=》" + x.Mydataint);

                count += 1;
                if (count == 10)
                {
                    signal.Set();
                }

                md.BeginReceive();
            };

            md.ConfigReceiveCompleted(funcd);

            md.BeginReceive();

            signal.WaitOne();
        }



    }
}
