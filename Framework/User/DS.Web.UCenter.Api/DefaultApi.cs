// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DefaultApi.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using DS.Web.UCenter.Api;

namespace DS.Web.UCenter
{
    public class DefaultApi : UcApiBase
    {
        public override ApiReturn DeleteUser(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn RenameUser(decimal uid, string oldUserName, string newUserName)
        {
            throw new NotImplementedException();
        }

        public override UcTagReturns GetTag(string tagName)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn SynLogin(decimal uid)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn SynLogout()
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdatePw(string userName, string passWord)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateBadWords(UcBadWords badWords)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateHosts(UcHosts hosts)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateApps(UcApps apps)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateClient(UcClientSetting client)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCredit(decimal uid, int credit, int amount)
        {
            throw new NotImplementedException();
        }

        public override UcCreditSettingReturns GetCreditSettings()
        {
            throw new NotImplementedException();
        }

        public override ApiReturn GetCredit(decimal uid, int credit)
        {
            throw new NotImplementedException();
        }

        public override ApiReturn UpdateCreditSettings(UcCreditSettings creditSettings)
        {
            throw new NotImplementedException();
        }
    }
}