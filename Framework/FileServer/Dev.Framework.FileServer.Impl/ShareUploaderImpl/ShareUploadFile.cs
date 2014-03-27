// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ShareUploadFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;
using Dev.Comm;
using Dev.Comm.NetFile;
using Dev.Comm.Utils;
using Dev.Framework.FileServer.Config;

namespace Dev.Framework.FileServer.ShareImpl
{
    /// <summary>
    /// 共享文件上传实现方式
    /// </summary>
    public class ShareUploadFile : IUploadFile
    {
        private IKey _currentKey;


        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="key"></param>
        public ShareUploadFile(IKey key)
        {
            this._currentKey = key;
        }
        #region IUploadFile Members

        /// <summary>
        /// 生成KEY方案
        /// </summary>
        /// <param name="key"></param>
        public void SetCurrentKey(IKey key)
        {
            this._currentKey = key;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"> </param>
        /// <param name="param"> </param>
        /// <returns></returns>
        public string SaveFile(byte[] bytefile, string fileKey, params object[] param)
        {
            FileSaveInfo fileInfo = this._currentKey.GetFileSavePath(fileKey, param);

            Server server = fileInfo.FileServer;

            var filehelper = new FileHelper
                                 {
                                     hostIp = server.hostip,
                                     password = server.password,
                                     username = server.username,
                                     startdirname = server.startdirname
                                 };

            filehelper.WriteFile(fileInfo.Dirname, fileInfo.Savefilename, bytefile);

            return fileKey;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        public string SaveFile(Stream stream, string fileKey, params object[] param)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return this.SaveFile(FileUtil.StreamToBytes(stream), fileKey, param);
        }


        /// <summary>
        ///  
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string UpdateFile(byte[] bytefile, string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filehelper = new FileHelper
                                 {
                                     hostIp = fileSaveInfo.FileServer.hostip,
                                     password = fileSaveInfo.FileServer.password,
                                     username = fileSaveInfo.FileServer.username,
                                     startdirname = fileSaveInfo.FileServer.startdirname
                                 };

            filehelper.UpdateFile(fileSaveInfo.Dirname, fileSaveInfo.Savefilename, bytefile);

            return fileKey;
        }

        /// <summary>
        /// 根据文件Key删除文件 
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public void DeleteFile(string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = this._currentKey.GetFileSavePath(fileKey, param);

            var filehelper = new FileHelper
            {
                hostIp = fileSaveInfo.FileServer.hostip,
                password = fileSaveInfo.FileServer.password,
                username = fileSaveInfo.FileServer.username,
                startdirname = fileSaveInfo.FileServer.startdirname
            };

            filehelper.DeleteFile(fileSaveInfo.Dirname, fileSaveInfo.Savefilename);

        }

        /// <summary>
        /// 删除File所在的Path
        /// </summary>
        /// <param name="fileKey"></param>
        public void DeltePath(string fileKey)
        {
            FileSaveInfo fileSaveInfo = this._currentKey.GetFileSavePath(fileKey);

            var filehelper = new FileHelper
            {
                hostIp = fileSaveInfo.FileServer.hostip,
                password = fileSaveInfo.FileServer.password,
                username = fileSaveInfo.FileServer.username,
                startdirname = fileSaveInfo.FileServer.startdirname
            };

            filehelper.DeletePath(fileSaveInfo.Dirname);
            //filehelper.DeleteFile(fileSaveInfo.dirname, fileSaveInfo.savefilename);
        }


        public string UpdateFile(Stream stream, string fileKey, params object[] param)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return this.UpdateFile(FileUtil.StreamToBytes(stream), fileKey, param);
        }

        #endregion
    }
}