using System;
using System.ServiceModel.Dispatcher;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Configuration;

namespace Dev.Wcf.Client.Security
{

    /**
     * 
      <behaviors>
      <endpointBehaviors>
        <behavior name="headersMapping">
          <attachContextHeader/>
        </behavior>
      </endpointBehaviors>
    </behaviors>
    <extensions>
      <behaviorExtensions>
        <add name="attachContextHeader" type="Dev.Wcf.Client.Security.AttachContextBehavior, Dev.Wcf.Client"/>
      </behaviorExtensions>
    </extensions>
    
     * 
     * 
       <endpoint address="http://localhost:15666/API/T.svc" binding="wsHttpBinding"
        bindingConfiguration="wsHttpBindingConfig" contract="ServiceReference7.IT"
        name="AppSvcClient" behaviorConfiguration="headersMapping" />
     * 
     <wsHttpBinding>
        <binding name="wsHttpBindingConfig" closeTimeout="00:00:01" sendTimeout="01:00:00"
          maxReceivedMessageSize="2147483647">
          <security mode="None" />
        </binding>
      </wsHttpBinding>
     * 
       AppContext.UserName = "aaa";
       AppContext.Password = "aaa";
     * 
     * 
     * */
    /// <summary>
    /// 
    /// </summary>
    public class AttachContextBehavior : BehaviorExtensionElement, IEndpointBehavior
    {

        const string Ns = "http://zbw911.cnblogs.com/";
        public override Type BehaviorType
        {
            get
            {
                return typeof(AttachContextBehavior);
            }
        }

        protected override object CreateBehavior()
        {
            return new AttachContextBehavior();
        }

        #region IEndpointBehavior Members

        public void AddBindingParameters(ServiceEndpoint endpoint,
            System.ServiceModel.Channels.BindingParameterCollection bindingParameters)
        {
            return;
        }

        public void ApplyClientBehavior(ServiceEndpoint endpoint,
            System.ServiceModel.Dispatcher.ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new IdentityHeaderInspector());
        }

        public void ApplyDispatchBehavior(ServiceEndpoint endpoint,
            System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {
            return;
        }

        #endregion

        private class IdentityHeaderInspector : IClientMessageInspector
        {
            #region IClientMessageInspector Members

            public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
            {
            }

            public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, System.ServiceModel.IClientChannel channel)
            {
                MessageHeader<string> header = new MessageHeader<string>(AppContext.UserName);
                request.Headers.Add(header.GetUntypedHeader("UserName", Ns));

                header = new MessageHeader<string>(AppContext.Password);
                request.Headers.Add(header.GetUntypedHeader("Password", Ns));
                return null;
            }

            #endregion

        }
    }
}
