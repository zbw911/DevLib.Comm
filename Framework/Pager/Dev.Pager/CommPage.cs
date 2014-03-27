// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CommPage.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using Dev.DBUtility;
using Dev.Pager.PageList;

namespace Dev.Pager
{
    /// <summary>
    /// 通用分页 , added by zbw911
    /// </summary>
    public class CommPage : IPage
    {
        private readonly string connString;

        public CommPage(string connString)
        {
            this.connString = connString;
        }

        public CommPage()
            : this(null)
        {
        }

        #region IPage Members

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="tbName">----要显示的表或多个表的连接</param>
        /// <param name="fldName">----要显示的字段列表</param>
        /// <param name="PageSize">----每页显示的记录个数</param>
        /// <param name="Page">----要显示那一页的记录</param>
        /// <param name="PageCount">----查询结果分页后的总页数</param>
        /// <param name="Counts">----查询到的记录数</param>
        /// <param name="fldSort">----排序字段列表或条件(如果是多字段排列Sort指代最后一个排序字段的排列顺序(最后一个排序字段不加排序标记)--程序传参如：' SortA Asc,SortB Desc,SortC ')</param>
        /// <param name="Sort">----排序方法，0为升序，1为降序</param>
        /// <param name="strCondition">----查询条件,不需where</param>
        /// <param name="ID">----主表的主键</param>
        /// <param name="DISTINCT">----是否添加查询字段的 DISTINCT 默认0不添加/1添加</param>
        /// <returns></returns>       
        IList<T> IPage.PageNav<T>(string tbName, string ID, string fldName, int PageSize, int Page, int Counts,
                                  string fldSort, int Sort, string strCondition, bool UseRowNo,
                                  QueryParameterCollection param, bool DISTINCT)
        {
            string noallowstr;
            if (-1 == CheckSQL.CheckSQLText(new[] { tbName, ID, fldName, fldSort, strCondition }, out noallowstr))
            {
                throw new ArgumentException("" + noallowstr);
            }

            string sql = TranslatePageList.getPageListSql(tbName, ID, fldName, PageSize, Page, Counts, fldSort, Sort,
                                                          strCondition, UseRowNo, DISTINCT);

            //Console.WriteLine(sql);

            var list = new List<T>();

            if (Counts == 0) //如果是空,后面的事件就不必做了
                return list;

            IDataAccess dao = DataAccessFactory.CreateDataAccess(connString ?? DataAccessFactory.DBConnString);


            dao.ExecuteReader(reader =>
                                  {
                                      while (reader.Read())
                                      {
                                          var item = ReaderData<T>(reader);
                                          list.Add(item);
                                      }
                                  }, sql, param);


            return list;
        }

        int IPage.Count(string tbName, string ID, string strCondition, QueryParameterCollection param, bool DISTINCT)
        {
            string noallowstr;
            if (-1 == CheckSQL.CheckSQLText(new[] { tbName, ID, strCondition }, out noallowstr))
            {
                throw new ArgumentException("" + noallowstr);
            }

            string sql = TranslatePageList.getPageListCounts(tbName, ID, strCondition, DISTINCT);
            
            IDataAccess dao = DataAccessFactory.CreateDataAccess(connString ?? DataAccessFactory.DBConnString);
            object obj = dao.ExecuteScalar(sql, param);

            if (obj is DBNull)
                return 0;

            int count = Convert.ToInt32(obj);

            return count;
        }

        #endregion

        /// <summary>
        /// 返回reader
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="reader"></param>
        /// <returns></returns>
        public T ReaderData<T>(IDataReader reader) where T : new()
        {
            Type type = typeof(T);

            var v = new T();

            PropertyInfo[] properties = type.GetProperties();

            foreach (var propertie in properties)
            {
                if (propertie.Name == "__rowno__")
                    continue;

                object o = reader[propertie.Name];

                if (o == null || o is DBNull)
                    continue;


                propertie.SetValue(v, reader[propertie.Name], null);
            }

            return v;
        }
    }
}