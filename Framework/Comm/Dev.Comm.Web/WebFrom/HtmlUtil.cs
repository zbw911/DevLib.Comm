//// ***********************************************************************************
//// Created by zbw911 
//// 创建于：2012年12月18日 10:44
//// 
//// 修改于：2013年02月18日 18:24
//// 文件名：HtmlUtil.cs
//// 
//// 如果有更好的建议或意见请邮件至zbw911#gmail.com
//// ***********************************************************************************

//namespace Dev.Comm.Web.WebFrom
//{
//    using System.Text;
//    using System.Web;
//    using System.Web.UI;
//    using System.Web.UI.WebControls;

//    public class HtmlUtil
//    {
//        public static string ControlInnerText(Control textctl)
//        {
//            var result = new StringBuilder();

//            string tmp = StrUtil.FormatValue(AsmUtil.GetPropertyValue(textctl, "Text", null));
//            tmp = StrUtil.FormatHtmlInnerText(tmp.Trim()).Trim();
//            if (!string.IsNullOrEmpty(tmp))
//            {
//                result.Append(tmp);
//                result.Append(" ");
//            }
//            foreach (Control ctl in textctl.Controls)
//                result.Append(ControlInnerText(ctl));
//            return result.ToString();
//        }

//        public static void MargeCells(TableRow row, int TargetCount)
//        {
//            int CellCnt = row.Cells.Count;

//            if (CellCnt <= TargetCount || TargetCount <= 0) return;

//            int colspan = CellCnt - TargetCount;

//            row.Cells[colspan].ColumnSpan = colspan + 1;

//            for (int i = 0; i < colspan; i++)
//                row.Cells.RemoveAt(0);
//        }

//        /// <summary>
//        /// .net to php
//        /// </summary>
//        /// <param name="p"></param>
//        /// <returns></returns>
//        public static string urldecode(string p)
//        {
//            return HttpUtility.UrlDecode(p);
//        }
//    }
//}