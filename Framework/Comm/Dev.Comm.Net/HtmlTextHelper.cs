using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Dev.Comm.Net
{
    /// <summary>
    /// 对返回的Html进行分析的帮助方法
    /// </summary>
    public class HtmlTextHelper
    {
        /// <summary>
        /// 使正则表达式取得Input的Value
        /// </summary>
        /// <param name="str"></param>
        /// <param name="inputname"></param>
        /// <returns></returns>
        public static string FindValueByName(string str, string inputname)
        {
            string reg = @"<input [\s\S]*? name=""(?<name>.*?)"" [\s\S]*?value=""(?<value>.*?)"" [\s\S]*?>";
            Regex r = new Regex(reg, RegexOptions.None);
            Match match = r.Match(str);
            string aa = "";
            while (match.Success)
            {
                string name = match.Groups["name"].ToString();
                string value = match.Groups["value"].ToString();
                if (name == inputname)
                {
                    return value;
                }
                else
                {
                    match = match.NextMatch();
                }
            }
            return aa;
        }
    }
}
