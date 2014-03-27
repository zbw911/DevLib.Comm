// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：UcRequestArguments.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace DS.Web.UCenter.Api
{
    /// <summary>
    /// Requser参数
    /// Dozer 版权所有
    /// 允许复制、修改，但请保留我的联系方式！
    /// http://www.dozer.cc
    /// mailto:dozer.cc@gmail.com
    /// </summary>
    public class UcRequestArguments : IUcRequestArguments
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="request">Request</param>
        public UcRequestArguments(HttpRequest request)
        {
            Code = request.QueryString["code"];
            if (Code == null)
                throw new Exception("传入参数错误");
            FormData = HttpUtility.UrlDecode(request.Form.ToString(), Encoding.GetEncoding(UcConfig.UcCharset));
            QueryString = HttpUtility.ParseQueryString(UcUtility.AuthCodeDecode(Code));
            Action = QueryString["action"];
            long time;
            if (long.TryParse(QueryString["time"], out time)) Time = time;
            IsInvalidRequest = request.QueryString.Count == 0 && UcActions.Contains(Action);
            IsAuthracationExpiried = (UcUtility.PhpTimeNow() - Time) > 0xe10;
        }

        private string Code { get; set; }

        #region IUcRequestArguments Members

        /// <summary>
        /// Action
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; private set; }

        /// <summary>
        /// Query参数
        /// </summary>
        public NameValueCollection QueryString { get; private set; }

        /// <summary>
        /// Form参数
        /// </summary>
        public string FormData { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsInvalidRequest { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAuthracationExpiried { get; private set; }

        #endregion
    }
}