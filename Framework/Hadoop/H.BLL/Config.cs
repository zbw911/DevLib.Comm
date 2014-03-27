using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace H.BLL
{
    public class Config
    {
        /// <summary>
        /// 是否使用Hbase功能
        /// </summary>
        public static bool IsUseHbase
        {
            private set;
            get;
        }

        static Config()
        {
            string configuseHbase = System.Configuration.ConfigurationManager.AppSettings["usehbase"];

            if (!string.IsNullOrEmpty(configuseHbase))
            {
                IsUseHbase = bool.Parse(configuseHbase);
            }
        }
    }
}
