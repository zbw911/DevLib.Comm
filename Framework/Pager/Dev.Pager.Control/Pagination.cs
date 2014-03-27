// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Pagination.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Text;

namespace Dev.Pager
{
    public class Pagination
    {
        private string __js_tmp_pageindex__ = "__js_tmp_pageindex__";

        public Pagination(int currentPage, int count, int pageSize, string currentUrl, string queryString,
                          string pageQueryString = "page")
        {
            this.currentUrl = currentUrl;
            this.queryString = queryString;

            var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            PageSize = pageSize;
            Init(currentPage, totalPages, pageQueryString);
        }

        public int CurrentPage { get; private set; }
        public int TotalPages { get; private set; }
        public string Controller { get; private set; }
        public string Action { get; private set; }
        public bool Shorten { get; private set; }
        public int Start { get; private set; }
        public int Stop { get; private set; }

        public string PageQueryString { get; private set; }
        public int PageSize { get; private set; }
        public string queryString { get; private set; }
        public string currentUrl { get; private set; }

        /// <summary>
        /// 用于JS的页码模板
        /// </summary>
        public string Js_tmp_pageindex
        {
            get { return __js_tmp_pageindex__; }
            set { __js_tmp_pageindex__ = value; }
        }

        public string BuildUrlString(string sk, string sv)
        {
            var ubuilder = new StringBuilder(80);
            bool keyFound = false;
            int num = (queryString != null) ? queryString.Length : 0;
            for (int i = 0; i < num; i++)
            {
                int startIndex = i;
                int num4 = -1;
                while (i < num)
                {
                    char ch = queryString[i];
                    if (ch == '=')
                    {
                        if (num4 < 0)
                        {
                            num4 = i;
                        }
                    }
                    else if (ch == '&')
                    {
                        break;
                    }
                    i++;
                }
                string skey = null;
                string svalue;
                if (num4 >= 0)
                {
                    skey = queryString.Substring(startIndex, num4 - startIndex);
                    svalue = queryString.Substring(num4 + 1, (i - num4) - 1);
                }
                else
                {
                    svalue = queryString.Substring(startIndex, i - startIndex);
                }
                ubuilder.Append(skey).Append("=");
                if (skey == sk)
                {
                    keyFound = true;
                    ubuilder.Append(sv);
                }
                else
                    ubuilder.Append(svalue);
                ubuilder.Append("&");
            }
            if (!keyFound)
                ubuilder.Append(sk).Append("=").Append(sv);


            ubuilder.Insert(0, "?").Insert(0, currentUrl);
            return ubuilder.ToString().Trim('&');
        }

        public string BuildALink(int pageindex, string pageText = null)
        {
            return string.Format("<a href=\"{0}\">{1}<a>", BuildUrlString(PageQueryString, pageindex.ToString()),
                                 pageText ?? pageindex.ToString());
        }

        /// <summary>
        /// 用于JS的
        /// </summary>
        /// <returns></returns>
        public string BuildALinkTemp()
        {
            return BuildUrlString(PageQueryString, "__js_tmp_pageindex__");
        }


        public string BuildLinkUrl(int pageindex)
        {
            return BuildUrlString(PageQueryString, pageindex.ToString());
        }


        private void Init(int currentPage, int totalPages, string pageQueryString)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;

            PageQueryString = pageQueryString;
            Shorten = TotalPages >= 10;

            Start = 2;
            Stop = TotalPages - 1;

            if (Shorten)
            {
                if (CurrentPage - 3 > 0) Start = CurrentPage - 2;
                if (Start == 3 && CurrentPage == 5) Start--;

                if (TotalPages - CurrentPage > 4) Stop = CurrentPage + 2;


                if (Start == 1) Start++;
                if (Stop == TotalPages || Stop > TotalPages) Stop = TotalPages - 1;
            }
        }
    }
}