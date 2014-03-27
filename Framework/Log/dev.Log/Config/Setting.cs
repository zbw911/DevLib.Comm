using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Log.Config
{
    /// <summary>
    /// 
    /// </summary>
    public class Setting
    {
        /// <summary>
        /// 附加log
        /// </summary>
        /// <param name="observer"></param>
        public static void AttachLog(ILog observer)
        {
            SingletonLogger.Instance.Attach(observer);
        }

        /// <summary>
        /// 去除log
        /// </summary>
        /// <param name="observer"></param>
        public static void DetachLog(ILog observer)
        {
            SingletonLogger.Instance.Detach(observer);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="logSeverity"></param>
        public static void SetLogSeverity(LogSeverity logSeverity)
        {
            SingletonLogger.Instance.Severity = logSeverity;
        }
    }
}
