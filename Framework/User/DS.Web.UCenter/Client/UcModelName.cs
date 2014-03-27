// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcModelName.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace DS.Web.UCenter.Client
{
    /// <summary>
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    internal static class UcUserModelName
    {
        public static string ModelName
        {
            get { return "user"; }
        }

        public static string ActionRegister
        {
            get { return "register"; }
        }

        public static string ActionLogin
        {
            get { return "login"; }
        }

        public static string ActionInfo
        {
            get { return "get_user"; }
        }

        public static string ActionEdit
        {
            get { return "edit"; }
        }

        public static string ActionDelete
        {
            get { return "delete"; }
        }

        public static string ActionDeleteAvatar
        {
            get { return "deleteavatar"; }
        }

        public static string ActionSynLogin
        {
            get { return "synlogin"; }
        }

        public static string ActionSynLogout
        {
            get { return "synlogout"; }
        }

        public static string ActionCheckEmail
        {
            get { return "check_email"; }
        }

        public static string ActionAddProtected
        {
            get { return "addprotected"; }
        }

        public static string ActionDeleteProtected
        {
            get { return "deleteprotected"; }
        }

        public static string ActionGetProtected
        {
            get { return "getprotected"; }
        }

        public static string ActionMerge
        {
            get { return "merge"; }
        }

        public static string ActionMergeRemove
        {
            get { return "merge_remove"; }
        }

        public static string ActionGetCredit
        {
            get { return "getcredit"; }
        }
    }

    internal static class UcPmModelName
    {
        public static string ModelName
        {
            get { return "pm"; }
        }

        public static string ActionCheckNew
        {
            get { return "check_newpm"; }
        }

        public static string ActionSend
        {
            get { return "sendpm"; }
        }

        public static string ActionDelete
        {
            get { return "delete"; }
        }

        public static string ActionDeleteUser
        {
            get { return "deleteuser"; }
        }

        public static string ActionReadStatus
        {
            get { return "readstatus"; }
        }

        public static string ActionList
        {
            get { return "ls"; }
        }

        public static string ActionView
        {
            get { return "view"; }
        }

        public static string ActionViewNode
        {
            get { return "viewnode"; }
        }

        public static string ActionIgnore
        {
            get { return "ignore"; }
        }

        public static string ActionBlacklsGet
        {
            get { return "blackls_get"; }
        }

        public static string ActionBlacklsSet
        {
            get { return "blackls_set"; }
        }

        public static string ActionBlacklsAdd
        {
            get { return "blackls_add"; }
        }

        public static string ActionBlacklsDelete
        {
            get { return "blackls_delete"; }
        }
    }

    internal static class UcFriendModelName
    {
        public static string ModelName
        {
            get { return "friend"; }
        }

        public static string ActionAdd
        {
            get { return "add"; }
        }

        public static string ActionDelete
        {
            get { return "delete"; }
        }

        public static string ActionTotalNum
        {
            get { return "totalnum"; }
        }

        public static string ActionList
        {
            get { return "ls"; }
        }
    }

    internal static class UcCreditModelName
    {
        public static string ModelName
        {
            get { return "credit"; }
        }

        public static string ActionExchangeRequest
        {
            get { return "request"; }
        }
    }

    internal static class UcTagModelName
    {
        public static string ModelName
        {
            get { return "tag"; }
        }

        public static string ActionGet
        {
            get { return "gettag"; }
        }
    }

    internal static class UcFeedModelName
    {
        public static string ModelName
        {
            get { return "feed"; }
        }

        public static string ActionAdd
        {
            get { return "add"; }
        }

        public static string ActionGet
        {
            get { return "get"; }
        }
    }

    internal static class UcAppModelName
    {
        public static string ModelName
        {
            get { return "app"; }
        }

        public static string ActionList
        {
            get { return "ls"; }
        }
    }

    internal static class UcMailModelName
    {
        public static string ModelName
        {
            get { return "mail"; }
        }

        public static string ActionAdd
        {
            get { return "add"; }
        }
    }
}