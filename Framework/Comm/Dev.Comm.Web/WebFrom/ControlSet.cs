//// ***********************************************************************************
//// Created by zbw911 
//// 创建于：2012年12月18日 10:44
//// 
//// 修改于：2013年02月18日 18:24
//// 文件名：ControlSet.cs
//// 
//// 如果有更好的建议或意见请邮件至zbw911#gmail.com
//// ***********************************************************************************

//namespace Dev.Comm.Web.WebFrom
//{
//    using System;
//    using System.Collections;
//    using System.Data;
//    using System.Web;
//    using System.Web.UI.WebControls;

//    public class ControlSet
//    {
//        #region WEB页面分页控制函数

//        /// <summary>
//        ///  页面分页控制函数
//        /// </summary>
//        /// <param name="TotalCount">是总记录数</param>
//        /// <param name="PerPageCount">每页数目</param>
//        /// <param name="FileName">连接地址</param>
//        /// <param name="CurrentPage">当前页</param>
//        public static void WebCrossPage(int TotalCount, int PerPageCount, string FileName, int CurrentPage)
//        {
//            int TotalPage; //计算总页数
//            if ((TotalCount%PerPageCount) == 0)
//                TotalPage = TotalCount/PerPageCount;
//            else
//                TotalPage = TotalCount/PerPageCount + 1;
//            if (TotalPage == 0) //总记录为0时为一页
//                TotalPage = 1;

//            HttpContext.Current.Response.Write(
//                "<table width=\"100%\" border=\"0\" height=\"100%\" cellspacing=\"0\" cellpadding=\"0\" valign=\"Top\">");
//            HttpContext.Current.Response.Write("<tr><td align=\"center\">");
//            HttpContext.Current.Response.Write("<font color=\"#000080\">");
//            if (CurrentPage < 2)
//                HttpContext.Current.Response.Write("首页 上一页 "); //当前页是第一页时,首页和上一页没有连接
//            else
//            {
//                HttpContext.Current.Response.Write("<a href=\"" + FileName +
//                                                   "?Page=1\"><font color=\"#000080\">首页 </font></a> "); //首页有连接
//                HttpContext.Current.Response.Write("<a href=\"" + FileName + "?Page=" + (CurrentPage - 1) +
//                                                   "\"><font color=\"#000080\">上一页 </font></a>");
//            }
//            if (TotalPage - CurrentPage < 1)
//                HttpContext.Current.Response.Write("下一页 尾页 ");
//            else
//            {
//                HttpContext.Current.Response.Write("<a href=\"" + FileName + "?Page=" + (CurrentPage + 1) +
//                                                   "\"><font color=\"#000080\">下一页 </font></a>");
//                HttpContext.Current.Response.Write("<a href=" + FileName + "?Page=" + TotalPage +
//                                                   "><font color=\"#000080\">尾页 </font></a>");
//            }
//            HttpContext.Current.Response.Write("页次：<strong><font color=red>" + CurrentPage + "</font>/" + TotalPage +
//                                               "</strong>页 ");
//            HttpContext.Current.Response.Write("共<b>" + TotalCount + "</b>条信息（<b>" + PerPageCount + "</b>条/页）");
//            HttpContext.Current.Response.Write(
//                "转到：<input type='text' name=\"Page\" size=\"4\" maxlength=\"10\" class=\"smallInput\" value=" +
//                CurrentPage + ">");
//            HttpContext.Current.Response.Write(
//                "<input class=\"buttonface\" type=\"submit\"  value=\"Goto\"  name=\"cndok\"></font></td></tr></table>");
//        }

//        #endregion

//        #region 取某个字段的部分内容显示在DataGrid中

//        /// <summary>
//        /// 取某个字段的部分内容显示在DataGrid中
//        /// </summary>
//        /// <param name="strCon">内容</param>
//        /// <param name="intLength">长度</param>
//        /// <returns>STRING</returns>
//        public static string ShowPartConToDg(Object strCon, int intLength)
//        {
//            string strReturn = strCon.ToString();
//            if (strReturn.Length <= intLength)
//            {
//                return strReturn;
//            }
//            else
//            {
//                strReturn = strReturn.Substring(0, intLength) + "...";
//                return strReturn;
//            }
//        }

//        #endregion

//        #region 初始化DropDownList与数据库绑定的数据

//        /// <summary>
//        /// 初始化DropDownList与数据库绑定的数据
//        /// </summary>
//        /// <param name="DdlName">DropDownList名称</param>
//        /// <param name="strSql">DataSet</param>
//        /// <param name="strDataField">显示值</param>
//        /// <param name="strFieldId">绑定值</param>
//        public static void DataBindDropdl(DropDownList DdlName, DataSet Ds, string TextField, string ValueField)
//        {
//            if (Ds == null)
//                return;
//            DdlName.DataSource = Ds;
//            DdlName.DataTextField = TextField;
//            DdlName.DataValueField = ValueField;
//            DdlName.DataBind();
//        }

//        #endregion

//        #region 用年初始化DropDownList

//        /// <summary>
//        /// 初始化DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">数量</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlYear(DropDownList DdlName, int intValue)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 0; i < intValue; i++)
//            {
//                strTemp = Convert.ToInt32(DateTime.Now.Year - i).ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//        }

//        #endregion

//        #region 用月初始化DropDownList

//        /// <summary>
//        /// 初始化DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">数量</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlMonth(DropDownList DdlName)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 1; i <= 12; i++)
//            {
//                if (i.ToString().Length == 1)
//                    strTemp = "0" + i.ToString();
//                else
//                    strTemp = i.ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//            int intCurr = Convert.ToInt32(DateTime.Now.Month);
//            DdlName.Items[intCurr - 1].Selected = true;
//        }

//        #endregion

//        #region 用天初始化DropDownList

//        /// <summary>
//        /// 初始化DropDownList
//        /// </summary>
//        /// <param name="DdlName">DropDownList</param>
//        /// <param name="intValue">数量</param>
//        /// <param name="strType">Year,Month,Day,Comm</param>
//        public static void DataBindDdlDaye(DropDownList DdlName)
//        {
//            string strTemp = "";
//            var ArrliMinute = new ArrayList();
//            for (int i = 1; i <= 31; i++)
//            {
//                if (i.ToString().Length == 1)
//                    strTemp = "0" + i.ToString();
//                else
//                    strTemp = i.ToString();
//                ArrliMinute.Add(strTemp);
//            }
//            DdlName.DataSource = ArrliMinute;
//            DdlName.DataBind();
//            int intCurr = Convert.ToInt32(DateTime.Now.Day);
//            DdlName.Items[intCurr - 1].Selected = true;
//        }

//        #endregion

//        #region 在DropDownList中插入一条记录

//        /// <summary>
//        /// 在DropDownList中插入一条记录
//        /// </summary>
//        /// <param name="ddlname">DropDownList名</param>
//        /// <param name="strName">插入的内容</param>
//        public static void AddDataToDdl(DropDownList ddlname, string strName)
//        {
//            var li = new ListItem();
//            li.Value = "0";
//            li.Text = strName;
//            ddlname.Items.Insert(0, li);
//            li.Selected = true;
//        }

//        #endregion

//        #region DataGrid数据绑定

//        /// <summary>
//        /// DataGrid数据绑定
//        /// </summary>
//        /// <param name="Dg">待绑定的DataGrid</param>
//        /// <param name="strSql">DataSet</param>
//        public static void DataGridDataBind(DataGrid Dg, DataSet Ds)
//        {
//            Dg.DataSource = Ds;
//            Dg.DataBind();
//        }

//        #endregion

//        #region 格式化DATAGRID中的某列中的行

//        /// <summary>
//        /// 格式化DATAGRID中的某列中的行
//        /// </summary>
//        /// <param name="dgr">DataGrid列</param>
//        public static void UniteDataGrid(DataGrid Dg, int col)
//        {
//            int i;
//            String LastType;
//            int LastCell;
//            if (Dg.Items.Count > 0)
//            {
//                LastType = Dg.Items[0].Cells[col].Text;
//                Dg.Items[0].Cells[col].RowSpan = 1;
//                LastCell = 0;
//                for (i = 1; i < Dg.Items.Count; i++)
//                {
//                    if (Dg.Items[i].Cells[col].Text == LastType)
//                    {
//                        Dg.Items[i].Cells[col].Visible = false;
//                        Dg.Items[LastCell].Cells[col].RowSpan++;
//                    }
//                    else
//                    {
//                        LastType = Dg.Items[i].Cells[col].Text;
//                        LastCell = i;
//                        Dg.Items[i].Cells[col].RowSpan = 1;
//                    }
//                }
//            }
//        }

//        #endregion

//        #region 格式化DATAGRID中的行列	lianyee

//        public static void UniteDataGridNew(DataGrid Dg, int col)
//        {
//            int i;
//            string LastType;
//            string strs = "";
//            string str = "";
//            int LastCell;
//            if (Dg.Items.Count > 0)
//            {
//                LastType = Dg.Items[0].Cells[col].Text;
//                strs = Dg.Items[0].Cells[col + 1].Text;
//                Dg.Items[0].Cells[col].RowSpan = 1;
//                LastCell = 0;
//                for (i = 1; i < Dg.Items.Count; i++)
//                {
//                    str = Dg.Items[i].Cells[col + 1].Text;
//                    if (Dg.Items[i].Cells[col].Text == LastType)
//                    {
//                        strs = strs + " " + str;
//                        Dg.Items[LastCell].Cells[col + 1].Text = strs;
//                        Dg.Items[i].Cells[col].Visible = false;
//                        Dg.Items[i].Visible = false;
//                    }
//                    else
//                    {
//                        LastType = Dg.Items[i].Cells[col].Text;
//                        LastCell = i;
//                        Dg.Items[LastCell].Cells[col + 1].Text = str;
//                        strs = Dg.Items[LastCell].Cells[col + 1].Text;
//                    }
//                }
//            }
//        }

//        #endregion

//        #region 动态改变DataGrid中链接地址

//        /// <summary>
//        ///  DataGrid中连接地址
//        /// </summary>
//        /// <param name="strAddr">连接的地址</param>
//        /// <param name="pathname">传的参数</param>
//        /// <returns></returns>
//        public static string LinkAddress(string strAddr, Object pathname)
//        {
//            return strAddr + "?ID=" + pathname;
//        }

//        #endregion

//        #region 取DataGrid中某列的部分数据

//        /// <summary>
//        /// 取DataGrid中某列的部分数据
//        /// </summary>
//        /// <param name="strConn"></param>
//        /// <param name="intNum"></param>
//        /// <returns></returns>
//        public static string GetDgPartStr(string strConn, int intNum)
//        {
//            return strConn = strConn.Substring(0, intNum);
//        }

//        #endregion

//        #region 得到当前日期

//        ///// <summary>
//        ///// 得到当前日期
//        ///// </summary>
//        ///// <returns>string</returns>
//        //public static string GetCurrDayWeek()
//        //{
//        //    var dt = new string[7] {"星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六"};
//        //    string strDate = DateTime.Today.Year.ToString() + "年" + DateTime.Today.Month.ToString() + "月" +
//        //                     DateTime.Today.Day.ToString() + "日   " + dt[(int) DateTime.Today.DayOfWeek];
//        //    return strDate;
//        //}

//        #endregion

//        #region 获取表名称

//        public static string GetTblName(string startTime, string tblName)
//        {
//            string inidate = startTime.Substring(0, 7).Replace("-", "");
//            string curdate = DateTime.Today.ToString().Substring(0, 7).Replace("-", "");
//            if (Convert.ToInt32(startTime.Substring(0, 4)) >= 2008)
//            {
//                if (inidate != curdate)
//                {
//                    tblName = tblName + inidate;
//                }
//            }
//            return tblName;
//        }

//        #endregion
//    }
//}