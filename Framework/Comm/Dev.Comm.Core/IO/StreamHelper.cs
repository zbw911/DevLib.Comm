// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/StreamHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Text;
using System.IO;

namespace Dev.Comm.IO
{
    /// <summary>
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        ///   to begin
        /// </summary>
        /// <param name="stream"> </param>
        public static void StreamToBegin(Stream stream)
        {
           
            stream.Seek(0, SeekOrigin.Begin);
        }

        #region ReadData

        /// <summary>
        ///   把流读到Bype【】
        /// </summary>
        /// <param name="stream"> </param>
        /// <returns> </returns>
        public static byte[] ReadToByteArray(Stream stream)
        {
            var data = new byte[stream.Length];

            stream.Read(data, 0, data.Length);

            return data;
        }

        #endregion

        #region ReadString

        /// <summary>
        ///   Reads the string.
        /// </summary>
        /// <param name="src"> The SRC. </param>
        /// <returns> </returns>
        public static string ReadString(Stream stream)
        {
            return ReadString(stream, Encoding.UTF8);
        }

        ///// <summary>
        ///// 注意使用UTF8方式
        ///// </summary>
        ///// <param name="bytes"></param>
        ///// <returns></returns>
        //public static string ReadStringUtf8(byte[] bytes)
        //{
        //    return System.Text.Encoding.UTF8.GetString(bytes);
        //}


        /// <summary>
        ///   Reads the string.
        /// </summary>
        /// <param name="src"> The SRC. </param>
        /// <param name="encoding"> The encoding. </param>
        /// <returns> </returns>
        public static string ReadString(Stream stream, Encoding encoding)
        {
            stream.Seek(0, SeekOrigin.Begin);
            TextReader reader = new StreamReader(stream, encoding);
            return reader.ReadToEnd();
        }

        #endregion

        #region SaveAs

        /// <summary>
        ///   Copies to.
        /// </summary>
        /// <param name="src"> The SRC. </param>
        /// <param name="dest"> The dest. </param>
        public static void CopyTo(Stream src, Stream dest)
        {
            src.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[0x10000];
            int bytes;
            try
            {
                while ((bytes = src.Read(buffer, 0, buffer.Length)) > 0)
                {
                    dest.Write(buffer, 0, bytes);
                }
            }
            finally
            {
                src.Seek(0, SeekOrigin.Begin);
                dest.Seek(0, SeekOrigin.Begin);
                dest.Flush();
            }
        }


        /// <summary>
        ///   Copies to.
        /// </summary>
        /// <param name="src"> The SRC. </param>
        /// <param name="dest"> The dest. </param>
        public static Stream CopyFrom(Stream src)
        {
            Stream dest = new MemoryStream();
            CopyTo(src, dest);
            return dest;
        }


        /// <summary>
        ///   Saves as.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <param name="fileName"> Name of the file. </param>
        public static string SaveAs(Stream stream, string filePath)
        {
            return SaveAs(stream, filePath, true);
        }

        /// <summary>
        ///   Saves as.
        /// </summary>
        /// <param name="stream"> The stream. </param>
        /// <param name="fileName"> Name of the file. </param>
        /// <param name="isOverwrite"> if set to <c>true</c> [is overwrite]. </param>
        public static string SaveAs(Stream stream, string filePath, bool isOverwrite)
        {
            var data = new byte[stream.Length];
            var length = stream.Read(data, 0, (int) stream.Length);
            return SaveAs(data, filePath, isOverwrite);
        }

        /// <summary>
        ///   Saves as.
        /// </summary>
        /// <param name="data"> The data. </param>
        /// <param name="fileName"> Name of the file. </param>
        /// <param name="isOverwrite"> if set to <c>true</c> [is overwrite]. </param>
        /// <returns> saved file absolute path </returns>
        public static string SaveAs(byte[] data, string filePath, bool isOverwrite)
        {
            string directory = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (isOverwrite)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            if (!isOverwrite && File.Exists(filePath))
            {
                string fileNameWithoutEx = Path.GetFileNameWithoutExtension(filePath);
                string extension = Path.GetExtension(filePath);

                int i = 1;
                do
                {
                    filePath = Path.Combine(directory, string.Format("{0}-{1}{2}", fileNameWithoutEx, i, extension));
                    i++;
                } while (File.Exists(filePath));
            }

            using (var fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Write(data, 0, data.Length);
            }
            return filePath;
        }

        #endregion

        #region WriteString

        public static void WriteString(Stream src, string s)
        {
            WriteString(src, s, Encoding.UTF8);
        }

        /// <summary>
        ///   Writes the string.
        /// </summary>
        /// <param name="src"> The SRC. </param>
        /// <param name="s"> The s. </param>
        public static void WriteString(Stream src, string s, Encoding encoding)
        {
            TextWriter writer = new StreamWriter(src, encoding);
            writer.Write(s);
            writer.Flush();
        }

        #endregion
    }
}