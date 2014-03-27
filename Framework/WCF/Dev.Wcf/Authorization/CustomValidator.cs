using System;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using Dev.Wcf.User;

//http://www.codeproject.com/Articles/33872/Custom-Authorization-in-WCF
namespace Dev.Wcf.Authorization
{
    /// <summary>
    /// Summary description for CustomValidator
    /// </summary>
    public class CustomValidator : UserNamePasswordValidator
    {
        public override void Validate(string userName, string password)
        {
            // validate arguments
            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            // check the user credentials from database
            //int userid = 0;
            //CheckUserNameAndPassword(userName, password, out userid);
            //if (0 == userid)
            //    throw new SecurityTokenException("Unknown username or password");

            if (!AuthUserManager.CheckUser(userName, password))
            {
                throw new SecurityTokenException("Unknown username or password");
            }
        }
    }
}
