// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IUploadFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Framework.FileServer
{
    /// <summary>
    /// The interface to UploadImage
    /// 理论上文件服务器是可以无限扩充的，但是当前的上传方案是最简单的方案之一，
    /// 当前方案的问题在后期的文件的难于进行移动，当然，这种很次的方案至少可以顶2年以上，
    /// added by zbw911
    /// </summary>
    public interface IUploadFile
    {
        /// <summary>
        /// 当前的Key生成策略
        /// </summary>
        /// <param name="key"></param>
        void SetCurrentKey(IKey key);

        /// <summary>
        /// 保存图片，原图片的名称，返回运算后的图片名
        /// </summary>
        /// <returns></returns>
        string SaveFile(byte[] bytefile, string fileKey, params object[] param);

        /// <summary>
        /// 使用流保存文件
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        string SaveFile(Stream stream, string fileKey, params object[] param);

        /// <summary>
        /// 通过文件流修改文件
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string UpdateFile(Stream stream, string fileKey, params object[] param);

        /// <summary>
        ///  通过文件Byte修改文件
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        string UpdateFile(byte[] bytefile, string fileKey, params object[] param);

        /// <summary>
        /// 根据文件Key删除文件 
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        void DeleteFile(string fileKey, params object[] param);


        /// <summary>
        /// 删除File所在的Path
        /// </summary>
        /// <param name="fileKey"></param>
        void DeltePath(string fileKey);

    }
}