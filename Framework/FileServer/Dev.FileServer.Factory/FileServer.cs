// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：FileServer.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using Dev.Framework.FileServer;
using Dev.Framework.FileServer.Config;
using Dev.Framework.FileServer.DocFile;
using Dev.Framework.FileServer.ShareImpl;

namespace Dev.FileServer.Factory
{
    public class FileServer
    {
        private static readonly IKey ShareFileKey = new ShareFileKey();
        private static readonly IUploadFile UploadFile = new ShareUploadFile(ShareFileKey);


        private static IDocFile uploadFile;

        static FileServer()
        {
            var x = new ReadConfig();
        }

        public static IDocFile DocFile
        {
            get
            {
                //这里配置的不同类型的上传方法
                if (uploadFile == null)
                    uploadFile = new DocFileUploader(UploadFile, ShareFileKey);

                return uploadFile;
            }
        }
    }
}