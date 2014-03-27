// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：WebFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.IO;
using System.Text;
using System.Web;

namespace Dev.Comm.Web
{
    /// <summary>
    /// 文件管理
    /// </summary>
    public class WebFile
    {
        public WebFile()
        {
        }

        #region Fields/Attributes/Properties

        private HttpResponse Response;
        private FileInfo fileInfo;

        #endregion

        #region Constructor

        public WebFile(HttpResponse response)
        {
            Response = response;
        }

        #endregion

        #region Check Method

        /// <summary>
        /// 指定文件是否存在
        /// </summary>
        /// <param name="fullName">指定文件完整路径</param>
        /// <returns>存在返回true;否则返回false</returns>
        public bool IsFileExists(string fullName)
        {
            fileInfo = new FileInfo(fullName);
            if (fileInfo.Exists)
                return true;
            return false;
        }

        #endregion

        #region File Upload

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile">上传的文件</param>
        /// <param name="saveAsFullName">文件保存的完整路径，但不能重名</param>
        /// <returns>成功返回true;否则返回false</returns>
        public bool UploadFile(HttpPostedFile postedFile, string saveAsFullName)
        {
            return UploadFile(postedFile, saveAsFullName, false);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="postedFile">上传的文件</param>
        /// <param name="saveAsFullName">文件保存的完整路径</param>
        /// <param name="isReplace">如果有同名文件存在，是否覆盖</param>
        /// <returns>成功返回true,否则返回false</returns>
        public bool UploadFile(HttpPostedFile postedFile, string saveAsFullName, bool isReplace)
        {
            try
            {
                if (!isReplace && IsFileExists(saveAsFullName))
                    return false;
                postedFile.SaveAs(saveAsFullName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Download

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <returns>下载成功返回true,否则返回false</returns>
        public bool DownloadFile(string fullName)
        {
            return DownloadFile(fullName, fullName.Substring(fullName.LastIndexOf(@"\") + 1));
        }

        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <param name="sendFileName">发送到客户端显示的文件名</param>
        /// <returns>下载成功返回true,否则返回false</returns>
        public bool DownloadFile(string fullName, string sendFileName)
        {
            if (!IsFileExists(fullName))
                return false;


            fileInfo = new FileInfo(fullName);
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("Content-Disposition",
                                                   "attachment; filename=" +
                                                   HttpUtility.UrlEncode(sendFileName, Encoding.UTF8));
            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.WriteFile(fileInfo.FullName);
            //HttpContext.Current.Response.End();
            HttpContext.Current.ApplicationInstance.CompleteRequest();
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Clear();
            return true;
        }

        #endregion

        #region File Delete

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <returns>删除成功返回true,否则返回false</returns>
        public bool DeleteFile(string fullName)
        {
            if (!IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.Delete();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Move To

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <param name="newFullName">文件移动到的完整路径（可重新命名，但不能重名）</param>
        /// <returns>移动成功返回true,否则返回false</returns>
        public bool MoveTo(string fullName, string newFullName)
        {
            return MoveTo(fullName, newFullName, false);
        }

        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <param name="newFullName">文件移动到的完整路径（可重新命名）</param>
        /// <param name="isReplace">如果有同名文件存在，是否覆盖</param>
        /// <returns>移动成功返回true,否则返回false</returns>
        public bool MoveTo(string fullName, string newFullName, bool isReplace)
        {
            if (!isReplace && IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.MoveTo(newFullName);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region File Copy To

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <param name="newFullName">文件移动到的完整路径（可重新命名）</param>
        /// <returns>复制成功返回true,否则返回false</returns>
        public bool CopyTo(string fullName, string newFullName)
        {
            return CopyTo(fullName, newFullName, false);
        }

        /// <summary>
        /// 复制文件
        /// </summary>
        /// <param name="fullName">文件完整路径</param>
        /// <param name="newFullName">文件移动到的完整路径（可重新命名）</param>
        /// <param name="isReplace">如果有同名文件存在，是否覆盖</param>
        /// <returns>复制成功返回true,否则返回false</returns>
        public bool CopyTo(string fullName, string newFullName, bool isReplace)
        {
            if (!isReplace && IsFileExists(fullName))
                return false;
            try
            {
                fileInfo = new FileInfo(fullName);
                fileInfo.CopyTo(newFullName, false);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}