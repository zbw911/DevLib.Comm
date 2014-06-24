using System;

namespace Dev.Framework.MessageQuene
{
    public interface IMessageReceiver<T>
    {
        void ReceiveMessage(Action<T> messageReceiver);
    }
}