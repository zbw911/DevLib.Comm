using System.Security.Principal;

namespace Dev.Wcf.Authorization
{
    class CustomIIdentity : IIdentity
    {
        public string Name { get; private set; }
        public string AuthenticationType { get; private set; }
        public bool IsAuthenticated { get; private set; }
    }
}