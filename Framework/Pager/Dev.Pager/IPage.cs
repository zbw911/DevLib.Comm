// ***********************************************************************************
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
        /// <typeparam name="T"></typeparam>
        /// <param name="tbName"></param>
        /// <param name="ID"></param>
        /// <param name="fldName"></param>
        /// <param name="PageSize"></param>
        /// <param name="Page"></param>
        /// <param name="Counts"></param>
        /// <param name="fldSort"></param>
        /// <param name="Sort"></param>
        /// <param name="strCondition"></param>
        /// <param name="DISTINCT"></param>
        /// <returns></returns>
        IList<T> PageNav<T>(string tbName, string ID, string fldName, int PageSize, int Page, int Counts, string fldSort,
                            int Sort, string strCondition, bool UseRowNo = false, QueryParameterCollection param = null,
                            bool DISTINCT = false) where T : new();

        /// <summary>
        /// 个数
        /// </summary>
        /// <param name="tbName"></param>
        /// <param name="ID"></param>
        /// <param name="strCondition"></param>
        /// <param name="DISTINCT"></param>
        /// <returns></returns>
        int Count(string tbName, string ID, string strCondition, QueryParameterCollection param = null,
                  bool DISTINCT = false);
    }
}