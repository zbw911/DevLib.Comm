using System;

namespace Dev.Framewrok.MessageQuene
{
    public interface IMessageReceiver<T>
    {
        void ReceiveMessage(Action<T> messageReceiver);
    }
}