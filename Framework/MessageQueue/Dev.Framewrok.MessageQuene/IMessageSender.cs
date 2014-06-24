namespace Dev.Framework.MessageQuene
{
    public interface IMessageSender<in T>
    {
        void Send(T message);
    }
}
