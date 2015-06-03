using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Framework.MessageQuene.RabbitMq
{
    public class RabbitMqQueneImpl<T> : IMsgQuene<T>
    {


        public RabbitMqQueneImpl()
        {

        }
        public void Createqueue()
        {
            throw new NotImplementedException();
        }

        public void CreateIfNotExist()
        {
            throw new NotImplementedException();
        }

        public bool Exists()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            throw new NotImplementedException();
        }

        public List<T> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public T Peek()
        {
            throw new NotImplementedException();
        }

        public T PeekById(string id)
        {
            throw new NotImplementedException();
        }

        public T Receive()
        {
            throw new NotImplementedException();
        }

        public T ReceiveById(string id)
        {
            throw new NotImplementedException();
        }

        public void Send(T msg)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public int GetMessageCount()
        {
            throw new NotImplementedException();
        }

        public void ConfigReceiveCompleted(Action<T> func)
        {
            throw new NotImplementedException();
        }

        public void ConfigReceiveCompleted(Action<T, IAsyncResult> func)
        {
            throw new NotImplementedException();
        }

        public IAsyncResult BeginReceive()
        {
            throw new NotImplementedException();
        }

        public T EndRecive(IAsyncResult asyncResult)
        {
            throw new NotImplementedException();
        }
    }
}
