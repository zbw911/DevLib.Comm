using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Messaging;

namespace Dev.Framework.MessageQuene
{
    /// <summary>
    /// 微软消息队列的默认实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsmqQueneImpl<T> : IMsgQuene<T>
    {
        private readonly string _queuePath;
        private MessageQueue _myQueue;
        private XmlMessageFormatter formatter;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queuePath"></param>
        /// <param name="isLocalMachine"></param>
        public MsmqQueneImpl(string queuePath, bool isLocalMachine)
        {
            _queuePath = queuePath;

            if (isLocalMachine)
                CreateIfNotExist();

            this._myQueue = new MessageQueue(_queuePath);

            formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        }



        /// <summary>
        /// 创建消息队列
        /// </summary>
        public void Createqueue()
        {
            this._myQueue = MessageQueue.Create(_queuePath);


            //this._myQueue.ReceiveCompleted += _myQueue_ReceiveCompleted;
            //throw new NotImplementedException();

            _myQueue.BeginReceive();
        }

        /// <summary>
        /// 配置异步，建议只调用一次
        /// </summary>
        /// <param name="func"></param>
        public void ConfigReceiveCompleted(Action<T> func)
        {

            this._myQueue.ReceiveCompleted += (sender, e) =>
            {
                e.Message.Formatter = formatter;
                func((T)e.Message.Body);
            };
        }
        /// <summary>
        /// 配置异步，建议只调用一次
        /// </summary>
        /// <param name="func"></param>
        public void ConfigReceiveCompleted(Action<T, IAsyncResult> func)
        {

            this._myQueue.ReceiveCompleted += (sender, e) =>
            {
                e.Message.Formatter = formatter;
                func((T)e.Message.Body, e.AsyncResult);
            };
        }




        /// <summary>
        /// 开始异步
        /// </summary>
        public IAsyncResult BeginReceive()
        {
            return this._myQueue.BeginReceive();
        }


        /// <summary>
        /// 结束异步
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        public T EndRecive(IAsyncResult asyncResult)
        {
            var message = this._myQueue.EndReceive(asyncResult);
            return (T)message.Body;
        }





        public void CreateIfNotExist()
        {
            if (!Exists())
                Createqueue();
        }

        /// <summary>
        /// 查看指定消息队列是否存在 
        /// </summary>
        /// <returns></returns>
        public bool Exists()
        {
            return MessageQueue.Exists(_queuePath);
        }

        /// <summary>
        /// 删除现有的消息队列
        /// </summary>
        /// <returns></returns>
        public void Delete()
        {
            MessageQueue.Delete(_queuePath);



        }

        /// <summary>
        /// 得到队列中的所有消息
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllMessages()
        {


            Message[] message = this._myQueue.GetAllMessages();


            List<T> list = new List<T>();


            foreach (Message msg in message)
            {
                msg.Formatter = formatter;

                //_myQueue.ReceiveById(msg.Id);
                list.Add((T)msg.Body);
            }



            return list;
        }

        /// <summary>
        /// 查看某个特定队列中的消息队列，但不从该队列中移出消息。
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {

            //try
            //{
            //从队列中接收消息
            Message myMessage = this._myQueue.Peek();
            T context = (T)myMessage.Body; //获取消息的内容
            return context;
            //}
            //catch (MessageQueueException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
            //catch (InvalidCastException e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        /// <summary>
        /// 根据ID Peek 队列中消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T PeekById(string id)
        {
            var myMessage = this._myQueue.PeekById(id);
            T context = (T)myMessage.Body; //获取消息的内容
            return context;
        }

        /// <summary>
        /// 检索指定消息队列中最前面的消息并将其从该队列中移除。
        /// </summary>
        /// <returns></returns>
        public T Receive()
        {
            //this._myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            //try
            //{
            //从队列中接收消息
            this._myQueue.Formatter = formatter;
            Message myMessage = this._myQueue.Receive();
            T context = (T)myMessage.Body; //获取消息的内容
            return context;
        }

        /// <summary>
        /// 根据ID 检索指定消息队列中最前面的消息并将其从该队列中移除。
        /// </summary>
        /// <returns></returns>
        public T ReceiveById(string id)
        {
            Message myMessage = this._myQueue.ReceiveById(id);
            T context = (T)myMessage.Body; //获取消息的内容
            return context;
        }

        /// <summary>
        /// 发送消息到指定的消息队列
        /// </summary>
        /// <param name="msg"></param>
        public void Send(T msg)
        {
            //try
            //{
            //连接到本地的队列
            Message myMessage = new Message();
            myMessage.Formatter = formatter;
            myMessage.Body = msg;

            //发送消息到队列中
            this._myQueue.Send(myMessage);
            //}
            //catch (ArgumentException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }


        protected Message PeekWithoutTimeout(MessageQueue q, Cursor cursor, PeekAction action)
        {
            Message ret = null;
            try
            {
                ret = q.Peek(new TimeSpan(1), cursor, action);
            }
            catch (MessageQueueException mqe)
            {
                if (!mqe.Message.ToLower().Contains("timeout"))
                {
                    throw;
                }
            }
            return ret;
        }

        /// <summary>
        /// 取得MessageQueue队列的大小
        /// </summary>
        /// <returns></returns>
        public int GetMessageCount()
        {
            return GetPowerShellCount();

        }


        /// <summary>
        /// 清空指定队列的消息
        /// </summary>
        public void Clear()
        {
            this._myQueue.Purge();
        }


        private int GetPowerShellCount()
        {
            return GetPowerShellCount(this._queuePath, Environment.MachineName, "", "");
        }
        private int GetPowerShellCount(string queuePath, string machine, string username, string password)
        {
            var path = string.Format(@"\\{0}\root\CIMv2", machine);
            ManagementScope scope;
            if (string.IsNullOrEmpty(username))
            {
                scope = new ManagementScope(path);
            }
            else
            {
                var options = new ConnectionOptions { Username = username, Password = password };
                scope = new ManagementScope(path, options);
            }
            scope.Connect();
            if (queuePath.StartsWith(".\\")) queuePath = queuePath.Replace(".\\", string.Format("{0}\\", machine));

            string queryString = String.Format("SELECT * FROM Win32_PerfFormattedData_msmq_MSMQQueue");
            var query = new ObjectQuery(queryString);
            var searcher = new ManagementObjectSearcher(scope, query);
            IEnumerable<int> messageCountEnumerable =
                from ManagementObject queue in searcher.Get()
                select (int)(UInt64)queue.GetPropertyValue("MessagesInQueue");
            //IEnumerable<string> messageCountEnumerable =
            //  from ManagementObject queue in searcher.Get()
            //  select (string)queue.GetPropertyValue("Name");
            var x = messageCountEnumerable.First();
            return x;
        }
    }
}