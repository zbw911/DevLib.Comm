// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年11月13日 16:47
//  
//  修改于：2013年11月13日 16:49
//  文件名：Dev.Libs/Dev.Comm.WinForm/ThreadInvoke.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Windows.Forms;

namespace Dev.Comm.WinForm
{
    /// <summary>
    ///   跨线程调用控件
    /// </summary>
    public static class ThreadInvoke
    {
        /// <summary>
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="t"> </param>
        /// <param name="action"> </param>
        public static void Invork<T>(T t, Action<T> action) where T : Control
        {
            var handler = new EventHandler<EventArgs>((object sender, EventArgs e) => action(t));

            if (t.InvokeRequired)
            {
                t.Invoke(handler, new object[] { t, null });
            }
            else
            {
                handler(t, null);
            }
        }


        /// <summary>
        /// </summary>
        /// <typeparam name="T"> </typeparam>
        /// <param name="t"> </param>
        /// <param name="action"> </param>
        public static void BeginInvork<T>(T t, Action<T> action) where T : Control
        {
            var handler = new EventHandler<EventArgs>((object sender, EventArgs e) => action(t));

            if (t.InvokeRequired)
            {
                t.BeginInvoke(handler, new object[] { t, null });
            }
            else
            {
                handler(t, null);
            }


        }


        /// <summary>
        /// 在线程中安全调用WinForm控件的扩展方法
        /// </summary>
        /// <param name="control"></param>
        /// <param name="action"></param>
        public static void ThreadInvokeAction(this Control control, Action<Control> action)
        {
            Invork(control, action);
        }
    }
}