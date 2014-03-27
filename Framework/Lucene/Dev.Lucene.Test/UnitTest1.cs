using System;
using System.Collections.Generic;
using System.IO;
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
namespace Dev.LuceneExt.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
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

            Console.WriteLine("over");


            ts.CloneAttributes();
            reader.Close();
            a.Close();

        }

        [TestMethod]
        public void MyTestMethod()
        {
            var list = new[] { 1, 3, 4 };

            var dic = new Dictionary<int, string>
                          {

                          };

            dic.Add(1, "1");
            dic.Add(4, "4");
            dic.Add(3, "3");



        }


        [TestMethod]
        public void MyTestMethod1()
        {
            //IndexWriter writer = new IndexWriter(@"c:/index/", new ChineseAnalyzer(), true);
            Lucene.Net.Analysis.Standard.StandardAnalyzer a = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);

            Lucene.Net.Store.Directory dir = Lucene.Net.Store.FSDirectory.Open("d:/index/");
            IndexWriter writer = new IndexWriter(dir, a, true, new IndexWriter.MaxFieldLength(100));

            Document doc = new Document();






        }

        private const string file = @"// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：IPage.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections.Generic;
using Dev.DBUtility;

namespace Dev.Pager
{
    /// <summary>
    /// 通用接口 added by zbw911
    /// </summary>
    public interface IPage
    {
        /// <summary>
        /// 
        /// </summary>
       
        IList<T> PageNav<T>(string tbName, string ID, string fldName, int PageSize, int Page, int Counts, string fldSort,
                            int Sort, string strCondition, bool UseRowNo = false, QueryParameterCollection param = null,
                            bool DISTINCT = false) where T : new();

        /// <summary>
        /// 个数
        /// </summary>
         
        int Count(string tbName, string ID, string strCondition, QueryParameterCollection param = null,
                  bool DISTINCT = false);
    }
}";

        [TestMethod]
        public void MyTestMethod_index()
        {
            string strIndexDir = @"D:\Index";
            Lucene.Net.Store.Directory indexDir = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30); //Version parameter is used for backward compatibility. Stop words can also be passed to avoid indexing certain words


            using (IndexWriter idxw = new IndexWriter(indexDir, std, true, IndexWriter.MaxFieldLength.UNLIMITED)) //Create an Index writer object.
            {




                Lucene.Net.Documents.Document doc = new Lucene.Net.Documents.Document();

                //var file = System.IO.File.ReadAllText(
                //    @"d:\test.txt");
                Lucene.Net.Documents.Field fldText = new Lucene.Net.Documents.Field("text", file, Lucene.Net.Documents.Field.Store.YES,
                                                                                    Lucene.Net.Documents.Field.Index.
                                                                                        ANALYZED,
                                                                                    Lucene.Net.Documents.Field.
                                                                                        TermVector.YES);


                doc.Add(fldText);

                doc.Add(new Field("addtime", System.DateTime.Now.ToString(), Lucene.Net.Documents.Field.Store.YES,
                                  Field.Index.ANALYZED, Field.TermVector.YES));

                //write the document to the index
                idxw.AddDocument(doc);
                //optimize and close the writer
                idxw.Optimize();
            }
            Console.WriteLine("Indexing Done");

        }



        [TestMethod]
        public void MyTestMethod_IndexDir()
        {
            string strIndexDir = @"D:\Index";
            Lucene.Net.Store.Directory indexDir = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30); //Version parameter is used for backward compatibility. Stop words can also be passed to avoid indexing certain words


            using (IndexWriter idxw = new IndexWriter(indexDir, std, true, IndexWriter.MaxFieldLength.UNLIMITED)) //Create an Index writer object.
            {
                Helpers.IndexDirectory(idxw, new FileInfo(@"D:\[程序]\Code\FileWatcher"));
                //optimize and close the writer
                idxw.Optimize();
            }
            Console.WriteLine("Indexing Done");
        }

        [TestMethod]
        public void MyTestMethod_Search()
        {
            var searchtext = "有";
            Search(searchtext);
        }

        [TestMethod]
        public void MyTestMethod_Search建议()
        {
            var searchtext = "建议";
            Search(searchtext);
        }

        private static void Search(string searchtext)
        {
            string strIndexDir = @"D:\Index";
            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            Lucene.Net.Search.Searcher srchr =
                new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true));


            var parser = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_30, "text", std);
            Lucene.Net.Search.Query qry = parser.Parse(searchtext);

            var cllctr = srchr.Search(qry, 1000);

            Console.WriteLine(cllctr.TotalHits);

            ScoreDoc[] hits = cllctr.ScoreDocs;
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].Doc;
                float score = hits[i].Score;
                Lucene.Net.Documents.Document doc = srchr.Doc(docId);
                Console.WriteLine("索引时间：" + doc.Get("addtime"));
                Console.WriteLine("Searched from Text: " + doc.Get("text"));
            }
            Console.WriteLine("over");
        }


        [TestMethod]
        public void MyTestMethod_inCSfile()
        {
            string fld = "contents";
            string txt = "修改";

            SearchByFld2(fld, txt);
        }

        [TestMethod]
        public void MyTestMethod_Search2()
        {

            string fld = "text";
            string txt = "有";

            SearchByFld(fld, txt);

            //TopScoreDocCollector cllctr = TopScoreDocCollector.Create(100, true);
            //ScoreDoc[] hits = cllctr.TopDocs().ScoreDocs;
            //for (int i = 0; i < hits.Length; i++)
            //{
            //    int docId = hits[i].Doc;
            //    float score = hits[i].Score;
            //    Lucene.Net.Documents.Document doc = srchr.Doc(docId);
            //    Console.WriteLine("Searched from Text: " + doc.Get("text"));
            //}

            Console.WriteLine("over");


        }

        private static void SearchByFld2(string fld, string txt)
        {
            string strIndexDir = @"D:\Index";
            Analyzer std = new StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            Lucene.Net.Search.Searcher srchr =
                new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true));


            var parser = new Lucene.Net.QueryParsers.QueryParser(Lucene.Net.Util.Version.LUCENE_30, fld, std);
            Lucene.Net.Search.Query qry = parser.Parse(txt);

            var cllctr = srchr.Search(qry, 1000);

            Console.WriteLine(cllctr.TotalHits);

            ScoreDoc[] hits = cllctr.ScoreDocs;
            for (int i = 0; i < hits.Length; i++)
            {
                int docId = hits[i].Doc;
                float score = hits[i].Score;
                Lucene.Net.Documents.Document doc = srchr.Doc(docId);
                Console.WriteLine("索引时间：" + doc.Get("addtime"));
                Console.WriteLine("Searched from Text: " + doc.Get(fld));
            }
            Console.WriteLine("over");
        }

        private static void SearchByFld(string fld, string txt)
        {
            string strIndexDir = @"D:\Index";

            Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));
            //Provide the directory where index is stored
            using (var srchr = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true)))
            //true opens the index in read only mode
            {
                Query query = new TermQuery(new Term(fld, txt));
                TopDocs result = srchr.Search(query, 10);
                Console.WriteLine(result.TotalHits);

                foreach (var scoreDoc in result.ScoreDocs)
                {
                    int docId = scoreDoc.Doc;
                    Lucene.Net.Documents.Document doc = srchr.Doc(docId);

                    var contents = doc.Get(fld);

                    Console.WriteLine(contents);
                }
            }
        }

        //[TestMethod]
        //public void MyTestMethod_Search3()
        //{
        //    string strIndexDir = @"D:\Index";

        //    Lucene.Net.Store.Directory directory = Lucene.Net.Store.FSDirectory.Open(new System.IO.DirectoryInfo(strIndexDir));  //Provide the directory where index is stored
        //    Lucene.Net.Search.Searcher searcher = new Lucene.Net.Search.IndexSearcher(Lucene.Net.Index.IndexReader.Open(directory, true));//true opens the index in read only mode
        //    Lucene.Net.Search.TopDocs results = searcher.Search(booleanQuery, null, hits_limit);


        //    foreach (ScoreDoc scoreDoc in results.ScoreDocs)
        //    {
        //        // retrieve the document from the 'ScoreDoc' object
        //        Lucene.Net.Documents.Document doc = searcher.Doc(scoreDoc.Doc);
        //        string myFieldValue = doc.Get("myField");
        //    }



        //    Console.WriteLine("over");


        //}
    }
}
