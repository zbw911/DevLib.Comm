using System;
using System.Collections.Generic;

namespace Dev.Framework.MessageQuene
{
    public interface IMsgQuene<T>
    {
        /// <summary>
        /// 创建消息队列
        /// </summary>
        void Createqueue();

        /// <summary>
        /// 创建如果不存在
        /// </summary>
        void CreateIfNotExist();

        /// <summary>
        /// 查看指定消息队列是否存在 
        /// </summary>
        /// <returns></returns>
        bool Exists();

        /// <summary>
        /// 删除现有的消息队列
        /// </summary>
        /// <returns></returns>
        void Delete();
        /// <summary>
        /// 得到队列中的所有消息
        /// </summary>
        /// <returns></returns>
        List<T> GetAllMessages();
        ///// <summary>
        ///// 在“消息队列”网络中定位消息队列。
        ///// </summary>
        //void GetPublicQueues();
        /// <summary>
        /// 查看某个特定队列中的消息队列，但不从该队列中移出消息。
        /// </summary>
        /// <returns></returns>
        T Peek();

        /// <summary>
        /// 根据ID Peek 队列中消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T PeekById(string id);
        /// <summary>
        /// 检索指定消息队列中最前面的消息并将其从该队列中移除。
        /// </summary>
        /// <returns></returns>
        T Receive();

        /// <summary>
        /// 根据ID 检索指定消息队列中最前面的消息并将其从该队列中移除。
        /// </summary>
        /// <returns></returns>
        T ReceiveById(string id);
        /// <summary>
        /// 发送消息到指定的消息队列
        /// </summary>
        /// <param name="msg"></param>
        void Send(T msg);

        /// <summary>
        /// 清空指定队列的消息
        /// </summary>
        void Clear();

        /// <summary>
        /// 取得MessageQueue队列的大小
        /// </summary>
        /// <returns></returns>
        int GetMessageCount();

        /// <summary>
        /// 配置异步，建议只调用一次
        /// </summary>
        /// <param name="func"></param>
        void ConfigReceiveCompleted(Action<T> func);

        /// <summary>
        /// 配置异步，建议只调用一次
        /// </summary>
        /// <param name="func"></param>
        void ConfigReceiveCompleted(Action<T, IAsyncResult> func);

        /// <summary>
        /// 开始异步
        /// </summary>
        IAsyncResult BeginReceive();

        /// <summary>
        /// 结束异步
        /// </summary>
        /// <param name="asyncResult"></param>
        /// <returns></returns>
        T EndRecive(IAsyncResult asyncResult);
    }
}
