using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Framewrok.MessageQuene
{
    /// <summary>
    /// 消息队列提供者
    /// </summary>
    public static class MsgQueueProvider<T>
    {
        private static IMsgQuene<T> _msgQuene;

        /// <summary>
        /// 提供者
        /// </summary>
        public static IMsgQuene<T> Provider
        {
            get
            {
                if (_msgQuene == null)
                {
                    var queuePath = @".\Private$\loq" + typeof(T).Name;
                    var isLocalMachine = true;
                    _msgQuene = new MsmqQueneImpl<T>(queuePath, isLocalMachine);

                }
                //IMsgQuene = new MsmqQueneImpl<T>()

                return _msgQuene;
            }
            set
            {
                _msgQuene = value;
            }
        }
    }
}
