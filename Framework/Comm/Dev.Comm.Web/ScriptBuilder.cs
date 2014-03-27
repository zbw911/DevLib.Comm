// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ScriptBuilder.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System.Collections;
using System.Text;

namespace Dev.Comm.Web
{
    public class ScriptBuilder
    {
        public const string START_SCRIPT = "<script language=\"javascript\" type=\"text/javascript\">";
        public const string END_SCRIPT = "</script>";

        public static string GetScriptBlock(string AScriptText)
        {
            var sb = new StringBuilder();
            sb.Append(START_SCRIPT);
            sb.Append(AScriptText);
            sb.Append(END_SCRIPT);

            return sb.ToString();
        }

        public static string RowMouseOverByTxb()
        {
            var sb = new StringBuilder();
            sb.Append("function RowMouseOverByTxb(source)");
            sb.Append("{");
            sb.Append("     source.select();");
            sb.Append("}");

            return sb.ToString();
        }

        #region 打开窗口

        public static string OpenWindow(string url, string win, string width, string height)
        {
            var sb = new StringBuilder();
            sb.Append("var name='");
            sb.Append(win);
            sb.Append("'; var url='");
            sb.Append(url);
            sb.Append("'; var iWidth;");
            sb.Append("iWidth=");
            sb.Append(width);
            sb.Append("; var iHeight;");
            sb.Append("iHeight=");
            sb.Append(height);
            sb.Append("; var iTop = (window.screen.availHeight-30-iHeight)/2;");
            sb.Append("var iLeft = (window.screen.availWidth-10-iWidth)/2;");

            sb.Append("try{");
            sb.Append("    var windowRef = ");
            sb.Append(
                "    window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');");
            sb.Append("    __windowArray_dx[__windowArray_dx.length] = windowRef;");
            sb.Append("}");
            sb.Append("catch(ex)");
            sb.Append("{");
            sb.Append("    alert('打开窗口错误:'+ex.message);");
            sb.Append("}");
            return sb.ToString();
        }

        public static string ConfirmOpenWindow(string msg, string url, string win, string width, string height)
        {
            var sb = new StringBuilder();

            sb.Append(ConfirmMsg(msg));
            sb.Append(OpenWindow(url, win, width, height));

            return sb.ToString();
        }

        public static string OpenWindow(string url)
        {
            var sb = new StringBuilder();

            sb.Append("var url='");
            sb.Append(url);
            sb.Append("'; var iWidth;");
            sb.Append("iWidth=window.screen.availWidth-10;");
            sb.Append("var iHeight;");
            sb.Append("iHeight=window.screen.availHeight-30;");
            sb.Append("var iTop = (window.screen.availHeight-30-iHeight)/2;");
            sb.Append("var iLeft = (window.screen.availWidth-10-iWidth)/2;");
            //sb.Append("window.open(url,'','height=,width=,top=0,left=0,toolbar=no,menubar=no,scrollbars=yes,resizeable=no,location=no,status=no');");

            sb.Append("try{");
            sb.Append("    var windowRef = ");
            sb.Append(
                "    window.open(url,'','height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=yes,resizeable=no,location=no,status=no');");
            sb.Append("    __windowArray_dx[__windowArray_dx.length] = windowRef;");
            sb.Append("}");
            sb.Append("catch(ex)");
            sb.Append("{");
            sb.Append("    alert('打开窗口错误:'+ex.message);");
            sb.Append("}");
            return sb.ToString();
        }

        public static string ConfirmOpenWindow(string msg, string url)
        {
            var sb = new StringBuilder();

            sb.Append(ConfirmMsg(msg));
            sb.Append(OpenWindow(url));

            return sb.ToString();
        }

        public static string GetOpenWindowScript()
        {
            var sb = new StringBuilder();
            sb.Append("function _OpenWindow(url,win,iWidth,iHeight){");
            sb.Append("var name='win';");
            sb.Append("var iTop = (window.screen.availHeight-30-iHeight)/2;");
            sb.Append("var iLeft = (window.screen.availWidth-10-iWidth)/2;");

            sb.Append("try{");
            sb.Append("    var windowRef = ");
            sb.Append(
                "    window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=yes,resizeable=no,location=no,status=no');");
            sb.Append("    __windowArray_dx[__windowArray_dx.length] = windowRef;");
            sb.Append("}");
            sb.Append("catch(ex)");
            sb.Append("{");
            sb.Append("    alert('打开窗口错误:'+ex.message);");
            sb.Append("}");
            sb.Append("}");

            return sb.ToString();
        }

        public static string GetCloseAllWindowScript()
        {
            var sb = new StringBuilder();
            sb.Append("function _closeAllWindows()");
            sb.Append("{");
            sb.Append("    for (var i=__windowArray_dx.length-1; i>=0; i--)");
            sb.Append("    {");
            sb.Append("        if ( __windowArray_dx.length <= 0 )");
            sb.Append("            break;");

            sb.Append("        var windowRef = __windowArray_dx[i];");

            sb.Append("        if ( (windowRef != null) && (windowRef.closed == false) )");
            sb.Append("        {");
            sb.Append("            windowRef.close();");
            sb.Append("        }");
            sb.Append("        __windowArray_dx.pop();");
            sb.Append("    }");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 确认框

        public static string ConfirmMsg(string msg)
        {
            var sb = new StringBuilder();

            sb.Append("if (!confirm('");
            sb.Append(msg);
            sb.Append("'))");
            sb.Append("     return;");

            return sb.ToString();
        }

        #endregion

        #region 取得showModalDialog脚本

        public static string GetshowModalDialog(string url, string data, string arg)
        {
            var sb = new StringBuilder();
            sb.Append("var returnValue = window.showModalDialog('");
            sb.Append(url);
            sb.Append("',");
            sb.Append(data);
            sb.Append(",'");
            sb.Append(arg);
            sb.Append("');");
            return sb.ToString();
        }

        #endregion

        #region 取得客户端onload脚本

        public static string GetClientOnLoadScript(string SubScript)
        {
            var sb = new StringBuilder();
            sb.Append("window.onload = function()");
            sb.Append("{");
            sb.Append(SubScript);
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 获取全选反选的脚本

        /// <summary>
        /// 用户button出发的全选和反选
        /// </summary>
        /// <param name="Flag">true 全选,false 反选</param>
        /// <returns></returns>
        public static string GetSelectAll4Btn(bool Flag)
        {
            var sb = new StringBuilder();
            if (Flag)
                sb.Append("function checkallbtn(source){");
            else
                sb.Append("function Uncheckbtn(source){");
            sb.Append("for (var i=0;i<document.form1.elements.length;i++)");
            sb.Append("{");
            sb.Append("    var e = document.form1.elements[i];");
            sb.Append("    if ((e.type == 'checkbox') && (e.id.indexOf(source) != -1))");
            sb.Append("    {");
            if (Flag)
                sb.Append("        e.checked = false;");
            sb.Append("        e.click();");
            sb.Append("    }");
            sb.Append("}");
            sb.Append("}");
            return sb.ToString();
        }

        /// <summary>
        /// 用于checkbox出发的全选
        /// </summary>
        /// <returns></returns>
        public static string GetSelectAll4Sub()
        {
            var sb = new StringBuilder();
            sb.Append("function checkallSub(t,c)");
            sb.Append("{");
            sb.Append("for (var i=0;i<document.form1.elements.length;i++)");
            sb.Append("{");
            sb.Append("    var e = document.form1.elements[i];");
            sb.Append("    if ((e.type == 'checkbox') && (e != t) && e.id.indexOf(c) != -1)");
            sb.Append("    {");
            sb.Append("        e.checked = !t.checked;");
            sb.Append("        e.click();");
            sb.Append("    }");
            sb.Append("}");
            sb.Append("}");
            //e.click();//用于反选

            return sb.ToString();
        }

        /// <summary>
        /// 用于checkbox出发的全选
        /// </summary>
        /// <returns></returns>
        public static string GetSelectAll4Cbk()
        {
            var sb = new StringBuilder();
            sb.Append("function checkall(t)");
            sb.Append("{");
            sb.Append("for (var i=0;i<document.form1.elements.length;i++)");
            sb.Append("{");
            sb.Append("    var e = document.form1.elements[i];");
            sb.Append("    if ((e.type == 'checkbox') && (e != t))");
            sb.Append("    {");
            sb.Append("        e.checked = !t.checked;");
            sb.Append("        e.click();");
            sb.Append("    }");
            sb.Append("}");
            sb.Append("}");
            //e.click();//用于反选

            return sb.ToString();
        }

        public static string GetUnSelectALL4Cbk()
        {
            var sb = new StringBuilder();
            sb.Append("function uncheck()");
            sb.Append("{");
            sb.Append("    for (var i=0;i<document.form1.elements.length;i++)");
            sb.Append("    {");
            sb.Append("        var e = document.form1.elements[i];");
            sb.Append("        if (e.name == 'flag') ");
            sb.Append("        {");
            sb.Append("            e.checked = !e.checked;");
            sb.Append("        }");
            sb.Append("    }");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 重定向

        public static string GetRedirect(string url)
        {
            return string.Format("window.location = '{0}';", url);
        }

        public static string ParentGetRedirect(string url)
        {
            return string.Format("window.parent.location = '{0}';", url);
        }

        public static string FrameGetRedirect(string url, string frame)
        {
            return string.Format("window.opener.top.frames['{0}'].location = '{1}';", frame, url);
        }

        #endregion

        #region 获取剪贴板数据

        public static string GetClipboardData()
        {
            var sb = new StringBuilder();
            sb.Append("function GetClipboardData(){");
            sb.Append("     return window.clipboardData.getData(\"Text\");");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 给控件填充剪贴板数据

        public static string GetSetValueFromClipboard(string ID)
        {
            var sb = new StringBuilder();
            sb.Append("var ctl = document.getElementById('");
            sb.Append(ID);
            sb.Append("');");
            sb.Append("if (ctl != null){");
            sb.Append("ctl.select();");
            sb.Append("ctl.value = GetClipboardData();}");

            return sb.ToString();
        }

        #endregion

        #region 输入备注信息

        public static string InputRemark(string RemarkID)
        {
            var sb = new StringBuilder();

            sb.Append("var name=prompt('确定要进行删除操作吗？请您输入备注信息','');");
            sb.Append("var ctl = document.getElementById('");
            sb.Append(RemarkID);
            sb.Append("');");
            sb.Append("while ( name=='')");
            sb.Append("{");
            sb.Append("     alert('请您输入备注信息');");
            sb.Append("     name = null;");
            sb.Append("     name=prompt('确定要进行删除操作吗？请您输入备注信息','');");
            sb.Append("}");
            sb.Append("if ( name==null)");
            sb.Append("     return false;");
            //sb.Append("     alert('请您输入备注信息');");
            //sb.Append("     return false;");
            //sb.Append("}else");
            //sb.Append("{");
            sb.Append("     if (ctl != null) ctl.value = name;");
            //sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region js创建表单提交

        public static string CreateFormAndSubmit(string Action, string Method, string Target, Hashtable Data)
        {
            var sb = new StringBuilder();
            sb.Append("var f = document.createElement(\"form\");");
            sb.Append("document.body.appendChild(f);");
            int cnt = 0;
            foreach (DictionaryEntry de in Data)
            {
                sb.Append("var i");
                sb.Append(cnt);
                sb.Append(" = document.createElement(\"input\");");
                sb.Append("i");
                sb.Append(cnt);
                sb.Append(".type = \"hidden\";");
                sb.Append("i");
                sb.Append(cnt);
                sb.Append(".value = \"");
                sb.Append(de.Value);
                sb.Append("\";");
                sb.Append("i");
                sb.Append(cnt);
                sb.Append(".name = \"");
                sb.Append(de.Key);
                sb.Append("\";");
                sb.Append("f.appendChild(i");
                sb.Append(cnt);
                sb.Append(");");
                cnt++;
            }

            sb.Append("f.action = \"");
            sb.Append(Action);
            sb.Append("\";");
            if (!string.IsNullOrEmpty(Method))
            {
                sb.Append("f.method = \"");
                sb.Append(Method);
                sb.Append("\";");
            }
            if (!string.IsNullOrEmpty(Target))
            {
                sb.Append("f.target = \"");
                sb.Append(Target);
                sb.Append("\";");
            }
            sb.Append("f.submit();");

            return sb.ToString();
        }

        #endregion

        #region 获取顶级的窗口

        public static string GetTopWindow()
        {
            var sb = new StringBuilder();
            sb.Append("function _GetTopWindow(){");
            sb.Append("    var win;");
            sb.Append("    if (window.top !=null)");
            sb.Append("        win = window.top;");
            sb.Append("    else");
            sb.Append("        win = window;");
            sb.Append("    return win;");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion

        #region 注册和解除事件

        public static string GetAddEvent()
        {
            var sb = new StringBuilder();
            sb.Append("function _addEvent(obj, evType, fn){");
            sb.Append("     if (obj.addEventListener){");
            sb.Append("        obj.addEventListener(evType, fn, false);");
            sb.Append("        return true;");
            sb.Append("     } else if (obj.attachEvent){");
            sb.Append("        var r = obj.attachEvent('on'+evType, fn);");
            sb.Append("        return r;");
            sb.Append("     } else {");
            sb.Append("        return false;");
            sb.Append("     }");
            sb.Append("    }");

            return sb.ToString();
        }

        public static string GetRemoveEvent()
        {
            var sb = new StringBuilder();
            sb.Append("function _removeEvent(obj, evType, fn, useCapture){");
            sb.Append("    if (obj.removeEventListener){");
            sb.Append("      obj.removeEventListener(evType, fn, useCapture);");
            sb.Append("      return true;");
            sb.Append("    } else if (obj.detachEvent){");
            sb.Append("      var r = obj.detachEvent('on'+evType, fn);");
            sb.Append("      return r;");
            sb.Append("    } else {");
            sb.Append("      alert('Handler could not be removed');");
            sb.Append("    }");
            sb.Append("}");

            return sb.ToString();
        }

        #endregion
    }
}