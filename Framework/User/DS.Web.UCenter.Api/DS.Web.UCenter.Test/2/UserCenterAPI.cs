using System;
using System.Text;
using Commons;
using System.Web;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.IO;
//using UserCenter.DBUtility;
using System.Configuration;


namespace UserCenter.UserCenter
{
    public class UserCenterAPI
    {
        private static string UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
        #region 标准接口函数

        /// <summary>
        /// 头像修改接口
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public string uc_avatar(int UID)
        {
            string uc_input = uc_api_input("uid=" + UID.ToString());
            string uc_avatarflash = SqlHelper.UC_API + "/images/camera.swf?inajax=1&appid=" + SqlHelper.UC_APPID + "&input=" + uc_input + "&agent=" + uc_Authcode.MD5(UserAgent) + "&ucapi=" + urlencode(SqlHelper.UC_API);
            return "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=9\" width=447 height=477 id=\"mycamera\"><param name=\"movie\" value=\"" + uc_avatarflash + "\"><param name=\"quality\" value=\"high\"><param name=\"menu\" value=\"false\"><embed src=\"" + uc_avatarflash + "\" quality=\"high\" menu=\"false\"  width=\"447\" height=\"477\" type=\"application/x-shockwave-flash\" pluginspage=\"http://www.macromedia.com/shockwave/download/index.cgi?P1_Prod_Version=ShockwaveFlash\" name=\"mycamera\" swLiveConnect=\"true\"></embed></object>";
        }
        /// <summary>
        /// 用户注册接口
        /// </summary>
        public int uc_user_register(string username, string password, string email)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", username);
            ht.Add("password", password);
            ht.Add("email", email);
            //Hashtable hb = XmlCompent.GetTable(call_user_func("user", "register", ht));
            return int.Parse(call_user_func("user", "register", ht));
        }
        public int uc_user_register(string username, string password, string email, string questionid, string answer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", username);
            ht.Add("password", password);
            ht.Add("email", email);
            ht.Add("questionid", questionid);
            ht.Add("answer", answer);
            return int.Parse(call_user_func("user", "register", ht));
        }
        /// <summary>
        /// 用户登陆接口
        /// </summary>
        public string[] uc_user_login(string uname, string pword)
        {
            return uc_user_login(uname, pword, 0, 0, 0, "");
        }
        public string[] uc_user_login(string uname, string pword, int id)
        {
            return uc_user_login(uname, pword, id, 0, 0, "");
        }
        public string[] uc_user_login(string uname, string pword, int id, int checkques, int questionid, string answer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", uname);
            ht.Add("password", pword);
            ht.Add("isuid", id);
            ht.Add("checkques", checkques);
            ht.Add("questionid", questionid);
            ht.Add("answer", answer);
            Hashtable hb = XmlCompent.GetTable(call_user_func("user", "login", ht));
            string[] s = new string[5];
            s[0] = hb["item_0"].ToString();//返回用户 ID，表示用户登录成功
            s[1] = hb["item_1"].ToString();//用户名
            s[2] = hb["item_2"].ToString();//密码
            s[3] = hb["item_3"].ToString();//Email
            s[4] = hb["item_4"].ToString();//用户名是否重名,如果应用程序是升级过来的，并且当前登录用户和已有用户重名，那么返回的数组中 [4] 的值将返回 1
            return s;
        }
        /// <summary>
        /// 获取用户数据接口
        /// </summary>
        public string[] uc_get_user(string username)
        {
            return uc_get_user(username, 0);
        }
        public string[] uc_get_user(string username, int isuid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", username);
            ht.Add("isuid", isuid);
            string ss = call_user_func("user", "get_user", ht);
            string[] s = new string[3];
            if (ss.Length < 20)
            {
                s[0] = ss;
                s[1] = "";
                s[2] = "";
            }
            else
            {
                Hashtable hb = XmlCompent.GetTable(ss);
                s[0] = hb["item_0"].ToString();//用户 ID
                s[1] = hb["item_1"].ToString();//用户名
                s[2] = hb["item_2"].ToString();//Email
            }
            return s;
        }
        /// <summary>
        /// 更新用户资料
        /// </summary>
        public decimal uc_user_edit(string username, string oldpw, string newpw, string email)
        {
            return uc_user_edit(username, oldpw, newpw, email, 0, 0, "");//不忽略，更改资料需要验证密码
        }
        public decimal uc_user_edit(string username, string oldpw, string newpw, string email, int ignoreoldpw)
        {
            return uc_user_edit(username, oldpw, newpw, email, ignoreoldpw, 0, "");
        }
        public decimal uc_user_edit(string username, string oldpw, string newpw, string email, int ignoreoldpw, int questionid, string answer)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", username);
            ht.Add("oldpw", oldpw);
            ht.Add("newpw", newpw);
            ht.Add("email", email);
            ht.Add("ignoreoldpw", ignoreoldpw);
            ht.Add("questionid", questionid);
            ht.Add("answer", answer);

            string temp = call_user_func("user", "edit", ht);
            //ygj 2011-6-14 自定义一个错误
            if (string.IsNullOrEmpty(temp))
            {
                temp = "-9";
            }

            //return decimal.Parse(temp);
            decimal _temp = 0;
            decimal.TryParse(temp, out _temp);
            return _temp;

        }
        /// <summary>
        /// 检查用户名称
        /// </summary>
        public int uc_user_checkname(string username)
        {
            Hashtable ht = new Hashtable();
            ht.Add("username", username);
            return int.Parse(call_user_func("user", "check_username", ht)); //Utils.StrToInt(call_user_func("user", "check_username", ht), -10);//-10表示系统繁忙
        }
        public int uc_user_checkemail(string email)
        {
            Hashtable ht = new Hashtable();
            ht.Add("email", email);
            return int.Parse(call_user_func("user", "check_email", ht));//-10表示系统繁忙
        }
        /// <summary>
        /// 删除用户信息接口
        /// </summary>
        public int uc_user_delete(int uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            Hashtable hb = XmlCompent.GetTable(call_user_func("user", "delete", ht));
            return int.Parse(hb["item_0"].ToString());
        }
        /// <summary>
        /// 同步登陆
        /// </summary>
        public string uc_user_synlogin(int uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            return call_user_func("user", "synlogin", ht);//js脚本代码  - 同步登录的 HTML 代码
        }
        /// <summary>
        /// 同步退出代码
        /// </summary>
        public string uc_user_synlogout()
        {
            return call_user_func("user", "synlogout", new Hashtable());//js脚本代码  - 同步登录的 HTML 代码
        }




        /// <summary>
        /// 进入短消息中心
        /// </summary>
        /// <param name="uid">用户 ID</param>
        public void uc_pm_location(int uid)
        {
            uc_pm_location(uid, 0);
        }
        public void uc_pm_location(int uid, int newpm)
        {
            string apiurl = uc_api_url("pm_client", "ls", "uid=" + uid.ToString(), newpm == 1 ? "&folder=newbox" : "");
            //HttpContext.Current.Response.AddHeader("Expires", "0");
            //HttpContext.Current.Response.AddHeader("Cache-Control", "private, post-check=0, pre-check=0, max-age=0");
            //HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            //HttpContext.Current.Response.Redirect(apiurl, true);
        }

        /// <summary>
        /// 检查新的短消息
        /// </summary>
        public string uc_pm_checknew(int uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("more", "0");
            return call_user_func("pm", "check_newpm", ht);
        }
        public Hashtable uc_pm_checknew(int uid, int more)
        {
            if (more == 0) more = 1;//至少为1
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("more", more);
            return XmlCompent.GetTable(call_user_func("pm", "check_newpm", ht));
        }
        /// <summary>
        /// 发送短消息
        /// </summary>
        /// <param name="fromuid">发件人用户 ID，0 为系统消息</param>
        /// <param name="msgto">收件人用户名 / 用户 ID，多个用逗号分割</param>
        /// <param name="subject">消息标题</param>
        /// <param name="message">消息内容</param>
        /// <param name="instantly">是否直接发送</param>
        /// <param name="replypmid">回复的消息 ID</param>
        /// <param name="isusername">0 = msgto 为 uid、1 = msgto 为 username</param>
        /// <returns></returns>
        public int uc_pm_send(int fromuid, string msgto, string subject, string message)
        {
            return uc_pm_send(fromuid, msgto, subject, message, 0, 1, true);
        }
        public int uc_pm_send(int fromuid, string msgto, string subject, string message, int replypmid)
        {
            return uc_pm_send(fromuid, msgto, subject, message, replypmid, 1, true);
        }
        public int uc_pm_send(int fromuid, string msgto, string subject, string message, int replypmid, int isusername)
        {
            return uc_pm_send(fromuid, msgto, subject, message, replypmid, isusername, true);
        }
        public int uc_pm_send(int fromuid, string msgto, string subject, string message, int replypmid, int isusername, bool instantly)
        {
            if (!instantly)
            {
                uc_pm_send_instantly(fromuid, msgto, subject, message, replypmid, isusername);
            }
            Hashtable ht = new Hashtable();
            ht.Add("fromuid", fromuid);
            ht.Add("msgto", msgto);
            ht.Add("subject", subject);
            ht.Add("message", message);
            ht.Add("replypmid", replypmid < 0 ? 0 : replypmid);
            ht.Add("isusername", isusername);
            return int.Parse(call_user_func("pm", "sendpm", ht));//默认为发送失败
        }
        /// <summary>
        /// 进入发送短消息的界面
        /// </summary>
        public void uc_pm_send_instantly(int fromuid, string msgto, string subject, string message, int replypmid, int isusername)
        {
            subject = urlencode(subject);
            msgto = urlencode(msgto);
            message = urlencode(message);
            string replyadd = replypmid > 0 ? "&pmid=" + replypmid.ToString() + "&do=reply" : "";
            string apiurl = uc_api_url("pm_client", "send", "uid=" + fromuid.ToString(), "&msgto=" + msgto + "&subject=" + subject + "&message=" + message + replyadd);
            //HttpContext.Current.Response.AddHeader("Expires", "0");
            //HttpContext.Current.Response.AddHeader("Cache-Control", "private, post-check=0, pre-check=0, max-age=0");
            //HttpContext.Current.Response.AddHeader("Pragma", "no-cache");
            //HttpContext.Current.Response.Redirect(apiurl, true);
        }

        /// <summary>
        /// 删除短消息
        /// </summary>
        /// <param name="uid">用户 ID</param>
        /// <param name="folder">短消息所在的文件夹[inbox=收件箱，outbox=发件箱]</param>
        /// <param name="pmids">要删除的消息ID数组</param>
        /// <returns>>0 成功  xiaoyu dengyu0 失败</returns>
        public int uc_pm_delete(int uid, string folder, Hashtable pmids)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("folder", folder);
            ht.Add("pmids", pmids);
            return int.Parse(call_user_func("pm", "delete", ht));//默认为删除失败
        }

        /// <summary>
        /// 删除和 uid 对话的 touids 中的所有短消息。
        /// </summary>
        public int uc_pm_deleteuser(int uid, Hashtable touids)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("touids", touids);
            return int.Parse(call_user_func("pm", "deleteuser", ht));//默认为删除失败
        }

        /// <summary>
        /// 标记短消息已读/未读状态
        /// </summary>
        public void uc_pm_readstatus(int uid, Hashtable uids)
        {
            uc_pm_readstatus(uid, uids, new Hashtable(), 0);
        }
        public void uc_pm_readstatus(int uid, Hashtable uids, Hashtable pmids)
        {
            uc_pm_readstatus(uid, uids, pmids, 0);
        }
        public void uc_pm_readstatus(int uid, Hashtable uids, Hashtable pmids, int status)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("uids", uids);
            ht.Add("pmids", pmids);
            ht.Add("status", status);
            call_user_func("pm", "readstatus", ht);
        }

        /// <summary>
        /// 获取短消息列表
        /// </summary>
        /// <param name="uid">用户 ID</param>
        /// <param name="page">当前页编号，默认值 1</param>
        /// <param name="pagesize">每页最大条目数，默认值 10</param>
        /// <param name="folder">打开的目录 newbox=未读消息，inbox=收件箱，outbox=发件箱</param>
        /// <param name="filter">过滤方式 newpm=未读消息，systempm=系统消息，announcepm=公共消息</param>
        /// <param name="msglen">截取的消息文字长度</param>
        /// <returns>array('count' => 消息总数, 'data' => 短消息数据)</returns>

        public Hashtable uc_pm_list(int uid, int page, int pagesize, string folder, string filter, int msglen)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("page", page);
            ht.Add("pagesize", pagesize);
            ht.Add("folder", folder);
            ht.Add("filter", filter);
            ht.Add("msglen", msglen);
            return XmlCompent.GetTable(call_user_func("pm", "ls", ht));
        }
        /// <summary>
        /// 忽略未读消息提示
        /// </summary>
        public void uc_pm_ignore(int uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            call_user_func("pm", "ignore", ht);
        }
        /// <summary>
        /// 获取短消息内容
        /// </summary>
        /// <param name="uid">用户 ID</param>
        /// <param name="pmid">消息 ID</param>
        /// <param name="touid">消息对方用户 ID</param>
        /// <param name="daterange">日期范围 1=今天,2=昨天,3=前天,4=上周,5=更早</param>
        /// <returns>短消息内容数组</returns>
        public Hashtable uc_pm_view(int uid, int pmid)
        {
            return uc_pm_view(uid, pmid, 0, 1);
        }
        public Hashtable uc_pm_view(int uid, int pmid, int touid)
        {
            return uc_pm_view(uid, pmid, touid, 1);
        }
        public Hashtable uc_pm_view(int uid, int pmid, int touid, int daterange)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("pmid", pmid);
            ht.Add("touid", touid);
            ht.Add("daterange", daterange);
            return XmlCompent.GetTable(call_user_func("pm", "view", ht));
        }
        /// <summary>
        /// 获取单条短消息内容
        /// </summary>
        public Hashtable uc_pm_viewnode(int uid)
        {
            return uc_pm_viewnode(uid, 0, 0);
        }
        public Hashtable uc_pm_viewnode(int uid, int type, int pmid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("type", type);
            ht.Add("pmid", pmid);
            return XmlCompent.GetTable(call_user_func("pm", "viewnode", ht));
        }
        /// <summary>
        /// 获取黑名单
        /// </summary>
        public string uc_pm_blackls_get(int uid)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            return call_user_func("pm", "blackls_get", ht);
        }
        /// <summary>
        /// 更新黑名单
        /// </summary>
        public bool uc_pm_blackls_set(int uid, string blackls)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("blackls", blackls);
            string t = call_user_func("pm", "blackls_set", ht);
            if (t == "1")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 添加黑名单项目
        /// </summary>
        public bool uc_pm_blackls_add(int uid, Hashtable username)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("username", username);
            string t = call_user_func("pm", "blackls_add", ht);
            if (t == "1")
                return true;
            else
                return false;
        }
        /// <summary>
        /// 删除黑名单项目
        /// </summary>
        public void uc_pm_blackls_delete(int uid, Hashtable username)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("username", username);
            call_user_func("pm", "blackls_delete", ht);
        }
        #region 积分部分

        /// <summary>
        /// 获得用户积分
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="uid"></param>
        /// <param name="credit"></param>
        /// <returns></returns>
        public int uc_user_getcredit(int appid, int uid, int credit)
        {
            Hashtable ht = new Hashtable();
            ht.Add("appid", appid);
            ht.Add("uid", uid);
            ht.Add("credit", credit);
            //string s = call_user_func("user", "getcredit", ht);
            return int.Parse(call_user_func("user", "getcredit", ht));//默认为删除失败
        }
        /// <summary>
        /// 获得指定应用的积分
        /// </summary>
        public int uc_credit_updatecredit(int uid, int to, int toappid, int amount)
        {
            Hashtable ht = new Hashtable();
            ht.Add("uid", uid);
            ht.Add("to", to);
            ht.Add("toappid", toappid);
            ht.Add("amount", amount);
            //string s = call_user_func("credit", "update", ht);
            return int.Parse(call_user_func("credit", "update", ht));//默认为删除失败
        }

        #endregion

        #endregion

        public string geturl(string url, string postdata)
        {
            return uc_fopen2(url, 500000, postdata, string.Empty, true, "", 20);
        }

        #region API接口部分
        public string uc_api_url(string module, string action)
        {
            return uc_api_url(module, action, "", "");
        }
        public string uc_api_url(string module, string action, string arg, string extra)
        {
            return SqlHelper.UC_API + "/index.php?" + uc_api_requestdata(module, action, arg, extra);
        }


        public string call_user_func(string module, string action, Hashtable hb)
        {
            string s = "";
            string sep = "";
            foreach (DictionaryEntry de in hb)
            {
                if (de.Value.GetType().ToString() == "System.Collections.Hashtable")
                {
                    string s2 = "";
                    string sep2 = "";
                    Hashtable ht = (Hashtable)de.Value;
                    foreach (DictionaryEntry de1 in ht)
                    {
                        s2 = sep2 + de.Key.ToString() + "[" + de1.Key.ToString() + "]=" + urlencode(de1.Value.ToString()); // System.Text.Encoding.GetEncoding(de1.Value.ToString());
                        sep2 = "&";
                    }
                    s += sep2 + s2;
                }
                else
                {


                    s += sep + de.Key.ToString() + "=" + Dev.Comm.Utils.MockUrlCode.UrlEncode(de.Value.ToString());
                    //s += sep + de.Key.ToString() + "=" + urlencode(de.Value.ToString());
                }
                sep = "&";
            }
            string postdata = uc_api_requestdata(module, action, s, "");
            // XZ.Common.WebClient client = new XZ.Common.WebClient();
            //client.Encoding = System.Text.Encoding.Default;
            // client.OpenRead(SqlHelper.UC_API + "/index.php?", postdata);
            return uc_fopen2(SqlHelper.UC_API + "/index.php", 500000, postdata, string.Empty, true, SqlHelper.UC_IP, 20);
            //return client.RespHtml;
        }
        public string uc_api_requestdata(string module, string action, string arg, string extra)
        {
            string input = uc_api_input(arg);
            string post = "m=" + module + "&a=" + action + "&inajax=2&input=" + input + "&appid=" + SqlHelper.UC_APPID + extra;
            return post;
        }
        public string uc_api_input(string data)
        {
            string s = urlencode(uc_Authcode.DiscuzAuthcodeEncode(data + "&agent=" + uc_Authcode.MD5(UserAgent) + "&time=" + gettimestamp(), SqlHelper.UC_KEY));
            return s;
        }
        public string gettimestamp()
        {
            DateTime timeStamp = new DateTime(1970, 1, 1);  //得到1970年的时间戳
            long s = (DateTime.UtcNow.Ticks - timeStamp.Ticks) / 10000000;
            return s.ToString();

            //注意这里有时区问题，用now就要减掉8个小时
            //DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            //DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            //TimeSpan toNow = dtNow.Subtract(dtStart);
            //string timeStamp = toNow.Ticks.ToString();
            //return timeStamp.Substring(0, timeStamp.Length - 7);
        }
        /// <summary>
        /// php的urlencode函数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string urlencode(string str)
        {
            //此处有问题，因为php的urlencode不同于HttpUtility.UrlEncode
            //return HttpUtility.UrlEncode(str);
            string tmp = string.Empty;
            string strSpecial = "_-.1234567890abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < str.Length; i++)
            {
                string crt = str.Substring(i, 1);
                if (strSpecial.Contains(crt))
                    tmp += crt;
                else
                {
                    byte[] bts = System.Text.Encoding.Default.GetBytes(crt);
                    foreach (byte bt in bts)
                    {
                        tmp += "%" + bt.ToString("X");
                    }
                }
            }
            return tmp;
        }
        #endregion
        #region uc_fopen
        private static string uc_fopen(string url)
        {
            return uc_fopen(url, 0, string.Empty, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen(string url, int limit)
        {
            return uc_fopen(url, limit, string.Empty, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen(string url, int limit, string post)
        {
            return uc_fopen(url, limit, post, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen(string url, int limit, string post, string cookie)
        {
            return uc_fopen(url, limit, post, cookie, false, string.Empty, 15, true);
        }

        private static string uc_fopen(string url, int limit, string post, string cookie, bool bysocket)
        {
            return uc_fopen(url, limit, post, cookie, bysocket, string.Empty, 15, true);
        }

        private static string uc_fopen(string url, int limit, string post, string cookie, bool bysocket, string ip)
        {
            return uc_fopen(url, limit, post, cookie, bysocket, ip, 15, true);
        }

        private static string uc_fopen(string url, int limit, string post, string cookie, bool bysocket, string ip, int timeout)
        {
            return uc_fopen(url, limit, post, cookie, bysocket, ip, timeout, true);
        }

        private static string uc_fopen(string url, int limit, string post, string cookie, bool bysocket, string ip, int timeout, bool block)
        {
            //时间锁定 过期时间为
            //DateTime ndt = Convert.ToDateTime("2009-2-10");
            //TimeSpan tss = ndt - DateTime.Now;
            //if (tss.TotalDays < 0)
            //{
            //    HttpContext.Current.Response.Write("使用过期了");
            //    HttpContext.Current.Response.End();
            //    return "";
            //}
            bool bRetVal = false;
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(url);
            //myRequest.AllowAutoRedirect = true;
            //if (string.IsNullOrEmpty(HttpContext.Current.Request.UserAgent))
            //    myRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            //else                

            //myRequest.UserAgent = UserAgent;
            //myRequest.UserAgent = HttpContext.Current.Request.UserAgent;
            //myRequest.KeepAlive = true;
            //if (HttpContext.Current.Request.Url != null)
            //    myRequest.Referer = HttpContext.Current.Request.Url.ToString();

            //myRequest.CookieContainer = ;

            if (string.IsNullOrEmpty(post))
            {
                myRequest.Method = "GET";
            }
            else
            {
                //myRequest.Method = "POST";
                //Stream myStream = new MemoryStream();//定义这个Stream是只是为了得到发送字串 经过编码之后得到的byte的长度。                    
                //StreamWriter myStreamWriter = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(SqlHelper.UC_CHARSET));//用默认编码 得到Stream
                //myStreamWriter.Write(post);
                //myStreamWriter.Flush();
                //long len = myStream.Length;//目的完成
                //myStreamWriter.Close();
                //myStream.Close();

                //myRequest.ContentType = "application/x-www-form-urlencoded";
                //myRequest.ContentLength = len;//如果字符串中存在中文 使用loginWebView.postContent.Length得到长度和编码之后的长度是不一样的:(

                //Stream newStream = myRequest.GetRequestStream();
                //myStreamWriter = new StreamWriter(myStream, System.Text.Encoding.GetEncoding(SqlHelper.UC_CHARSET));//编码使用Encoding.Default 换来换去编码方法 还是用它得到的中文不会出现乱码

                //myStreamWriter.Write(post);
                //myStreamWriter.Close();
                //myStream.Close();

                Encoding encoding = Encoding.GetEncoding(SqlHelper.UC_CHARSET);
                byte[] data = encoding.GetBytes(post);

                myRequest.UserAgent = UserAgent;
                myRequest.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn");
                myRequest.Method = "POST";
                myRequest.ContentType = "application/x-www-form-urlencoded";
                myRequest.ContentLength = data.Length;

                Stream newStream = myRequest.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Close();

            }

            //创建接收回馈的字节流类
            string responseHtml = string.Empty;
            //myRequest.Timeout = timeout;
            HttpWebResponse webResponse = null;
            try
            {
                webResponse = (HttpWebResponse)myRequest.GetResponse();
                bRetVal = true;
                if (webResponse.StatusCode != HttpStatusCode.OK)
                    bRetVal = false;

                if (bRetVal)
                {
                    //创建接收回馈的字节流类
                    Stream receiveStream = webResponse.GetResponseStream();//得到回写的字节流
                    StreamReader readStream = new StreamReader(receiveStream, System.Text.Encoding.GetEncoding(SqlHelper.UC_CHARSET));
                    responseHtml = readStream.ReadToEnd();
                    readStream.Close();
                }
                if (webResponse != null)
                    webResponse.Close();
            }
            catch (Exception exp)
            {
                throw;
            }

            return responseHtml;
        }
        #endregion

        #region uc_fopen2
        private static string uc_fopen2(string url)
        {
            return uc_fopen2(url, 0, string.Empty, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen2(string url, int limit)
        {
            return uc_fopen2(url, limit, string.Empty, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen2(string url, int limit, string post)
        {
            return uc_fopen2(url, limit, post, string.Empty, false, string.Empty, 15, true);
        }

        private static string uc_fopen2(string url, int limit, string post, string cookie)
        {
            return uc_fopen2(url, limit, post, cookie, false, string.Empty, 15, true);
        }

        private static string uc_fopen2(string url, int limit, string post, string cookie, bool bysocket)
        {
            return uc_fopen2(url, limit, post, cookie, bysocket, string.Empty, 15, true);
        }

        private static string uc_fopen2(string url, int limit, string post, string cookie, bool bysocket, string ip)
        {
            return uc_fopen2(url, limit, post, cookie, bysocket, ip, 15, true);
        }

        private static string uc_fopen2(string url, int limit, string post, string cookie, bool bysocket, string ip, int timeout)
        {
            return uc_fopen2(url, limit, post, cookie, bysocket, ip, timeout, true);
        }

        private static string uc_fopen2(string url, int limit, string post, string cookie, bool bysocket, string ip, int timeout, bool block)
        {
            int times = 1;
            //if (HttpContext.Current.Request["__times__"] != null)
            //{
            //    times = int.Parse(HttpContext.Current.Request["__times__"].ToString()) + 1;
            //}
            //if (times > 2)
            //    return string.Empty;

            //url += url.Contains("?") ? "&" : "?" + "__times__=" + times;

            return uc_fopen(url, limit, post, cookie, bysocket, ip, timeout, block);
        }
        #endregion
        #region 将UNIX时间戳转换成系统时间

        public DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }
        #endregion
    }

    /// <summary>
    /// SqlHelper类是专门提供给广大用户用于高性能、可升级和最佳练习的sql数据操作
    /// </summary>
    public abstract class SqlHelper
    {

        //        define('UC_CHARSET', 'utf-8');
        //define('UC_KEY', 'l9e5f387Y5IcW9I6U3q6Abibfccag4Qc86pfQ53ai4L9KdJfFfIci7I6Oeyb472c');
        //define('UC_API', 'http://localhost:34382/ucServer');
        //define('UC_APPID', '1');
        //数据库连接字符串
        //public static readonly string ConnectionStringLocalTransaction = ConfigurationManager.ConnectionStrings["SQLConnString"].ConnectionString;
        //关键接口字符串
        public static readonly string UC_KEY = "l9e5f387Y5IcW9I6U3q6Abibfccag4Qc86pfQ53ai4L9KdJfFfIci7I6Oeyb472c";//Dx.Common.EncryptFunc.JieMi(ConfigurationManager.AppSettings["UC_KEY"].ToString());
        public static readonly string UC_APPID = "1";// ConfigurationManager.AppSettings["UC_APPID"];
        public static readonly string UC_IP = "";// ConfigurationManager.AppSettings["UC_IP"];
        public static readonly string UC_API = "http://localhost:34382/ucServer";// ConfigurationManager.AppSettings["UC_API"];
        public static readonly string UC_CHARSET = "utf-8";// ConfigurationManager.AppSettings["UC_CHARSET"];
        //public static readonly string UserCookie1 = ConfigurationManager.AppSettings["CookieField1"];
        //public static readonly string UserCookie2 = ConfigurationManager.AppSettings["CookieField2"];
        //public static readonly string CodeFlag = ConfigurationManager.AppSettings["CodeFlag"];
        //public static readonly string ManageCode = ConfigurationManager.AppSettings["ManageCode"];
        //public static readonly string ServerNum = ConfigurationManager.AppSettings["ServerNum"];
        //public static readonly string GameType = ConfigurationManager.AppSettings["GameType"];
        //public static readonly string AreaNo = ConfigurationManager.AppSettings["AreaNo"];
        //public static readonly string GameURL = ConfigurationManager.AppSettings["GameURL"];
        //public static readonly string PayURL = ConfigurationManager.AppSettings["PayURL"];
    }
}
