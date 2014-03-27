using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;

namespace Dev.Wcf.Client.WebService
{
    class MyAssertion : PolicyAssertion
    {
        public override SoapFilter CreateClientInputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override SoapFilter CreateClientOutputFilter(FilterCreationContext context)
        {
            return new MyPolicy();
        }

        public override SoapFilter CreateServiceInputFilter(FilterCreationContext context)
        {
            return null;
        }

        public override SoapFilter CreateServiceOutputFilter(FilterCreationContext context)
        {
            return null;
        }
    }
}
