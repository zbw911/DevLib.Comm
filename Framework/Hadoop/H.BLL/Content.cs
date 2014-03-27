using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H.HbaseProvider;
using H.Comm;
namespace H.BLL
{
    /// <summary>
    /// 内容相关
    /// 用户所产生的内容，用于数据挖掘
    /// </summary>
    public class Content
    {
        public static void UserContent(string uid, string content, DateTime dt)
        {
            if (!Config.IsUseHbase) return;

            string tableName = "usercontent";

            using (var hclient = HBaseClientPool.GetHclient())
            {
                string strtime = H.Comm.TimeUtility.DescTimeStamp(dt).ToString();
                string row = H.Comm.StringUtility.FixedLenString(uid, 20) + strtime;

                hclient.Client.mutateRow(tableName.ToBytes(), row.ToBytes(),
                    new List<Apache.Hadoop.Hbase.Mutation> { 
                new Apache.Hadoop.Hbase.Mutation{ Column= "c:uid".ToBytes(), Value = uid.ToBytes() },
                new Apache.Hadoop.Hbase.Mutation{ Column= "c:content".ToBytes(), Value = content.ToBytes() },
                new Apache.Hadoop.Hbase.Mutation{ Column= "c:dt".ToBytes(), Value = dt.ToString().ToBytes() }
                });

            }
        }
    }
}
