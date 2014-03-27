// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ExportData.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Dev.Comm.Web
{
    public class ExportData
    {
        //文档类型 

        #region DocumentType enum

        public enum DocumentType
        {
            Word,
            Excel
        }

        #endregion

        /// <summary> 
        /// 将Web控件导出 
        /// </summary> 
        /// <param name="source">控件实例</param> 
        /// <param name="type">类型:Excel或Word</param> 
        public static void ExpertControl(Control source, string filename, DocumentType type)
        {
            //设置Http的头信息,编码格式 
            if (type == DocumentType.Excel)
            {
                //Excel 
                HttpContext.Current.Response.AppendHeader("Content-Disposition",
                                                          "attachment;filename=" + filename + ".xls");
                HttpContext.Current.Response.ContentType = "application/ms-excel";
            }
            else if (type == DocumentType.Word)
            {
                //Word 
                HttpContext.Current.Response.AppendHeader("Content-Disposition",
                                                          "attachment;filename=" + filename + ".doc");
                HttpContext.Current.Response.ContentType = "application/ms-word";
            }
            HttpContext.Current.Response.Charset = "UTF-8";
            HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;

            //关闭控件的视图状态 
            source.Page.EnableViewState = false;

            //初始化HtmlWriter 
            var writer = new StringWriter();
            var htmlWriter = new HtmlTextWriter(writer);
            source.RenderControl(htmlWriter);

            //输出 
            HttpContext.Current.Response.Write(writer.ToString());
            HttpContext.Current.Response.End();
        }
    }
}