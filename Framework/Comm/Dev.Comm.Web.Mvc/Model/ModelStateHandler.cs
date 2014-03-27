using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Dev.Comm.Web.Mvc.Model
{
    /// <summary>
    /// ModelState 的处理
    /// </summary>
    public class ModelStateHandler
    {
        /// <summary>
        /// 取得 ModelState 中的全部错误
        /// </summary>
        /// <param name="modelState"></param>
        /// <returns></returns>
        public static IEnumerable<ModelError> GetAllError(ModelStateDictionary modelState)
        {
            var allErrors = modelState.Values.SelectMany(v => v.Errors);

            return allErrors;

        }

        /// <summary>
        /// 取得错误字符串
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="newline"></param>
        /// <param name="template">用于展现的模板 </param>
        /// <returns></returns>
        public static string GetAllErrorStr(ModelStateDictionary modelState, string newline = "<br/>", string template = "{0}{1}")
        {
            var allErrors = GetAllError(modelState);

            var errorstr = allErrors.Aggregate("", (current, modelError) => current + string.Format(template, modelError.ErrorMessage, newline));

            return errorstr;
        }


        /// <summary>
        /// 清除与Key相关的Error
        /// </summary>
        /// <param name="modelState"></param>
        /// <param name="key"></param>
        public static void ClearError(ModelStateDictionary modelState, string key)
        {
            var state = modelState[key];
            if (state != null)
            {
                state.Errors.Clear();
            }
        }


        /// <summary>
        /// 清除全部Error,实际上可以用 modelState.Clear();加在这里只是保持完整性罢了
        /// </summary>
        /// <param name="modelState"></param>
        public static void ClearAllError(ModelStateDictionary modelState)
        {
            modelState.Clear();
            //foreach (var key in modelState.Keys)
            //{
            //    ClearError(modelState, key);
            //}
        }




    }
}
