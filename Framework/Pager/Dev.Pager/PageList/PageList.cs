// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：PageList.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
namespace Dev.Pager.PageList
{
    /**/

    /// <summary>
    /// PageList 的摘要说明。
    /// </summary>
    public sealed class TranslatePageList
    {
        /// <summary>
        /// 分页查询数据记录总数获取
        /// </summary>
        /// <param name="tbName">----要显示的表或多个表的连接</param>
        /// <param name="ID">----主表的主键</param>
        /// <param name="strCondition">----查询条件,不需where</param>        
        /// <param name="DISTINCT">----是否添加查询字段的 DISTINCT 默认0不添加/1添加</param>
        /// <returns></returns>
        public static string getPageListCounts(string tbName, string ID, string strCondition, bool DISTINCT = false)
        {
            //---存放取得查询结果总数的查询语句                    
            //---对含有DISTINCT的查询进行SQL构造
            //---对含有DISTINCT的总数查询进行SQL构造
            string strTmp = "", SqlSelect = "", SqlCounts = "";

            if (!DISTINCT)
            {
                SqlSelect = "SELECT ";
                SqlCounts = "COUNT(" + ID + ")";
            }
            else
            {
                SqlSelect = "SELECT DISTINCT ";
                SqlCounts = "COUNT(DISTINCT " + ID + ")";
            }
            if (strCondition == string.Empty)
            {
                strTmp = SqlSelect + " " + SqlCounts + " FROM " + tbName;
            }
            else
            {
                strTmp = SqlSelect + " " + SqlCounts + " FROM " + tbName + " WHERE (1=1) " + strCondition;
            }
            return strTmp;
        }

        /// <summary>
        /// 获取分页数据查询SQL
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
        public static string getPageListSql(string tbName, string ID, string fldName, int PageSize, int Page, int Counts,
                                            string fldSort, int Sort, string strCondition, bool UserRowNo = false,
                                            bool DISTINCT = false)
        {
            string strTmp = ""; //---strTmp用于返回的SQL语句
            string SqlSelect = "", strSortType = "", strFSortType = "";

            if (!DISTINCT)
            {
                SqlSelect = "SELECT ";
            }
            else
            {
                SqlSelect = "SELECT DISTINCT ";
            }

            if (fldName == "")
            {
                fldName = "*";
            }

            if (Sort == 0)
            {
                strFSortType = " ASC";
                strSortType = " DESC";
            }
            else
            {
                strFSortType = " DESC";
                strSortType = " ASC";
            }

            //----取得查询结果总数量-----
            int tmpCounts = 1;
            if (Counts != 0)
            {
                tmpCounts = Counts;
            }
            //          --取得分页总数
            int PageCount = (tmpCounts + PageSize - 1) / PageSize;

            //对于超出总页数的那就没有必要再返回数据了，直接返回的空


            if (Page > PageCount)
            {
                return string.Format(" select * from {0} where 1!=1 ", tbName);
                //Page = PageCount;
            }
            if (Page <= 0)
            {
                Page = 1;
            }


            if (UserRowNo)
            {
                string sql = " select  " + fldName + " from (SELECT   ( row_number() over(order   by  " + fldSort +
                             " )) as __rowno__ , a." + fldName
                             + " FROM " + tbName + " where 1=1 " + strCondition + "  ) x WHERE  "
                             + " __rowno__ <=  " + (Page) * PageSize + " and __rowno__ > " + (Page - 1) * PageSize;

                return sql;
            }


            //          --/*-----数据分页2分处理-------*/
            int pageIndex = tmpCounts / PageSize;
            int lastCount = tmpCounts % PageSize;
            if (lastCount > 0)
            {
                pageIndex = pageIndex + 1;
            }
            else
            {
                lastCount = PageSize;
            }
            if (strCondition == string.Empty) // --没有设置显示条件
            {
                if (pageIndex < 2 || Page <= (pageIndex / 2 + pageIndex % 2)) //--前半部分数据处理
                {
                    if (Page == 1)
                    {
                        strTmp = SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " + tbName + " ORDER BY " +
                                 fldSort + " " + strFSortType;
                    }
                    else
                    {
                        strTmp = SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " + tbName + " WHERE " + ID +
                                 " <(SELECT MIN(myid) FROM (" + SqlSelect + " TOP " + PageSize * (Page - 1) + " (" + ID +
                                 ") as myid FROM " + tbName +
                                 " ORDER BY " + fldSort + " " + strFSortType + ") AS TBMinID) ORDER BY " + fldSort + " " +
                                 strFSortType;
                    }
                }
                else
                {
                    Page = pageIndex - Page + 1; //后半部分数据处理
                    if (Page <= 1) //--最后一页数据显示
                    {
                        strTmp = SqlSelect + " * FROM (" + SqlSelect + " TOP " + lastCount + " " + fldName + " FROM " +
                                 tbName + " ORDER BY " + fldSort + " " + strSortType + ") AS TempTB" + " ORDER BY " +
                                 fldSort + " " + strFSortType;
                    }
                    else
                    {
                        strTmp = SqlSelect + " * FROM (" + SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " +
                                 tbName +
                                 " WHERE " + ID + " >(SELECT MAX(myid) FROM(" + SqlSelect + " TOP " +
                                 (PageSize * (Page - 2) + lastCount) + " (" + ID + ") as myid FROM " + tbName +
                                 " ORDER BY " + fldSort + " " + strSortType + ") AS TBMaxID) ORDER BY " + fldSort + " " +
                                 strSortType + ") AS TempTB ORDER BY " + fldSort + " " + strFSortType;
                    }
                }
            }
            else // --有查询条件
            {
                if (pageIndex < 2 || Page <= (pageIndex / 2 + pageIndex % 2)) //--前半部分数据处理
                {
                    if (Page == 1)
                    {
                        strTmp = SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " + tbName + " WHERE 1=1 " +
                                 strCondition + " ORDER BY " + fldSort + " " + strFSortType;
                    }
                    else
                    {
                        strTmp = SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " + tbName +
                                 " WHERE " + ID + " <(SELECT MIN(myid) FROM (" + SqlSelect + " TOP " +
                                 (PageSize * (Page - 1)) + " (" + ID + ") as myid FROM " + tbName +
                                 " WHERE 1=1 " + strCondition + " ORDER BY " + fldSort + " " + strFSortType +
                                 ") AS TBMaxID) " + strCondition +
                                 " ORDER BY " + fldSort + " " + strFSortType;
                    }
                }
                else //--后半部分数据处理
                {
                    Page = pageIndex - Page + 1;
                    if (Page <= 1) //--最后一页数据显示
                    {
                        strTmp = SqlSelect + " * FROM (" + SqlSelect + " TOP " + lastCount + " " + fldName + " FROM " +
                                 tbName +
                                 " WHERE 1=1 " + strCondition + " ORDER BY " + fldSort + " " + strSortType +
                                 ") AS TempTB ORDER BY " + fldSort + " " + strFSortType;
                    }
                    else
                    {
                        strTmp = SqlSelect + " * FROM (" + SqlSelect + " TOP " + PageSize + " " + fldName + " FROM " +
                                 tbName +
                                 " WHERE " + ID + " >(SELECT MAX(myid) FROM(" + SqlSelect + " TOP " +
                                 (PageSize * (Page - 2) + lastCount) + " (" + ID + ") as myid FROM " + tbName +
                                 " WHERE 1=1 " + strCondition + " ORDER BY " + fldSort + " " + strSortType +
                                 ") AS TBMaxID) " + strCondition +
                                 " ORDER BY " + fldSort + " " + strSortType + ") AS TempTB ORDER BY " + fldSort + " " +
                                 strFSortType;
                    }
                }
            }

            return strTmp;
        }
    }
}