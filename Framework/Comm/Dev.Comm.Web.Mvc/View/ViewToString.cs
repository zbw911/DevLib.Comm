// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年07月16日 15:13
//  
//  修改于：2013年07月16日 15:28
//  文件名：Dev.Libs/Dev.Comm.Web.Mvc/ViewEngine.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System.IO;
using System.Web.Mvc;

namespace Dev.Comm.Web.Mvc.View
{
    /// <summary>
    ///   独立的ViewRender
    /// </summary>
    public class ViewToString
    {
        #region Class Methods

        /// <summary>
        ///   PartialView To String
        /// </summary>
        /// <param name="viewName"> </param>
        /// <param name="controller"> </param>
        /// <returns> </returns>
        public static string PartialView(Controller controller, string viewName)
        {
            var view = ViewEngines.Engines.FindPartialView(controller.ControllerContext, viewName);
            return ViewToContent(controller, view);
        }

        /// <summary>
        ///   View To String
        /// </summary>
        /// <param name="viewName"> </param>
        /// <param name="controller"> </param>
        /// <param name="masterName"> default is null, use the default Master </param>
        /// <returns> </returns>
        public static string View(Controller controller, string viewName, string masterName = null)
        {
            //var viewName = "_jsLogin";
            var view = ViewEngines.Engines.FindView(controller.ControllerContext, viewName, masterName);

            return ViewToContent(controller, view);
        }

        private static string ViewToContent(Controller controller, ViewEngineResult view)
        {
            string content;
            using (var writer = new StringWriter())
            {
                var context = new ViewContext(controller.ControllerContext, view.View, controller.ViewData,
                                              controller.TempData,
                                              writer);
                view.View.Render(context, writer);

                writer.Flush();
                content = writer.ToString();
            }
            return content;
        }

        #endregion
    }
}