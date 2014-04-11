// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ShareUploadFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.IO;
using Dev.Comm.NetFile;
using Dev.Comm.Utils;
using Dev.Framework.FileServer.Config;

namespace Dev.Framework.FileServer.ShareImpl
{
    /// <summary>
    ///     共享文件上传实现方式
    /// </summary>
    public class ShareUploadFile : IUploadFile
    {
        #region Fields

        private IKey _currentKey;

        #endregion

        #region C'tors

        /// <summary>
        ///     Ctor
        /// </summary>
        /// <param name="key"></param>
        public ShareUploadFile(IKey key)
        {
            _currentKey = key;
        }

        #endregion

        #region IUploadFile Members



        /// <summary>
        ///     生成KEY方案
        /// </summary>
        /// <param name="key"></param>
        public void SetCurrentKey(IKey key)
        {
            _currentKey = key;
        }

        /// <summary>
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"> </param>
        /// <param name="param"> </param>
        /// <returns></returns>
        public string SaveFile(byte[] bytefile, string fileKey, params object[] param)
        {
            FileSaveInfo fileInfo = _currentKey.GetFileSavePath(fileKey, param);

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

        public string SaveFileBySpecifyName(Stream stream, string fileKey, string specifyName)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return SaveFileBySpecifyName(FileUtil.StreamToBytes(stream), fileKey, specifyName);
        }


        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <returns></returns>
        public string SaveFile(Stream stream, string fileKey, params object[] param)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return SaveFile(FileUtil.StreamToBytes(stream), fileKey, param);
        }


        /// <summary>
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string UpdateFile(byte[] bytefile, string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey, param);

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

        public string SaveFileBySpecifyName(byte[] bytefile, string fileKey, string specifyName)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey);

            var filehelper = new FileHelper
            {
                hostIp = fileSaveInfo.FileServer.hostip,
                password = fileSaveInfo.FileServer.password,
                username = fileSaveInfo.FileServer.username,
                startdirname = fileSaveInfo.FileServer.startdirname
            };

            filehelper.UpdateFile(fileSaveInfo.Dirname, specifyName, bytefile);

            return fileKey;
        }

        /// <summary>
        ///     根据文件Key删除文件
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public void DeleteFile(string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey, param);

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
        ///     删除File所在的Path
        /// </summary>
        /// <param name="fileKey"></param>
        public void DeltePath(string fileKey)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey);

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

        /// <summary>
        ///     判断文件是否存在
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool ExistFile(string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey, param);

            var filehelper = new FileHelper
            {
                hostIp = fileSaveInfo.FileServer.hostip,
                password = fileSaveInfo.FileServer.password,
                username = fileSaveInfo.FileServer.username,
                startdirname = fileSaveInfo.FileServer.startdirname
            };

            return filehelper.ExistFile(fileSaveInfo.Dirname, fileSaveInfo.Savefilename);
        }

        /// <summary>
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string GetFileStorePath(string fileKey, params object[] param)
        {
            FileSaveInfo fileSaveInfo = _currentKey.GetFileSavePath(fileKey, param);

            var filehelper = new FileHelper
            {
                hostIp = fileSaveInfo.FileServer.hostip,
                password = fileSaveInfo.FileServer.password,
                username = fileSaveInfo.FileServer.username,
                startdirname = fileSaveInfo.FileServer.startdirname
            };

            return filehelper.GetFileName(fileSaveInfo.Dirname, fileSaveInfo.Savefilename);
        }


        public string UpdateFile(Stream stream, string fileKey, params object[] param)
        {
            stream.Seek(0, SeekOrigin.Begin);
            return UpdateFile(FileUtil.StreamToBytes(stream), fileKey, param);
        }

        #endregion
    }
}