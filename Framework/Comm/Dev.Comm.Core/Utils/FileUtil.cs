// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/FileUtil.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Dev.Comm.Utils
{
    /// <summary>
    ///   文件帮助方法
    /// </summary>
    public class FileUtil
    {
        /// <summary>
        ///   指向的路径是否是文件及文件是否存在
        /// </summary>
        /// <param name="path"> </param>
        /// <returns> </returns>
        public static bool IsFile(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        ///   取得文件信息
        /// </summary>
        /// <param name="filepath"> </param>
        /// <returns> </returns>
        public static FileInfo GetFileInfo(string filepath)
        {
            if (!IsFile(filepath))
            {
                return null;
            }


            var FileInfo = new FileInfo(filepath);
            return FileInfo;
        }

        /// <summary>
        ///   是不是图片类型的文件
        /// </summary>
        /// <param name="path"> </param>
        /// <returns> </returns>
        public static bool IsImageFile(string path)
        {
            // throw new NotImplementedException();
            //本方法 未实现，
            string extname = Path.GetExtension(path);

            if (extname.ToLower() == ".jpg" || extname.ToLower() == ".jpeg" || extname.ToLower() == ".gif" ||
                extname.ToLower() == ".png")
                return true;
            return false;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filepath"></param>
        public static void DeleteFile(string filepath)
        {
            if (File.Exists(filepath))
            {
                File.SetAttributes(filepath, FileAttributes.Normal);
                File.Delete(filepath);
            }
        }


        /// <summary>
        ///   创建图片保存目录
        /// </summary>
        /// <param name="path"> </param>
        public static void FolderCreate(string path)
        {
            path = Path.GetDirectoryName(path);
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        ///   将 Stream 转成 byte[]
        /// </summary>
        public static byte[] StreamToBytes(Stream stream)
        {
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);

            // 设置当前流的位置为流的开始 
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        ///   将二进制流写为图片
        /// </summary>
        /// <param name="imageByte"> </param>
        /// <param name="Path"> </param>
        /// <returns> </returns>
        public static string SaveImageFile(byte[] imageByte, string path)
        {
            Stream stream = new MemoryStream(imageByte);
            return SaveImageFile(stream, path);
        }

        public static string SaveImageFile(Stream stream, string path)
        {
            Image im = Image.FromStream(stream);


            FolderCreate(path);

            im.Save(path);

            return path;
        }

        /// <summary>
        ///   类型
        /// </summary>
        /// <param name="stream"> </param>
        /// <returns> </returns>
        public static string CheckImageExt(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            try
            {
                Image im = Image.FromStream(stream);
                ImageFormat format = im.RawFormat;

                if (format.Guid.ToString() == ImageFormat.Bmp.Guid.ToString())
                    return ".bmp";
                if (format.Guid.ToString() == ImageFormat.Emf.Guid.ToString())
                    return ".emf";
                if (format.Guid.ToString() == ImageFormat.Exif.Guid.ToString())
                    return ".exif";
                if (format.Guid.ToString() == ImageFormat.Gif.Guid.ToString())
                    return ".gif";
                if (format.Guid.ToString() == ImageFormat.Icon.Guid.ToString())
                    return ".icon";
                if (format.Guid.ToString() == ImageFormat.Jpeg.Guid.ToString())
                    return ".jpg";
                if (format.Guid.ToString() == ImageFormat.MemoryBmp.Guid.ToString())
                    return ".bmp";
                if (format.Guid.ToString() == ImageFormat.Png.Guid.ToString())
                    return ".png";
                if (format.Guid.ToString() == ImageFormat.Tiff.Guid.ToString())
                    return ".tiff";
                if (format.Guid.ToString() == ImageFormat.Wmf.Guid.ToString())
                    return ".wmf";
            }
            catch
            {
                throw;
            }


            return null;
        }


        protected static FileExtension CheckTextFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            string fileType = string.Empty;
            try
            {
                byte data = br.ReadByte();
                fileType += data.ToString();
                data = br.ReadByte();
                fileType += data.ToString();
                FileExtension extension;
                try
                {
                    extension = (FileExtension)Enum.Parse(typeof(FileExtension), fileType);
                }
                catch
                {
                    extension = FileExtension.VALIDFILE;
                }
                return extension;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                    br.Close();
                }
            }
        }

        /// <summary>
        ///   判断是否是文本文件
        /// </summary>
        /// <param name="fileName"> </param>
        /// <returns> </returns>
        public static bool CheckIsTextFile(string fileName)
        {
            var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            bool isTextFile = true;
            try
            {
                int i = 0;
                var length = (int)fs.Length;
                byte data;
                while (i < length && isTextFile)
                {
                    data = (byte)fs.ReadByte();
                    isTextFile = (data != 0);
                    i++;
                }
                return isTextFile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }


        /// <summary>
        ///   将文件传到服务器上临时目录中
        /// </summary>
        /// <param name="stream"> 文件流 </param>
        /// <param name="fileName"> 文件上传的路径 </param>
        /// <returns> </returns>
        public static void StreamToFile(Stream stream, string fileName)
        {
            //stream.Seek(0, SeekOrigin.Begin);
            //// 把 Stream 转换成 byte[]
            //var bytes = new byte[stream.Length];


            //stream.Read(bytes, 0, bytes.Length);
            //// 设置当前流的位置为流的开始
            //stream.Seek(0, SeekOrigin.Begin);

            //FolderCreate(fileName);
            //// 把 byte[] 写入文件
            //var fs = new FileStream(fileName, FileMode.Create);
            //var bw = new BinaryWriter(fs);
            //bw.Write(bytes);

            //bw.Close();
            //fs.Close();


            StreamToFile(stream, fileName, null);
        }


        public delegate void ProgressCallback(long position, long total);
        public static void StreamToFile(Stream inputStream, string outputFile, ProgressCallback progressCallback)
        {
            FolderCreate(outputFile);

            if (File.Exists(outputFile))
                DeleteFile(outputFile);

            inputStream.Seek(0, SeekOrigin.Begin);

            using (var outputStream = File.OpenWrite(outputFile))
            {
                const int bufferSize = 4096;
                while (inputStream.Position < inputStream.Length)
                {
                    byte[] data = new byte[bufferSize];
                    int amountRead = inputStream.Read(data, 0, bufferSize);
                    outputStream.Write(data, 0, amountRead);

                    if (progressCallback != null)
                        progressCallback(inputStream.Position, inputStream.Length);
                }
                outputStream.Flush();
            }
        }

        #region Nested type: FileExtension

        protected enum FileExtension
        {
            JPG = 255216,
            GIF = 7173,
            PNG = 13780,
            BMP = 6677,
            SWF = 6787,
            RAR = 8297,
            ZIP = 8075,
            _7Z = 55122,
            VALIDFILE = 9999999
            // 255216 jpg; 

            // 7173 gif; 


            // 13780 png; 

            // 6787 swf 

            // 7790 exe dll, 

            // 8297 rar 

            // 8075 zip 

            // 55122 7z 

            // 6063 xml 

            // 6033 html 

            // 239187 aspx 

            // 117115 cs 

            // 119105 js 

            // 102100 txt 

            // 255254 sql  
        }

        #endregion

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="bytefile"></param>
        ///// <returns></returns>
        //public static Stream ByteToStream(byte[] bytefile)
        //{
        //    var stream = new MemoryStream(bytefile);

        //    stream.Seek(0, SeekOrigin.Begin);

        //    return stream;
        //}

        /// <summary>
        ///   Byte to Stream
        /// </summary>
        /// <param name="bytefile"> </param>
        /// <returns> </returns>
        public static Stream BytesToStream(byte[] bytefile)
        {
            var stream = new MemoryStream(bytefile);

            stream.Seek(0, SeekOrigin.Begin);

            return stream;
        }



        /// <summary>
        /// 对文件流进行MD5加密
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /// <example></example>
        public static string Md5Stream(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            return Md5Stream(fs);
        }

        /// <summary>
        /// 将文件流进行Md5
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        public static string Md5Stream(FileStream fs)
        {
            var md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(fs);
            fs.Close();

            byte[] b = md5.Hash;
            md5.Clear();

            var sb = new StringBuilder(32);
            for (int i = 0; i < b.Length; i++)
            {
                sb.Append(b[i].ToString("X2"));
            }

            if (fs.CanSeek)
            {
                fs.Seek(0, SeekOrigin.Begin);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 对文件进行MD5加密
        /// </summary>
        /// <param name="filePath"></param>
        public static string Md5File(string filePath)
        {
            var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            const int bufferSize = 1048576; // 缓冲区大小，1MB
            byte[] buff = new byte[bufferSize];

            var md5 = new MD5CryptoServiceProvider();
            md5.Initialize();

            long offset = 0;
            while (offset < fs.Length)
            {
                long readSize = bufferSize;
                if (offset + readSize > fs.Length)
                {
                    readSize = fs.Length - offset;
                }

                fs.Read(buff, 0, Convert.ToInt32(readSize)); // 读取一段数据到缓冲区

                if (offset + readSize < fs.Length) // 不是最后一块
                {
                    md5.TransformBlock(buff, 0, Convert.ToInt32(readSize), buff, 0);
                }
                else // 最后一块
                {
                    md5.TransformFinalBlock(buff, 0, Convert.ToInt32(readSize));
                }

                offset += bufferSize;
            }

            fs.Close();
            byte[] result = md5.Hash;
            md5.Clear();

            StringBuilder sb = new StringBuilder(32);
            for (int i = 0; i < result.Length; i++)
            {
                sb.Append(result[i].ToString("X2"));
            }


            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="filepath"></param>
        /// <exception cref="NotImplementedException"></exception>
        public static void BytesToFile(byte[] bytes, string filepath)
        {
            System.IO.FileStream _FileStream = new System.IO.FileStream(filepath, System.IO.FileMode.Create,
                              System.IO.FileAccess.Write);

            _FileStream.Write(bytes, 0, bytes.Length);

            // close file stream
            _FileStream.Close();



        }
    }
}