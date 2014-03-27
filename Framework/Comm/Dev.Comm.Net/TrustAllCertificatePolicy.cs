// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：TrustAllCertificatePolicy.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Net;
using System.Security.Cryptography.X509Certificates;

namespace Dev.Comm.Net
{
    /// <summary>
    /// TrustAllCertificatePolicy ��ժҪ˵����
    /// </summary>
    public class TrustAllCertificatePolicy : ICertificatePolicy
    {
        #region ICertificatePolicy Members

        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem)
        {
            return true;
        }

        #endregion
    }
}