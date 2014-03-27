using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Web.Services3;
using Microsoft.Web.Services3.Design;

namespace Dev.Wcf.Client.WebService
{
    public static class WebServiceIniter
    {
        /// <summary>
        /// 对某个WS进行初始化
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        public static void Init(WebServicesClientProtocol ws, string username, string password)
        {
            AppContext.UserName = username;
            AppContext.Password = password;

            Policy policy = new Policy();
            policy.Assertions.Add(new MyAssertion());
            ws.SetPolicy(policy);
        }
    }
}
