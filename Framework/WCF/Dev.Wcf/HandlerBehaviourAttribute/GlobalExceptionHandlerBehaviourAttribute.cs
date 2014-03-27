using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Dev.Wcf.HandlerBehaviourAttribute
{

    /// <summary>
    ///  
    /// <![CDATA[
    /// using System;
    ///namespace WcfService
    ///{
    /// [GlobalExceptionHandlerBehaviour(typeof (GlobalExceptionHandler))]
    ///public class SomeService : ISomeService
    ///]]>
    ///  
    /// </summary>
    public class GlobalExceptionHandlerBehaviourAttribute : Attribute, IServiceBehavior
    {
        private readonly Type _errorHandlerType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorHandlerType"></param>
        public GlobalExceptionHandlerBehaviourAttribute(Type errorHandlerType)
        {
            _errorHandlerType = errorHandlerType;
        }

        #region IServiceBehavior Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="serviceHostBase"></param>
        public void Validate(ServiceDescription description,
                             ServiceHostBase serviceHostBase)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="serviceHostBase"></param>
        /// <param name="endpoints"></param>
        /// <param name="parameters"></param>
        public void AddBindingParameters(ServiceDescription description,
                                         ServiceHostBase serviceHostBase,
                                         Collection<ServiceEndpoint> endpoints,
                                         BindingParameterCollection parameters)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        /// <param name="serviceHostBase"></param>
        public void ApplyDispatchBehavior(ServiceDescription description,
                                          ServiceHostBase serviceHostBase)
        {
            var handler =
                (IErrorHandler)Activator.CreateInstance(_errorHandlerType);

            foreach (ChannelDispatcherBase dispatcherBase in
                serviceHostBase.ChannelDispatchers)
            {
                var channelDispatcher = dispatcherBase as ChannelDispatcher;
                if (channelDispatcher != null)
                    channelDispatcher.ErrorHandlers.Add(handler);
            }
        }

        #endregion
    }
}