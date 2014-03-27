using Microsoft.Web.Services3;

namespace Dev.Wcf.Client.WebService
{
    class MyPolicy : SoapFilter
    {



        public override SoapFilterResult ProcessMessage(SoapEnvelope envelope)
        {
            CreateNode(ref envelope, AppContext.UserName, AppContext.Password);
            //CreateNode(ref envelope, "2", "bbbbb");
            return SoapFilterResult.Continue;
        }


        private static void CreateNode(ref SoapEnvelope envelope, string name, string password)
        {

            const string Ns = "http://zbw911.cnblogs.com/";
            /*
   * 
   * <UserName xmlns="http://zbw911.cnblogs.com/">2</UserName>
  <Password xmlns="http://zbw911.cnblogs.com/">bbbbb</Password>
   * */
            var nodeName = envelope.CreateNode("element", "UserName", Ns);
            nodeName.InnerText = name;
            nodeName.Prefix = envelope.Prefix;
            envelope.Header.AppendChild(nodeName);

            var nodePass = envelope.CreateNode("element", "Password", Ns);
            nodePass.InnerText = password;
            nodePass.Prefix = envelope.Prefix;
            envelope.Header.AppendChild(nodePass);
        }


    }
}