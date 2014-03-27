// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/FileHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.IO;

namespace Dev.Comm.NetFile
{
    /// <summary>
    ///   网络文件帮助方法
    ///   added by zbw911 2011-4-25
    /// </summary>
    public class FileHelper
    {
        /// <summary>
        ///   密码
        /// </summary>
        public string password { get; set; }

        /// <summary>
        ///   登录名
        /// </summary>
        public string username { get; set; }

        /// <summary>
        ///   IP
        /// </summary>
        public string hostIp { get; set; }

        /// <summary>
        ///   起始路径
        /// </summary>
        public string startdirname { get; set; }

        /// <summary>
        ///   取得文件
        /// </summary>
        /// <param name="dirname"> </param>
        /// <returns> </returns>
        public string[] findFile(string dirname)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                //try
                //{
                return Directory.GetFiles(@"\\" + hostIp + @"\" + dirname);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //}
                //return null;
            }
        }

        /// <summary>
        ///   取得目录
        /// </summary>
        /// <param name="dirname"> </param>
        /// <returns> </returns>
        public string[] GetDirectories(string dirname)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                //try
                //{
                return Directory.GetDirectories(@"\\" + hostIp + @"\" + dirname);
                //}
                //catch (Exception e)
                //{
                //    Console.WriteLine(e.Message);
                //}
                //return null;
            }
        }

        /// <summary>
        ///   写入指定的文件，如果不存在创建目录并写入文件
        /// </summary>
        /// <param name="dirname"> </param>
        /// <param name="filename"> </param>
        /// <param name="fileByte"> </param>
        public void WriteFile(string dirname, string filename, byte[] fileByte)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                string filepath = GetFileName(dirname, filename);

                string path = Path.GetDirectoryName(filepath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fs_stream = new FileStream(filepath, FileMode.CreateNew);

                var writefile = new BinaryWriter(fs_stream);

                writefile.Write(fileByte);

                writefile.Close();
            }
        }


        /// <summary>
        ///   更新指定的文件，如果不存在创建目录并写入文件
        /// </summary>
        /// <param name="dirname"> </param>
        /// <param name="filename"> </param>
        /// <param name="fileByte"> </param>
        public void UpdateFile(string dirname, string filename, byte[] fileByte)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                string filepath = GetFileName(dirname, filename);

                string path = Path.GetDirectoryName(filepath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                var fs_stream = new FileStream(filepath, FileMode.Create);

                var writefile = new BinaryWriter(fs_stream);

                writefile.Write(fileByte);

                writefile.Close();
            }
        }

        public void WriteFile(string dirname, string filename, Stream stream)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                string filepath = GetFileName(dirname, filename);

                string path = Path.GetDirectoryName(filepath);

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var sw = new StreamWriter(stream))
                {
                    sw.WriteLine(stream);
                }
            }
        }

        /// <summary>
        ///   删除文件
        /// </summary>
        /// <param name="dirname"> </param>
        /// <param name="filename"> </param>
        public void DeleteFile(string dirname, string filename)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                string filepath = GetFileName(dirname, filename);

                if (File.Exists(filepath))
                    File.Delete(filepath);
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="dirname"></param>
        public void DeletePath(string dirname)
        {
            using (var iss = new IdentityScope(username, hostIp, password))
            {
                string filepath = GetFileName(dirname, "");
                if (Directory.Exists(filepath))
                    Directory.Delete(filepath, true);
            }
        }




        private string GetFileName(string dirname, string filename)
        {
            string filepath = @"\\" + hostIp + @"\" + startdirname + @"\" + dirname + @"\" + filename;
            return filepath;
        }
    }
}