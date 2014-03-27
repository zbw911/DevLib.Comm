// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：DevRequest.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using Dev.Comm.Utils;

namespace Dev.Comm.Web
{
    /// <summary>
    /// Request操作类
    /// </summary>
    public class DevRequest
    {
        /// <summary>
        /// 判断当前页面是否接收到了Post请求
        /// </summary>
        /// <returns>是否接收到了Post请求</returns>
        public static bool IsPost()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("POST");
        }

        /// <summary>
        /// 判断当前页面是否接收到了Get请求
        /// </summary>
        /// <returns>是否接收到了Get请求</returns>
        public static bool IsGet()
        {
            return HttpContext.Current.Request.HttpMethod.Equals("GET");
        }


        /// <summary>
        /// 判断当前访问是否来自浏览器软件
        /// </summary>
        /// <returns>当前访问是否来自浏览器软件</returns>
        public static bool IsBrowserGet()
        {
            string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
            string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
            for (int i = 0; i < BrowserName.Length; i++)
            {
                if (curBrowser.IndexOf(BrowserName[i]) >= 0)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判断是否来自搜索引擎链接
        /// </summary>
        /// <returns>是否来自搜索引擎链接</returns>
        public static bool IsSearchEnginesGet()
        {
            if (HttpContext.Current.Request.UrlReferrer == null)
                return false;

            string[] SearchEngine =
                {
                    "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom",
                    "yisou", "iask", "soso", "gougou", "zhongsou"
                };
            string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
            for (int i = 0; i < SearchEngine.Length; i++)
            {
                if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
                    return true;
            }
            return false;
        }


        /// <summary>
        /// 是否来自于移动设备的请求
        /// 
        /// </summary>
        /// <returns></returns>
        public static bool IsMobileRequest()
        {
            //参考：http://detectmobilebrowsers.com/
            //http://stackoverflow.com/questions/13086856/mobile-device-detection-in-asp-net
            Regex MobileCheck = new Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows (ce|phone)|xda|xiino", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
            Regex MobileVersionCheck = new Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);


            Debug.Assert(HttpContext.Current != null);

            if (HttpContext.Current.Request != null && HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"] != null)
            {
                var u = HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].ToString();

                if (u.Length < 4)
                    return false;

                if (MobileCheck.IsMatch(u) || MobileVersionCheck.IsMatch(u.Substring(0, 4)))
                    return true;
            }

            return false;

        }


        /// <summary>
        /// 返回表单或Url参数的总个数
        /// </summary>
        /// <returns></returns>
        public static int GetParamCount()
        {
            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.QueryString.Count;
        }

        #region GetString

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName)
        {
            return GetQueryString(strName, false);
        }

        /// <summary>
        /// 获得指定Url参数的值
        /// </summary> 
        /// <param name="strName">Url参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url参数的值</returns>
        public static string GetQueryString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.QueryString[strName] == null)
                return "";

            if (sqlSafeCheck && !Security.IsSafeSqlString(HttpContext.Current.Request.QueryString[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.QueryString[strName];
        }


        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName)
        {
            return GetFormString(strName, false);
        }

        /// <summary>
        /// 获得指定表单参数的值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>表单参数的值</returns>
        public static string GetFormString(string strName, bool sqlSafeCheck)
        {
            if (HttpContext.Current.Request.Form[strName] == null)
                return "";

            if (sqlSafeCheck && !Security.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
                return "unsafe string";

            return HttpContext.Current.Request.Form[strName];
        }

        public static string GetRouteDataString(string strName, bool sqlSafeCheck)
        {
            RouteData rotedata = HttpContext.Current.Request.RequestContext.RouteData;
            if (rotedata != null)
            {
                if (rotedata.Values.ContainsKey(strName))
                {
                    string data = rotedata.Values[strName].ToString();

                    if (sqlSafeCheck && !Security.IsSafeSqlString(data))
                        throw new UnSafeRequestException("unsafe " + data);

                    return data;
                }
            }

            return "";
        }

        public static string GetRouteDataString(string strName)
        {
            return GetRouteDataString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName)
        {
            return GetString(strName, false);
        }

        /// <summary>
        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">参数</param>
        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
        /// <returns>Url或表单参数的值</returns>
        public static string GetString(string strName, bool sqlSafeCheck)
        {
            if (!"".Equals(GetQueryString(strName)))
                return GetQueryString(strName, sqlSafeCheck);


            if (!"".Equals(GetFormString(strName)))
                return GetFormString(strName, sqlSafeCheck);

            if (!"".Equals(GetRouteDataString(strName)))
                return GetRouteDataString(strName, sqlSafeCheck);

            return "";
        }

        #endregion

        #region GetInit

        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.QueryString[strName], 0);
        }


        /// <summary>
        /// 获得指定Url参数的int类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static int GetQueryInt(string strName, int defValue)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的int类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的int类型值</returns>
        public static int GetFormInt(string strName, int defValue)
        {
            return TypeConverter.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
        }


        public static int GetRouteDataInt(string strName, int defValue)
        {
            RouteData rotedata = HttpContext.Current.Request.RequestContext.RouteData;
            if (rotedata != null)
            {
                if (rotedata.Values.ContainsKey(strName))
                {
                    string data = rotedata.Values[strName].ToString();
                    return TypeConverter.StrToInt(data, defValue);
                }
            }

            return defValue;
        }

        /// <summary>
        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static int GetInt(string strName, int defValue)
        {
            if (GetQueryInt(strName, defValue) != defValue)
                return GetQueryInt(strName, defValue);
            if (GetFormInt(strName, defValue) != defValue)
                return GetFormInt(strName, defValue);
            if (GetRouteDataInt(strName, defValue) != defValue)
                return GetRouteDataInt(strName, defValue);

            return defValue;
        }

        #endregion

        #region Float

        /// <summary>
        /// 获得指定Url参数的float类型值
        /// </summary>
        /// <param name="strName">Url参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url参数的int类型值</returns>
        public static float GetQueryFloat(string strName, float defValue)
        {
            return TypeConverter.StrToFloat(HttpContext.Current.Request.QueryString[strName], defValue);
        }


        /// <summary>
        /// 获得指定表单参数的float类型值
        /// </summary>
        /// <param name="strName">表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>表单参数的float类型值</returns>
        public static float GetFormFloat(string strName, float defValue)
        {
            return TypeConverter.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
        }

        public static float GetRouteDataFloat(string strName, float defValue)
        {
            RouteData rotedata = HttpContext.Current.Request.RequestContext.RouteData;
            if (rotedata != null)
            {
                if (rotedata.Values.ContainsKey(strName))
                {
                    string data = rotedata.Values[strName].ToString();
                    return TypeConverter.StrToFloat(data, defValue);
                }
            }

            return defValue;
        }

        /// <summary>
        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
        /// </summary>
        /// <param name="strName">Url或表单参数</param>
        /// <param name="defValue">缺省值</param>
        /// <returns>Url或表单参数的int类型值</returns>
        public static float GetFloat(string strName, float defValue)
        {
            if (GetQueryFloat(strName, defValue) != defValue)
                return GetQueryFloat(strName, defValue);

            if (GetFormFloat(strName, defValue) != defValue)
                return GetFormFloat(strName, defValue);
            if (GetRouteDataFloat(strName, defValue) != defValue)
                return GetRouteDataFloat(strName, defValue);

            return defValue;
        }

        #endregion

        /// <summary>
        /// 取得类型
        /// </summary>
        /// <param name="strName"></param>
        /// <param name="defValue"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Get<T>(string strName, T defValue)
        {
            var val = GetString(strName);
            if (val == "")
                return defValue;

            return TypeConverter.ConvertType(val, defValue);
        }

        /// <summary>
        /// Reqest 文件处理
        /// </summary>
        public static class File
        {
            /// <summary>
            /// 
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public static Stream GetFilesStream(string name)
            {
                return HttpPostFiles[name].InputStream;
            }

            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public static Stream GetFilesStream(int index)
            {
                return HttpPostFiles[index].InputStream;
            }


            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="name"></param>
            /// <param name="filename"></param>
            public static void SavaAs(string name, string filename)
            {
                HttpPostFiles[name].SaveAs(filename);
            }

            /// <summary>
            /// 保存
            /// </summary>
            /// <param name="index"></param>
            /// <param name="filename"></param>
            public static void SavaAs(int index, string filename)
            {
                HttpPostFiles[index].SaveAs(filename);
            }


            /// <summary>
            /// Mime类型
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public static string ContentType(string name)
            {
                return
                    HttpPostFiles[name].ContentType;
            }

            /// <summary>
            /// Mime类型
            /// </summary>
            /// <param name="index"> </param>
            /// <returns></returns>
            public static string ContentType(int index)
            {
                return
                    HttpPostFiles[index].ContentType;
            }


            /// <summary>
            /// 上传的文件名
            /// </summary>
            /// <param name="index"></param>
            /// <returns></returns>
            public static string FileName(int index)
            {
                return HttpPostFiles[index].FileName;
            }
            /// <summary>
            /// 上传的文件名
            /// </summary>
            /// <param name="name"></param>
            /// <returns></returns>
            public static string FileName(string name)
            {
                return HttpPostFiles[name].FileName;
            }


            /// <summary>
            /// 上传怕文件
            /// </summary>
            public static HttpFileCollection HttpPostFiles
            {
                get { return HttpContext.Current.Request.Files; }
            }
        }
    }
}