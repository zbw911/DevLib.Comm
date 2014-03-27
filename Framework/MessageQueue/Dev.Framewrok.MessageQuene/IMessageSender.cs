using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Framewrok.MessageQuene
{
    public interface IMessageSender<in T>
    {
        void Send(T message);
    }
}
