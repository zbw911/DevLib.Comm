//// ***********************************************************************************
////  Created by zbw911 
////  创建于：2013年06月07日 14:25
////  
////  修改于：2013年09月17日 11:32
////  文件名：Dev.Libs/Dev.Comm.Core/StrUtil.cs
////  
////  如果有更好的建议或意见请邮件至 zbw911#gmail.com
//// ***********************************************************************************

//using System;
//using System.Collections;
//using System.IO;
//using System.IO.Compression;
//using System.Text;
//using System.Text.RegularExpressions;

//namespace Dev.Comm.Utils
//{
//    public class StrUtil
//    {
//        //private static readonly char[] _markupChar = {' ', ' ', ' ', '<', '>', '&', '"', '*', '/'};

//        //private static readonly string[] _replaceString =
//        //    {
//        //        "&ensp;", "&emsp;", "&nbsp;", "&lt;", "&gt;", "&amp;",
//        //        "&quot;", "&times;", "&divide;"
//        //    };

//        //public static string FormatDateValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        DateTime value = Convert.ToDateTime(AValue);
//        //        return value.ToString("yyyy-MM-dd");
//        //    }
//        //}

//        //public static string FormatTimeValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        DateTime value = Convert.ToDateTime(AValue);
//        //        return value.ToString("HH:mm:ss");
//        //    }
//        //}

//        //public static string FormatDateTimeValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        DateTime value = Convert.ToDateTime(AValue);
//        //        if (value.Equals(DateTime.MinValue))
//        //            return "";
//        //        return value.ToString("yyyy-MM-dd HH:mm:ss");
//        //    }
//        //}

//        //public static string FormatDateTimeValue(object AValue, string FormatStr)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        DateTime value = Convert.ToDateTime(AValue);
//        //        return value.ToString(FormatStr);
//        //    }
//        //}

//        //public static string FormatMoneyValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        Decimal value = Convert.ToDecimal(AValue);
//        //        return value.ToString("￥#0.00#");
//        //    }
//        //}

//        //public static string FormatDecimalValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        Decimal value = Convert.ToDecimal(AValue);
//        //        return value.ToString("#0.00#");
//        //    }
//        //}

//        //public static string FormatPercentValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        Decimal value = Convert.ToDecimal(AValue);
//        //        return value.ToString("0.00%");
//        //    }
//        //}

//        //public static string FormatEnumValue(object AValue, Type enums)
//        //{
//        //    if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    else
//        //    {
//        //        var tmp = Enum.Parse(enums, AValue.ToString()) as Enum;
//        //        return tmp.ToString("G").Equals(AValue) ? string.Empty : tmp.ToString("G");
//        //    }
//        //}

//        //public static string FormatBoolValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue))
//        //    {
//        //        return "true";
//        //    }
//        //    else
//        //    {
//        //        bool value = Convert.ToBoolean(AValue);
//        //        return value ? "true" : "false";
//        //    }
//        //}

//        //public static string FormatZHBoolValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue))
//        //    {
//        //        return "是";
//        //    }
//        //    else
//        //    {
//        //        bool value = Convert.ToBoolean(AValue);
//        //        return value ? "是" : "否";
//        //    }
//        //}


//        //public static string FormatValue(object AValue)
//        //{
//        //    if (ObjUtil.IsNull(AValue))
//        //    {
//        //        return String.Empty;
//        //    }
//        //    switch (AValue.GetType().FullName.ToLower())
//        //    {
//        //        case "system.decimal":
//        //            return FormatDecimalValue(AValue);
//        //        case "system.double":
//        //            return FormatDecimalValue(AValue);
//        //        case "system.datetime":
//        //            return FormatDateTimeValue(AValue);
//        //        case "system.boolean":
//        //            return FormatZHBoolValue(AValue);
//        //        case "system.enum":
//        //            return FormatEnumValue(AValue, AValue.GetType());
//        //        default:
//        //            if (ObjUtil.IsNull(AValue))
//        //            {
//        //                return String.Empty;
//        //            }
//        //            else
//        //            {
//        //                return AValue.ToString();
//        //            }
//        //    }
//        //}

//        //public static string ReplaceMarkupChar(string source)
//        //{
//        //    for (int i = 0; i < _replaceString.Length; i++)
//        //        source = source.Replace(_replaceString[i], _markupChar[i].ToString());

//        //    return source;
//        //}

//        //public static string List2String(IList list)
//        //{
//        //    return List2String(list, ',');
//        //}

//        //public static string List2String(IList list, char separator)
//        //{
//        //    var sb = new StringBuilder();
//        //    if (list == null) return string.Empty;
//        //    foreach (var o in list)
//        //    {
//        //        if (sb.Length != 0)
//        //            sb.Append(separator);
//        //        sb.Append(o);
//        //    }
//        //    return sb.ToString();
//        //}

//        //public static void ParseMatrix(string Format, out string Matrix, out int MatrixCol, out string MatrixDisplay)
//        //{
//        //    Matrix = "";
//        //    MatrixCol = 0;
//        //    MatrixDisplay = "0";

//        //    string[] str = Format.Split("=".ToCharArray());

//        //    if (str.Length > 0)
//        //        Matrix = str[0];

//        //    if (str.Length > 1)
//        //    {
//        //        string[] tempstr = str[1].Split(",".ToCharArray());
//        //        if (tempstr.Length > 0)
//        //            int.TryParse(tempstr[0], out MatrixCol);

//        //        if (tempstr.Length > 1)
//        //            MatrixDisplay = tempstr[1];
//        //    }
//        //}

//        //#region 转换ASCII和CHAR

//        //public static int Asc(string character)
//        //{
//        //    if (character.Length == 1)
//        //    {
//        //        var asciiEncoding = new ASCIIEncoding();
//        //        int intAsciiCode = asciiEncoding.GetBytes(character)[0];
//        //        return (intAsciiCode);
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("Character is not valid.");
//        //    }
//        //}

//        //public static string Chr(int asciiCode)
//        //{
//        //    if (asciiCode >= 0 && asciiCode <= 255)
//        //    {
//        //        var asciiEncoding = new ASCIIEncoding();
//        //        var byteArray = new[] {(byte) asciiCode};
//        //        string strCharacter = asciiEncoding.GetString(byteArray);
//        //        return (strCharacter);
//        //    }
//        //    else
//        //    {
//        //        throw new Exception("ASCII Code is not valid.");
//        //    }
//        //}

//        //#endregion

//        //#region 压缩解压缩

//        //public static string Compress(string target)
//        //{
//        //    Encoding encoding = Encoding.GetEncoding("gb2312");
//        //    return Compress(target, encoding);
//        //}

//        //public static string Compress(string target, Encoding encoding)
//        //{
//        //    using (var ms = new MemoryStream())
//        //    {
//        //        using (var compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true))
//        //        {
//        //            //GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
//        //            byte[] buffer = encoding.GetBytes(target);
//        //            compressedzipStream.Write(buffer, 0, buffer.Length);
//        //            compressedzipStream.Close();
//        //            return Convert.ToBase64String(ms.ToArray());
//        //        }
//        //    }
//        //}

//        //public static string DeCompress(string target)
//        //{
//        //    Encoding encoding = Encoding.GetEncoding("gb2312");
//        //    return DeCompress(target, encoding);
//        //}

//        //public static string DeCompress(string target, Encoding encoding)
//        //{
//        //    using (var ms = new MemoryStream())
//        //    {
//        //        byte[] bytes = Convert.FromBase64String(target);
//        //        ms.Write(bytes, 0, bytes.Length);
//        //        ms.Position = 0;
//        //        //GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress);
//        //        using (var zipStream = new GZipStream(ms, CompressionMode.Decompress))
//        //        {
//        //            var sb = new StringBuilder();

//        //            while (true)
//        //            {
//        //                var buffer = new byte[100];
//        //                int result = zipStream.Read(buffer, 0, buffer.Length);
//        //                if (result == 0)
//        //                    break;

//        //                sb.Append(encoding.GetString(buffer, 0, result));
//        //            }

//        //            zipStream.Close();
//        //            return sb.ToString();
//        //        }
//        //    }
//        //}

//        //#endregion

//        //#region 转换矩阵为HTML格式

//        ////0 第一行数字 1 第一行字母
//        //public static string Matrix2Html(string Matrix, int MatrixCol, string MatrixDisplay)
//        //{
//        //    if (MatrixCol == 0) return "";
//        //    string[] ary = Matrix.Split(',');
//        //    int len = ary.Length;
//        //    int rowcount = len/MatrixCol;
//        //    var vTable = new StringBuilder();
//        //    int code = 64;

//        //    string normal =
//        //        "border-bottom: 1px solid #BCBCBC;border-right: 1px solid #BCBCBC;padding: 5px;text-align: center;";
//        //    string th =
//        //        "text-align:center; padding:5px; border-bottom:1px solid #BCBCBC; border-right:1px solid #BCBCBC;background:none repeat scroll 0 0 #797979;";
//        //    string sr = "background:#FFF;";
//        //    string dr = "background:#E6E6E6;";

//        //    vTable.Append(
//        //        "<table border='0' cellspacing='0' cellpadding='0' style='border-top:1px solid #666; border-left:1px solid #666; margin:0px auto 0;' >");

//        //    for (int i = 0; i < rowcount + 1; i++)
//        //    {
//        //        vTable.Append("<tr");

//        //        //if (i == 0)
//        //        //    vTable.AppendFormat(" style='width:15px;{0}'>", th);
//        //        //else
//        //        vTable.Append(">");

//        //        for (int j = 0; j < MatrixCol + 1; j++)
//        //        {
//        //            vTable.Append("<td");

//        //            if (j == 0 && i == 0)
//        //            {
//        //                vTable.AppendFormat(" style='width:15px;{0}'>&nbsp;</td>", th);
//        //                continue;
//        //            }

//        //            if (j == 0)
//        //            {
//        //                string left = string.Empty;
//        //                if (MatrixDisplay == "0" || MatrixDisplay == "")
//        //                    left = Chr(code + i);
//        //                else
//        //                    left = i.ToString();

//        //                vTable.AppendFormat(" style='{0}'>{1}</td>", th, left);
//        //                continue;
//        //            }

//        //            if (i == 0)
//        //            {
//        //                string top = string.Empty;
//        //                if (MatrixDisplay == "0" || MatrixDisplay == "")
//        //                    top = j.ToString();
//        //                else
//        //                    top = Chr(code + j);

//        //                vTable.AppendFormat(" style='width:15px;{0}'>{1}</td>", th, top);
//        //                continue;
//        //            }

//        //            string ys = string.Empty;
//        //            if (i%2 == 0)
//        //                ys = sr;
//        //            else
//        //                ys = dr;

//        //            string value = string.Empty;
//        //            int wz = (i - 1)*MatrixCol + (j - 1);
//        //            if (wz < ary.Length)
//        //                value = ary[wz];
//        //            else
//        //                value = "&nbsp;";

//        //            vTable.AppendFormat(" style='{0}{1}'>{2}</td>", normal, ys, value);
//        //        }

//        //        vTable.Append("</tr>");
//        //    }
//        //    return vTable.ToString();
//        //}

//        //#endregion

//        //#region 固定位置插入制定字符串

//        //public static string CycleInsert(string Source, int Cyc, int StartIndex, string Target)
//        //{
//        //    if (ObjUtil.IsNull(Source)) return Source;
//        //    if (Cyc <= 0) return Source;
//        //    if (StartIndex < 0) return Source;
//        //    if (ObjUtil.IsNull(Target)) return Source;
//        //    if (Cyc <= Target.Length) return Source;

//        //    if (Source.Length - 1 < Cyc + StartIndex) return Source;

//        //    Source = Source.Insert(StartIndex + Cyc, Target);

//        //    Source = CycleInsert(Source, Cyc, Cyc + StartIndex, Target);

//        //    return Source;
//        //}

//        //#endregion

//        //#region 获取html字符串的innertext

//        //public static string FormatHtmlInnerText(string AHtml)
//        //{
//        //    var regex = new Regex(@"<[^>]*>");
//        //    AHtml = regex.Replace(AHtml, "");

//        //    AHtml = ReplaceMarkupChar(AHtml);

//        //    return AHtml;
//        //}

//        //#endregion
//    }
//}