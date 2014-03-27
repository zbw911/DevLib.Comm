// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年05月13日 18:15
//  
//  修改于：2013年05月13日 18:20
//  文件名：FrameworkOnly/Dev.Comm.Web.Mvc/WatchAttribute.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace Dev.Comm.Web.Mvc.Filter
{

    #region TraceRunAttribute
    //ToDo:其实这里跟踪使用 StopWatch 要比DateTime好，But...

    /// <summary>
    /// 用于存储跟踪信息的树结点，
    /// 其中 child 应该用 CollectBase会好些
    /// </summary>
    internal class WatchNode
    {
        private List<WatchNode> _child = new List<WatchNode>();
        public string Name { get; set; }
        public long OnActionExecuting { get; set; }
        public long OnActionExecuted { get; set; }
        public long OnResultExecuted { get; set; }
        public long OnResultExecuting { get; set; }

        public WatchNode Parent { get; set; }
        public List<WatchNode> Child
        {
            get { return this._child; }
            set { this._child = value; }
        }


        public long All
        {
            get { return (OnResultExecuted - OnActionExecuting) / 10000; }
        }

        public long Action
        {
            get { return (this.OnActionExecuted - this.OnActionExecuting) / 10000; }
        }

        public long Result
        {
            get { return (this.OnResultExecuted - this.OnResultExecuting) / 10000; }
        }

        public bool IsChild { get; set; }

        public string ParentName
        {
            get
            {
                if (Parent == null) return null;
                return Parent.Name;
            }
        }
    }

    /// <summary>
    /// 运行时数据
    /// </summary>
    public class NameTime
    {
        public string Name { get; set; }
        public long Time { get; set; }
        public string Do { get; set; }

        public ViewContext Parent
        {
            get;
            set;
        }

        public string ParentName
        {
            get
            {
                if (Parent == null)
                    return null;

                return Parent.RouteData.Values["action"].ToString();
            }
        }
    }
    /// <summary>
    /// 跟踪MVC Action Result 运行所用时间
    /// </summary>
    public class TraceRunAttribute : ActionFilterAttribute
    {
        private string _newLine = "<br/>\r\n";
        private const string __List__ = "______List__________";
        internal const string ViewResultTypeKey = "______________________ViewType________";
        public TraceRunAttribute()
        {
        }

        public bool IsShow
        {
            get;
            set;
        }

        /// <summary>
        /// 新行
        /// </summary>

        public string NewLine
        {
            get { return this._newLine; }
            set { this._newLine = value; }
        }


        /// <summary>
        /// 标题模板
        /// </summary>
        public string TitleTemple { get; set; }
        /// <summary>
        /// 组模板
        /// </summary>
        public string ItemTemple { get; set; }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string name = filterContext.ActionDescriptor.ActionName;
            var context = filterContext.HttpContext;

            var parent = filterContext.ParentActionViewContext;

            this.CheckItem(context, name, "OnActionExecuted", parent);

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            string name = filterContext.ActionDescriptor.ActionName;
            var parent = filterContext.ParentActionViewContext;
            var context = filterContext.HttpContext;

            this.CheckItem(context, name, "OnActionExecuting", parent);


            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            var parent = filterContext.ParentActionViewContext;

            string name = filterContext.RouteData.Values["action"].ToString();
            var context = filterContext.HttpContext;

            this.CheckItem(context, name, "OnResultExecuted", parent);

            if (!filterContext.IsChildAction && filterContext.Result is ViewResult)
            {
                //这个Key仅用于跟踪 ResponeRequest指示用
                context.Items.Add(ViewResultTypeKey, "ViewResult");
                WriteReport(context);
            }


            base.OnResultExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            string name = filterContext.RouteData.Values["action"].ToString();
            var context = filterContext.HttpContext;
            var parent = filterContext.ParentActionViewContext;
            this.CheckItem(context, name, "OnResultExecuting", parent);

            base.OnResultExecuting(filterContext);
        }

        private void WriteReport(HttpContextBase context)
        {

            if (!IsShow)
                context.Response.Write("<!--");
            context.Response.Write("<b>调试信息</b>" + NewLine);

            var list = context.Items[__List__] as List<NameTime>;



            foreach (var nameTime in list)
            {
                context.Response.Write("" + nameTime.Name + "->" + nameTime.Do + "->" + nameTime.Time + "->" + (nameTime.Parent != null ? nameTime.Parent.RouteData.Values["action"].ToString() : "NONE") + NewLine);
            }
            context.Response.Write("<br/>-------------------------------------------------------------" + NewLine);

            var node = Parse(list);

            var str = PrintNode(node);

            context.Response.Write(str);

            if (!IsShow)
                context.Response.Write("-->");


            // CS html Trace

            var cshtmlstr = CshtmlTrance.Print(IsShow, NewLine);

            context.Response.Write(cshtmlstr);


            context.Items.Remove(__List__);
        }


        private string PrintNode(WatchNode node)
        {

            var s = "name:" + node.Name + " = " + node.All + "  action->" + node.Action + " Result->" + node.Result + " parent->" + node.ParentName + NewLine;

            if (node.Child != null && node.Child.Count > 0)
            {
                s += "c___:" + node.Name + "=" + node.Child.Sum(x => x.All) + "  action->" +
                     node.Child.Sum(x => x.Action) + " Result->" + node.Child.Sum(x => x.Result) + NewLine;
            }

            foreach (var watchData in node.Child)
            {
                var temp = watchData;
                var c = 0;
                while (temp.Parent != null)
                {
                    temp = temp.Parent;
                    c++;
                }
                s += "".PadLeft(c * 2, '-') + PrintNode(watchData);
            }

            return s;

        }



        private WatchNode Parse(List<NameTime> list)
        {
            WatchNode TopNode = new WatchNode();
            var current = TopNode;
            foreach (var nameTime in list)
            {

                switch (nameTime.Do)
                {
                    case "OnActionExecuting":
                        if (nameTime.Parent == null)
                        {
                            TopNode.IsChild = false;
                            TopNode.OnActionExecuting = nameTime.Time;
                        }
                        else
                        {
                            var newnode = new WatchNode();
                            newnode.Parent = current;
                            current.Child.Add(newnode);
                            current = newnode;
                            current.IsChild = true;
                            current.OnActionExecuting = nameTime.Time;
                        }

                        current.Name = nameTime.Name;

                        break;
                    case "OnActionExecuted":
                        current.OnActionExecuted = nameTime.Time;

                        break;
                    case "OnResultExecuting":
                        current.OnResultExecuting = nameTime.Time;
                        break;
                    case "OnResultExecuted":
                        current.OnResultExecuted = nameTime.Time;

                        if (current.Parent != null && current.Parent.Name == nameTime.ParentName)
                        {
                            current = current.Parent;
                        }

                        break;

                }

            }

            return TopNode;
        }

        private void CheckItem(HttpContextBase httpcontext, string name, string Do, ViewContext parent)
        {

            if (!httpcontext.Items.Contains(__List__))
            {
                httpcontext.Items.Add(__List__, new List<NameTime>());
            }

            var list = httpcontext.Items[__List__] as List<NameTime>;

            list.Add(new NameTime
            {
                Name = name,
                Time = System.DateTime.Now.Ticks,
                Do = Do,
                Parent = parent
            });

        }



    }

    #endregion

    #region CSHTML TRACE

    internal class CshtmlTraceModel
    {
        public string Name { get; set; }
        public long TimeTicks { get; set; }
    }

    /// <summary>
    /// CSHtml页面TRance
    /// </summary>
    public class CshtmlTrance
    {
        private const string CshtmlTrancekey = "_________CshtmlTrancekey_____________";
        public static void Add(string name, long timeTicket)
        {
            var httpcontext = HttpContext.Current;

            if (!httpcontext.Items.Contains(CshtmlTrancekey))
            {
                httpcontext.Items.Add(CshtmlTrancekey, new List<CshtmlTraceModel>());
            }

            var list = httpcontext.Items[CshtmlTrancekey] as List<CshtmlTraceModel>;

            list.Add(new CshtmlTraceModel
            {
                Name = name,
                TimeTicks = timeTicket
            });
        }


        public static string Print(bool isshow = false, string newLine = "<br/>\r\n")
        {
            var httpcontext = HttpContext.Current;

            var list = new List<CshtmlTraceModel>();

            if (httpcontext.Items.Contains(CshtmlTrancekey))
            {
                list = httpcontext.Items[CshtmlTrancekey] as List<CshtmlTraceModel>;
            }


            StringBuilder sb = new StringBuilder();




            if (!isshow)
                sb.Append("<!-- ").Append(newLine);

            sb.Append("cshtml 跟踪报告").Append(newLine);

            foreach (var cshtmlTraceModel in list)
            {
                sb.Append("" + cshtmlTraceModel.Name + "->" + cshtmlTraceModel.TimeTicks / 10000).Append(newLine);
            }

            if (!isshow)
                sb.Append("-->");

            httpcontext.Items.Remove(CshtmlTrancekey);
            return sb.ToString();
        }

    }

    #endregion

    #region RequestTrace
    /// <summary>
    /// 对Request一次的时间进行监测
    /// </summary>
    public class BeginEndRequestTrace
    {

        private const string Key = "_____________BeginEndRequest___________";

        private const string BeginRequest = Key + "BeginRequest";

        private const string EndRequest = Key + "EndRequest";

        /// <summary>
        /// http://stackoverflow.com/questions/1123741/why-cant-my-host-softsyshosting-com-support-beginrequest-and-endrequest-event
        /// <![CDATA[public class MySmartApp: HttpApplication{
        /// public override void Init(){
        ///     this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
        ///    this.EndRequest += new EventHandler(MvcApplication_EndRequest);
        ///  }
        ///  protected void Application_Start(){
        ///      RegisterRoutes(RouteTable.Routes);
        ///  } 
        ///}
        ///or like this: 
        ///public class MySmartApp: HttpApplication{
        /// public MySmartApp(){
        ///  this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
        /// this.EndRequest += new EventHandler(MvcApplication_EndRequest);
        /// }
        ///protected void Application_Start(){
        /// RegisterRoutes(RouteTable.Routes);
        ///} 
        ///}
        ///]]>
        /// </summary>
        /// <param name="app"></param>
        ///<param name="isshow">是否在页面上显示 </param>
        ///<param name="newLine">使用什么换行 </param>
        public static void Init(HttpApplication app, bool isshow = false, string newLine = "<br/>\r\n")
        {

            app.BeginRequest += (sender, e) =>
            {
                app.Context.Items[BeginRequest] = System.DateTime.Now.Ticks;
                //app.Response.Write(System.DateTime.Now.Ticks);
            };

            app.EndRequest += (sender, e) =>
            {
                app.Context.Items[EndRequest] = System.DateTime.Now.Ticks;
                //app.Response.Write(System.DateTime.Now.Ticks);

                var strPrint = Print(isshow, newLine);

                app.Response.Write(strPrint);
            };
        }

        private static string Print(bool isshow = false, string newLine = "<br/>\r\n")
        {

            var httpcontext = HttpContext.Current;


            //对于ajax请求，不附加结果
            var xmlhttp = httpcontext.Request.Headers["X-Requested-With"];
            if (xmlhttp == "XMLHttpRequest")
            {
                return string.Empty;
            }

            //根据返回的的类型，只有返回text/html时，才返回报告 
            var responetype = httpcontext.Response.ContentType;
            if (responetype != "text/html")
            {
                return string.Empty;
            }

            //只打印 ViewResult类型的，这个方法不太通用，这样将会依赖于 TraceRunAttribute ，现在暂时没有更好的办法，先这样用
            if (!httpcontext.Items.Contains(TraceRunAttribute.ViewResultTypeKey) || (string)httpcontext.Items[TraceRunAttribute.ViewResultTypeKey] != "ViewResult")
            {
                httpcontext.Items.Remove(TraceRunAttribute.ViewResultTypeKey);
                return string.Empty;
            }

            long tbegin = 0;
            long tend = 0;

            if (httpcontext.Items.Contains(BeginRequest))
            {
                tbegin = (long)httpcontext.Items[BeginRequest];
                httpcontext.Items.Remove(BeginRequest);
            }


            if (httpcontext.Items.Contains(EndRequest))
            {
                tend = (long)httpcontext.Items[EndRequest];
                httpcontext.Items.Remove(EndRequest);
            }


            StringBuilder sb = new StringBuilder();



            if (!isshow)
                sb.Append("<!-- ").Append(newLine);
            sb.Append(newLine);
            sb.Append("Request 跟踪报告").Append(newLine);


            if (tbegin == 0 || tend == 0)
            {
                sb.Append("Request->Not Setup").Append(newLine);
            }
            else
            {
                sb.Append("BeginRequest->" + tbegin).Append(newLine);
                sb.Append("EndRequest->" + tend).Append(newLine);
                sb.Append("Request->" + (tend - tbegin) / 10000).Append(newLine);

            }

            if (!isshow)
                sb.Append("-->");

            return sb.ToString();
        }


    }



    /// <summary>
    /// 使用IHttpModule 方式注册监听事件
    /// </summary>
    public class BeginEndRequestTraceHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            BeginEndRequestTrace.Init(context);
            //throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            //throw new System.NotImplementedException();
        }
    }


    /// <summary>
    /// Begin  End Request Trace
    /// </summary>
    public static class BeginEndRequestTraceHttpModuleRegister
    {
        /// <summary>
        /// Do it
        /// </summary>
        public static void Do()
        {
            DynamicModuleUtility.RegisterModule(typeof(BeginEndRequestTraceHttpModule));
        }
    }

    #endregion
}