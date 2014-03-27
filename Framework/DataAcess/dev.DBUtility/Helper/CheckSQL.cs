// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：CheckSQL.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Text.RegularExpressions;

namespace Dev.DBUtility
{
    /// <summary>
    /// 
    /// </summary>
    public class CheckSQL
    {
        // 从数据层去对付SQL注入就是一件很2B的事情,如果这样的话,现在暂时先这样吧,随后我们将重构这种方案,  add by zbw911 2012-11-12
        /// <summary>
        /// 
        /// </summary>
        private static string keyword_level0 =
            "declare|exec|execute|sysdatabases|database|dbid|db_name|user_name|shutdown|drop|truncate|master|cmdshell|col_name|syscolumns|xtype|object_id|sysobjects";

        private static readonly string keyword_level1 = keyword_level0 + "|" +
                                                        "%20|--|proc|xp_|hkey_|char(0x|selects|fromf|wherew|deleted|updateu|inserti|'|";


        private static readonly string[] keywords = keyword_level1.Split(new[] {'|'},
                                                                         StringSplitOptions.RemoveEmptyEntries);

        /// <summary>
        /// 对SQL语句进行注入检测
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="notallowstr"></param>
        /// <returns></returns>
        public static int CheckSQLText(string sql, out string notallowstr)
        {
            notallowstr = "";

            if (string.IsNullOrEmpty(sql))
            {
                return 1;
            }

            sql = sql.ToLower();

            if (keywords != null && keywords.Length > 0)
            {
                for (int i = 0; i < keywords.Length; i++)
                {
                    string curkey = keywords[i]; // "";

                     
                    if (sql.IndexOf(curkey) >= 0) //说明sql不合法
                    {
                        notallowstr = keywords[i];

                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqls"></param>
        /// <param name="notallowstr"></param>
        /// <returns></returns>
        public static int CheckSQLText(string[] sqls, out string notallowstr)
        {
            notallowstr = "";

            foreach (var sql in sqls)
            {
                if (CheckSQLText(sql, out notallowstr) == -1)
                    return -1;
            }

            return 1;
        }


        internal static int CheckSQLText(QueryParameterCollection commandParameters, out string notallow)
        {
            notallow = "";
            if (commandParameters == null) return 1;
            foreach (var item in commandParameters)
            {
                var parameter = item as QueryParameter;
                if (parameter == null)
                    continue;

                if (CheckSQLText(parameter.Value.ToString(), out notallow) == -1)
                    return -1;
            }


            return 1;
        }
    }
}