using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Dev.LuceneExt.Test
{
    class Helpers
    {
        public static void IndexDirectory(IndexWriter writer, FileInfo file)
        {
            if (Directory.Exists(file.FullName))//如果file是一个目录(该目录下面可能有文件、目录文件、空文件三种情况)
            {
                String[] files = Directory.GetFileSystemEntries(file.FullName);//获取此目录下子目录和文件集合
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        IndexDirectory(writer, new FileInfo(files[i]));  //这里是一个递归 
                    }
                }
            }
            else // 到达叶节点时，说明是一个File，而不是目录，则建立索引
            {
                if (file.Extension == ".txt" || file.Extension == ".htm" || file.Extension == ".html" || file.Extension == ".cs")//指定为特定的页面建立索引
                {
                    IndexFile(file, writer);
                }
            }
        }
        private static void IndexFile(FileInfo file, IndexWriter writer)
        {
            try
            {
                Document doc = new Document();
                output("正在建立索引" + file.FullName);
                //Field函数 第一个参数是字段名称，第二个参数是字段内容，第三个参数是存储类型，第四个参数是索引类型
                //Field.Store: 有三个属性：
                //Field.Store.YES：索引文件本来只存储索引数据, 此设计将原文内容直接也存储在索引文件中，如文档的标题。 
                //Field.Store.NO：原文不存储在索引文件中，搜索结果命中后，再根据其他附加属性如文件的Path，数据库的主键等，重新连接打开原文，适合原文内容较大的情况。 
                //Field.Store.COMPRESS 压缩存储； 
                //Field.Index:有四个属性：
                //Field.Index.TOKENIZED：分词索引 
                //Field.Index.UN_TOKENIZED：不分词进行索引，如作者名，日期等，Rod Johnson本身为一单词，不再需要分词。 
                //Field.Index.NO 和 Field.Index.NO_NORMS：
                //不进行索引，存放不能被搜索的内容如文档的一些附加属性如文档类型, URL等。 
                doc.Add(new Field("addtime", System.DateTime.Now.ToString(), Field.Store.YES, Field.Index.NO));
                doc.Add(new Field("filename", file.FullName, Field.Store.YES, Field.Index.NO));
                //这里一定要设置响应的编码格式，否则建立索引的时候不能正确读取内容并分词
                doc.Add(new Field("contents", GetContent(file), Field.Store.YES, Field.Index.ANALYZED));

                writer.AddDocument(doc);
            }
            catch (FileNotFoundException fnfe)
            {
                output(fnfe.Message);
            }
        }

        private static void output(string p)
        {
            Console.WriteLine(p);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private static string GetContent(FileInfo file)
        {
            string content = "";
            StreamReader filestream = new StreamReader(file.FullName, System.Text.Encoding.Default);
            content = filestream.ReadToEnd();
            filestream.Close();
            if (file.Extension == ".htm" || file.Extension == ".html")
            {
                content = parseHtml(content);
            }
            return content;
        }

        /// <summary>
        /// 出去网页中的html字符
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string parseHtml(string html)
        {

            string temp = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "");
            return temp.Replace("&nbsp;", " ");
        }
    }
}
