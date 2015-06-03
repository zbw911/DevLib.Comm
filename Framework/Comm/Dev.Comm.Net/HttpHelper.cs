using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Dev.Comm.Net
{
    /// <summary>
    /// 对原有的Http进行一个扩充，原有方法过于参数过于繁琐，将参数重构为对象,
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// GET 请求
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static ResponeInfo Get(RequestInfo requestInfo)
        {


            string outcookieheader;
            var httpcode = 200;

            var querystrings = requestInfo.QueryParams.Select(x => x.Key + "=" + x.Value);
            var querys = string.Join("&", querystrings);

            var url = requestInfo.Url;
            url = url.TrimEnd('?', '&');

            if (url.IndexOf("?", StringComparison.Ordinal) < 0)
            {
                url += "?" + querys;
            }
            else
            {
                url += "&" + querys;
            }

            var text = Http.GetUrl(url, requestInfo.Cookieheader, out outcookieheader, requestInfo.HeaderReferer,
                  requestInfo.AutoRedirect, requestInfo.HeaderUserAgent, requestInfo.HttpType, requestInfo.Encoding,
                  requestInfo.Timeout, requestInfo.Mywebproxy, requestInfo.NetworkCredentialName,
                  requestInfo.NetworkCredentialPassword, requestInfo.HttpExpect100Continue,
                  requestInfo.ServicePointManagerExpect100Continue, requestInfo.Mycharset, requestInfo.Headers);

            var respone = new ResponeInfo
            {
                HttpCodeState = httpcode,
                Outcookieheader = outcookieheader,
                ResponeText = text
            };
            return respone;
        }

        /// <summary>
        /// Post 请求
        /// </summary>
        /// <param name="requestInfo"></param>
        /// <returns></returns>
        public static ResponeInfo Post(RequestInfo requestInfo)
        {
            var httpcode = 200;

            string outcookieheader;
            var url = requestInfo.Url;
            var querystrings = requestInfo.QueryParams.Select(x => x.Key + "=" + x.Value);
            var querys = string.Join("&", querystrings);
            var responeText = Http.PostUrl(url, querys, requestInfo.Cookieheader, out outcookieheader,
                  requestInfo.HeaderReferer, requestInfo.AutoRedirect, requestInfo.HeaderUserAgent, requestInfo.HttpType,
                  requestInfo.Encoding, requestInfo.Timeout, requestInfo.Mywebproxy, requestInfo.NetworkCredentialName,
                  requestInfo.NetworkCredentialPassword, requestInfo.HttpExpect100Continue,
                  requestInfo.ServicePointManagerExpect100Continue,
                  requestInfo.Headers.Select(x => x.Key + ":" + x.Value).ToArray());
            var respone = new ResponeInfo
            {
                HttpCodeState = httpcode,
                Outcookieheader = outcookieheader,
                ResponeText = responeText
            };
            return respone;
        }
    }



    /// <summary>
    /// 请求信息
    /// </summary>
    public class RequestInfo
    {
        private bool _autoRedirect = true;
        private string _headerUserAgent = "";
        private int _timeout = 10 * 1000;
        private string _mywebproxy = "";
        private string _networkCredentialName = "";
        private string _networkCredentialPassword = "";
        private bool _httpExpect100Continue = true;
        private bool _servicePointManagerExpect100Continue = true;
        private string _mycharset = "";
        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        private string _url;
        private string _cookieheader = "";
        private string _headerReferer = "";
        private string _httpType = "";
        private string _encoding = "";
        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();

        public bool AutoRedirect
        {
            get { return _autoRedirect; }
            set { _autoRedirect = value; }
        }

        public string HeaderUserAgent
        {
            get { return _headerUserAgent; }
            set { _headerUserAgent = value; }
        }

        public int Timeout
        {
            get { return _timeout; }
            set { _timeout = value; }
        }

        public string Mywebproxy
        {
            get { return _mywebproxy; }
            set { _mywebproxy = value; }
        }

        public string NetworkCredentialName
        {
            get { return _networkCredentialName; }
            set { _networkCredentialName = value; }
        }

        public string NetworkCredentialPassword
        {
            get { return _networkCredentialPassword; }
            set { _networkCredentialPassword = value; }
        }

        public bool HttpExpect100Continue
        {
            get { return _httpExpect100Continue; }
            set { _httpExpect100Continue = value; }
        }

        public bool ServicePointManagerExpect100Continue
        {
            get { return _servicePointManagerExpect100Continue; }
            set { _servicePointManagerExpect100Continue = value; }
        }

        public string Mycharset
        {
            get { return _mycharset; }
            set { _mycharset = value; }
        }

        public Dictionary<string, string> Headers
        {
            get { return _headers; }
            set { _headers = value; }
        }

        public string Url
        {
            get { return _url; }
            set { _url = value; }
        }

        public string Cookieheader
        {
            get { return _cookieheader; }
            set { _cookieheader = value; }
        }

        public string HeaderReferer
        {
            get { return _headerReferer; }
            set { _headerReferer = value; }
        }

        public string HttpType
        {
            get { return _httpType; }
            set { _httpType = value; }
        }

        public string Encoding
        {
            get { return _encoding; }
            set { _encoding = value; }
        }


        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> QueryParams
        {
            get { return _queryParams; }
            set { _queryParams = value; }
        }
    }


    /// <summary>
    /// 回应参数
    /// </summary>
    public class ResponeInfo
    {
        private string outcookieheader;

        private string responeText;

        private int _httpCodeState;

        public string Outcookieheader
        {
            get { return outcookieheader; }
            set { outcookieheader = value; }
        }

        public string ResponeText
        {
            get { return responeText; }
            set { responeText = value; }
        }

        public int HttpCodeState
        {
            get { return _httpCodeState; }
            set { _httpCodeState = value; }
        }
    }
}
