using System;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Wcf.User
{
    /// <summary>
    /// 验证器
    /// </summary>
    public static class AuthUserManager
    {


        private static Func<IUsers> _funcusers;

        

        internal static bool CheckUser(string username, string password)
        {
            var list = GetList();
            //if (list.Count == 0)
            //    throw new Exception("用户标识列表不能为空");


            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Dev.Log.Loger.Warning("用户名：" + username + "用户或密码为空");
                return false;
            }

            return list.FirstOrDefault(x => x.UserName == username && x.Password == password) != null;
        }


        private static List<AuthUser> GetList()
        {
            IUsers users;
            if (_funcusers == null)
                users = new WebConfigUsers();

            else
                users = _funcusers();

            return users.GetList();
        }


        /// <summary>
        /// 设置当前的用户提取方法
        /// </summary>
        /// <param name="users"></param>
        /// <param name="funcusers"> </param>
        public static void SetCurrent(Func<IUsers> funcusers)
        {
            

            _funcusers = funcusers;
        }
    }
}