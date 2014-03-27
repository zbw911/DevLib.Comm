// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DocFileUploader.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Framework.FileServer.DocFile
{
    /// <summary>
    /// 
    /// </summary>
    public class DocFileUploader : IDocFile
    {
        private readonly IUploadFile _curFileDisposer;
        private readonly IKey _curKeyDisposer;

        /// <summary>
        /// 设置当前的文件处理器
        /// </summary>
        /// <param name="uploadFile"></param>
        /// <param name="key"></param>
        public DocFileUploader(IUploadFile uploadFile, IKey key)
        {
            this._curFileDisposer = uploadFile;
            //this.CurFileDisposer.SetCurrentKey(Key);
            this._curKeyDisposer = key;
        }

        #region IDocFile Members

        public string Save(Stream stream, string fileName)
        {
            string fileKey = this._curKeyDisposer.CreateFileKey(fileName);

            this._curFileDisposer.SaveFile(stream, fileKey);

            return fileKey;
        }

        public string Update(Stream stream, string fileKey)
        {
            this._curFileDisposer.UpdateFile(stream, fileKey);

            return fileKey;
        }


        public string GetDocUrl(string fileKey)
        {
            return this._curKeyDisposer.GetFileUrl(fileKey);
        }

        #endregion
    }
}