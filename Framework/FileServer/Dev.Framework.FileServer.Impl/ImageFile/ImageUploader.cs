// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ImageUploader.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.IO;
using Dev.Comm;
using Dev.Comm.Utils;
using ImageResizer.Plugins.AnimatedGifs;
using ImageResizer.Plugins.Basic;
using ImageResizer.Plugins.PrettyGifs;
using ImageResizer.Plugins.Watermark;

namespace Dev.Framework.FileServer.ImageFile
{
    /// <summary>
    /// </summary>
    public class ImageUploader : IImageFile
    {
        #region Readonly & Static Fields

        private readonly IUploadFile _curFileDisposer;
        private readonly IKey _curKeyDisposer;

        #endregion

        #region C'tors

        /// <summary>
        ///   设置当前的文件处理器
        /// </summary>
        /// <param name="uploadFile"> </param>
        /// <param name="key"> </param>
        public ImageUploader(IUploadFile uploadFile, IKey key)
        {
            this._curFileDisposer = uploadFile;
            //this._curFileDisposer.SetCurrentKey(key);
            this._curKeyDisposer = key;
        }

        #endregion

        #region Instance Methods

        private string AddOrUpdateFile(Stream stream, ImagesSize[] sizes, string fileKey)
        {
            if (sizes != null)
                foreach (var imagesSize in sizes)
                {
                    if (!this.checkWidthHeight(imagesSize.Width, imagesSize.Height))
                    {
                        throw new ArgumentException("图片大小参数错误");
                    }
                }

            var filename = this._curFileDisposer.SaveFile(stream, fileKey);


            //this.CurKeyDisposer.CreateFileKey(filename,
            if (sizes != null)
                foreach (var imagesSize in sizes)
                {
                    var width = imagesSize.Width;
                    var height = imagesSize.Height;
                    if (this.needThumb(width, height))
                    {
                        var streamtemp = Dev.Comm.IO.StreamHelper.CopyFrom(stream);

                        //缩略后的图像数据
                        var thumbObj = Thumbnail(streamtemp, width, height);
                        //保存
                        var thumbfilename = this._curFileDisposer.UpdateFile(thumbObj, fileKey, "-", width, "_", height);

                        //filename = thumbfilename;
                    }
                }
            return filename;
        }

        /// <summary>
        /// </summary>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        private bool checkWidthHeight(int width, int height)
        {
            if (width == 0 ^ height == 0)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        ///   是否要缩略
        /// </summary>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        private bool needThumb(int width, int height)
        {
            return width > 0 && height > 0;
        }

        #endregion

        #region IImageFile Members

        /// <summary>
        ///   对图片处理，生成缩略，
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="with"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        public Stream Thumbnail(Stream stream, int with, int height)
        {
            return Thumbnail(stream, with, height, "");
        }

        /// <summary>
        ///   生成水印
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="watermark"> 水印图片地址 </param>
        /// <returns> </returns>
        public Stream Watermark(Stream stream, string watermark)
        {
            return Thumbnail(stream, 0, 0, watermark);
        }

        /// <summary>
        ///   对图片处理，生成缩略，
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="with"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        public Stream Thumbnail(byte[] bytefile, int with, int height)
        {
            return this.Thumbnail(new MemoryStream(bytefile), with, height);
        }

        /// <summary>
        ///   生成水印
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="watermark"> 水印图片地址 </param>
        /// <returns> </returns>
        public Stream Watermark(byte[] bytefile, string watermark)
        {
            return this.Watermark(new MemoryStream(bytefile), watermark);
        }

        /// <summary>
        /// 改变大小 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Stream ResizeImage(byte[] bytefile, int with, int height)
        {
            return this.ResizeImage(new MemoryStream(bytefile), with, height);
        }

        

        /// <summary>
        /// 改变大小 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public Stream ResizeImage(Stream stream, int with, int height)
        {
            return Resize(stream, with, height);
        }

        /// <summary>
        /// 旋转图片 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="rotationDegree"></param>
        /// <returns></returns>
        public Stream RotateImage(byte[] bytefile, int rotationDegree)
        {
            return this.RotateImage(new MemoryStream(bytefile), rotationDegree);
        }

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="rotationDegree"></param>
        /// <returns></returns>
        public Stream RotateImage(Stream stream, int rotationDegree)
        {
            return Rotate(stream, rotationDegree);
        }

        public string SaveImageFile(byte[] bytefile, string fileName, int width = 0, int height = 0)
        {
            return this.SaveImageFile(new MemoryStream(bytefile), fileName, width, height);
        }

        /// <summary>
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="fileName"> </param>
        /// <param name="sizes"> </param>
        /// <returns> </returns>
        public string SaveImageFile(byte[] bytefile, string fileName, ImagesSize[] sizes)
        {
            return this.SaveImageFile(new MemoryStream(bytefile), fileName, sizes);
        }

        public string SaveImageFile(Stream stream, string fileName, int width = 0, int height = 0)
        {
            return this.SaveImageFile(stream, fileName, new ImagesSize[] { new ImagesSize(width, height) });
        }


        public string SaveImageFile(Stream stream, string fileName, ImagesSize[] sizes)
        {
            //实际判断文件类型忽略文件名
            var ext = FileUtil.CheckImageExt(stream);

            if (fileName == null)
                throw new Exception("上传文件非图片类型");

            fileName = "x" + ext;

            var fileKey = this._curKeyDisposer.CreateFileKey(fileName);

            var filename = this.AddOrUpdateFile(stream, sizes, fileKey);

            return filename;
        }

        /// <summary>
        ///   更新图片
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="fileKey"> </param>
        /// <param name="sizes"> </param>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateImageFile(Stream stream, string fileKey, ImagesSize[] sizes)
        {
            var filename = this.AddOrUpdateFile(stream, sizes, fileKey);
        }


        /// <summary>
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="fileKey"> </param>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        public void UpdateImageFile(Stream stream, string fileKey, int width = 0, int height = 0)
        {
            this.UpdateImageFile(stream, fileKey, new ImagesSize[] { new ImagesSize(width, height) });
        }

        /// <summary>
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="fileName"> </param>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        public void UpdateImageFile(byte[] bytefile, string fileName, int width = 0, int height = 0)
        {
            this.UpdateImageFile(new MemoryStream(bytefile), fileName, width, height);
        }


        public string GetImageUrl(string fileKey, int width = 0, int height = 0)
        {
            if (this.needThumb(width, height))
            {
                return this._curKeyDisposer.GetFileUrl(fileKey, "-", width, "_", height);
            }

            return this._curKeyDisposer.GetFileUrl(fileKey);
        }

        #endregion

        #region Class Methods

        /// <summary>
        ///   缩略与水印同一方法
        /// </summary>
        /// <param name="stream"> </param>
        /// <param name="maxwith"> </param>
        /// <param name="maxheight"> </param>
        /// <param name="watermark"> </param>
        /// <returns> </returns>
        private static Stream Thumbnail(Stream stream, int maxwith, int maxheight, string watermark)
        {
            var c = ImageResizer.Configuration.Config.Current;

            if (!c.Plugins.Has<PrettyGifs>())
                new PrettyGifs().Install(c);
            if (!c.Plugins.Has<AnimatedGifs>())
                new AnimatedGifs().Install(c);

            //c.Plugins.LoadPlugins();
            if (c.Plugins.Has<SizeLimiting>())
                c.Plugins.Get<SizeLimiting>().Uninstall(c);


            WatermarkPlugin water = null;
            if (!c.Plugins.Has<WatermarkPlugin>())
            {
                water = (WatermarkPlugin)new WatermarkPlugin().Install(c);
            }
            else
            {
                water = c.Plugins.Get<WatermarkPlugin>();
            }

            var strsetting = "";

            if (maxheight != 0 && maxheight != 0)
            {
                strsetting += "maxwidth=" + maxwith + "&maxheight=" + maxheight;
            }

            if (!string.IsNullOrEmpty(watermark))
            {
                water.OtherImages.Path = Path.GetDirectoryName(watermark);
                water.OtherImages.Align = System.Drawing.ContentAlignment.BottomRight;

                var mark = Path.GetFileName(watermark);
                strsetting += "&watermark=" + mark;
            }

            strsetting = strsetting.TrimStart('&');
            var outStream = new MemoryStream();
            c.BuildImage(stream, outStream, strsetting);

            var s = c.GetDiagnosticsPage();
            Console.WriteLine(s);

            return outStream;
        }


        private static Stream Resize(Stream stream, int with, int height)
        {

            var c = ImageResizer.Configuration.Config.Current;

            if (!c.Plugins.Has<PrettyGifs>())
                new PrettyGifs().Install(c);
            if (!c.Plugins.Has<AnimatedGifs>())
                new AnimatedGifs().Install(c);

            //c.Plugins.LoadPlugins();
            if (c.Plugins.Has<SizeLimiting>())
                c.Plugins.Get<SizeLimiting>().Uninstall(c);


            var strsetting = "";

            if (height != 0 && height != 0)
            {
                strsetting += "width=" + with + "&height=" + height;
            }

            strsetting = strsetting.TrimStart('&');
            var outStream = new MemoryStream();
            c.BuildImage(stream, outStream, strsetting);

            var s = c.GetDiagnosticsPage();
            Console.WriteLine(s);

            return outStream;

        }

        public static Stream Rotate(Stream stream, int rotationDegree)
        {
            var c = ImageResizer.Configuration.Config.Current;

            if (!c.Plugins.Has<PrettyGifs>())
                new PrettyGifs().Install(c);
            if (!c.Plugins.Has<AnimatedGifs>())
                new AnimatedGifs().Install(c);

            //c.Plugins.LoadPlugins();
            if (c.Plugins.Has<SizeLimiting>())
                c.Plugins.Get<SizeLimiting>().Uninstall(c);


            var strsetting = "";

            if (rotationDegree != 0)
            {
                strsetting += "rotate=" + rotationDegree;
            }

            strsetting = strsetting.TrimStart('&');
            var outStream = new MemoryStream();
            c.BuildImage(stream, outStream, strsetting);

            var s = c.GetDiagnosticsPage();
            Console.WriteLine(s);

            return outStream;
        }

        #endregion
    }
}