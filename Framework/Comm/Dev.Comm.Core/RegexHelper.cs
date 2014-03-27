// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/RegexHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace Dev.Comm
{
    public class RegexHelper
    {
        /// <summary>
        /// </summary>
        /// <param name="content"> </param>
        /// <param name="pattern"> </param>
        public static IList<KeyValuePair<int, string>> MatchesKeyIndex(string content, string pattern)
        {
            MatchCollection mc = Matches(content, pattern);
            IList<KeyValuePair<int, string>> kvs = new List<KeyValuePair<int, string>>();
            for (int i = 0; i < mc.Count; i++) //在输入字符串中找到所有匹配
            {
                string v = mc[i].Value; //将匹配的字符串添在字符串数组中
                int k = mc[i].Index; //记录匹配字符的位置

                kvs.Add(new KeyValuePair<int, string>(k, v));
            }
            return kvs;
        }

        public static MatchCollection Matches(string content, string pattern)
        {
            //Regex r = new Regex(pattern, RegexOptions.Singleline);
            var r = new Regex(
                pattern,
                RegexOptions.Singleline | RegexOptions.Multiline);
            MatchCollection mc = r.Matches(content);
            return mc;
        }


        public static string MatchesFirstGroup(string content, string pattern)
        {
            var r = new Regex(
                pattern,
                RegexOptions.Singleline | RegexOptions.Multiline);
            Match m = r.Match(content);
            if (m.Success && m.Groups.Count > 1)
                return m.Groups[1].Value;

            return null;
        }



        public static string MatchesString(string content, string pattern, int matchindex, int groupindex)
        {
            GroupCollection g = MatchesGroups(content, pattern, matchindex);
            if (g == null || groupindex >= g.Count)
            {
                return null;
            }

            return g[groupindex].Value;
        }

        /// <summary>
        ///   返回 所有 分组匹配项
        /// </summary>
        /// <param name="content"> </param>
        /// <param name="pattern"> </param>
        /// <param name="matchindex"> </param>
        /// <returns> </returns>
        public static string[] MatchesGroupsString(string content, string pattern, int GroupIndex)
        {
            MatchCollection mc = Matches(content, pattern);
            if (mc == null)
            {
                return null;
            }
            var vals = new string[mc.Count];
            for (int i = 0; i < mc.Count; i++) // m in mc)
            {
                if (mc[i].Groups.Count > GroupIndex)
                {
                    string v = mc[i].Groups[GroupIndex].Value;
                    vals[i] = v;
                }
                else
                {
                    throw new Exception("组编号大于现在长度  mc[" + i + "].Groups.Count > " + GroupIndex);
                }
            }

            //var g = MatchesGroups(content, pattern, matchindex);
            //if (g == null || 0 >= g.Count)
            //{
            //    return null;
            //}
            //string[] vals = new string[g.Count];
            //for (int i = 0; i < g.Count; i++)
            //{
            //    vals[i] = g[i].Value;
            //}
            return vals;
        }


        /// <summary>
        /// </summary>
        /// <param name="content"> </param>
        /// <param name="pattern"> </param>
        /// <param name="matchindex"> </param>
        /// <returns> </returns>
        public static GroupCollection MatchesGroups(string content, string pattern, int matchindex)
        {
            MatchCollection mc = Matches(content, pattern);

            if (mc == null || mc.Count <= matchindex)
            {
                return null;
            }

            Match m = mc[matchindex];

            if (m == null)
                return null;

            GroupCollection g = m.Groups;

            return g;
        }


        public static string Match(string content, string pattern)
        {
            var r = new Regex(pattern, RegexOptions.ExplicitCapture);
            Match mc = r.Match(content);
            if (mc.Success)
            {
                return mc.Value;
            }
            else
            {
                return null;
            }
        }





        /// <summary>
        ///  批量正则替换
        /// </summary>
        /// <param name="source"></param>
        /// <param name="patterns"> </param>
        /// <param name="replaces"> </param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static string PregReplace(string source, IEnumerable<string> patterns, IEnumerable<string> replaces)
        {
            if (patterns.Count() != replaces.Count())
                throw new ArgumentException("Replacement and Pattern Arrays must be balanced");


            var index = 0;

            var dest = source;

            foreach (var pattern in patterns)
            {
                dest = PregReplace(dest, pattern, replaces.ElementAt(index));
            }

            return dest;
        }




        /// <summary>
        /// 取得匹配结果
        /// </summary>
        /// <param name="content"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static Match GetMatch(string content, string pattern)
        {
            Regex r = new Regex(pattern);
            Match mc = r.Match(content);
            if (mc.Success)
            {
                return mc;
            }
            else
            {
                return null;
            }
        }



        /// <summary>
        ///   正则替换
        /// </summary>
        /// <param name="content"> </param>
        /// <param name="pattern"> </param>
        /// <returns> </returns>
        public static string PregReplace(string content, string pattern, string replacement)
        {
            var r = new Regex(pattern);
            return r.Replace(content, replacement);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="macth"></param>
        /// <param name="handler"></param>
        /// <returns></returns>
        public static string MatchAndHandler(string source, string macth, Func<string, GroupCollection, string> handler)
        {
            var ms = Dev.Comm.RegexHelper.Matches(source, macth);
            var dest = source;

            for (int i = 0; i < ms.Count; i++)
            {
                var s = ms[i].Success;
                if (!s)
                    continue;
                var group = ms[i].Groups;

                if (handler != null)
                    dest = handler(dest, group);
            }

            return dest;
        }

        /// <summary>
        ///   是否MATCH，兼容php
        /// </summary>
        /// <param name="content"> </param>
        /// <param name="pattern"> </param>
        /// <returns> </returns>
        public static bool Preg_match(string pattern, string content)
        {
            try
            {
                var r = new Regex(pattern);
                Match mc = r.Match(content);

                return mc.Success;
            }
            catch (Exception e)
            {
                throw new Exception("", e);
            }
        }

        public static int Preg_match_all(string content, string pattern, out MatchCollection mc)
        {
            var r = new Regex(pattern);
            mc = r.Matches(content);

            return mc.Count;
        }

        public static int Preg_match_all(string content, string pattern)
        {
            MatchCollection mc;
            return Preg_match_all(content, pattern, out mc);
        }
    }
}