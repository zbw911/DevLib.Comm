
using System;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;

namespace Dev.LuceneExt
{
    public class Class1
    {
        public void v()
        {

            //Analyzer analyzer = new CJKAnalyzer();
            //TokenStream tokenStream = analyzer.TokenStream("", new StringReader("我爱你中国China中华人名共和国"));
            //Lucene.Net.Analysis.Token token = null;
            //while ((token = tokenStream.Next()) != null)
            //{
            //    Response.Write(token.TermText() + "<br/>");
            //}

            Lucene.Net.Analysis.Standard.StandardAnalyzer a = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            string s = "我日中华人民共和国";
            System.IO.StringReader reader = new System.IO.StringReader(s);
            Lucene.Net.Analysis.TokenStream ts = a.TokenStream(s, reader);
            bool hasnext = ts.IncrementToken();
            Lucene.Net.Analysis.Tokenattributes.ITermAttribute ita;
            while (hasnext)
            {
                ita = ts.GetAttribute<Lucene.Net.Analysis.Tokenattributes.ITermAttribute>();
                Console.WriteLine(ita.Term);
                hasnext = ts.IncrementToken();
            }
            ts.CloneAttributes();
            reader.Close();
            a.Close();
            Console.ReadKey();
        }

        private string indexDirectory = "";

        public void GetIndex(int inum)
        {
            //定义分析器
            //Analyzer KTDAnalyzer = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_CURRENT);// (wordPath);

            ////PerFieldAnalyzerWrapper可以对不同的Field进行不同的分析
            //PerFieldAnalyzerWrapper wrapper = new PerFieldAnalyzerWrapper(KTDAnalyzer);

            //wrapper.AddAnalyzer("ID", KTDAnalyzer);
            //wrapper.AddAnalyzer("News_Url", KTDAnalyzer);
            //wrapper.AddAnalyzer("News_Date", KTDAnalyzer);



            ////判断是否已有索引
            //bool isure = !IndexReader.IndexExists(indexDirectory);
            ////创建索引的数据条数
            //allNum = dt.Rows.Count;
            ////创建IndexWriter
            //var writer = new IndexWriter(indexDirectory, wrapper, isure);
            //writer.SetUseCompoundFile(true); //显式设置索引为复合索引
            //writer.SetMaxFieldLength(int.MaxValue); //设置域最大长度为最大值
            //writer.SetMergeFactor(allNum + 100); //设置每100个段合并成一个大段
            //writer.SetMaxMergeDocs(10000); //设置一个段的最大文档数
            //writer.SetMaxBufferedDocs(1000); //设置在把索引写入磁盘前内存里文档的缓存个数
            ////创建IndexReader
            //IndexReader reader = null;
            //bool needre = inum == 1;

            //reader = IndexReader.Open(indexDirectory);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    completeNum = i + 1;
            //    string body = parseHtml(dt.Rows[i]["News_Body"].ToString());
            //    string title = parseHtml(dt.Rows[i]["News_Title"].ToString());

            //    if (title.Length > 2 && body.Length > 2)
            //    {
            //        if (needre)
            //        {
            //            Term term = new Term("ID", dt.Rows[i]["ID"].ToString());
            //            reader.DeleteDocuments(term);

            //        }
            //        Document document = new Document();

            //        document.Add(new Field("ID", dt.Rows[i]["ID"].ToString() ?? "", Field.Store.YES, Field.Index.UN_TOKENIZED));
            //        document.Add(new Field("News_Title", title, Field.Store.NO, Field.Index.TOKENIZED));
            //        document.Add(new Field("News_Body", body, Field.Store.NO, Field.Index.TOKENIZED));
            //        document.Add(new Field("News_Url", dt.Rows[i]["News_Url"].ToString() ?? "", Field.Store.YES, Field.Index.UN_TOKENIZED));
            //        document.Add(new Field("News_Date", DateField.DateToString(Convert.ToDateTime(dt.Rows[i]["News_Date"].ToString())) ?? "", Field.Store.YES, Field.Index.UN_TOKENIZED));
            //        writer.AddDocument(document); ;
            //    }
            //}
            //reader.Close();
            //writer.Optimize();
            //writer.Close();

        }

    }
}
