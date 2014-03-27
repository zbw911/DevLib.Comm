using System;
using System.Globalization;
using System.Web;
using System.Web.Mvc;

namespace Dev.Comm.Web.Mvc.Filter
{

    /// <summary>
    /// 对Action 设置及处理304请求
    /// 参考 http://stackoverflow.com/questions/602104/how-return-304-status-with-fileresult-in-asp-net-mvc-rc1
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class IfModifiedSinceAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 检查是否相关的文件
        /// </summary>
        public string RelateFilePath { set; get; }

        /// <summary>
        /// 强制304 ，不管文件是否已经更改
        /// </summary>
        public bool Force304 { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            Func<DateTime> funcModifyDate = () =>
            {
                DateTime modifyDate = DateTime.Now.Date;
                if (!string.IsNullOrEmpty(RelateFilePath))
                {
                    var scriptpath = System.Web.HttpContext.Current.Server.MapPath(RelateFilePath);

                    modifyDate = System.IO.File.GetLastWriteTime(scriptpath);
                }

                return modifyDate;
            };

            Func<DateTime, bool> isModifyed = (lastMod) =>
            {
                if (Force304 || DateTimeSecondsAreEquals(lastMod, funcModifyDate()))
                {
                    filterContext.Result = new ContentResult();

                    return false;
                }
                return true;
            };

            HttpStateHandler.Http304(isModifyed, funcModifyDate);

            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 比较时间的秒数，由于 If-Modify-Since 返回的只是秒的时间
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        private bool DateTimeSecondsAreEquals(DateTime t1, DateTime t2)
        {
            return (int)(t1 - DateTime.MinValue).TotalSeconds == (int)(t2 - DateTime.MinValue).TotalSeconds;
        }

    }
}