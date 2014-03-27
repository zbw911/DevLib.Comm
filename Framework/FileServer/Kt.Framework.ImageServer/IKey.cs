// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IKey.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using Dev.Framework.FileServer.Config;

namespace Dev.Framework.FileServer
{

    /// <summary>
    /// 文件的绝对路径
    /// </summary>
    public class FileSaveInfo
    {
        /// <summary>
        /// 文件服务器
        /// </summary>
        public Server FileServer { get; set; }

        /// <summary>
        /// 生成的路径信息
        /// </summary>
        public string Dirname { get; set; }

        /// <summary>
        /// 扩展名
        /// </summary>
        public string Extname { get; set; }

        /// <summary>
        /// 保存原始文件名
        /// </summary>
        public string Savefilename { get; set; }
    }

    /// <summary>
    /// 文件 Key 生成接口
    /// </summary>
    public interface IKey
    {
        /// <summary>
        /// 生成文件Key
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string CreateFileKey(string fileName, params object[] param);


        /// <summary>
        /// 通过URLKEY生成URL
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string GetFileUrl(string fileKey, params object[] param);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        FileSaveInfo GetFileSavePath(string fileKey, params object[] param);

        /// <summary>
        /// 通过文件URL取得文件原始Key
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <returns></returns>
        string GetFileKeyFromFileUrl(string fileUrl);

    }
}