// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：JScript.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************

namespace Dev.Comm.Web.WebFrom
{
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public static class JScript
    {
        public const string START_SCRIPT = "<script language=\"javascript\" type=\"text/javascript\">";
        public const string END_SCRIPT = "</script>";


        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void Show(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');</script>");
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAlert(Page page, string msg)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "message",
                                                        "<script language='javascript' type='text/javascript'>alert('" +
                                                        msg + "');</script>");
        }

        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAndCloseCeng(Page page, string msg, string rtn)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg +
                                                    "');window.top.hidePopWin(" + rtn + ")</script>");
        }


        /// <summary>
        /// 显示消息提示对话框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ExecJs(Page page, string js)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>" + js + "</script>");
        }

        /// <summary>
        /// 显示消息提示对话框,刷新打开窗口，关闭本窗口
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAndCloseAndRefrush(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');"
                                                    + "opener.location.reload();"
                                                    + "window.close();"
                                                    + "</script>");
        }

        /// <summary>
        /// 显示消息提示对话框,刷新打开窗口，关闭本窗口
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowAndClose(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg + "');"
                                                    + "window.close();"
                                                    + "</script>");
        }

        /// <summary>
        /// 控件点击 消息确认提示框
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        public static void ShowConfirm(WebControl Control, string msg)
        {
            //Control.Attributes.Add("onClick","if (!window.confirm('"+msg+"')){return false;}");
            Control.Attributes.Add("onclick", "return confirm('" + msg + "');");
        }

        /// <summary>
        /// 显示消息提示对话框，并进行页面跳转
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="msg">提示信息</param>
        /// <param name="url">跳转的目标URL</param>
        public static void ShowAndRedirect(Page page, string msg, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("self.location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript();
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }


        public static void ShowAndRefresh(Page page, string msg)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("self.location.href=self.location.href;");
            Builder.Append("</script>");

            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }


        public static void ShowAndRedirectTop(Page page, string msg, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("alert('{0}');", msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        public static void RedirectTop(Page page, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            //			Builder.AppendFormat("alert('{0}');",msg);
            Builder.AppendFormat("top.location.href='{0}'", url);
            Builder.Append("</script>");
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        /// <summary>
        /// 输出自定义脚本信息
        /// </summary>
        /// <param name="page">当前页面指针，一般为this</param>
        /// <param name="script">输出脚本</param>
        public static void ResponseScript(Page page, string script)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>" + script + "</script>");
        }

        public static void ShowAndCloseSubModal(Page page, string msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "message",
                                                    "<script language='javascript' defer>alert('" + msg +
                                                    "');window.top.hidePopWin();</script>");
        }

        public static void Redirect(Page page, string url)
        {
            var Builder = new StringBuilder();
            Builder.Append("<script language='javascript' defer>");
            Builder.AppendFormat("self.location.href='{0}'", url);
            Builder.Append("</script>");
            //page.RegisterStartupScript();
            page.ClientScript.RegisterStartupScript(page.GetType(), "message", Builder.ToString());
        }

        public static string GetScriptBlock(string AScriptText)
        {
            var sb = new StringBuilder();
            sb.Append(START_SCRIPT);
            sb.Append(AScriptText);
            sb.Append(END_SCRIPT);

            return sb.ToString();
        }

        /*黄建妙 2009.06.18*/

        public static void SetHtmlElementValue(string formName, string elementName, string elementValue)
        {
            string js = @"<Script language='JavaScript'>if(document." + formName + "." + elementName +
                        "!=null){document." + formName + "." + elementName + ".value =" + elementValue + ";}</Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void MainRedirect(string url)
        {
            string js = "<script language=javascript>if (parent.opener != null) {parent.opener.top.location.href = '" +
                        url + "';window.close();} else {window.top.location.href='" + url + "';}</script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void MainRedirect(string message, string url)
        {
            string js = "<script language=javascript>alert('" + message +
                        "');if (parent.opener != null) {parent.opener.location.href = '" + url +
                        "';window.close();} else {window.top.location.href='" + url + "';}</script>";
            HttpContext.Current.Response.Write(js);
        }

        #region 提示信息ShowMessageBox

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="strMsg">信息内容</param>
        public static void ShowMessageBox(string strMsg)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>window.alert('" + strMsg + "');</script>");
        }

        #endregion

        #region 提示信息ShowMessageBox

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="message">任意类型的变量</param>
        public static void ShowMessageBox(object message)
        {
            string js = @"<Script language='JavaScript'>
                    alert('{0}');  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }

        #endregion

        #region 指向指定的某个页面

        /// <summary>
        /// 指向指定的某个页面
        /// </summary>
        /// <param name="strUrl">页面地址</param>
        public static void RedirectPage(string strUrl)
        {
            HttpContext.Current.Response.Write("<script language='javascript'>window.parent.location.href('" + strUrl +
                                               "');</script>");
        }

        #endregion

        #region 确认是否删除提示 ImageButton

        /// <summary>
        /// 确认是否删除提示 ImageButton
        /// </summary>
        /// <param name="IB">ImageButton控件</param>
        public static void ShowMessageBox(ImageButton IB, string strMesg)
        {
            IB.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region 确认是否删除提示 Button

        /// <summary>
        /// 确认是否删除提示 Button
        /// </summary>
        /// <param name="IB">Button控件</param>
        public static void ShowMessageBox(Button BN, string strMesg)
        {
            BN.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region 确认要编辑提示 LinkButton

        /// <summary>
        /// 确认是否删除提示 LinkButton
        /// </summary>
        /// <param name="lb">LinkButton控件</param>
        public static void ShowMessageBox(LinkButton LB, string strMesg)
        {
            LB.Attributes.Add("onclick", "return confirm('" + strMesg + "');");
        }

        #endregion

        #region 弹出提示信息，同时打开有效的页面

        /// <summary>
        /// 弹出提示信息，同时打开有效的页面
        /// </summary>
        /// <param name="message">提示信息内容</param>
        /// <param name="toURL">页面地址</param>
        public static void AlertAndRedirect(string message, string toURL)
        {
            string js = "<script language=javascript>alert('{0}');window.location.replace('{1}')</script>";
            HttpContext.Current.Response.Write(string.Format(js, message, toURL));
        }

        public static void AlertAndRefresh(string message)
        {
            string js = "<script language=javascript>alert('{0}');window.location.href = window.location.href;</script>";
            HttpContext.Current.Response.Write(string.Format(js, message));
        }

        #endregion

        #region 回到历史页面

        /// <summary>
        /// 回到历史页面
        /// </summary>
        /// <param name="value">-1/1,1表示前进，－1表示后退</param>
        public static void GoHistory(int value)
        {
            string js = @"<Script language='JavaScript'>
                    history.go({0});  
                  </Script>";
            HttpContext.Current.Response.Write(string.Format(js, value));
            HttpContext.Current.Response.End();
        }

        #endregion

        #region 关闭当前窗口

        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        public static void CloseWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
            HttpContext.Current.Response.End();
        }

        #endregion

        #region 刷新父窗口

        /// <summary>
        /// 刷新父窗口
        /// </summary>
        public static void RefreshParent()
        {
            string js = @"<Script language='JavaScript'>
                    parent.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 格式化为JS可解释的字符串

        /// <summary>
        /// 格式化为JS可解释的字符串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string JSStringFormat(string s)
        {
            return s.Replace("\r", "\\r").Replace("\n", "\\n").Replace("'", "\\'").Replace("\"", "\\\"");
        }

        #endregion

        #region 刷新打开窗口

        /// <summary>
        /// 刷新打开窗口
        /// </summary>
        public static void RefreshOpener()
        {
            string js = @"<Script language='JavaScript'>
                    opener.location.reload();
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 新开页面去掉ie的菜单

        /// <summary>
        /// 新开页面去掉ie的菜单
        /// </summary>
        /// <param name="url"></param>
        public static void OpenWebForm(string url)
        {
            /*…………………………………………………………………………………………*/
            /*修改人员:		yxd						*/
            /*修改时间:	2005-12-1	*/
            /*修改目的:	新开页面去掉ie的菜单。。。						*/
            /*注释内容:								*/
            /*开始*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"');
			window.open('" + url +
                        @"','','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no');
			</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 打开窗口

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="name"></param>
        /// <param name="future"></param>
        public static void OpenWebForm(string url, string name, string future)
        {
            string js = @"<Script language='JavaScript'>
                     window.open('" + url + @"','" + name + @"','" + future + @"')
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 打开窗口

        public static void OpenOneWebForm(string url)
        {
            /*…………………………………………………………………………………………*/
            /*修改人员:		yxd						*/
            /*修改时间:	2003-4-9	*/
            /*修改目的:	新开页面去掉ie的菜单。。。						*/
            /*注释内容:								*/
            /*开始*/
            string js = @"<Script language='JavaScript'>
			window.open('" + url + @"','main');
			</Script>";
            /*结束*/
            /*…………………………………………………………………………………………*/

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 打开窗口

        /// <summary>
        /// 打开窗口
        /// </summary>
        /// <param name="url">地址</param>
        /// <param name="formName">窗口名称</param>
        public static void OpenWebForm(string url, string formName)
        {
            /*…………………………………………………………………………………………*/
            /*修改人员:		yxd						*/
            /*修改时间:	2003-4-9	*/
            /*修改目的:	新开页面去掉ie的菜单。。。						*/
            /*注释内容:								*/
            /*开始*/
            string js = @"<Script language='JavaScript'>
			//window.open('" + url + @"','" + formName + @"');
			window.open('" + url + @"','" + formName +
                        @"','height=0,width=0,top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no','main');
			</Script>";
            /*结束*/
            /*…………………………………………………………………………………………*/

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 打开WEB窗口

        /// <summary>		
        /// 函数名:OpenWebForm	
        /// 功能描述:打开WEB窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 杨秀东
        /// 日 期: 2005-12-2 17:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="url">WEB窗口</param>
        /// <param name="isFullScreen">是否全屏幕</param>
        public static void OpenWebForm(string url, bool isFullScreen, int intHeight, int intWidth, int intTop,
                                       int intLeft)
        {
            string js = @"<Script language='JavaScript'>";
            if (isFullScreen)
            {
                js += "var iWidth = 0;";
                js += "var iHeight = 0;";
                js += "iWidth=window.screen.availWidth-10;";
                js += "iHeight=window.screen.availHeight-50;";
                js +=
                    "var szFeatures ='width=' + iWidth + ',height=' + iHeight + ',top=0,left=0,location=no,menubar=no,resizable=yes,scrollbars=yes,status=yes,titlebar=no,toolbar=no,directories=no';";
                js += "window.open('" + url + @"','',szFeatures);";
            }
            else
            {
                js += "window.open('" + url + @"','','height=" + intHeight + ",width=" + intWidth + ",top=" + intTop +
                      ",left=" + intLeft +
                      ",location=no,menubar=no,resizable=no,scrollbars=no,status=no,titlebar=no,toolbar=no,directories=no');";
            }
            js += "</Script>";
            HttpContext.Current.Response.Write(js);
        }

        public static void OpenWebForm(Page page, string url, string win, string width, string height)
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
            sb.Append(
                "window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+',toolbar=no,menubar=no,scrollbars=auto,resizeable=no,location=no,status=no');");
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm_9u4984", GetScriptBlock(sb.ToString()));
        }

        public static void OpenWebForm(Page page, string url, string winName, string width, string height,
                                       string szFeatures)
        {
            var sb = new StringBuilder();
            sb.Append("var name='");
            sb.Append(winName);
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
            sb.Append(
                "window.open(url,name,'height='+iHeight+',innerHeight='+iHeight+',width='+iWidth+',innerWidth='+iWidth+',top='+iTop+',left='+iLeft+'," +
                szFeatures + "');");
            page.ClientScript.RegisterStartupScript(page.GetType(), "OpenWebForm_9u4984", GetScriptBlock(sb.ToString()));
        }

        #endregion

        #region 转向Url制定的页面

        /// <summary>
        /// 转向Url制定的页面
        /// </summary>
        /// <param name="url">Url</param>
        public static void JavaScriptLocationHref(string url)
        {
            string js = @"<Script language='JavaScript'>
                    window.location.replace('{0}');
                  </Script>";
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 指定的框架页面转换

        /// <summary>
        /// 指定的框架页面转换
        /// </summary>
        /// <param name="FrameName">框架名称</param>
        /// <param name="url">URL</param>
        public static void JavaScriptFrameHref(string FrameName, string url)
        {
            string js = @"<Script language='JavaScript'>
					
                    @obj.location.replace(""{0}"");
                  </Script>";
            js = js.Replace("@obj", FrameName);
            js = string.Format(js, url);
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 重置框架页面

        /// <summary>
        /// 重置页面
        /// </summary>
        /// <param name="strRows"></param>
        public static void JavaScriptResetPage(string strRows)
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.CenterFrame.rows='" + strRows + "';</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 客户端方法设置Cookie

        /// <summary>
        /// 函数名:JavaScriptSetCookie
        /// 功能描述:客户端方法设置Cookie
        /// 作者:yxd
        /// 日期：2003-4-9
        /// 版本：1.0
        /// </summary>
        /// <param name="strName">Cookie名</param>
        /// <param name="strValue">Cookie值</param>
        public static void JavaScriptSetCookie(string strName, string strValue)
        {
            string js = @"<script language=Javascript>
			var the_cookie = '" + strName + "=" + strValue + @"'
			var dateexpire = 'Tuesday, 01-Dec-2020 12:00:00 GMT';
			//document.cookie = the_cookie;//写入Cookie<BR>} <BR>
			document.cookie = the_cookie + '; expires='+dateexpire;			
			</script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 返回父窗口

        /// <summary>		
        /// 函数名:GotoParentWindow	
        /// 功能描述:返回父窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 杨秀东
        /// 日 期: 2003-04-30 10:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="parentWindowUrl">父窗口</param>		
        public static void GotoParentWindow(string parentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    this.parent.location.replace('" + parentWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 替换父窗口

        /// <summary>		
        /// 函数名:ReplaceParentWindow	
        /// 功能描述:替换父窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 杨秀东
        /// 日 期: 2003-04-30 10:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="parentWindowUrl">父窗口</param>
        /// <param name="caption">窗口提示</param>
        /// <param name="future">窗口特征参数</param>
        public static void ReplaceParentWindow(string parentWindowUrl, string caption, string future)
        {
            string js = "";
            if (future != null && future.Trim() != "")
            {
                js = @"<script language=javascript>this.parent.location.replace('" + parentWindowUrl + "','" + caption +
                     "','" + future + "');</script>";
            }
            else
            {
                js =
                    @"<script language=javascript>var iWidth = 0 ;var iHeight = 0 ;iWidth=window.screen.availWidth-10;iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';this.parent.location.replace('" +
                    parentWindowUrl + "','" + caption + "',szFeatures);</script>";
            }

            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 替换当前窗体的打开窗口

        /// <summary>		
        /// 函数名:ReplaceOpenerWindow	
        /// 功能描述:替换当前窗体的打开窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 孙洪彪
        /// 日 期: 2003-04-30 16:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口</param>		
        public static void ReplaceOpenerWindow(string openerWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.location.replace('" + openerWindowUrl + "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 替换当前窗体的打开窗口的父窗口

        /// <summary>		
        /// 函数名:ReplaceOpenerParentWindow	
        /// 功能描述:替换当前窗体的打开窗口的父窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 孙洪彪
        /// 日 期: 2003-07-03 19:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口的父窗口</param>		
        public static void ReplaceOpenerParentFrame(string frameName, string frameWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent." + frameName + ".location.replace('" + frameWindowUrl +
                        "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 替换当前窗体的打开窗口的父窗口

        /// <summary>		
        /// 函数名:ReplaceOpenerParentWindow	
        /// 功能描述:替换当前窗体的打开窗口的父窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 孙洪彪
        /// 日 期: 2003-07-03 19:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="openerWindowUrl">当前窗体的打开窗口的父窗口</param>		
        public static void ReplaceOpenerParentWindow(string openerParentWindowUrl)
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.parent.location.replace('" + openerParentWindowUrl +
                        "');</Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 关闭窗口

        /// <summary>		
        /// 函数名:CloseParentWindow	
        /// 功能描述:关闭窗口	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 杨秀东
        /// 日 期: 2003-04-30 16:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        public static void CloseParentWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.parent.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 关闭窗口

        /// <summary>
        /// 关闭窗口
        /// </summary>
        public static void CloseOpenerWindow()
        {
            string js = @"<Script language='JavaScript'>
                    window.opener.close();  
                  </Script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 返回打开模式窗口的脚本

        /// <summary>
        /// 函数名:ShowModalDialogJavascript	
        /// 功能描述:返回打开模式窗口的脚本	
        /// 处理流程:
        /// 算法描述:
        /// 作 者: 孙洪彪
        /// 日 期: 2003-04-30 15:00
        /// 修 改:
        /// 日 期:
        /// 版 本:
        /// </summary>
        /// <param name="webFormUrl"></param>
        /// <returns></returns>
        public static void ShowModalDialogJavascript(string webFormUrl)
        {
            string js = @"<script language=javascript>
							var iWidth = 0 ;
							var iHeight = 0 ;
							iWidth=window.screen.availWidth-10;
							iHeight=window.screen.availHeight-50;
							var szFeatures = 'dialogWidth:'+iWidth+';dialogHeight:'+iHeight+';dialogLeft:0px;dialogTop:0px;center:yes;help=no;resizable:on;status:on;scroll=yes';
							showModalDialog('" + webFormUrl + "','',szFeatures);</script>";
            HttpContext.Current.Response.Write(js);
        }

        #endregion

        #region 充值结果输出函数

        /// <summary>
        /// 充值结果输出函数
        /// </summary>
        /// <param name="RedirectUrl"></param>
        /// <param name="ProductName"></param>
        /// <param name="PlayerName"></param>
        /// <param name="Num"></param>
        /// <param name="SaveMsg"></param>
        /// <param name="SaveResult"></param>
        /// <param name="OtherMsg"></param>
        /// <param name="FromUrl"></param>
        /// <param name="ProductId"></param>
        /// <param name="ProductType"></param>
        /// <param name="SpUserId"></param>
        public static void ShowSaveResult(string RedirectUrl, string ProductName, string PlayerName, string Num,
                                          string SaveMsg, string SaveResult, string OtherMsg, string FromUrl,
                                          string ProductId, string ProductType, string SpUserId)
        {
            try
            {
                HttpContext.Current.Response.Redirect(RedirectUrl + "?ProductName=" + ProductName.Replace("\r\n", "") +
                                                      "&PlayerName=" + PlayerName + "&Num=" + Num + "&SaveMsg=" +
                                                      SaveMsg + "&SaveResult=" +
                                                      SaveResult.Replace("充值失败",
                                                                         "<font color=\"red\"><b>充值失败</b></font>") +
                                                      "&OtherMsg=" + OtherMsg + "&FromUrl=" + FromUrl + "&ProductId=" +
                                                      ProductId + "&ProductType=" + ProductType + "&SpUserId=" +
                                                      SpUserId);
            }
            catch
            {
                HttpContext.Current.Response.Write("出错啦！<BR>处理过程中出现错误，请及时查看您的销售记录。");
                HttpContext.Current.Response.End();
                return;
            }
        }

        #endregion
    }
}