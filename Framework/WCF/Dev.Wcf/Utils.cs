using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Dev.Wcf
{
    /// <summary>
    /// 一般性方法
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 取得来源IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            OperationContext context = OperationContext.Current;
            MessageProperties prop = context.IncomingMessageProperties;
            RemoteEndpointMessageProperty endpoint =
                prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            string ip = endpoint.Address;

            return ip;
        }


        /// <summary>
        /// 查找Header
        /// </summary>
        /// <param name="header"></param>
        /// <param name="namespace">命名空间，这里使用"http://zbw911.cnblogs.com/"，也可以换成其它的 </param>
        /// <returns></returns>
        public static string FindHeader(string header, string @namespace = "http://zbw911.cnblogs.com/")
        {
            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader(header, @namespace);
            if (index != -1)
                return OperationContext.Current.IncomingMessageHeaders.GetHeader<string>(header, @namespace);

            return null;
        }


        /// <summary>
        /// WCF 当前操作的名称
        /// </summary>
        /// <returns></returns>
        public static string CurrentOperationName()
        {
            //OperationContext.Current.RequestContext.RequestMessage.

            var action = OperationContext.Current.IncomingMessageHeaders.Action;
            var operationName = action.Substring(action.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) + 1);

            return operationName;
        }


        /// <summary>
        /// 是否是WCF 
        /// </summary>
        /// <returns></returns>
        public static bool IsWcf()
        {
            return OperationContext.Current != null;
        }
    }
}
