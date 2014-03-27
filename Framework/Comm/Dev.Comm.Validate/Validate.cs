// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Validate.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections;
using System.Text.RegularExpressions;
using Dev.Comm.Utils;

namespace Dev.Comm.Validate
{
    public class Validate
    {
        private static readonly Regex RegCHZN = new Regex("[一-龥]");
        private static readonly Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static readonly Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$");

        private static readonly Regex RegEmail =
            new Regex(
                "^(([0-9a-zA-Z]+)|([0-9a-zA-Z]+[_.0-9a-zA-Z-]*[0-9a-zA-Z]+))@([a-zA-Z0-9-]+[.])+(net|NET|com|COM|gov|GOV|mil|MIL|org|ORG|edu|EDU|int|INT|cn|CN)$");

        private static readonly Regex RegMobileTel = new Regex("^[1][3|5][0-9]{9}$");
        private static readonly Regex RegNumber = new Regex("^[0-9]+$");
        private static readonly Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");

        private static readonly Regex RegUrl = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");
        private static readonly Regex RegID = new Regex("^[0-9a-zA-Z]*$");

        private static readonly Regex RegData =
            new Regex(@"^[1-2]\d{3}-((0?[1-9])|(1[0-2]))-((0[1-9])|([1-2]?\d)|(3[0-1]))$");

        private static readonly Regex RegDataTime =
            new Regex(
                @"^[1-9]\d{3}-(0?[1-9]|1[0|1|2])-(0?[1-9]|[1|2][0-9]|3[0|1])\s(0?[0-9]|1[0-9]|2[0-3]):(0?[0-9]|[1|2|3|4|5][0-9]):(0?[0-9]|[1|2|3|4|5][0-9])$");


        public static bool isBlank(string strInput)
        {
            return ((strInput == null) || (strInput.Trim() == ""));
        }

        public static bool IsDecimal(string inputData)
        {
            return RegDecimal.Match(inputData).Success;
        }

        public static bool IsDecimalSign(string inputData)
        {
            return RegDecimalSign.Match(inputData).Success;
        }

        public static bool IsDouble(string strInput)
        {
            if (strInput.IndexOf("-") == 0)
            {
                strInput = strInput.Substring(1);
            }
            char[] chArray = strInput.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (((chArray[i] < '0') || (chArray[i] > '9')) && (chArray[i] != '.'))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsEmail(string inputData)
        {
            return RegEmail.Match(inputData.ToLower()).Success;
        }

        public static bool IsHasCHZN(string inputData)
        {
            return RegCHZN.Match(inputData).Success;
        }

        public static bool IsInt(string strInput)
        {
            if (strInput.IndexOf("-") == 0)
            {
                strInput = strInput.Substring(1);
            }
            char[] chArray = strInput.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if ((chArray[i] < '0') || (chArray[i] > '9'))
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsMobileTel(string inputData)
        {
            return RegMobileTel.Match(inputData).Success;
        }

        public static bool IsNumber(string inputData)
        {
            return RegNumber.Match(inputData).Success;
        }

        public static bool IsNumberSign(string inputData)
        {
            return RegNumberSign.Match(inputData).Success;
        }

        public static bool IsNumeric(string strInput)
        {
            if (strInput.IndexOf("-") == 0)
            {
                strInput = strInput.Substring(1);
            }
            char[] chArray = strInput.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (((chArray[i] < '0') || (chArray[i] > '9')) && (chArray[i] != '.'))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>是否电话,包含手机 </summary>
        /// <param name="strInput">输入字符串</param>
        /// <returns>true/false</returns>
        public static bool isPhone(string strInput)
        {
            if ((strInput == null) || (strInput == ""))
            {
                return false;
            }
            else
            {
                /*
                char[] ca =strInput.ToCharArray();
                for (int i=0;i<ca.Length;i++)
                {
                    if ((ca[i]<'0' || ca[i]>'9') && ca[i]!='-' && ca[i]!='(' && ca[i]!=')' && ca[i]!='+')
                    {					 
                        found=false;
                        break;
                    };
                };
                if (strInput.Substring(strInput.Length-1,1) == "-") found = false;
                */
                Match tt = Regex.Match(strInput, @"^((\(?\d{2,3})\)?)?-?(\(\d{3,4}\)|\d{3,4}-)?((\d{7,8})|(\d{11}))$");
                return tt.Success;
            }
        }

        /// <summary>
        /// 是否是URL
        /// </summary>
        /// <param name="strInput"></param>
        /// <returns></returns>
        public static bool isUrl(string strInput)
        {
            Match m = RegUrl.Match(strInput);
            return m.Success;
        }

        public static bool isnum(string strid)
        {
            Match m = RegID.Match(strid);
            return m.Success;
        }

        /// <summary>
        /// 是不是日期+时间格式
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsDateTime(string p)
        {
            Match m = RegDataTime.Match(p);
            return m.Success;
        }

        /// <summary>
        /// 是不是日期格式
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsDate(string p)
        {
            Match m = RegData.Match(p);
            return m.Success;
        }

        /// <summary>
        /// 判断是不是时间格式
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool IsDateOrDateTime(string p)
        {
            DateTime dt;
            return DateTime.TryParse(p, out dt);
        }

        #region 验证密码中的字符是否有数字

        /// <summary>
        /// 验证密码中的字符是否有数字
        /// </summary>
        /// <param name="strPwd">字符串</param>
        /// <returns>BOOL</returns>
        public static bool validateNumberCase(string strPwd)
        {
            bool foundNumber = false;
            for (int i = 0; i < strPwd.Length; i++)
            {
                if (foundNumber == false)
                    foundNumber = StringUtil.isNumberic(strPwd[i]);
            }
            if (foundNumber)
                return true;
            else
                return false;
        }

        #endregion

        #region 验证密码中的字符是否有特殊字符

        public static bool validateSpecialCase(string strPwd)
        {
            bool foundSpecial = false;
            for (int i = 0; i < strPwd.Length; i++)
            {
                if (foundSpecial == false)
                    foundSpecial = StringUtil.isSpecialCharacter(strPwd[i]);
            }
            if (foundSpecial)
                return true;
            else
                return false;
        }

        #endregion

        #region 验证密码中的字符是否大小写混合

        /// <summary>
        /// 验证密码中的字符是否大小写混合
        /// </summary>
        /// <param name="strPwd">密码字符串</param>
        /// <returns>bool</returns>
        public static bool validateMixedCase(string strPwd)
        {
            bool foundLower = false, foundUper = false;
            for (int i = 0; i < strPwd.Length; i++)
            {
                if (foundLower == false)
                    foundLower = StringUtil.isLower(strPwd[i]);
                if (foundUper == false)
                    foundUper = StringUtil.isUpper(strPwd[i]);
            }
            if (foundLower && foundUper)
                return true;
            else
                return false;
        }

        #endregion

        #region 对密码的长度进行检验

        /// <summary>
        /// 对密码的长度进行检验
        /// </summary>
        /// <param name="strPwd">密码字符串</param>
        /// <param name="intLen">密码长度</param>
        /// <returns>BOOL,false代表长度不够</returns>
        public static bool validatePasswordLength(string strPwd, int intLen)
        {
            if (strPwd.Length < intLen)
                return false;
            else
                return true;
        }

        #endregion

        #region 检验黑客SQL注入函数 [='/<>-*]

        public static bool CheckSqlImmitParams(params object[] args)
        {
            //string[] Lawlesses ={ "=", "'", "<", ">" ,"%"};

            string[] Lawlesses =
                {
                    "=", "'", "<", ">", "%", "exec", "insert", "select", "from", "join", "delete",
                    "update", "master", "truncate", "declare", "sp_executesql", "drop", "table"
                };

            if (Lawlesses == null || Lawlesses.Length <= 0)
                return true;
            // 构造正则表达式,例:Lawlesses是=号和'号,则正则表达式为 .*[=}'].*  
            //string str_Regex = ".*[";

            string str_Regex = "";
            for (int i = 0; i < Lawlesses.Length - 1; i++)
                str_Regex += Lawlesses[i] + "|";

            //str_Regex += Lawlesses[Lawlesses.Length - 1] + "].*";

            str_Regex = str_Regex.Substring(0, str_Regex.Length - 1);

            foreach (var arg in args)
            {
                if (arg is string) //如果是字符串,直接检查        
                {
                    if (Regex.Matches(arg.ToString().ToLower(), str_Regex).Count > 0)
                        return false;
                }
                else if (arg is ICollection) //如果是一个集合,则检查集合内元素是否字符串,是字符串,就进行检查       
                {
                    foreach (var obj in (ICollection) arg)
                    {
                        if (obj is string)
                        {
                            if (Regex.Matches(obj.ToString().ToLower(), str_Regex).Count > 0)
                                return false;
                        }
                    }
                }
            }
            return true;
        }

        #endregion
    }
}