// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:32
//  文件名：Dev.Libs/Dev.Comm.Core/DateUtil.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Globalization;

namespace Dev.Comm.Utils
{
    public class DateUtil
    {
        /// <summary>
        ///   取得某月的最后一天
        ///   方法一：使用算出該月多少天，年+月+加上多少天即得，舉例取今天這個月的最后一天
        /// </summary>
        /// <param name="DtStart"> </param>
        /// <param name="DtEnd"> </param>
        public static void GetLastDateForMonth(DateTime DtStart, out DateTime DtEnd)
        {

           
            int Dtyear, DtMonth;
            DtStart = DateTime.Now;
            Dtyear = DtStart.Year;
            DtMonth = DtStart.Month;
            int MonthCount = DateTime.DaysInMonth(Dtyear, DtMonth); //計算該月有多少天
            DtEnd = Convert.ToDateTime(Dtyear.ToString() + "-" + DtMonth.ToString() + "-" + MonthCount);
        }

        /// <summary>
        ///   取得某月的最后一天
        ///   方法二：取出下月的第一天減去一天便是這個月的最后一天
        /// </summary>
        /// <param name="DtStart"> </param>
        /// <param name="DtEnd"> </param>
        public static void GetLastDateForMonthEx(DateTime DtStart, out DateTime DtEnd)
        {
            int Dtyear, DtMonth;

            DtStart = DateTime.Now.AddMonths(1); //月份加1
            Dtyear = DtStart.Year;
            DtMonth = DtStart.Month;
            DtEnd = Convert.ToDateTime(Dtyear.ToString() + "-" + DtMonth.ToString() + "-" + "1").AddDays(-1);
            //取出下月的第一天減去一天
        }

        /// <summary>
        ///   获取时间差
        /// </summary>
        /// <param name="datepart"> yy表示相差的年，mm表示相差的月，dd表示相差的天，hh表示相差的小时，mi表示相差的分，ss表示相差的秒 </param>
        /// <param name="startdate"> </param>
        /// <param name="enddate"> </param>
        /// <returns> </returns>
        public static int DateDiff(string datepart, DateTime startdate, DateTime enddate)
        {
            TimeSpan Span = enddate - startdate;

            DateTime Age = DateTime.MinValue + Span;

            // note: MinValue is 1/1/1 so we have to subtract...
            int Years = Age.Year - 1;
            int Months = Age.Month - 1;
            int Days = Age.Day - 1;

            int result = 0;
            switch (datepart)
            {
                case "yy":
                    result = Years;
                    break;
                case "mm":
                    result = Years*12;
                    result += Months;
                    if (Days > 0)
                        result += 1;
                    break;
                case "dd":
                    result = Span.Days;
                    break;
                case "hh":
                    result = Span.Hours;
                    break;
                case "mi":
                    result = Span.Minutes;
                    break;
                case "ss":
                    result = Span.Seconds;
                    break;
            }
            return result;
        }

        /// <summary>
        ///   将系统时间转换成UNIX时间戳
        /// </summary>
        /// <param name="Date"> </param>
        /// <returns> </returns>
        public static string ToUnixTimeStamp(DateTime Date)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            TimeSpan toNow = Date.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            timeStamp = timeStamp.Substring(0, timeStamp.Length - 7);
            return timeStamp;
        }

        /// <summary>
        ///   将UNIX时间戳转换成系统时间
        /// </summary>
        /// <param name="timeStamp"> </param>
        /// <returns> </returns>
        public static DateTime GetUnixTimeStamp(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            var toNow = new TimeSpan(lTime);
            DateTime dtResult = dtStart.Add(toNow);
            return dtResult;
        }


        /// <summary>
        ///   时间格式化 added by zbw911
        /// </summary>
        /// <param name="dateTime"> </param>
        /// <param name="format"> </param>
        /// <returns> </returns>
        public static string my_date_format2(DateTime dateTime, string format = "M月d日 H时m分")
        {
            TimeSpan ts = DateTime.Now - dateTime;

            //一天以前显示 
            if (ts.Days >= 1)
            {
                return my_date_format(dateTime, format);
            }
            else if (ts.Days < 1 && ts.Hours >= 1)
            {
                return ts.Hours + "小时" + (ts.Minutes > 0 ? ts.Minutes + "分钟" : "") + "前";
            }
            else if (ts.Hours < 1 && ts.Minutes > 1)
            {
                return ts.Minutes + "分钟前";
            }
            else
            {
                return "刚刚";
            }
        }

        /// <summary>
        ///   常用时间格式化
        /// </summary>
        /// <param name="timestamp"> </param>
        /// <param name="format"> </param>
        /// <returns> </returns>
        public static string my_date_format(DateTime timestamp, string format = "yyyy-MM-dd H:m:s")
        {
            return timestamp.ToString(format);
        }

        public static string GetZhCnDayOfWeek(DateTime datetime)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(datetime.DayOfWeek);
        }

        public static string GetDateStrWithZhcnMonthDay(DateTime datetime)
        {
            return string.Format((datetime).ToString("M月d日 HH:mm"), (GetZhCnDayOfWeek(datetime)));
        }

        public static string GetDataStrWithZhcnDayofWeek(DateTime datetime)
        {
            return string.Format((datetime).ToString("M月d日 {0} HH:mm"), (GetZhCnDayOfWeek(datetime)));
        }

        /// <summary>
        ///   将UTC 时间 转化为本地化时间
        /// </summary>
        /// <param name="utcDate"> </param>
        /// <returns> </returns>
        public static DateTime UtcToLocal(DateTime utcDate)
        {
            DateTime convertedDate = DateTime.SpecifyKind(utcDate, DateTimeKind.Utc);

            var localtime = convertedDate.ToLocalTime();

            return localtime;
        }
    }
}