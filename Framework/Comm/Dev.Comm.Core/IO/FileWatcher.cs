// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月22日 11:31
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/FileWatcher.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Dev.Comm.IO
{
    /// <summary>
    ///   文件内容监视
    /// </summary>
    public static class FileContentWatcher
    {
        /// <summary>
        ///   读取最新文件内容，只管读就是了，FileWatch会自动加载最新的文件
        /// </summary>
        /// <param name="file"> </param>
        /// <returns> </returns>
        public static string GetFileCurrentContent(string file)
        {
            return GetFileCurrentContent(file, Encoding.Default, null, null);
        }

        /// <summary>
        ///   读取最新文件内容，只管读就是了，FileWatch会自动加载最新的文件，使用读取处理，及读取后处理方式
        /// </summary>
        /// <param name="file"> </param>
        /// <param name="preHander"> </param>
        /// <param name="afterHander"> </param>
        /// <returns> </returns>
        public static string GetFileCurrentContent(string file, Func<string, string> preHander,
                                                   Func<string, string> afterHander)
        {
            return GetFileCurrentContent(file, Encoding.Default, preHander, afterHander);
        }

        /// <summary>
        ///   读取最新文件内容，只管读就是了，FileWatch会自动加载最新的文件
        /// </summary>
        /// <param name="file"> </param>
        /// <param name="encoding"> </param>
        /// <param name="afterHander"> </param>
        /// <param name="preHander"> </param>
        /// <returns> </returns>
        /// <exception cref="FileNotFoundException"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static string GetFileCurrentContent(string file, System.Text.Encoding encoding,
                                                   Func<string, string> preHander, Func<string, string> afterHander)
        {
            if (!File.Exists(file))
            {
                throw new FileNotFoundException(file);
            }

            var content = FileWatchStorageProvider.Provider.Get(file);

            if (content == null)
            {
                Action readFile = () =>
                                      {
                                          var strFileContent = IOUtility.ReadAsString(file);
                                          if (preHander != null)
                                              strFileContent = preHander(strFileContent);

                                          content = encoding.GetBytes(strFileContent);

                                          FileWatchStorageProvider.Provider.AddOrUpdate(file, content);
                                      };
                readFile();

                var fw = new FileWatcher(file)
                             {
                                 OnChanged = (o, e) => readFile()
                             };
            }


            if (content == null) throw new NullReferenceException("content");

            var strcontent = encoding.GetString(content);

            if (afterHander != null)
            {
                return afterHander(strcontent);
            }

            return strcontent;
        }
    }

    /// <summary>
    ///   文件监控
    /// </summary>
    public class FileWatcher
    {
        private readonly string _file;

        private static readonly Dictionary<string, FileSystemWatcher> WatchList =
            new Dictionary<string, FileSystemWatcher>();

        /// <summary>
        ///   变更后
        /// </summary>
        public FileSystemEventHandler OnChanged
        {
            set { this._watcher.Changed += value; }
        }

        /// <summary>
        ///   创建
        /// </summary>
        public FileSystemEventHandler OnCreated
        {
            set { this._watcher.Created += value; }
        }

        /// <summary>
        ///   删除
        /// </summary>
        public FileSystemEventHandler OnDeleted
        {
            set { this._watcher.Deleted += value; }
        }

        /// <summary>
        ///   重命名
        /// </summary>
        public RenamedEventHandler OnRenamed
        {
            set { this._watcher.Renamed += value; }
        }

        private FileSystemWatcher _watcher;


        /// <summary>
        /// </summary>
        /// <param name="file"> </param>
        public FileWatcher(string file)
        {
            _file = file;

            AddWatch();
        }

        /// <summary>
        /// </summary>
        /// <exception cref="FileNotFoundException"></exception>
        private void AddWatch()
        {
            //if (!File.Exists(file))
            //{
            //    throw new FileNotFoundException(file);
            //}
            this._watcher = new FileSystemWatcher
                                {
                                    Path = Path.GetDirectoryName(_file),
                                    Filter = Path.GetFileName(_file),
                                    NotifyFilter = NotifyFilters.Size | NotifyFilters.LastWrite
                                                   | NotifyFilters.FileName | NotifyFilters.DirectoryName,
                                    EnableRaisingEvents = true
                                };

            // Add event handlers.

            //if (this.OnChanged != null)
            //{
            //    this._watcher.Changed += this.OnChanged;
            //    this._watcher.Created += this.OnCreated;
            //    this._watcher.Deleted += this.OnDeleted;
            //}
            //if (this.OnRenamed != null) this._watcher.Renamed += this.OnRenamed;

            // Begin watching.
        }


        /// <summary>
        ///   移除Watch
        /// </summary>
        /// <param name="file"> </param>
        public static void RemoveWatch(string file)
        {
            if (!WatchList.ContainsKey(file))
            {
                return;
            }

            var watch = WatchList[file];

            watch.EnableRaisingEvents = false;

            WatchList.Remove(file);

            watch.Dispose();
        }
    }


    /// <summary>
    ///   文件存储，默认内在中存放，以后可以扩展其它方式
    /// </summary>
    public class FileWatchStorageProvider
    {
        private static IFileWatchStorage _fileWatchStorage;

        /// <summary>
        ///   存储提供者
        /// </summary>
        public static IFileWatchStorage Provider
        {
            get
            {
                if (_fileWatchStorage == null)
                {
                    _fileWatchStorage = new MemFileWatchStorage();
                }

                return _fileWatchStorage;
            }
            set { _fileWatchStorage = value; }
        }
    }

    /// <summary>
    ///   用于监控的文件
    /// </summary>
    public interface IFileWatchStorage
    {
        /// <summary>
        ///   增加或更新
        /// </summary>
        /// <param name="file"> </param>
        /// <param name="content"> </param>
        void AddOrUpdate(string file, byte[] content);

        /// <summary>
        ///   移除
        /// </summary>
        /// <param name="file"> </param>
        void Remove(string file);

        /// <summary>
        ///   取得文件
        /// </summary>
        /// <param name="file"> </param>
        /// <returns> </returns>
        byte[] Get(string file);
    }

    /// <summary>
    ///   存放在内存中的方式
    /// </summary>
    internal class MemFileWatchStorage : IFileWatchStorage
    {
        private static readonly Dictionary<string, byte[]> FileContentDic = new Dictionary<string, byte[]>();

        /// <summary>
        ///   增加或更新
        /// </summary>
        /// <param name="file"> </param>
        /// <param name="content"> </param>
        public void AddOrUpdate(string file, byte[] content)
        {
            FileContentDic[file] = content;
        }

        /// <summary>
        ///   移除
        /// </summary>
        /// <param name="file"> </param>
        public void Remove(string file)
        {
            FileContentDic.Remove(file);
        }

        /// <summary>
        ///   取得文件
        /// </summary>
        /// <param name="file"> </param>
        /// <returns> </returns>
        public byte[] Get(string file)
        {
            if (FileContentDic.ContainsKey(file))
            {
                return FileContentDic[file];
            }

            return null;
        }
    }
}