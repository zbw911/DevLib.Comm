using System;
using System.Collections.Generic;

namespace Dev.Wcf.User
{
    class WebConfigUsers : IUsers
    {
        private static List<AuthUser> List;
        /// <summary>
        /// 取得用户
        /// </summary>
        /// <returns></returns>
        public List<AuthUser> GetList()
        {

            if (List != null)
                return List;

            var strUserList = System.Configuration.ConfigurationManager.AppSettings["wcfclientuser"];
            if (strUserList == null) throw new ArgumentNullException("strUserList", "如果使用Web.config 进行用户的验证，应加入wcfclientuser配置节");
            var listusers = strUserList.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            //修正这里的一个BUG
            List = new List<AuthUser>();
            foreach (var s in listusers)
            {
                var userpwdrole = s.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                var user = new AuthUser { UserName = userpwdrole[0], Password = userpwdrole[1] };

                if (userpwdrole.Length > 2)
                    user.Role = userpwdrole[2];

                AddUser(user);
            }

            return List;
        }



        /// <summary>
        /// 添加用户
        /// </summary>
        public void AddUser(AuthUser user)
        {
            List.Add(user);
        }

        /// <summary>
        /// 清空列表
        /// </summary>
        public void Empty()
        {
            List.Clear();

            List = null;
        }
    }
}