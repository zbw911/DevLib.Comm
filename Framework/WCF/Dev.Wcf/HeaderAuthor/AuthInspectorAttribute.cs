using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Dev.Wcf.User;

namespace Dev.Wcf.HeaderAuthor
{

    /**
     * 第一步
     * 
     *  <appSettings>
    
    <add key="wcfclientuser" value="aaa,aaa;bbb,bbb;ccc,ccc" />
  </appSettings>
     * 
     * 第二步
     * 
     <bindings>
     
      <wsHttpBinding>
        <binding name="wsHttpBindingConfig" closeTimeout="00:00:01" sendTimeout="01:00:00"
                 maxReceivedMessageSize="2147483647">
          <readerQuotas maxDepth="2147483647" maxStringContentLength="2147483647" maxArrayLength="2147483647"
                        maxBytesPerRead="2147483647" maxNameTableCharCount="2147483647" />
          <security mode="None">
            <transport clientCredentialType="None" />
            <message clientCredentialType="None" negotiateServiceCredential="false" establishSecurityContext="false" />
          </security>
        </binding>
      </wsHttpBinding>
    </bindings>
     * 
     * 第三步
     * 
     * 
      <behavior name="AssocFileServiceBehavior">
          <serviceMetadata httpGetEnabled="true" />
          <serviceDebug includeExceptionDetailInFaults="true" />

      </behavior>
     * 
     * 
     * 多绑定
     *  <service behaviorConfiguration="AssocFileServiceBehavior" name="Dx.Activity.DistributeService.Host.API.T">
        <endpoint address="mmm" binding="webHttpBinding" bindingConfiguration="webHttpBindingWithJsonP"
                  contract="Dx.Activity.DistributeService.Host.API.IT" behaviorConfiguration="httpbehavior" />
        <endpoint binding="wsHttpBinding" bindingConfiguration="wsHttpBindingConfig"
                  contract="Dx.Activity.DistributeService.Host.API.IT" />


      </service>
     * 
     * */

    public class AuthInspectorAttribute : Attribute, IOperationBehavior, IParameterInspector
    {
        const string Ns = "http://zbw911.cnblogs.com/";
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {
        }

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public void Validate(OperationDescription operationDescription)
        {
        }

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {
            //Console.WriteLine("Operation {0} returned: result = {1}", operationName, returnValue);

        }

        public object BeforeCall(string operationName, object[] inputs)
        {

            var msg = OperationContext.Current.RequestContext.RequestMessage.ToString();

            

            int index = OperationContext.Current.IncomingMessageHeaders.FindHeader("UserName", Ns);
            if (index != -1)
            {
                string userName = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("UserName", Ns);
                string password = OperationContext.Current.IncomingMessageHeaders.GetHeader<string>("Password", Ns);

                if (AuthUserManager.CheckUser(userName, password))
                    return null;
            }

            Dev.Log.Loger.Error("非法调用" + operationName + "\r\n" + this.GetHeaders());

            throw new UnauthorizedAccessException(operationName + "未经授权的调用");
        }

        private string GetHeaders()
        {
            var headers = OperationContext.Current.IncomingMessageHeaders;
            var allheaders = "";
            foreach (var messageHeader in headers)
            {
                allheaders += messageHeader.Name + "->" + messageHeader.Namespace + "->\r\n";
            }

            if (allheaders == "")
                allheaders = "无Header数据";

            return allheaders;
        }
    }
}