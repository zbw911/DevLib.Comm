// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ImageServer.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using Dev.Framework.FileServer;
using Dev.Framework.FileServer.Config;
using Dev.Framework.FileServer.ImageFile;
using Dev.Framework.FileServer.ShareImpl;

namespace Dev.FileServer
{
    public class ImageServer
    {
        private static readonly IKey ImageFileKey = new ShareFileKey();
        private static readonly IUploadFile UploadFile = new ShareUploadFile(ImageFileKey);

        private static IImageFile imageFile;

        static ImageServer()
        {
            var x = new ReadConfig();
        }

        public static IImageFile ImageFile
        {
            get
            {
                if (imageFile == null)
                    imageFile = new ImageUploader(UploadFile, ImageFileKey);

                return imageFile;
            }
        }
    }


    //Bind<Kt.Framework.FileServer.IUploadFile>().To<Kt.Framework.FileServer.ShareImpl.ShareUploadFile>();
    //      Bind<Kt.Framework.FileServer.IKey>().To<Kt.Framework.FileServer.ImageFile.ImageFileKey>();
    //      //Kt.Framework.FileServer.IKey key = 
    //      Bind<Kt.Framework.FileServer.IImageFile>().To<Kt.Framework.FileServer.ImageFile.ImageUploader>();
}