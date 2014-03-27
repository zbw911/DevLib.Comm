using System.IO;
using System.Linq;

namespace Dev.Comm.WinForm
{
    /// <summary>
    /// 只在文件中写一行文字，用于记录临时数据
    /// </summary>
    public class SingleFileLine
    {
        private readonly string _fileName;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public SingleFileLine(string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderid"></param>
        public void SetCurText(string orderid)
        {
            FileStream fileStream = null;
            StreamWriter writer = null;
            try
            {
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(_fileName);
                if (!fileInfo.Exists)
                {
                    fileStream = fileInfo.Create();
                    writer = new StreamWriter(fileStream);
                }
                else
                {
                    fileStream = fileInfo.Open(FileMode.Create, FileAccess.Write);
                    writer = new StreamWriter(fileStream);
                }
                writer.WriteLine(orderid);

            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                    writer.Dispose();
                    fileStream.Close();
                    fileStream.Dispose();
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetCurText()
        {
            var fileInfo = new System.IO.FileInfo(_fileName);
            if (!fileInfo.Exists)
            {
                return "";
            }
            else
            {
                var file = File.ReadLines(_fileName);
                var text = file.First();

                return text;
            }
        }
    }
}