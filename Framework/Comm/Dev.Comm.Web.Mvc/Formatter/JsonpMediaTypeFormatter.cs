// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年02月21日 16:51
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/JsonpMediaTypeFormatter.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************


namespace Dev.Comm.Web.Mvc.Formatter
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Formatting;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Web;

    /// <summary>
    ///   jsonp
    /// </summary>
    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private string callbackQueryParameter;

        public JsonpMediaTypeFormatter()
        {
            this.SupportedMediaTypes.Add(DefaultMediaType);
            this.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            this.MediaTypeMappings.Add(new UriPathExtensionMapping("jsonp", DefaultMediaType));
        }

        public string CallbackQueryParameter
        {
            get { return this.callbackQueryParameter ?? "callback"; }
            set { this.callbackQueryParameter = value; }
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
                                                TransportContext transportContext)
        {
            string callback;
            if (this.IsJsonpRequest(out callback))
            {
                return Task.Factory.StartNew(() =>
                                                 {
                                                     var writer = new StreamWriter(writeStream);
                                                     writer.Write(callback + "(");
                                                     writer.Flush();

                                                     base.WriteToStreamAsync(type, value, writeStream, content,
                                                                             transportContext).Wait();

                                                     writer.Write(")");
                                                     writer.Flush();
                                                 });
            }
            else
            {
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }
        }

        //public override Task WriteToStreamAsync(Type type, object value, Stream stream,  TransportContext transportContext)
        //{
        //    string callback;

        //    if (IsJsonpRequest(out callback))
        //    {
        //        return Task.Factory.StartNew(() =>
        //        {
        //            var writer = new StreamWriter(stream);
        //            writer.Write(callback + "(");
        //            writer.Flush();

        //            base.WriteToStreamAsync(type, value, stream, contentHeaders, transportContext).Wait();

        //            writer.Write(")");
        //            writer.Flush();
        //        });
        //    }
        //    else
        //    {
        //        return base.WriteToStreamAsync(type, value, stream, contentHeaders, transportContext);
        //    }
        //}


        private bool IsJsonpRequest(out string callback)
        {
            callback = null;

            if (HttpContext.Current.Request.HttpMethod != "GET")
                return false;

            callback = HttpContext.Current.Request.QueryString[this.CallbackQueryParameter];

            return !string.IsNullOrEmpty(callback);
        }
    }
}