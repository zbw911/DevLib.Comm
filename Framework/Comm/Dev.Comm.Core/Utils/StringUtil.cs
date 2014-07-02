// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/StringUtil.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.RegularExpressions;

namespace Dev.Comm.Utils
{
    public class StringUtil
    {
        #region 对字符串进行有效的HTML转换

        /// <summary>
        ///   对字符串进行有效的HTML转换
        /// </summary>
        /// <param name="inputString"> 字符串 </param>
        /// <param name="maxLength"> 最多长度 </param>
        /// <returns> </returns>
        public static string InputText(string inputString, int maxLength)
        {
            // 定义一个可变字符字符串
            var retVal = new StringBuilder();
            // 检测字符串是否为空
            if ((inputString != null) && (inputString != String.Empty))
            {
                // 去掉空格
                inputString = inputString.Trim();

                // 取最大长度，多余的截取掉
                if (inputString.Length > maxLength)
                    inputString = inputString.Substring(0, maxLength);

                // 把字符转换为HTML字符
                for (int i = 0; i < inputString.Length; i++)
                {
                    switch (inputString[i])
                    {
                        case '"':
                            retVal.Append("&quot;");
                            break;
                        case '<':
                            retVal.Append("&lt;");
                            break;
                        case '>':
                            retVal.Append("&gt;");
                            break;
                        default:
                            retVal.Append(inputString[i]);
                            break;
                    }
                }
                retVal.Replace("'", " ");
            }

            return retVal.ToString();
        }

        #endregion

        public static string GetJsonValue(string str, string key)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return "";
            }

            int pos = str.IndexOf(key);
            if (pos < 0)
            {
                return "";
            }

            string temp = str.Substring(pos + 1, str.Length - pos - 1);

            pos = temp.IndexOf(":");
            if (pos < 0)
            {
                return "";
            }

            temp = temp.Substring(pos + 1, temp.Length - pos - 1);

            pos = temp.IndexOf("\"");
            if (pos < 0)
            {
                return "";
            }

            temp = temp.Substring(pos + 1, temp.Length - pos - 1);

            pos = temp.IndexOf("\"");
            if (pos < 0)
            {
                return "";
            }

            string value = temp.Substring(0, pos);

            return value;
        }

        public static string GetDoMain(string url, string key)
        {
            if (string.IsNullOrEmpty(url))
            {
                return "";
            }

            if (string.IsNullOrEmpty(key))
            {
                return url;
            }

            int pos = url.IndexOf(key);

            if (pos < 0)
            {
                return url;
            }
            else
            {
                string rtn = url.Substring(0, pos - 1);
                return rtn;
            }
        }

        public static string inputHtml(string inputString)
        {
            string str = inputString;
            //将此实例中的指定 Unicode 字符的所有匹配项替换为其他指定的 Unicode 字符。
            str = str.Replace("\r", "<br>");
            str = str.Replace(" ", "&nbsp;");
            return str;
        }

        public static string HtmlInputTex(string inputHtml)
        {
            string str = inputHtml;
            str = str.Replace("<br>", "\r");
            str = str.Replace("&nbsp;", " ");
            return str;
        }

        public static string GetPlainText(string inputText, int outNum)
        {
            try
            {
                string tempStr = inputText;
                int num1 = GetStringCount(inputText, "&nbsp;");
                num1 = num1 * 6;
                int num2 = GetStringCount(inputText, "<br>");
                num2 = num2 * 4;
                int numCount = inputText.Length - num1 - num2;

                int forNum = outNum;
                if (outNum > inputText.Length)
                    forNum = inputText.Length;
                if (numCount > forNum)
                {
                    string tempDescr = "";
                    for (int i = 0; i < forNum; i++)
                    {
                        inputText = tempStr.Substring(0, i + 1);
                        tempDescr = inputText.Substring(i, 1);
                        if (tempDescr == "&" || tempDescr == "n" || tempDescr == "b" || tempDescr == "s" ||
                            tempDescr == "p"
                            || tempDescr == ";" || tempDescr == "<" || tempDescr == "b" || tempDescr == "r" ||
                            tempDescr == ">")
                        {
                            forNum = forNum + 1;
                        }
                    }
                }
                return inputText + "...";
            }
            catch
            {
                return inputText = inputText + "..."; // "格式问题，无法显示";
            }
        }

        public static string GetJsonAndReturn(ref string temp, string str, string key)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return "";
            }

            int pos = str.IndexOf(key);
            if (pos < 0)
            {
                return "";
            }

            temp = str.Substring(pos + 1, str.Length - pos - 1);

            pos = temp.IndexOf(":");
            if (pos < 0)
            {
                return "";
            }

            temp = temp.Substring(pos + 1, temp.Length - pos - 1);

            pos = temp.IndexOf("\"");
            if (pos < 0)
            {
                return "";
            }

            temp = temp.Substring(pos + 1, temp.Length - pos - 1);

            pos = temp.IndexOf("\"");
            if (pos < 0)
            {
                return "";
            }

            string value = temp.Substring(0, pos);

            return value;
        }


        /// <summary>
        ///   Returns a string with backslashes before characters that need to be quoted
        /// </summary>
        /// <param name="InputTxt"> Text string need to be escape with slashes </param>
        public static string AddSlashes(string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            //try
            //{
            Result = Regex.Replace(InputTxt, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
            //}
            //catch (Exception Ex)
            //{
            //    // handle any exception here
            //    Console.WriteLine(Ex.Message);
            //}

            return Result;
        }

        /// <summary>
        ///   Un-quotes a quoted string
        /// </summary>
        /// <param name="InputTxt"> Text string need to be escape with slashes </param>
        public static string StripSlashes(string InputTxt)
        {
            // List of characters handled:
            // \000 null
            // \010 backspace
            // \011 horizontal tab
            // \012 new line
            // \015 carriage return
            // \032 substitute
            // \042 double quote
            // \047 single quote
            // \134 backslash
            // \140 grave accent

            string Result = InputTxt;

            //try
            //{
            Result = Regex.Replace(InputTxt, @"(\\)([\000\010\011\012\015\032\042\047\134\140])", "$2");
            //}
            //catch (Exception Ex)
            //{
            //    // handle any exception here
            //    Console.WriteLine(Ex.Message);
            //}

            return Result;
        }


        /// <summary>
        ///   批量进行替换
        /// </summary>
        /// <param name="oldvalue"> </param>
        /// <param name="newvalue"> </param>
        /// <param name="content"> </param>
        /// <returns> </returns>
        public static string ReplaceBat(IList<string> oldvalue, IList<string> newvalue, string content)
        {
            //StringBuilder sb = new StringBuilder(content);

            for (int i = 0; i < oldvalue.Count; i++)
            {
                content = content.Replace(oldvalue[i], newvalue[i]);
                //sb = sb.Replace(dest[i], source[i]);
            }

            return content;
            //return sb.ToString();
        }


        /// <summary>
        ///   用分隔符连接多个串
        /// </summary>
        /// <param name="strs"> </param>
        /// <param name="spliter"> </param>
        /// <returns> </returns>
        public static string ConcatStrs(string[] strs, string spliter = "")
        {
            var sb = new StringBuilder();

            foreach (var str in strs)
            {
                if (sb.Length > 0)
                {
                    sb.Append(spliter);
                }
                sb.Append(str);
            }
            return sb.ToString();
        }

        /// <summary>
        ///   按字节数截取,并去除半个汉字
        /// </summary>
        /// <param name="str"> 字符串 </param>
        /// <param name="len"> 截取长度 </param>
        /// <returns> </returns>
        public static string CutGBStr(string str, int len, string dot = "")
        {
            if (string.IsNullOrEmpty(str)) return "";


            if (len >= Encoding.Default.GetByteCount(str))
                return str;


            string strRe = "";
            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                strRe += str.Substring(i, 1);
                count += Encoding.Default.GetByteCount(str.Substring(i, 1));
                if (count >= len)
                    break;
            }
            if (count > len)//截取字符串，最后如果是半个中文，舍掉最后的半个
                strRe = strRe.Substring(0, strRe.Length - 1);
            return strRe + dot;
        }


        public static int GetGBStrLen(string str)
        {
            return Encoding.Default.GetByteCount(str);
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        ///   去除HTML标记
        /// </summary>
        /// <param name="AHtml"> </param>
        /// <returns> </returns>
        public static string Strip_Tags(string AHtml)
        {
            if (AHtml == null)
                return null;
            var regex = new Regex(@"<[^>]*>");
            AHtml = regex.Replace(AHtml, "");

            AHtml = ReplaceMarkupChar(AHtml);

            return AHtml;
        }


        /// <summary>
        ///   计算文本长度，区分中英文字符，中文算两个长度，英文算一个长度
        /// </summary>
        /// <param name="Text"> 需计算长度的字符串 </param>
        /// <returns> int </returns>
        public static int GbStrLength(string Text)
        {
            int len = 0;

            for (int i = 0; i < Text.Length; i++)
            {
                byte[] byte_len = Encoding.Default.GetBytes(Text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2; //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1; //如果长度等于1，是英文，占一个字节，+1
            }

            return len;
        }


        public static int[] SplitIntString(string strContent, string strSplit, bool RemoveEmpty = true)
        {
            string[] list = SplitString(strContent, strSplit, RemoveEmpty);
            if (list.Length == 0) return new int[0] { };

            var intlist = new List<int>();
            for (int i = 0; i < list.Length; i++)
            {
                if (RemoveEmpty && !string.IsNullOrWhiteSpace(list[i]))
                    intlist.Add(int.Parse(list[i]));
                else
                {
                    int x;
                    int.TryParse(list[i], out x);

                    intlist.Add(x);
                }
            }
            return intlist.ToArray();
        }


        /// <summary>
        ///   分割字符串
        /// </summary>
        public static string[] SplitString(string strContent, string strSplit, bool RemoveEmpty = true)
        {
            if (!string.IsNullOrEmpty(strContent))
            {
                if (strContent.IndexOf(strSplit) < 0)
                    return new[] { strContent };

                if (RemoveEmpty)
                    return strContent.Split(strSplit.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                else
                    return Regex.Split(strContent, Regex.Escape(strSplit), RegexOptions.IgnoreCase);
            }
            else
                return new string[0] { };
        }

        /// <summary>
        ///   分割字符串
        /// </summary>
        /// <returns> </returns>
        public static string[] SplitString(string strContent, string strSplit, int count)
        {
            var result = new string[count];
            string[] splited = SplitString(strContent, strSplit);

            for (int i = 0; i < count; i++)
            {
                if (i < splited.Length)
                    result[i] = splited[i];
                else
                    result[i] = string.Empty;
            }

            return result;
        }

        /// <summary>
        ///   字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            if (str == null || str.Trim() == string.Empty)
                return true;

            return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////


        public static string GetPrecetStr(decimal x1, decimal all, int point = 2)
        {
            all = all == 0 ? 1 : all;

            decimal result = x1 / all;

            return result.ToString(string.Format("p{0}", point));
        }


        public static string GetPrecetStr(double x1, double all, int point = 2)
        {
            all = all == 0 ? 1 : all;

            double result = x1 / all;

            return result.ToString(string.Format("p{0}", point));
        }


        //public static string GetPrecetStr(int x1, int x2, int point = 2)
        //{
        //    x2 = x2 == 0 ? 1 : x2;

        //    var result = x1 / x2;

        //    return result.ToString(string.Format("p{0}", point));
        //}


        public static string GetPrecetStr(decimal strPercent, int point)
        {
            return strPercent.ToString(string.Format("p{0}", point));
        }

        public static int strpos(string str, string find)
        {
            return str.IndexOf(find);
        }

        #region 字符是否小写

        /// <summary>
        ///   字符是否小写
        /// </summary>
        /// <param name="ch"> 字符 </param>
        /// <returns> bool </returns>
        public static bool isLower(char ch)
        {
            if (ch >= 'a' && ch <= 'z')
                return true;
            else
                return false;
        }

        #endregion

        #region 字符是否大写

        /// <summary>
        ///   字符是否大写
        /// </summary>
        /// <param name="ch"> 字符 </param>
        /// <returns> bool </returns>
        public static bool isUpper(char ch)
        {
            if (ch >= 'A' && ch <= 'Z')
                return true;
            else
                return false;
        }

        #endregion

        #region 输入的字符是否是数字

        /// <summary>
        ///   输入的字符是否是数字
        /// </summary>
        /// <param name="ch"> 一个字符 </param>
        /// <returns> bool </returns>
        public static bool isNumberic(char ch)
        {
            if (ch >= '0' && ch <= '9')
                return true;
            else
                return false;
        }

        #endregion

        #region 特殊字符检验

        /// <summary>
        ///   特殊字符检验
        /// </summary>
        /// <param name="ch"> 特殊字符 </param>
        /// <returns> </returns>
        public static bool isSpecialCharacter(char ch)
        {
            if (ch == '!')
                return true;
            else if (ch == '@')
                return true;
            else if (ch == '#')
                return true;
            else if (ch == '$')
                return true;
            else if (ch == '^')
                return true;
            else if (ch == '&')
                return true;
            else if (ch == '*')
                return true;
            else if (ch == '?')
                return true;
            else if (ch == '/')
                return true;
            else if (ch == '\\')
                return true;
            else
                return false;
        }

        #endregion

        #region 从字符串中的尾部删除指定的字符串

        /// <summary>
        ///   从字符串中的尾部删除指定的字符串
        /// </summary>
        /// <param name="sourceString"> </param>
        /// <param name="removedString"> </param>
        /// <returns> </returns>
        public static string Remove(string sourceString, string removedString)
        {
            try
            {
                if (sourceString.IndexOf(removedString) < 0) //判断删除的字符串是否存在
                    throw new Exception("原字符串中不包含移除字符串！");
                string result = sourceString;
                int lengthOfSourceString = sourceString.Length;
                int lengthOfRemovedString = removedString.Length;
                int startIndex = lengthOfSourceString - lengthOfRemovedString;
                string tempSubString = sourceString.Substring(startIndex);
                if (tempSubString.ToUpper() == removedString.ToUpper())
                {
                    result = sourceString.Remove(startIndex, lengthOfRemovedString);
                }
                return result;
            }
            catch
            {
                return sourceString;
            }
        }

        #endregion

        #region 获取拆分符右边的字符串

        /// <summary>
        ///   获取拆分符右边的字符串
        /// </summary>
        /// <param name="sourceString"> </param>
        /// <param name="splitChar"> </param>
        /// <returns> </returns>
        public static string RightSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[tempString.Length - 1];
            }
            return result;
        }

        #endregion

        #region 获取拆分符左边的字符串

        /// <summary>
        ///   获取拆分符左边的字符串
        /// </summary>
        /// <param name="sourceString"> </param>
        /// <param name="splitChar"> </param>
        /// <returns> </returns>
        public static string LeftSplit(string sourceString, char splitChar)
        {
            string result = null;
            string[] tempString = sourceString.Split(splitChar);
            if (tempString.Length > 0)
            {
                result = tempString[0];
            }
            return result;
        }

        #endregion

        #region 去掉最后一个逗号

        /// <summary>
        ///   去掉最后一个逗号
        /// </summary>
        /// <param name="origin"> </param>
        /// <returns> </returns>
        public static string DelLastComma(string origin)
        {
            if (origin.IndexOf(",") == -1)
            {
                return origin;
            }
            return origin.Substring(0, origin.LastIndexOf(","));
        }

        #endregion

        #region 删除不可见字符

        /// <summary>
        ///   删除不可见字符
        /// </summary>
        /// <param name="sourceString"> </param>
        /// <returns> </returns>
        public static string DeleteUnVisibleChar(string sourceString)
        {
            var sBuilder = new StringBuilder(131);
            for (int i = 0; i < sourceString.Length; i++)
            {
                int Unicode = sourceString[i];
                if (Unicode >= 16)
                {
                    sBuilder.Append(sourceString[i].ToString());
                }
            }
            return sBuilder.ToString();
        }

        #endregion

        #region 获取数组元素的合并字符串

        /// <summary>
        ///   获取数组元素的合并字符串
        /// </summary>
        /// <param name="stringArray"> </param>
        /// <returns> </returns>
        public static string GetArrayString(string[] stringArray)
        {
            string totalString = null;
            for (int i = 0; i < stringArray.Length; i++)
            {
                totalString = totalString + stringArray[i];
            }
            return totalString;
        }

        #endregion

        #region 获取某一字符串在字符串数组中出现的次数

        /// <summary>
        ///   获取某一字符串在字符串数组中出现的次数
        /// </summary>
        /// <param name="stringArray"> 字符数字 </param>
        /// <param name="findString"> 寻找的字符串 </param>
        /// <returns> INT </returns>
        public static int GetStringCount(string[] stringArray, string findString)
        {
            int count = -1;
            string totalString = GetArrayString(stringArray); //获取数组元素的合并字符串	
            string subString = totalString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = totalString.Substring(subString.IndexOf(findString));
                count += 1;
            }
            return count;
        }

        #endregion

        #region 获取某一字符串在字符串中出现的次数

        /// <summary>
        ///   获取某一字符串在字符串中出现的次数
        /// </summary>
        /// <param name="stringArray" type="string">
        ///   <para> 原字符串 </para>
        /// </param>
        /// <param name="findString" type="string">
        ///   <para> 匹配字符串 </para>
        /// </param>
        /// <returns> 匹配字符串数量 </returns>
        public static int GetStringCount(string sourceString, string findString)
        {
            int count = 0;
            int findStringLength = findString.Length;
            string subString = sourceString;

            while (subString.IndexOf(findString) >= 0)
            {
                subString = subString.Substring(subString.IndexOf(findString) + findStringLength);
                count += 1;
            }
            return count;
        }

        #endregion

        #region 截取从startString开始到原字符串结尾的所有字符

        /// <summary>
        ///   截取从startString开始到原字符串结尾的所有字符
        /// </summary>
        /// <param name="sourceString" type="string">
        ///   <para> </para>
        /// </param>
        /// <param name="startString" type="string">
        ///   <para> </para>
        /// </param>
        /// <returns> A string value... </returns>
        public static string GetSubString(string sourceString, string startString)
        {
            try
            {
                int index = sourceString.ToUpper().IndexOf(startString);
                if (index > 0)
                {
                    return sourceString.Substring(index);
                }
                return sourceString;
            }
            catch
            {
                return "";
            }
        }

        #endregion

        #region ????

        /// <summary>
        /// </summary>
        /// <param name="sourceString"> </param>
        /// <param name="beginRemovedString"> </param>
        /// <param name="endRemovedString"> </param>
        /// <returns> </returns>
        public static string GetSubString(string sourceString, string beginRemovedString, string endRemovedString)
        {
            try
            {
                if (sourceString.IndexOf(beginRemovedString) != 0)
                    beginRemovedString = "";

                if (sourceString.LastIndexOf(endRemovedString, sourceString.Length - endRemovedString.Length) < 0)
                    endRemovedString = "";

                int startIndex = beginRemovedString.Length;
                int length = sourceString.Length - beginRemovedString.Length - endRemovedString.Length;
                if (length > 0)
                {
                    return sourceString.Substring(startIndex, length);
                }
                return sourceString;
            }
            catch
            {
                return sourceString;
                ;
            }
        }

        #endregion

        #region 按字节数取出字符串的长度

        /// <summary>
        ///   按字节数取出字符串的长度
        /// </summary>
        /// <param name="strTmp"> 要计算的字符串 </param>
        /// <returns> 字符串的字节数 </returns>
        public static int GetByteCount(string strTmp)
        {
            int intCharCount = 0;
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intCharCount = intCharCount + 2;
                }
                else
                {
                    intCharCount = intCharCount + 1;
                }
            }
            return intCharCount;
        }

        #endregion

        #region 按字节数要在字符串的位置

        /// <summary>
        ///   按字节数要在字符串的位置
        /// </summary>
        /// <param name="intIns"> 字符串的位置 </param>
        /// <param name="strTmp"> 要计算的字符串 </param>
        /// <returns> 字节的位置 </returns>
        public static int GetByteIndex(int intIns, string strTmp)
        {
            int intReIns = 0;
            if (strTmp.Trim() == "")
            {
                return intIns;
            }
            for (int i = 0; i < strTmp.Length; i++)
            {
                if (Encoding.UTF8.GetByteCount(strTmp.Substring(i, 1)) == 3)
                {
                    intReIns = intReIns + 2;
                }
                else
                {
                    intReIns = intReIns + 1;
                }
                if (intReIns >= intIns)
                {
                    intReIns = i + 1;
                    break;
                }
            }
            return intReIns;
        }

        #endregion

        #region 去掉字符中的空格

        /// <summary>
        ///   去掉字符中的空格
        /// </summary>
        /// <param name="str"> </param>
        /// <returns> </returns>
        public static string RemoveMiddleSpace(string str)
        {
            char[] ch = str.ToCharArray();
            var sb = new StringBuilder();

            foreach (var c in ch)
            {
                if (char.IsWhiteSpace(c))
                {
                    continue;
                }
                else
                {
                    sb.Append(c.ToString());
                }
            }
            return sb.ToString();
        }

        #endregion

        #region 记算流水号

        /// <summary>
        ///   记算流水号
        /// </summary>
        /// <param name="strId"> 开始字符串 </param>
        /// <param name="i"> 流水位数 </param>
        /// <returns> 字符串 </returns>
        private static string DoInc(string strId, int i)
        {
            string chrId;
            if (i > 0)
            {
                chrId = strId.Substring(i - 1, 1);
                switch (chrId)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                    case "5":
                    case "6":
                    case "7":
                    case "8":
                        return strId.Substring(0, i - 1) + (int.Parse(chrId) + 1).ToString() +
                               strId.Substring(i, strId.Length - i);
                    case "9":
                        if (i == 1)
                        {
                            return "10" + strId.Substring(1, strId.Length - 1);
                            ;
                        }
                        else
                        {
                            return DoInc(strId.Substring(0, i - 1) + "0" + strId.Substring(i, strId.Length - i), i - 1);
                        }
                    default:
                        return DoInc(strId, i - 1);
                }
            }
            else
            {
                return strId;
            }
        }

        #endregion

        #region 记算流水号

        /// <summary>
        ///   记算流水号
        /// </summary>
        /// <param name="strId"> 输入的字符 </param>
        /// <returns> </returns>
        public static string IncStr(string strId)
        {
            return DoInc(strId, strId.Length);
        }

        #endregion

        #region 按格式取出字符串中变量值-杨栋添加

        public static string GetDataArea(string StrDataArea, string strData)
        {
            int i_PositionBof = 0;
            int i_PositionEof = 0;
            int i_length = 0;
            string StrRet = "";
            //			i_PositionBof = InStr(LCase(strData), LCase(StrDataArea)); //'查找域的起始位置
            i_PositionBof = strData.IndexOf(StrDataArea);
            if (i_PositionBof == -1)
            {
                return "";
            }
            i_PositionBof = i_PositionBof + StrDataArea.Length + 1;
            i_PositionEof = strData.IndexOf("&", i_PositionBof); //  InStr(i_PositionBof, strData, "&");      
            i_length = i_PositionEof - i_PositionBof;
            if (i_PositionEof == -1) //'如果没有找到分号;，说明是最后一个域		
            {
                StrRet = strData.Substring(i_PositionBof); //Mid(strData, m_PositionBof);		
            }
            else
            {
                StrRet = strData.Substring(i_PositionBof, i_length); //Mid(strData, m_PositionBof, m_length);		
            }
            return StrRet;
        }

        #endregion

        #region 获取包含代理ip的ip串的第一个IP

        public static string GetFirstIp(string ips)
        {
            if ((ips == null) || (ips.Length <= 0))
            {
                return "";
            }

            string[] ip = ips.Split(',');

            if (ip.Length > 0)
            {
                return ip[0].Trim();
            }
            else
            {
                return ips;
            }
        }

        #endregion

        #region 字符串分解排序 lianyee

        public static string StrSorts(string text, string splitStr)
        {
            char[] splitChar = splitStr.ToCharArray();
            string[] keywords = text.Split(splitChar);
            string newtext = "";
            Array.Sort(keywords);
            var list = new ArrayList();
            for (int i = 0; i < keywords.Length; i++)
            {
                newtext = newtext + keywords[i];
            }
            return newtext;
        }

        #endregion

        #region 转换ArrayList为字符串

        public static string ConvertArrayList2String(ArrayList list)
        {
            return ConvertArrayList2String(list, ',');
        }

        public static string ConvertArrayList2String(ArrayList list, char separator)
        {
            var sb = new StringBuilder();
            if (list == null) return string.Empty;
            foreach (var o in list)
            {
                if (sb.Length != 0)
                    sb.Append(separator);
                sb.Append(o);
            }
            return sb.ToString();
        }

        #endregion

        #region 取随机数字

        private static Object thisLock = new Object();

        #endregion

        #region 替换html字符

        private static readonly char[] _markupChar = { ' ', ' ', ' ', '<', '>', '&', '"', '*', '/' };

        private static readonly string[] _replaceString =
            {
                "&ensp;", "&emsp;", "&nbsp;", "&lt;", "&gt;", "&amp;",
                "&quot;", "&times;", "&divide;"
            };

        public static string ReplaceMarkupChar(string source)
        {
            for (int i = 0; i < _replaceString.Length; i++)
                source = source.Replace(_replaceString[i], _markupChar[i].ToString());

            return source;
        }

        #endregion

        #region 获取html字符串的innertext

        public static string FormatHtmlInnerText(string AHtml)
        {
            var regex = new Regex(@"<[^>]*>");
            AHtml = regex.Replace(AHtml, "");

            AHtml = ReplaceMarkupChar(AHtml);

            return AHtml;
        }

        #endregion

        #region 得到小数点后面的位数

        public static int GetDotNumCount(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return 0;
            }

            int pos = value.IndexOf(".");

            if (pos <= 0)
            {
                return 0;
            }

            string nextdot = value.Substring(pos + 1, value.Length - pos - 1);

            return nextdot.Length;
        }

        #endregion

        #region 小数点后保留几位

        public static string FormatDot(string value, int num)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            int pos = value.IndexOf(".");

            if (pos <= 0)
            {
                return value;
            }

            string predot = value.Substring(0, pos);

            string nextdot = value.Substring(pos + 1, value.Length - pos - 1);

            if (nextdot.Length <= num)
            {
                return predot + "." + nextdot;
            }
            else
            {
                return predot + "." + nextdot.Substring(0, num);
            }
        }


        public static string DelLast0(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            int pos = value.IndexOf(".");
            while (pos > 0)
            {
                if (value.Substring(value.Length - 1, 1) == "0")
                {
                    value = value.Substring(0, value.Length - 1);
                    pos = 1;
                }
                else if (value.Substring(value.Length - 1, 1) == ".")
                {
                    value = value.Substring(0, value.Length - 1);
                    pos = -1;
                }
                else
                {
                    pos = -1;
                }
            }

            return value;
        }

        #endregion

        #region 汉字拼音首字母

        public static string GetChineseSpell(string strText)
        {
            int len = strText.Length;
            string myStr = "";
            for (int i = 0; i < len; i++)
            {
                myStr += getSpell(strText.Substring(i, 1));
            }
            return myStr;
        }

        public static string getSpell(string cnChar)
        {
            byte[] arrCN = Encoding.Default.GetBytes(cnChar);
            if (arrCN.Length > 1)
            {
                int area = arrCN[0];
                int pos = arrCN[1];
                int code = (area << 8) + pos;
                int[] areacode =
                    {
                        45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324,
                        49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980,
                        53689, 54481
                    };
                for (int i = 0; i < 26; i++)
                {
                    int max = 55290;
                    if (i != 25) max = areacode[i + 1];
                    if (areacode[i] <= code && code < max)
                    {
                        return Encoding.Default.GetString(new[] { (byte)(65 + i) });
                    }
                }
                return "*";
            }
            else return cnChar;
        }

        #endregion

        /// <summary>
        ///   把字符串中某些字符进行替换
        /// </summary>
        /// <param name="str"> </param>
        /// <param name="from"> </param>
        /// <param name="len"> </param>
        /// <param name="mask"> </param>
        /// <returns> </returns>
        public static string Mask(string str, int from, int len, char mask = '*')
        {
            if (str.Length <= from + 1)
                return str;

            string firstpart = str.Substring(0, from + 1);

            if (str.Length <= from + 1 + len)
            {
                return firstpart.PadRight(str.Length, mask);
            }
            string secondpart = str.Substring(from + len + 1);

            var retstr = firstpart.PadRight(from + 1 + len, mask) + secondpart;
            return retstr;
        }

        public static string Mask(string str, int from, char mask = '*')
        {
            return Mask(str, from, int.MaxValue, mask);
        }

        public static string MaskRight(string str, int len, char mask = '*')
        {
            if (str.Length < len)
                return "".PadRight(str.Length, mask);

            return str.Substring(0, str.Length - len).PadRight(str.Length, mask);
        }


        public static string RMB(decimal? rmb)
        {
            if (rmb == null)
                return "0";
            if (Convert.ToInt32(rmb) == rmb)
                return Convert.ToInt32(rmb).ToString();

            return decimal.Round(rmb.Value, 2).ToString();
        }

        /// <summary>
        ///   生成随机颜色
        /// </summary>
        /// <returns> </returns>
        public static string GetColor()
        {
            var nums = new List<string> { "0", "3", "6", "9", "C", "F" };
            var clr = "#";
            for (var i = 0; i < 6; i++)
            {
                var n = Dev.Comm.Randoms.CreateRandomNumber(6);
                clr = clr + nums[n];
            }
            return clr;
        }



        public static string FormatDateValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                DateTime value = Convert.ToDateTime(AValue);
                return value.ToString("yyyy-MM-dd");
            }
        }

        public static string FormatTimeValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                DateTime value = Convert.ToDateTime(AValue);
                return value.ToString("HH:mm:ss");
            }
        }

        public static string FormatDateTimeValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                DateTime value = Convert.ToDateTime(AValue);
                if (value.Equals(DateTime.MinValue))
                    return "";
                return value.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        public static string FormatDateTimeValue(object AValue, string FormatStr)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                DateTime value = Convert.ToDateTime(AValue);
                return value.ToString(FormatStr);
            }
        }

        public static string FormatMoneyValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                Decimal value = Convert.ToDecimal(AValue);
                return value.ToString("￥#0.00#");
            }
        }

        public static string FormatDecimalValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                Decimal value = Convert.ToDecimal(AValue);
                return value.ToString("#0.00#");
            }
        }

        public static string FormatPercentValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                Decimal value = Convert.ToDecimal(AValue);
                return value.ToString("0.00%");
            }
        }

        public static string FormatEnumValue(object AValue, Type enums)
        {
            if (ObjUtil.IsNull(AValue) || ObjUtil.IsDbNull(AValue))
            {
                return String.Empty;
            }
            else
            {
                var tmp = Enum.Parse(enums, AValue.ToString()) as Enum;
                return tmp.ToString("G").Equals(AValue) ? string.Empty : tmp.ToString("G");
            }
        }

        public static string FormatBoolValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue))
            {
                return "true";
            }
            else
            {
                bool value = Convert.ToBoolean(AValue);
                return value ? "true" : "false";
            }
        }

        public static string FormatZHBoolValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue))
            {
                return "是";
            }
            else
            {
                bool value = Convert.ToBoolean(AValue);
                return value ? "是" : "否";
            }
        }


        public static string FormatValue(object AValue)
        {
            if (ObjUtil.IsNull(AValue))
            {
                return String.Empty;
            }
            switch (AValue.GetType().FullName.ToLower())
            {
                case "system.decimal":
                    return FormatDecimalValue(AValue);
                case "system.double":
                    return FormatDecimalValue(AValue);
                case "system.datetime":
                    return FormatDateTimeValue(AValue);
                case "system.boolean":
                    return FormatZHBoolValue(AValue);
                case "system.enum":
                    return FormatEnumValue(AValue, AValue.GetType());
                default:
                    if (ObjUtil.IsNull(AValue))
                    {
                        return String.Empty;
                    }
                    else
                    {
                        return AValue.ToString();
                    }
            }
        }



        public static string List2String(IList list)
        {
            return List2String(list, ',');
        }

        public static string List2String(IList list, char separator)
        {
            var sb = new StringBuilder();
            if (list == null) return string.Empty;
            foreach (var o in list)
            {
                if (sb.Length != 0)
                    sb.Append(separator);
                sb.Append(o);
            }
            return sb.ToString();
        }

        public static void ParseMatrix(string Format, out string Matrix, out int MatrixCol, out string MatrixDisplay)
        {
            Matrix = "";
            MatrixCol = 0;
            MatrixDisplay = "0";

            string[] str = Format.Split("=".ToCharArray());

            if (str.Length > 0)
                Matrix = str[0];

            if (str.Length > 1)
            {
                string[] tempstr = str[1].Split(",".ToCharArray());
                if (tempstr.Length > 0)
                    int.TryParse(tempstr[0], out MatrixCol);

                if (tempstr.Length > 1)
                    MatrixDisplay = tempstr[1];
            }
        }

        #region 转换ASCII和CHAR

        public static int Asc(string character)
        {
            if (character.Length == 1)
            {
                var asciiEncoding = new ASCIIEncoding();
                int intAsciiCode = asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }
        }

        public static string Chr(int asciiCode)
        {
            if (asciiCode >= 0 && asciiCode <= 255)
            {
                var asciiEncoding = new ASCIIEncoding();
                var byteArray = new[] { (byte)asciiCode };
                string strCharacter = asciiEncoding.GetString(byteArray);
                return (strCharacter);
            }
            else
            {
                throw new Exception("ASCII Code is not valid.");
            }
        }

        #endregion

        #region 压缩解压缩

        public static string Compress(string target, string endcoding = "gb2312")
        {
            Encoding encoding = Encoding.GetEncoding(endcoding);
            return Compress(target, encoding);
        }

        public static string Compress(string target, Encoding encoding)
        {
            using (var ms = new MemoryStream())
            {
                using (var compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true))
                {
                    //GZipStream compressedzipStream = new GZipStream(ms, CompressionMode.Compress, true);
                    byte[] buffer = encoding.GetBytes(target);
                    compressedzipStream.Write(buffer, 0, buffer.Length);
                    compressedzipStream.Close();
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string DeCompress(string target, string endcoding = "gb2312")
        {
            Encoding encoding = Encoding.GetEncoding(endcoding);
            return DeCompress(target, encoding);
        }

        public static string DeCompress(string target, Encoding encoding)
        {
            using (var ms = new MemoryStream())
            {
                byte[] bytes = Convert.FromBase64String(target);
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                //GZipStream zipStream = new GZipStream(ms, CompressionMode.Decompress);
                using (var zipStream = new GZipStream(ms, CompressionMode.Decompress))
                {
                    var sb = new StringBuilder();

                    while (true)
                    {
                        var buffer = new byte[100];
                        int result = zipStream.Read(buffer, 0, buffer.Length);
                        if (result == 0)
                            break;

                        sb.Append(encoding.GetString(buffer, 0, result));
                    }

                    zipStream.Close();
                    return sb.ToString();
                }
            }
        }

        #endregion

        #region 转换矩阵为HTML格式

        //0 第一行数字 1 第一行字母
        public static string Matrix2Html(string Matrix, int MatrixCol, string MatrixDisplay)
        {
            if (MatrixCol == 0) return "";
            string[] ary = Matrix.Split(',');
            int len = ary.Length;
            int rowcount = len / MatrixCol;
            var vTable = new StringBuilder();
            int code = 64;

            string normal =
                "border-bottom: 1px solid #BCBCBC;border-right: 1px solid #BCBCBC;padding: 5px;text-align: center;";
            string th =
                "text-align:center; padding:5px; border-bottom:1px solid #BCBCBC; border-right:1px solid #BCBCBC;background:none repeat scroll 0 0 #797979;";
            string sr = "background:#FFF;";
            string dr = "background:#E6E6E6;";

            vTable.Append(
                "<table border='0' cellspacing='0' cellpadding='0' style='border-top:1px solid #666; border-left:1px solid #666; margin:0px auto 0;' >");

            for (int i = 0; i < rowcount + 1; i++)
            {
                vTable.Append("<tr");

                //if (i == 0)
                //    vTable.AppendFormat(" style='width:15px;{0}'>", th);
                //else
                vTable.Append(">");

                for (int j = 0; j < MatrixCol + 1; j++)
                {
                    vTable.Append("<td");

                    if (j == 0 && i == 0)
                    {
                        vTable.AppendFormat(" style='width:15px;{0}'>&nbsp;</td>", th);
                        continue;
                    }

                    if (j == 0)
                    {
                        string left = string.Empty;
                        if (MatrixDisplay == "0" || MatrixDisplay == "")
                            left = Chr(code + i);
                        else
                            left = i.ToString();

                        vTable.AppendFormat(" style='{0}'>{1}</td>", th, left);
                        continue;
                    }

                    if (i == 0)
                    {
                        string top = string.Empty;
                        if (MatrixDisplay == "0" || MatrixDisplay == "")
                            top = j.ToString();
                        else
                            top = Chr(code + j);

                        vTable.AppendFormat(" style='width:15px;{0}'>{1}</td>", th, top);
                        continue;
                    }

                    string ys = string.Empty;
                    if (i % 2 == 0)
                        ys = sr;
                    else
                        ys = dr;

                    string value = string.Empty;
                    int wz = (i - 1) * MatrixCol + (j - 1);
                    if (wz < ary.Length)
                        value = ary[wz];
                    else
                        value = "&nbsp;";

                    vTable.AppendFormat(" style='{0}{1}'>{2}</td>", normal, ys, value);
                }

                vTable.Append("</tr>");
            }
            return vTable.ToString();
        }

        #endregion

        #region 固定位置插入制定字符串

        public static string CycleInsert(string Source, int Cyc, int StartIndex, string Target)
        {
            if (ObjUtil.IsNull(Source)) return Source;
            if (Cyc <= 0) return Source;
            if (StartIndex < 0) return Source;
            if (ObjUtil.IsNull(Target)) return Source;
            if (Cyc <= Target.Length) return Source;

            if (Source.Length - 1 < Cyc + StartIndex) return Source;

            Source = Source.Insert(StartIndex + Cyc, Target);

            Source = CycleInsert(Source, Cyc, Cyc + StartIndex, Target);

            return Source;
        }

        #endregion


    }
}