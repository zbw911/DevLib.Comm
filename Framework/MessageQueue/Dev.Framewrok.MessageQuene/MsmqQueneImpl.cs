using System;
using System.Collections.Generic;
using System.Messaging;

namespace Dev.Framewrok.MessageQuene
{
    /// <summary>
    /// 微软消息队列的默认实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MsmqQueneImpl<T> : IMsgQuene<T>
    {
        private readonly string _queuePath;
        private MessageQueue _myQueue;

        public MsmqQueneImpl(string queuePath, bool isLocalMachine)
        {
            _queuePath = queuePath;

            if (isLocalMachine)
                CreateIfNotExist();

            this._myQueue = new MessageQueue(_queuePath);
        }

        /// <summary>
        /// 创建消息队列
        /// </summary>
        public void Createqueue()
        {
            this._myQueue = MessageQueue.Create(_queuePath);

            //throw new NotImplementedException();
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
            XmlMessageFormatter formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

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
            this._myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
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
            this._myQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            //try
            //{
            //从队列中接收消息
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
            myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
            myMessage.Body = msg;

            //发送消息到队列中
            this._myQueue.Send(myMessage);
            //}
            //catch (ArgumentException e)
            //{
            //    Console.WriteLine(e.Message);
            //}

        }

        /// <summary>
        /// 清空指定队列的消息
        /// </summary>
        public void Clear()
        {
            this._myQueue.Purge();
        }
    }
}