using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dev.Comm.WinForm
{
    /// <summary>
    /// 一个极为简单日志小工具
    /// </summary>
    public class SimpleTxtLog
    {
        private string logFile;
        private StreamWriter writer;
        private FileStream fileStream = null;

        public SimpleTxtLog(string fileName)
        {
            this.logFile = fileName;
            this.CreateDirectory(this.logFile);
        }

        public void log(string info)
        {

            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(this.logFile);
                if (!fileInfo.Exists)
                {
                    this.fileStream = fileInfo.Create();
                    this.writer = new StreamWriter(this.fileStream);
                }
                else
                {
                    this.fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                    this.writer = new StreamWriter(this.fileStream);
                }
                this.writer.WriteLine(info);

            }
            finally
            {
                if (this.writer != null)
                {
                    this.writer.Close();
                    this.writer.Dispose();
                    this.fileStream.Close();
                    this.fileStream.Dispose();
                }
            }
        }

        public void CreateDirectory(string infoPath)
        {
            DirectoryInfo directoryInfo = Directory.GetParent(infoPath);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
        }
    }
}
