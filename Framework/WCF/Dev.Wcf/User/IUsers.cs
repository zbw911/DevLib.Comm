using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Wcf.User
{
    /// <summary>
    /// 用户集合
    /// </summary>
    public interface IUsers
    {
        /// <summary>
        /// 取得用户
        /// </summary>
        /// <returns></returns>
        List<AuthUser> GetList();


        ///// <summary>
        ///// 添加用户
        ///// </summary>
        ///// <param name="authUser"></param>
        //void AddUser(AuthUser authUser);


        /// <summary>
        /// 清空列表
        /// </summary>
        void Empty();

    }
}
