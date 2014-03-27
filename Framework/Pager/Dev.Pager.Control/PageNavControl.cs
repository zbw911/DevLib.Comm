// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：PageNavControl.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Web.UI;

namespace Dev.Pager.Control
{
    /// <summary>
    /// 通用分页控件
    /// added by zbw911
    /// </summary>
    public class PageNavControl : UserControl
    {
        protected Pagination Model = null;
        private int count;
        private string currentUrl;
        private string pageQueryString = "page";
        private int pageSize = 10;
        private string queryString;


        public int Index
        {
            get
            {
                int index;
                int.TryParse(Page.Request.QueryString[pageQueryString], out index);
                if (index <= 0)
                    index = 1;
                return index;
            }
            //set { index = value; }
        }


        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int Count
        {
            get
            {
                if (count < 0)
                    throw new ArgumentException("参数错误");
                return count;
            }
            set { count = value; }
        }

        public string PageQueryString
        {
            get { return pageQueryString; }
            set { pageQueryString = value; }
        }

        protected override void OnPreRender(EventArgs e)
        {
            init();
            base.OnPreRender(e);
        }

        public void init()
        {
            currentUrl = Page.Request.Path;
            queryString = Page.Request.ServerVariables["Query_String"];

            Model = new Pagination(Index, count, pageSize, currentUrl, queryString);
        }
    }
}