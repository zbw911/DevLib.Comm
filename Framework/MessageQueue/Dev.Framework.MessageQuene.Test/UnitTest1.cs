using System;
using System.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.MessageQuene.Test
{
    [TestClass]
    public class UnitTest1
    {

        /// <summary>
        /// http://www.cnblogs.com/hananbaum/archive/2009/01/26/1380997.html
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            /*    注意事项：
       *    1. 发送和接受消息的电脑都要安装MSMQ。
       *    2. 在工作组模式下不能访问public队列。
       *    3. 访问本地队列和远程队列，path字符串格式不太一样。
       *    4. public队列存在于消息网络中所有主机的消息队列中。
       *    5. private队列则只存在于创建队列的那台主机上。
       */

            #region 以下是private队列访问示例：

            //访问本地电脑上的消息队列时Path的格式可以有如下几种：


            var path = @".\Private$\test";
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
            var formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MessageQueue mq = new MessageQueue();
            mq.Formatter = formatter;
            mq.Path = path;
            //mq.Path = @"sf00902395d34\Private$\test";  //sf00902395d34是主机名
            //mq.Path = @"FormatName:DIRECT=OS:sf00902395d34\Private$\test";
            //mq.Path = @"FormatName:DIRECT=OS:localhost\Private$\test";

            //访问远程电脑上的消息队列时Path的格式
            //mq.Path = @"FormatName:DIRECT=OS:server\Private$\test";


            //构造消息
            Message msg = new Message();



            for (int i = 0; i < 1000000; i++)
            {
                msg.Body = i + "Hello,world. This is a test message. " + DateTime.Now.ToString();
                //向队列发送消息
                mq.Send(msg);

            }
            //mq.BeginReceive(TimeSpan.FromSeconds(10), null, ar =>
            //                                           {
            //                                               Message m = mq.EndReceive(ar);

            //                                               Console.WriteLine(m.Body.ToString());
            //                                               var a = 1;
            //                                           });
            //读取队列中的所有消息
            //Message[] msgs = mq.GetAllMessages();
            //foreach (Message m in msgs)
            //{
            //    Console.WriteLine(m.Body.ToString());
            //
            //}




            //清除队列中的所有消息
            //mq.Purge();

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(1));

            #endregion
        }




        [TestMethod]
        public void TestMethod_Recive()
        {
            var path = @".\Private$\test";
            if (!MessageQueue.Exists(path))
            {
                MessageQueue.Create(path);
            }
            var formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            MessageQueue mq = new MessageQueue();
            mq.Formatter = formatter;
            mq.Path = path;


            do
            {

                try
                {
                    var msg = mq.Receive(TimeSpan.FromSeconds(0.1));

                    if (msg == null)
                        break;


                    Console.WriteLine(msg.Body.ToString());
                }
                catch (System.Messaging.MessageQueueException)
                {

                    Console.WriteLine("time out");

                    break;
                }


            } while (true);

        }




    }
}
