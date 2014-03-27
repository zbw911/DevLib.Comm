// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：Mail.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Dev.Comm.Net
{
    /// <summary>
    ///模块编号：
    ///功能描述：通用模块
    ///创 建 人：李旭珍
    ///创建日期：2009-04-18
    /// <summary>
    /// 发送邮件类
    /// </summary>
    public class Mail
    {
        #region 属性 变量 构造等

        /// <summary>
        /// 邮件发送优先级
        /// </summary>
        public enum Priority
        {
            /// <summary>最高级别</summary>
            HIGH = 1,

            /// <summary>默认级别</summary>
            NORMAL = 3,

            /// <summary>最低级别</summary>
            LOW = 5
        }

        private readonly string _Server = "";

        /// <summary>
        /// SMTP服务器反馈的信息
        /// </summary>
        private readonly string strResponse;

        /// <summary>
        /// 错误反馈信息
        /// </summary>
        private string strErrMessage;

        /// <summary>
        /// 构造函数
        /// </summary>
        public Mail(string Server)
        {
            strErrMessage = "";

            strResponse = "";

            _Server = Server;
        }

        /// <summary>
        /// 取得错误反馈信息
        /// </summary>
        public string ErrorMessage
        {
            get { return strErrMessage; }
        }

        /// <summary>
        /// 取得SMTP服务器反馈的信息
        /// </summary>
        public string ServerResponse
        {
            get { return strResponse; }
        }

        #endregion

        #region base64编码

        /// <summary>
        /// 进行BASE64编码
        /// </summary>
        /// <param name="Data">数据</param>
        /// <returns>字符串</returns>
        private string Encode(string Data)
        {
            byte[] bteData;

            bteData = Encoding.GetEncoding("gb2312").GetBytes(Data);

            //return Convert.ToBase64String(bteData);

            string w = Convert.ToBase64String(bteData);

            return w;
        }

        /// <summary>
        /// 进行BASE64解码
        /// </summary>
        /// <param name="Data">数据</param>
        /// <returns>字符串</returns>
        private string Decode(string Data)
        {
            byte[] bteData;

            bteData = Convert.FromBase64String(Data);

            return Encoding.GetEncoding("gb2312").GetString(bteData);
        }

        #endregion

        #region 返回错误描述

        /// <summary>
        /// 返回错误描述
        /// </summary>
        /// <param name="str">服务器返回的信息</param>
        /// <returns>错误描述</returns>
        private string FormatString(string str)
        {
            var smtpcMail = new SMTPClient();

            string s = str.Substring(0, 3);

            str = str.Substring(0, str.IndexOf("\0"));

            switch (s)
            {
                case "500":

                    return "邮箱地址错误" + "，服务器原始反馈信息：" + str;

                case "501":

                    return "参数格式错误" + "，服务器原始反馈信息：" + str;

                case "502":

                    return "命令不可实现" + "，服务器原始反馈信息：" + str;

                case "503":

                    return "服务器需要SMTP验证" + "，服务器原始反馈信息：" + str;

                case "504":

                    return "命令参数不可实现" + "，服务器原始反馈信息：" + str;

                case "421":

                    return "服务未就绪，关闭传输信道" + "，服务器原始反馈信息：" + str;

                case "450":

                    return "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）" + "，服务器原始反馈信息：" + str;

                case "550":

                    return "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）" + "，服务器原始反馈信息：" + str;

                case "451":

                    return "放弃要求的操作；处理过程中出错" + "，服务器原始反馈信息：" + str;

                case "551":

                    return "用户非本地，请尝试<forward-path>" + "，服务器原始反馈信息：" + str;

                case "452":

                    return "系统存储不足，要求的操作未执行" + "，服务器原始反馈信息：" + str;

                case "552":

                    return "过量的存储分配，要求的操作未执行" + "，服务器原始反馈信息：" + str;

                case "553":

                    return "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）" + "，服务器原始反馈信息：" + str;

                case "432":

                    return "需要一个密码转换" + "，服务器原始反馈信息：" + str;

                case "534":

                    return "认证机制过于简单" + "，服务器原始反馈信息：" + str;

                case "538":

                    return "当前请求的认证机制需要加密" + "，服务器原始反馈信息：" + str;

                case "454":

                    return "临时认证失败" + "，服务器原始反馈信息：" + str;

                case "530":

                    return "需要认证" + "，服务器原始反馈信息：" + str;

                default:

                    return "没有匹配的错误处理，服务器返回参数：" + str;
            }
        }

        #endregion

        #region 发送邮件 主要

        /// <summary>
        /// 发邮件，web.config中读取配置节点，拆分以后调用发邮件方法
        /// </summary>
        /// <param name="configMessage"></param>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="emailMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int ToSendMail(string configMessage, string mail, string name, string subject, string emailMessage,
                              out string errorMessage)
        {
            return ToSendMail("", configMessage, mail, name, subject, emailMessage, out errorMessage);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendUsername"></param>
        /// <param name="configMessage"></param>
        /// <param name="mail"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="emailMessage"></param>
        /// <param name="errorMessage"></param>
        /// <returns></returns>
        public int ToSendMail(string sendUsername, string configMessage, string mail, string name, string subject, string emailMessage,
                                     out string errorMessage)
        {

            string cMessage = "";
            //截取配置信息
            string[] strMail = configMessage.Split(";".ToCharArray());

            int result_mail = SendMail(strMail[0], Convert.ToInt32(strMail[1]), strMail[2], sendUsername, true,
                                       strMail[2], strMail[3], mail, name, Priority.HIGH, true, "", subject,
                                       emailMessage, out cMessage);

            //发送邮件返回结果不是0的都是错误
            errorMessage = cMessage;
            return result_mail;
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="smtpHost">SMTP服务器</param>
        /// <param name="port">SMTP服务器端口</param>
        /// <param name="from">邮件发送者</param>
        /// <param name="displayFromName">显示的发送者名称</param>
        /// <param name="authentication">是否进行认证</param>
        /// <param name="userName">认证用户名</param>
        /// <param name="password">认证密码</param>
        /// <param name="To">邮件接收者</param>
        /// <param name="displayToName">显示的接收者名称</param>
        /// <param name="priority">优先级</param>
        /// <param name="html">是否为HTML</param>
        /// <param name="Base">URL</param>
        /// <param name="subject">邮件主题</param>
        /// <param name="message">邮件内容</param>
        /// <param name="errmsg">错误信息 </param>
        public int SendMail(string smtpHost, int port, string @from, string displayFromName, bool authentication,
                            string userName, string password, string To, string displayToName, Priority priority,
                            bool html, string Base, string subject, string message, out string errmsg)
        {
            try
            {
                @from = "<" + @from + ">";
                To = "<" + To + ">";
                ////if (To.IndexOf(";") > -1)
                ////    To = "<" + To.Replace(";", ">,<") + ">";
                ////else
                ////    To = "<" + To + ">";


                var Name = new[]
                               {
                                   "服务器地址", "端口", "发信邮箱", "发送者名称", "是否认证(Smtp登录)", "认证用户名", "认证密码", "邮件接受者", "接受者姓名",
                                   "优先级"
                                   , "是否HTML", "相对连接地址(在有图片或连接时)", "主题", "正文"
                               };

                var NameParameter = new[]
                                        {
                                            smtpHost, port.ToString(), @from, displayFromName, authentication.ToString(),
                                            userName, password, To, displayToName, priority.ToString(), html.ToString(),
                                            Base, subject, message
                                        };

                //以上为发送邮件失败时记录到网站根目录的文本文件中

                string strResponseNumber; //smtp服务器返回的信息

                var smtpcMail = new SMTPClient(); //实例化连接类

                smtpcMail.Connect(smtpHost, port); //连接SmtpHost服务器,端口Port

                bool boolConnect = smtpcMail.isConnected(); //返回连接是否成功

                //判断是否进行了连接

                if (!boolConnect)
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name,  string.Join(",", NameParameter), "未能取得连接", "");


                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>未能取得连接");

                    errmsg = "smtpcMail.Connect error:未能取得连接";
                    return -1;
                }

                strResponseNumber = smtpcMail.GetServerResponse(); //得到服务器返回的信息

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "220"))) //包含220表示成功连接
                {
                    smtpcMail.Close();

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>请求连接失败");
                   
                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "请求连接失败",
                    //                 FormatString(strResponseNumber).Trim());
                    errmsg = "smtpcMail.GetServerResponse error:" + FormatString(strResponseNumber).Trim();
                    return -1;
                }

                smtpcMail.SendCommandToServer("HELO kaidongmei\r\n");
                Thread.Sleep(50);

                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "与服务器连接初期（打招呼出错）",
                    //                 FormatString(strResponseNumber).Trim());

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>与服务器连接初期（打招呼出错）");
                  
                    errmsg = FormatString(strResponseNumber).Trim();
                    return 1;
                }

                //第一步结束

                if (authentication) //是否认证
                {
                    //与服务器通讯第二步：请求登录

                    smtpcMail.SendCommandToServer("AUTH LOGIN\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "334")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "请求登录出现错误",
                        //                 FormatString(strResponseNumber).Trim());

                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>请求登录出现错误");
                  
                        errmsg = FormatString(strResponseNumber).Trim();
                        return 2;
                    }

                    //与服务器通讯第三步：输入用户帐号

                    smtpcMail.SendCommandToServer(Encode(userName) + "\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "334")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "输入用户名称出错",
                        //                 FormatString(strResponseNumber).Trim());


                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>输入用户名称出错");

                        errmsg = FormatString(strResponseNumber).Trim();
                        return 3;
                    }

                    //第三步结束

                    //与服务器通讯第四步：输入用户密码

                    smtpcMail.SendCommandToServer(Encode(password) + "\r\n");
                    Thread.Sleep(50);
                    strResponseNumber = smtpcMail.GetServerResponse();

                    if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "235")))
                    {
                        smtpcMail.Close();

                        //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "输入用户密码出错",
                        //                 FormatString(strResponseNumber).Trim());

                        Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>输入用户密码出错");

                        errmsg = FormatString(strResponseNumber).Trim();
                        return 4;
                    }

                    //第四步结束
                }

                //与服务器通讯第五步：输入发送邮件的信箱地址

                smtpcMail.SendCommandToServer("MAIL FROM: " + @from + /* +  " AUTH = " + From*/ "\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "输入发信邮箱出错",
                    //                 FormatString(strResponseNumber).Trim());
                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>输入发信邮箱出错");


                    errmsg = FormatString(strResponseNumber).Trim();
                    return 5;
                }

                //第五步结束

                //与服务器通讯第六步：输入接收邮件的信箱地址

                smtpcMail.SendCommandToServer("RCPT TO: " + To + "\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "输入接收邮箱出错",
                    //                 FormatString(strResponseNumber).Trim());
                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>输入接收邮箱出错");

                    errmsg = FormatString(strResponseNumber).Trim();
                    return 6;
                }

                //第六步结束

                //与服务器通讯第七步：输入邮箱主题内容				

                smtpcMail.SendCommandToServer("DATA\r\n");
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "354")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "打开输入邮件主内容出错",
                    //                 FormatString(strResponseNumber).Trim());


                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>打开输入邮件主内容出错");
                    errmsg = "smtpcMail.SendCommandToServer error:" + FormatString(strResponseNumber).Trim();
                    return 7;
                }

                /*邮件主要内容开始			               相关参考	 
				
                FROM:<姓名><邮件地址>                      格式：FROM:管理员 
				  
                TO:  <姓名><邮件地址>                      格式：TO:Name<1234@sina.com>  
				
                SUBJECT:<标题>                             格式：SUBJECT:今天的天气很不错！ 
				
                DATE:<时间>                                格式：DATE: Thu, 29 Aug 2002 09:52:47 +0800 (CST)
				
                REPLY-TO:<收邮件地址>                      格式：REPLY-TO:webmaster@sina.com 
				
                Content-Type:<邮件类型>                    格式：Content-Type: multipart/mixed; boundary=unique-boundary-1 
				
                X-Priority:<邮件优先级>                    格式：X-Priority:3 
				
                MIME-Version：<版本>                       格式：MIME-Version:1.0 
				
                Content-Transfer-Encoding:<内容传输编码>   格式：Content-Transfer-Encoding:Base64 
				
                X-Mailer:<邮件发送者>                      格式：X-Mailer:FoxMail 4.0 beta 1 [cn]  */

                string strData = "";

                strData = string.Concat("From: ", displayFromName + @from);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "To: ");

                strData = string.Concat(strData, displayToName + To);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "Subject: ");

                strData = string.Concat(strData, subject);

                strData = string.Concat(strData, "\r\n");

                if (html)
                {
                    strData = string.Concat(strData, "Content-Type: text/html;charset=\"gb2312\"");
                }
                else
                {
                    strData = string.Concat(strData, "Content-Type: text/plain;charset=\"gb2312\"");
                }

                strData = string.Concat(strData, "\r\n");

                //strData = string.Concat(strData,"Content-Transfer-Encoding: base64;");

                //strData = string.Concat(strData,"\r\n");

                strData = string.Concat(strData, "X-Priority: " + priority);

                strData = string.Concat(strData, "\r\n");

                strData = string.Concat(strData, "MIME-Version: 1.0");

                strData = string.Concat(strData, "\r\n");

                //strData = string.Concat(strData,"Content-Type: text/html;" );			

                //strData = string.Concat(strData,"\r\n");							

                strData = string.Concat(strData, "X-Mailer: Email自动发送程序 1.0 ");

                strData = string.Concat(strData, "\r\n" + "\r\n");

                strData = string.Concat(strData, message);

                //执行.命令结束传输

                strData = string.Concat(strData, "\r\n.\r\n");

                smtpcMail.SendCommandToServer(strData);
                Thread.Sleep(50);
                strResponseNumber = smtpcMail.GetServerResponse();

                if (!(smtpcMail.DoesStringContainSMTPCode(strResponseNumber, "250")))
                {
                    smtpcMail.Close();

                    //TextLog.ErrorLog(_Server, "ErrorText.txt", Name, NameParameter, "添加-结束添加邮件内容出错",
                    //                 FormatString(strResponseNumber).Trim());

                    Dev.Log.Loger.Error(string.Join(",", NameParameter) + "=>添加-结束添加邮件内容出错");
                    errmsg = "smtpcMail.SendCommandToServer(strData) error:" + FormatString(strResponseNumber).Trim();
                    return 7;
                }

                //第七步完成

                //最后执行.QUIT命令断开连接

                smtpcMail.SendCommandToServer("QUIT\r\n");
                Thread.Sleep(50);
                smtpcMail.Close();
            }
            catch (SocketException err)
            {
                strErrMessage += err.Message + " " + err.StackTrace;
                errmsg = "SocketException err:" + strErrMessage;
                return -1;
            }
            catch (Exception e)
            {
                strErrMessage += e.Message + " " + e.StackTrace;
                errmsg = "Exception e:" + strErrMessage;
                return -1;
            }
            errmsg = "邮件发送成功";
            return 0;
        }

        /// <summary>
        /// 群发邮件
        /// </summary>
        /// <param name="SmtpHost"></param>
        /// <param name="Port"></param>
        /// <param name="From"></param>
        /// <param name="DisplayFromName"></param>
        /// <param name="Authentication"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="To">多个时以;分隔</param>
        /// <param name="DisplayToName"></param>
        /// <param name="Priority"></param>
        /// <param name="Html"></param>
        /// <param name="Base"></param>
        /// <param name="Subject"></param>
        /// <param name="Message"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public int SendMoreMail(string SmtpHost, int Port, string From, string DisplayFromName, bool Authentication,
                                string UserName, string Password, string To, string DisplayToName, Priority Priority,
                                bool Html, string Base, string Subject, string Message)
        {
            var from = new MailAddress(From, DisplayToName); //邮件的发件人,后面为显示的名字
            var mail = new MailMessage();
            mail.Subject = Subject; //设置邮件的标题
            mail.From = from; //设置邮件的发件人

            //多人发送
            string address = "";
            string displayName = "";
            string[] mailNames = (To + ";").Split(';');
            foreach (var name in mailNames)
            {
                if (name != string.Empty)
                {
                    if (name.IndexOf('<') > 0)
                    {
                        displayName = name.Substring(0, name.IndexOf('<'));
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    else
                    {
                        displayName = string.Empty;
                        address = name.Substring(name.IndexOf('<') + 1).Replace('>', ' ');
                    }
                    mail.CC.Add(new MailAddress(address, displayName)); //收件人地址的集合
                }
            }

            mail.Body = Message; //设置邮件的内容            
            mail.BodyEncoding = Encoding.UTF8; //设置邮件的格式
            mail.IsBodyHtml = true; //设置正文是否为HTML格式
            mail.Priority = MailPriority.Normal; //设置邮件的发送级别
            ////if (txtMailTo.Text != "")
            ////{
            ////    string fileName = txtUpFile.Text.Trim();                                   //设置邮件的附件
            ////    fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);             //取文件名
            ////    mail.Attachments.Add(new Attachment(fileName));                            //添加附件到邮件当中
            ////}
            mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
            var client = new SmtpClient();
            client.Host = SmtpHost; //设置SMTP的地址，注意：是什么邮箱就应该用相对应的地址          
            client.Port = Port; //设置用于 SMTP 事务的端口，默认的是 25
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(UserName, Password); //我的邮箱的登录名和密码。就是发送方的用户名和密码，要对应上面的Host地址
            client.DeliveryMethod = SmtpDeliveryMethod.Network;

            client.Send(mail);
            return 1;
        }

        #endregion

        #region 根据本地文件路径获得内容

        /// <summary>
        /// 根据 本地文件 获得内容模板
        /// </summary>
        /// <param name="path">本地文件模板路径</param>
        /// <param name="replaceContent">文件中的参数名和参数值以键值对的形式对应</param>
        /// <returns></returns>
        public string GetHtmlBody(string path, Dictionary<string, string> replaceContent)
        {
            FileStream fs = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            var f = new byte[fs.Length];
            fs.Read(f, 0, (int)fs.Length);
            fs.Close();
            string result = Encoding.GetEncoding("GB2312").GetString(f);

            if (replaceContent != null)
            {
                foreach (var str in replaceContent.Keys)
                {
                    result = result.Replace(str, replaceContent[str]);
                }
            }

            return result;
        }

        #endregion

        public static bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format. 
            return Regex.IsMatch(strIn,
                                 @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
    }

    /// <summary>
    /// TcpClient派生类，用来进行SMTP服务器的连接工作
    /// </summary>
    public class SMTPClient : TcpClient
    {
        #region 与服务器连接 构造函数 当前状态 发送命令的方法 获取服务器反馈 检索字符串

        /// <summary>
        /// 是否以连接
        /// </summary>
        /// <returns>连接为True，不连接为False</returns>
        public bool isConnected()
        {
            return Active;
        }

        /// <summary>
        /// 向服务器发送命令
        /// </summary>
        /// <param name="Command">命令</param>
        public void SendCommandToServer(string Command)
        {
            NetworkStream ns = GetStream();

            byte[] WriteBuffer;

            WriteBuffer = new byte[1024];

            WriteBuffer = Encoding.Default.GetBytes(Command);

            ns.Write(WriteBuffer, 0, WriteBuffer.Length);

            return;
        }

        /// <summary>
        /// 取得服务器反馈信息
        /// </summary>
        /// <returns>字符串</returns>
        public string GetServerResponse()
        {
            int StreamSize;

            string ReturnValue = "";

            byte[] ReadBuffer;

            NetworkStream ns = GetStream();

            ReadBuffer = new byte[1024];

            StreamSize = ns.Read(ReadBuffer, 0, ReadBuffer.Length);

            if (StreamSize == 0)
            {
                return ReturnValue;
            }
            else
            {
                ReturnValue = Encoding.Default.GetString(ReadBuffer);

                return ReturnValue;
            }
        }

        /// <summary>
        /// 判断返回的信息中是否有指定的SMTP代码出现
        /// </summary>
        /// <param name="Message">信息</param>
        /// <param name="SMTPCode">SMTP代码</param>
        /// <returns>存在返回False，不存在返回True</returns>
        public bool DoesStringContainSMTPCode(string Message, string SMTPCode)
        {
            return (Message.IndexOf(SMTPCode, 0, 10) == -1) ? false : true;
        }

        #endregion
    }

    ///// <summary>
    ///// 记录日志到文本
    ///// </summary>
    //public class TextLog
    //{
    //    #region 邮件发送失败的时候 记录日志到文本

    //    /// <summary>
    //    ///  记录日志到Text文件
    //    /// </summary>
    //    /// <param name="Server">  服务器路径</param>
    //    /// <param name="TextName">Text名称_虚拟路径下</param>
    //    /// <param name="Name">    参数名称</param>
    //    /// <param name="NameParameter">参数值</param>
    //    public static void ErrorLog(string Server, string TextName, string[] Name, string[] NameParameter, string Error,
    //                                string ErrorBody)
    //    {
    //        try
    //        {
    //            StreamWriter sw = null;

    //            if (!(File.Exists(Server + @"\" + TextName)))
    //            {
    //                sw = File.CreateText(Server + @"\" + TextName);
    //            }
    //            else
    //            {
    //                sw = File.AppendText(Server + @"\" + TextName);
    //            }

    //            sw.WriteLine("\n");

    //            sw.WriteLine("错误发生时间：" + DateTime.Now.ToString() + "\n");

    //            sw.WriteLine("错误本地描述：" + Error + "   错误描述和服务器反馈信息：" + ErrorBody + "\n");

    //            for (int i = 0; i < Name.Length; i++)
    //            {
    //                sw.WriteLine("参数名称：" + Name[i] + "  值：" + NameParameter[i]);

    //                sw.WriteLine("\n");
    //            }

    //            sw.Flush();

    //            sw.Close();
    //        }
    //        catch
    //        {
    //            //忽略错误
    //        }
    //    }

    //    /// <summary>
    //    ///  记录日志到Text文件
    //    /// </summary>
    //    /// <param name="Server">  服务器路径</param>
    //    /// <param name="TextName">Text名称_虚拟路径下</param>
    //    /// <param name="Name">    参数名称</param>
    //    /// <param name="NameParameter">参数值</param>
    //    public static void ErrorLog(string Server, string TextName, string Name, string NameParameter, string Error,
    //                                string ErrorBody)
    //    {
    //        string[] s, s1;

    //        s = new[] { Name };

    //        s1 = new[] { NameParameter };

    //        ErrorLog(Server, TextName, s, s1, Error, ErrorBody);
    //    }

    //    #endregion
    //}
}