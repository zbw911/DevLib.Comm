using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Dev.Log;

namespace Dev.Wcf.HandlerBehaviourAttribute
{
    /// <summary>
    /// GlobalExceptionHandler
    /// </summary>
    public class GlobalExceptionHandler : IErrorHandler
    {


        #region IErrorHandler Members
        /// <summary>
        /// HandleError
        /// </summary>
        /// <param name="ex">ex</param>
        /// <returns>true</returns>
        public bool HandleError(Exception ex)
        {
            return true;
        }

        /// <summary>
        /// ProvideFault
        /// </summary>
        /// <param name="ex">ex</param>
        /// <param name="version">version</param>
        /// <param name="msg">msg</param>
        public void ProvideFault(Exception ex, MessageVersion version, ref Message msg)
        {

            Loger.Error("Wcf异常", ex);
            //// 写入log4net
            //log.Error("WCF异常", ex);
            var newEx = new FaultException(string.Format("WCF接口出错 {0}", ex.TargetSite.Name + "=>msg:" + ex.Message));
            MessageFault msgFault = newEx.CreateMessageFault();
            msg = Message.CreateMessage(version, msgFault, newEx.Action);
        }
        #endregion
    }
}