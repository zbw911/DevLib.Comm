namespace Dev.Wcf.User
{


    /// <summary>
    /// 用于验证的用户名及密码等
    /// </summary>
    public class AuthUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}