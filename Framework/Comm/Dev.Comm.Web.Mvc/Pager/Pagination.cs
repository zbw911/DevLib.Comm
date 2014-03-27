// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年03月01日 11:19
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/Pagination.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Comm.Web.Mvc.Pager
{
    using System;
    using System.Web.Routing;

    /// <summary>
    /// </summary>
    public class Pagination
    {
        //public Pagination(int currentPage, int totalPages, string action, string controller, object routeValues = null, string pageQueryString = "page")
        //{
        //    RouteValueDictionary routeDictionary = new RouteValueDictionary(routeValues);
        //    routeDictionary.Add("controller", controller);
        //    routeDictionary.Add("action", action);
        //    this.Init(currentPage, totalPages, routeDictionary, pageQueryString);
        //}

        #region Constructors and Destructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="routeValues"></param>
        /// <param name="pageQueryString"></param>
        public Pagination(
            int currentPage,
            int count,
            int pageSize,
            string action,
            string controller,
            object routeValues = null,
            string pageQueryString = "page")
            : this(
                currentPage, count, pageSize, action, controller, new RouteValueDictionary(routeValues), pageQueryString
                )
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="currentPage"> </param>
        /// <param name="count"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="action"> </param>
        /// <param name="controller"> </param>
        /// <param name="routeDictionary"> </param>
        /// <param name="pageQueryString"> </param>
        public Pagination(
            int currentPage,
            int count,
            int pageSize,
            string action,
            string controller,
            RouteValueDictionary routeDictionary = null,
            string pageQueryString = "page")
        {
            routeDictionary = routeDictionary ?? new RouteValueDictionary();

            routeDictionary.Add("controller", controller);
            routeDictionary.Add("action", action);
            var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            this.PageSize = pageSize;
            this.Init(currentPage, totalPages, routeDictionary, pageQueryString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="routeValues"></param>
        /// <param name="pageQueryString"></param>
        public Pagination(int currentPage, int count, int pageSize, object routeValues, string pageQueryString = "page")
        {
            var routeDictionary = new RouteValueDictionary(routeValues);
            this.PageSize = pageSize;
            var totalPages = (int)Math.Ceiling((decimal)count / pageSize);
            this.Init(currentPage, totalPages, routeDictionary, pageQueryString);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="count"></param>
        /// <param name="pageSize"></param>
        /// <param name="routeDictionary"></param>
        /// <param name="pageQueryString"></param>
        public Pagination(int currentPage,
            int count,
            int pageSize,
            RouteValueDictionary routeDictionary = null,
            string pageQueryString = "page")
            : this(currentPage, count, pageSize, Dev.Comm.Web.DevRequest.GetString("action"), Dev.Comm.Web.DevRequest.GetString("controller"), routeDictionary, pageQueryString)
        {

        }

        /// <summary>
        ///   AJAX方法调用的构造函数
        /// </summary>
        /// <param name="currentPage"> </param>
        /// <param name="count"> </param>
        /// <param name="pageSize"> </param>
        /// <param name="JavascriptFun"> </param>
        public Pagination(int currentPage, int count, int pageSize, string JavascriptFun)
            : this(currentPage, count, pageSize, "", "", null, "page")
        {
            this.JavascriptFun = JavascriptFun;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// 应用的Action
        /// </summary>
        public string Action { get; private set; }

        /// <summary>
        /// 应用的Controller
        /// </summary>
        public string Controller { get; private set; }

        /// <summary>
        /// </summary>
        public int CurrentPage { get; private set; }

        /// <summary>
        ///   Javascript 方法体 ， 例如 ： @"ajaxpage({0})"
        ///   <![CDATA[
        ///  function ajaxpage(pageNo){
        ///      pageSize, 条件 等从页面中取得或写成固定值。。。
        ///      //  调用 Ajax 的方法 从服务器读取数据
        ///  }
        /// ]]>
        /// </summary>
        public string JavascriptFun { get; set; }

        /// <summary>
        ///  Page用什么参数
        /// </summary>
        public string PageQueryString { get; private set; }

        /// <summary>
        /// 页面大小 
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 路由集合
        /// </summary>
        public RouteValueDictionary RouteValues { get; private set; }

        /// <summary>
        /// 是否超过最大页面，然后决定截断
        /// </summary>
        public bool Shorten { get; private set; }

        /// <summary>
        /// 开始
        /// </summary>
        public int Start { get; private set; }

        /// <summary>
        /// 结束
        /// </summary>
        public int Stop { get; private set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }

        #endregion

        #region Public Methods and Operators

        public RouteValueDictionary GetDictionary(int pageNumber)
        {
            this.RouteValues[this.PageQueryString] = pageNumber;
            return this.RouteValues;
        }

        #endregion

        #region Methods

        private void Init(int currentPage, int totalPages, RouteValueDictionary routeValues, string pageQueryString)
        {
            this.CurrentPage = currentPage;
            this.TotalPages = totalPages;
            this.Controller = (string)routeValues["controller"];
            this.Action = (string)routeValues["action"];
            this.PageQueryString = pageQueryString;
            this.Shorten = this.TotalPages >= 10;

            this.Start = 2;
            this.Stop = this.TotalPages - 1;

            if (this.Shorten)
            {
                if (this.CurrentPage - 3 > 0)
                {
                    this.Start = this.CurrentPage - 2;
                }
                if (this.Start == 3 && this.CurrentPage == 5)
                {
                    this.Start--;
                }

                if (this.TotalPages - this.CurrentPage > 4)
                {
                    this.Stop = this.CurrentPage + 2;
                }

                if (this.Start == 1)
                {
                    this.Start++;
                }
                if (this.Stop == this.TotalPages || this.Stop > this.TotalPages)
                {
                    this.Stop = this.TotalPages - 1;
                }
            }

            routeValues.Add(pageQueryString, 0);
            routeValues.Add("pagesize", this.PageSize);
            this.RouteValues = routeValues;
        }

        #endregion
    }
}