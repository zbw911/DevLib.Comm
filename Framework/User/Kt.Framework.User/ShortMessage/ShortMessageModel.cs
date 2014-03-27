// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 10:44
// 
// 修改于：2013年02月18日 18:24
// 文件名：ShortMessageModel.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
using System;

namespace Dev.Framework.User.ShortMessage
{
    public class ShortMessageModel
    {
        public string Mobile { get; set; }

        public DateTime LastTime { get; set; }

        public int SendTimes { get; set; }
    }
}