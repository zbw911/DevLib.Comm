///*
// * 建立一个Form，命名为DemoIndex，添加四个控件：
// * Button按钮命名button1，添加单击事件
// * RichTextBox控件，命名richTextBox1
// * StatusBar，命名statusBar1
// * FolderBrowserDialog，命名folderBrowserDialog1
// * 分词类使用中文分词类库ChineseAnalyzer，也可以使用Lucene自带的类库
// */
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Text;
//using System.Windows.Forms;
//using Lucene.Net.Documents;
//using Lucene.Net.Index;
//using Lucene.Net.QueryParsers;
//using Lucene.Net.Analysis.Cn;
//using System.IO;

//namespace WebCheck
//{
//    public partial class DemoIndex : Form
//    {
//        public DemoIndex()
//        {
//            InitializeComponent();
//        }
//        const string INDEX_STORE_PATH = "index";
//        DateTime start = DateTime.Now;

//        private void button1_Click(object sender, EventArgs e)
//        {
//            DialogResult result = folderBrowserDialog1.ShowDialog();
//            if (result == DialogResult.OK)
//            {
//                IndexWriter writer = null;
//                try
//                {
//                    ///INDEX_STORE_PATH索引所在目录，ChineseAnalyzer()分词类，true是新建索引，flase追加索引。

//                    writer = new IndexWriter(INDEX_STORE_PATH, new ChineseAnalyzer(), true);
//                    writer.SetMaxFieldLength(250000);//设置建立索引的长度，就是对数据的前多少条建立索引    
//                    writer.SetMaxBufferedDocs(100);//控制写入一个新的segment前内存中保存的document的数目,设置较大的数目可以加快建索引速度   
//                    writer.SetMaxMergeDocs(100);// 控制一个segment中可以保存的最大document数目,值较小有利于追加索引的速度   
//                    writer.SetMergeFactor(100);// 控制多个segment合并的频率,值较大时建立索引速度较快,默认是10   
//                    output("开始建立索引..../n");
//                    start = DateTime.Now;
//                    #region 同步索引
//                    //IndexDirectory(writer,new FileInfo(this.folderBrowserDialog1.SelectedPath));
//                    //如果是同步索引的话，调用优化索引的函数可以对索引优化，如果是异步就不行了。
//                    //异步的话，最好把writer也当做参数传递给回调函数里，在回调函数里优化，这里
//                    //传递了一个时间参数进去，你可以传递一个包含start和writer的自定义对象进去。
//                    //为了让示例简单我没有这个做。
//                    //writer.Optimize();
//                    //writer.Close();
//                    #endregion

//                    #region 异步索引
//                    // BeginInvoke 方法可启动异步调用。
//                    //它与您需要异步执行的方法具有相同的参数，另外它还有两个可选参数。
//                    //第一个参数是一个 AsyncCallback 委托，该委托引用在异步调用完成时要调用的方法。
//                    //第二个参数是一个用户定义的对象，该对象可向回调方法传递信息。
//                    //BeginInvoke 立即返回，不等待异步调用完成。BeginInvoke 会返回 IAsyncResult，这个结果可用于监视异步调用进度。
//                    //结果对象IAsyncResult是从开始操作返回的，并且可用于获取有关异步开始操作是否已完成的状态。
//                    //结果对象被传递到结束操作，该操作返回调用的最终返回值。
//                    //在开始操作中可以提供可选的回调。如果提供回调，在调用结束后，将调用该回调；并且回调中的代码可以调用结束操作。

//                    CheckForIllegalCrossThreadCalls = false;
//                    AsyncIndexDirectoryCaller caller = new AsyncIndexDirectoryCaller(IndexDirectory);
//                    IAsyncResult ar = caller.BeginInvoke(writer, new FileInfo(this.folderBrowserDialog1.SelectedPath), new AsyncCallback(searchCallback), writer);
//                    caller.EndInvoke(ar);
//                    this.statusBar1.Text = "准备";
//                    #endregion
//                }
//                catch (Exception ex)
//                {
//                    output(ex.Message);
//                }

//            }
//        }
//        delegate void AsyncIndexDirectoryCaller(IndexWriter writer, FileInfo file);//委托
//        public void IndexDirectory(IndexWriter writer, FileInfo file)
//        {
//            if (Directory.Exists(file.FullName))//如果file是一个目录(该目录下面可能有文件、目录文件、空文件三种情况)
//            {
//                String[] files = Directory.GetFileSystemEntries(file.FullName);//获取此目录下子目录和文件集合
//                if (files != null)
//                {
//                    for (int i = 0; i < files.Length; i++)
//                    {
//                        IndexDirectory(writer, new FileInfo(files[i]));  //这里是一个递归 
//                    }
//                }
//            }
//            else // 到达叶节点时，说明是一个File，而不是目录，则建立索引
//            {
//                if (file.Extension == ".txt" || file.Extension == ".htm" || file.Extension == ".html")//指定为特定的页面建立索引
//                {
//                    IndexFile(file, writer);
//                }
//            }
//        }
//        private void IndexFile(FileInfo file, IndexWriter writer)
//        {
//            try
//            {
//                Document doc = new Document();
//                output("正在建立索引" + file.FullName);
//                //Field函数 第一个参数是字段名称，第二个参数是字段内容，第三个参数是存储类型，第四个参数是索引类型
//                //Field.Store: 有三个属性：
//                //Field.Store.YES：索引文件本来只存储索引数据, 此设计将原文内容直接也存储在索引文件中，如文档的标题。 
//                //Field.Store.NO：原文不存储在索引文件中，搜索结果命中后，再根据其他附加属性如文件的Path，数据库的主键等，重新连接打开原文，适合原文内容较大的情况。 
//                //Field.Store.COMPRESS 压缩存储； 
//                //Field.Index:有四个属性：
//                //Field.Index.TOKENIZED：分词索引 
//                //Field.Index.UN_TOKENIZED：不分词进行索引，如作者名，日期等，Rod Johnson本身为一单词，不再需要分词。 
//                //Field.Index.NO 和 Field.Index.NO_NORMS：
//                //不进行索引，存放不能被搜索的内容如文档的一些附加属性如文档类型, URL等。 

//                doc.Add(new Field("filename", file.FullName, Field.Store.YES, Field.Index.ANALYZED));
//                //这里一定要设置响应的编码格式，否则建立索引的时候不能正确读取内容并分词
//                doc.Add(new Field("contents", GetContent(file), Field.Store.NO, Field.Index.ANALYZED));
//                writer.AddDocument(doc);
//            }
//            catch (FileNotFoundException fnfe)
//            {
//                output(fnfe.Message);
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="file"></param>
//        /// <returns></returns>
//        private string GetContent(FileInfo file)
//        {
//            string content = "";
//            StreamReader filestream = new StreamReader(file.FullName, System.Text.Encoding.Default);
//            content = filestream.ReadToEnd();
//            filestream.Close();
//            if (file.Extension == ".htm" || file.Extension == ".html")
//            {
//                content = parseHtml(content);
//            }
//            return content;
//        }
//        /// <summary>
//        /// 出去网页中的html字符
//        /// </summary>
//        /// <param name="html"></param>
//        /// <returns></returns>
//        private string parseHtml(string html)
//        {

//            string temp = System.Text.RegularExpressions.Regex.Replace(html, "<[^>]*>", "");
//            return temp.Replace("&nbsp;", " ");
//        }
//        void searchCallback(IAsyncResult ar)
//        {
//            IndexWriter writer = (IndexWriter)ar.AsyncState;
//            writer.Optimize();//Optimize()方法是对索引进行优化
//            writer.Close();
//            TimeSpan s = DateTime.Now - start;
//            output("索引完成,共用时" + s.Milliseconds + "毫秒");

//        }
//        void output(string s)
//        {
//            richTextBox1.AppendText(s + "/n");
//            this.statusBar1.Text = s;
//        }
//    }
//}
