//using System.Collections.Generic;
//using System.Text.RegularExpressions;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
///*
// * 二级域名的实现类（来自网上的方法，自己添加了一个写入DataTokens中的功能）
// * Author：Jobily
// * Email:jobily@foxmail.com
// * UpdateTime：2011-08-19 10:45
// */
//using Dev.Comm.Web.Mvc.Domain;

//namespace HB.Controllers
//{
//    public class DomainRoute : Route
//    {
//        private Regex domainRegex;
//        private Regex pathRegex;

//        public string Domain { get; set; }

//        public DomainRoute(string domain, string url, RouteValueDictionary defaults)
//            : base(url, defaults, new MvcRouteHandler())
//        {
//            Domain = domain;
//        }

//        public DomainRoute(string domain, string url, RouteValueDictionary defaults, IRouteHandler routeHandler)
//            : base(url, defaults, routeHandler)
//        {
//            Domain = domain;
//        }

//        public DomainRoute(string domain, string url, object defaults)
//            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
//        {
//            Domain = domain;
//        }
//        public DomainRoute(string domain, string url, object defaults, object constraints)
//            : base(url, new RouteValueDictionary(defaults), new MvcRouteHandler())
//        {
//            Domain = domain;
//            Constraints = new RouteValueDictionary(constraints);
//        }

        

//        public DomainRoute(string domain, string url, object defaults, IRouteHandler routeHandler)
//            : base(url, new RouteValueDictionary(defaults), routeHandler)
//        {
//            Domain = domain;
//        }

//        public override RouteData GetRouteData(HttpContextBase httpContext)
//        {
//            #region 取得请求的域名以及路径
//            Url = Url.ToLower();
//            Domain = Domain.ToLower();
//            // 请求信息
//            string requestDomain = httpContext.Request.Headers["host"];
//            if (!string.IsNullOrEmpty(requestDomain))
//            {
//                if (requestDomain.IndexOf(":") > 0)
//                {
//                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
//                }
//            }
//            else
//            {
//                requestDomain = httpContext.Request.Url.Host;
//            }
//            string requestPath = httpContext.Request.AppRelativeCurrentExecutionFilePath.Substring(2) + httpContext.Request.PathInfo;

//            requestDomain = requestDomain.ToLower();
//            requestPath = requestPath.ToLower();
//            #endregion

//            //if (requestDomain == "www.pinpaide.com" || requestDomain == "localhost")
//            //{
//            //    if (Domain != "www.pinpaide.com" && Domain != "localhost")
//            //    {
//            //        return null;
//            //    }
//            //    return base.GetRouteData(httpContext);
//            //}
//            // 构造 regex
//            domainRegex = CreateRegex(Domain);
//            pathRegex = CreateRegex(Url, Constraints);


//            // 匹配域名和路由
//            Match domainMatch = domainRegex.Match(requestDomain);
//            Match pathMatch = pathRegex.Match(requestPath);

//            // 路由数据
//            RouteData data = null;
//            //data = base.GetRouteData(httpContext);
//            //1、去掉非法URL请求的情况【目的:让 xx.xxx.pinpaide.com 不能访问任何文件】
//            if (!domainMatch.Success) return null;
//            //2、去掉请求URL与路由中的URL不一致的情况【目的:让daigou.pinpaide.com 不能访问 www.pinpaide.com下的路由】
//            if (Domain != "{area}.pinpaide.com"&&Domain != requestDomain) return null;
//            //3、泛域名解析时去掉其它命名空间的路由【目的:让 xxx.pinpaide.com 不能访问 www.pinpaide.com 下的路由】
//            if (Domain == "{area}.pinpaide.com" && Defaults.ContainsKey("Namespaces"))
//            {
//                if (((string[])Defaults["Namespaces"])[0] != "PP.Web.BrandSiteController")
//                {
//                    return null;
//                }
//            }
//            if (Domain == "www.pinpaide.com" || Domain == "localhost")
//            {
//                return null;
//            }


//            data = base.GetRouteData(httpContext);
//            if (data == null) return null;
//            if (requestDomain == "daigou.pinpaide.com")
//            {
                
//                // 添加默认选项
//                if (Defaults != null)
//                {
//                    foreach (KeyValuePair<string, object> item in Defaults)
//                    {
//                        if (!data.Values.ContainsKey(item.Key))
//                            data.Values[item.Key] = item.Value;
//                        if (item.Key.Equals("area") || item.Key.Equals("Namespaces"))
//                        {
//                            if (item.Key.Equals("Namespaces"))
//                            {
//                                string[] names = (string[])item.Value;
//                                if (names[0] != "PP.Web.Areas.DaiGou.Controllers") return null;
//                            }
//                            data.DataTokens[item.Key] = item.Value;
//                        }
//                    }
//                }

//            }
//            else
//            {
//                // 添加默认选项
//                if (Defaults != null)
//                {
//                    foreach (KeyValuePair<string, object> item in Defaults)
//                    {
//                        if (item.Key.Equals("Namespaces"))
//                        {
//                            string[] names = (string[])item.Value;
//                            if (names[0] != "PP.Web.BrandSiteController") return null;
//                        }

//                        if (!data.Values.ContainsKey(item.Key))
//                            data.Values[item.Key] = item.Value;
//                        if (item.Key.Equals("area") || item.Key.Equals("Namespaces"))
//                        {
//                            data.DataTokens[item.Key] = item.Value;
//                        }
//                        //if (item.Key.Equals("area"))
//                        //    data.Values[item.Key] = requestDomain.Substring(0, requestDomain.IndexOf("."));
//                    }
//                }
//            }

//            return data;

//            #region 已注释
//            if (domainMatch.Success && pathMatch.Success )
//            {


//                if(data==null)
//                data = new RouteData(this, RouteHandler);
                
//                // 添加默认选项
//                if (Defaults != null)
//                {
//                    foreach (KeyValuePair<string, object> item in Defaults)
//                    {
//                        if (!data.Values.ContainsKey(item.Key))
//                        data.Values[item.Key] = item.Value;
//                        if (item.Key.Equals("area") || item.Key.Equals("Namespaces"))
//                        {
//                            data.DataTokens[item.Key]=item.Value;
//                        }
//                    }
//                }

//                // 匹配域名路由
//                for (int i = 1; i < domainMatch.Groups.Count; i++)
//                {
//                    Group group = domainMatch.Groups[i];
//                    if (group.Success)
//                    {
//                        string key = domainRegex.GroupNameFromNumber(i);

//                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
//                        {
//                            if (!string.IsNullOrEmpty(group.Value))
//                            {
//                                if (!data.Values.ContainsKey(key))
//                                data.Values[key] = group.Value;
//                                if (key.Equals("area"))
//                                {
//                                    data.DataTokens[key]=group.Value;
//                                }
//                            }
//                        }
//                    }
//                }

//                // 匹配域名路径
//                for (int i = 1; i < pathMatch.Groups.Count; i++)
//                {
//                    Group group = pathMatch.Groups[i];
//                    if (group.Success)
//                    {
//                        string key = pathRegex.GroupNameFromNumber(i);

//                        if (!string.IsNullOrEmpty(key) && !char.IsNumber(key, 0))
//                        {
//                            if (!string.IsNullOrEmpty(group.Value))
//                            {
//                                data.Values[key] = group.Value;
//                                if (key.Equals("area"))
//                                {
//                                    data.DataTokens[key]= group.Value;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
           

//            if (Constraints != null)
//            {
//                foreach (KeyValuePair<string, object> item in Constraints)
//                {
                    
//                    bool isMatch = base.ProcessConstraint(httpContext, Constraints, item.Key, data.Values, RouteDirection.IncomingRequest);
//                    if (!isMatch) return null;
//                }
//            }

            
//            return data;

//            #endregion

//        }

//        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
//        {
//            // 请求信息
//            string requestDomain = requestContext.HttpContext.Request.Headers["host"];
//            if (!string.IsNullOrEmpty(requestDomain))
//            {
//                if (requestDomain.IndexOf(":") > 0)
//                {
//                    requestDomain = requestDomain.Substring(0, requestDomain.IndexOf(":"));
//                }
//            }
//            else
//            {
//                requestDomain = requestContext.HttpContext.Request.Url.Host;
//            }
//            if (requestDomain == "www.pinpaide.com" || requestDomain == "localhost")
//                return base.GetVirtualPath(requestContext, values);
//            else return base.GetVirtualPath(requestContext, RemoveDomainTokens(values));
//        }

//        public DomainData GetDomainData(RequestContext requestContext, RouteValueDictionary values)
//        {
//            // 获得主机名
//            string hostname = Domain;
//            foreach (KeyValuePair<string, object> pair in values)
//            {
//                hostname = hostname.Replace("{" + pair.Key + "}", pair.Value.ToString());
//            }

//            // Return 域名数据
//            return new DomainData
//            {
//                Protocol = "http",
//                HostName = hostname,
//                Fragment = ""
//            };
//        }

//        private Regex CreateRegex(string source, RouteValueDictionary Constraints)
//        {
//            //productId = @"[\d]*"
//            if (source.IndexOf("story") != -1)
//            {
//                string tt = "";
//            }

//            //if (Constraints.Keys.Count == 5) 
//            //{
//            //    string dd = "";
//            //}
//                // 替换
//            source = source.Replace("/", @"\/?");
//            source = source.Replace(".", @"\.?");
//            //source = source.Replace("-", @"\-?");
//            //source = source.Replace("{", @"(?<");
//            ////source = source.Replace("}", @">([a-zA-Z0-9_]*))");
//            //source = source.Replace("}", @">([\s\S_]*))");
//            if (Constraints != null)
//            {
//                foreach (var item in Constraints)
//                {
//                    string key = item.Key.ToString();
//                    //source = source.Replace("{" + key + "}", item.Value.ToString());
//                    //source = source.Replace("{" + key + "}", @"(?<" + key + ">" + Constraints[key].ToString() + ")");
//                    source = source.Replace("{" + key + "}", @"(?<" + key + ">(" + item.Value.ToString() + "))");

//                    //if (key != "controller" && key != "action" && key != "area" && key != "namespaces") 
//                    //if (key != "namespaces")
//                    //{
//                    //    //source = source.Replace("{"+key+"}",mDefaults[key].ToString());

//                    //    if (Constraints[key].ToString().IndexOf("[") != -1)
//                    //        source = source.Replace("{" + key + "}", @"(?<" + key + ">" + mDefaults[key].ToString() + ")");
//                    //    //else source = source.Replace("{" + key + "}", mDefaults[key].ToString());
//                    //    //source = source.Replace("}", @"");
//                    //}
//                }
//            }

//                return new Regex("^" + source + "$");
//            //return new Regex("^" + source + "$");
//        }

        


//        private Regex CreateRegex(string source)
//        {
//            // 替换
//            source = source.Replace("/", @"\/?");
//            source = source.Replace(".", @"\.?");
//            source = source.Replace("-", @"\-?");
//            source = source.Replace("{", @"(?<");
//            source = source.Replace("}", @">([a-zA-Z0-9_]*))");

//            return new Regex("^" + source + "$");
//        }


//        private RouteValueDictionary RemoveDomainTokens(RouteValueDictionary values)
//        {
//            Regex tokenRegex = new Regex(@"({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?({[a-zA-Z0-9_]*})*-?\.?\/?");
//            Match tokenMatch = tokenRegex.Match(Domain);
//            for (int i = 0; i < tokenMatch.Groups.Count; i++)
//            {
//                Group group = tokenMatch.Groups[i];
//                if (group.Success)
//                {
//                    string key = group.Value.Replace("{", "").Replace("}", "");
//                    if (values.ContainsKey(key))
//                        values.Remove(key);
//                }
//            }

//            return values;
//        }
//    }
//}
