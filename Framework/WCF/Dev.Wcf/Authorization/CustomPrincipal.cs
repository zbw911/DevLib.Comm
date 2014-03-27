using System.Linq;
using System.Security.Principal;
using System.Threading;

namespace Dev.Wcf.Authorization
{
    class CustomPrincipal : IPrincipal
    {
        IIdentity _identity;
        string[] _roles;

        public CustomPrincipal(IIdentity identity)
        {

             
            _identity = identity;
        }

        // helper method for easy access (without casting)
        public static CustomPrincipal Current
        {
            get
            {
                return Thread.CurrentPrincipal as CustomPrincipal;
            }
        }

        public IIdentity Identity
        {
            get { return _identity; }
        }

        // return all roles
        public string[] Roles
        {
            get
            {
                EnsureRoles();
                return _roles;
            }
        }

        // IPrincipal role check
        public bool IsInRole(string role)
        {
            EnsureRoles();

            return _roles.Contains(role);
        }

        // read Role of user from database
        protected virtual void EnsureRoles()
        {
            if (_identity.Name == "AnhDV")
                _roles = new string[1] { "ADMIN" };
            else
                _roles = new string[1] { "USER" };
        }
    }
}
