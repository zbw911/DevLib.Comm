// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IDocFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Framework.FileServer
{
    /// <summary>
    /// 文档类文件
    /// </summary>
    public interface IDocFile
    {
        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string Save(Stream stream, string fileName);

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        string Update(Stream stream, string fileKey);

        /// <summary>
        /// 取得文件地址
        /// </summary>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        string GetDocUrl(string fileKey);
    }
}