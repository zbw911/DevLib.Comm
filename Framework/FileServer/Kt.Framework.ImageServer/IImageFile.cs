// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IImageFile.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System;
using System.IO;

namespace Dev.Framework.FileServer
{
    /// <summary>
    /// 图片辅助类
    /// </summary>
    public struct ImagesSize
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public ImagesSize(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }


        /// <summary>
        /// 图片 宽
        /// </summary>
        public int Width;
        /// <summary>
        /// 图片高
        /// </summary>
        public int Height;
    }


    /// <summary>
    /// 图片服务接口, 
    /// 1,新增批量缩略.zbw911 2012-11-19
    /// </summary>
    public interface IImageFile
    {
        /// <summary>
        /// 对图片处理，生成缩略，
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="with"> </param>
        /// <param name="height"> </param>
        /// <returns></returns>
        Stream Thumbnail(Stream stream, int with, int height);
        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="watermark">水印图片地址</param>
        /// <returns></returns>
        Stream Watermark(Stream stream, string watermark);


        /// <summary>
        /// 对图片处理，生成缩略，
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="with"> </param>
        /// <param name="height"> </param>
        /// <returns></returns>
        Stream Thumbnail(byte[] bytefile, int with, int height);

        /// <summary>
        /// 生成水印
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="watermark">水印图片地址</param>
        /// <returns></returns>
        Stream Watermark(byte[] bytefile, string watermark);

        /// <summary>
        /// 改变大小 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Stream ResizeImage(byte[] bytefile, int with, int height);

        /// <summary>
        /// 改变大小 
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="with"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        Stream ResizeImage(Stream stream, int with, int height);

        /// <summary>
        /// 旋转图片 
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="rotationDegree"></param>
        /// <returns></returns>
        Stream RotateImage(byte[] bytefile, int rotationDegree);

        /// <summary>
        /// 旋转图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="rotationDegree"></param>
        /// <returns></returns>
        Stream RotateImage(Stream stream, int rotationDegree);


        /// <summary>
        /// 保存图片，原图片的名称，返回运算后的图片名
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <param name="fileName"></param>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <returns></returns>
        string SaveImageFile(byte[] bytefile, string fileName, int width = 0, int height = 0);

        /// <summary>
        /// 保存图片文件
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileName"></param>
        /// <param name="sizes"></param>
        /// <returns></returns>
        string SaveImageFile(byte[] bytefile, string fileName, ImagesSize[] sizes);

        /// <summary>
        /// 生成缩略图，并生成根据要求大小的图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        string SaveImageFile(Stream stream, string fileName, int width = 0, int height = 0);


        /// <summary>
        /// 上传图片,批量生成缩略图
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileName"></param>
        /// <param name="sizes"></param>
        /// <returns></returns>
        string SaveImageFile(Stream stream, string fileName, ImagesSize[] sizes);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void UpdateImageFile(Stream stream, string fileKey, int width = 0, int height = 0);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="bytefile"></param>
        /// <param name="fileKey"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        void UpdateImageFile(byte[] bytefile, string fileKey, int width = 0, int height = 0);

        /// <summary>
        /// 取得图片地址
        /// </summary>
        /// <param name="fileKey"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        string GetImageUrl(string fileKey, int width = 0, int height = 0);

        /// <summary>
        /// 更新图片
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileKey"></param>
        /// <param name="sizes"></param>
        void UpdateImageFile(Stream stream, string fileKey, ImagesSize[] sizes);




    }
}