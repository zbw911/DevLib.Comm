using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Comm.Php
{
    /// <summary>
    /// php C# 方法
    /// </summary>
    public class PhpUtils
    {
        /// <summary>
        /// 计算Php格式的当前时间
        /// </summary>
        /// <returns>Php格式的时间</returns>
        public static long PhpTimeNow()
        {
            return DateTimeToPhpTime(DateTime.UtcNow);
        }

        /// <summary>
        /// PhpTime转DataTime
        /// </summary>
        /// <returns></returns>
        public static DateTime PhpTimeToDateTime(long time)
        {
            var timeStamp = new DateTime(1970, 1, 1);
            long t = (time + 8 * 60 * 60) * 10000000 + timeStamp.Ticks;
            return new DateTime(t);
        }

        /// <summary>
        /// DataTime转PhpTime
        /// </summary>
        /// <param name="datetime">时间</param>
        /// <returns></returns>
        public static long DateTimeToPhpTime(DateTime datetime)
        {
            var timeStamp = new DateTime(1970, 1, 1);
            return (datetime.Ticks - timeStamp.Ticks) / 10000000;
        }
    }
}
