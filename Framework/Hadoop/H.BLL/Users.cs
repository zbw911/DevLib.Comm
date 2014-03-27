using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using H.HbaseProvider;
using H.Comm;

namespace H.BLL
{
    /// <summary>
    /// 用户关系输入
    /// </summary>
    public class Users
    {
        /// <summary>
        /// 一个用户与另一个用户的联系，这里只考虑一个单向的关系
        /// </summary>
        /// <param name="fromUid"></param>
        /// <param name="toUid"></param>
        /// <param name="UpdateTime"></param>
        public static void AddRelation(string fromUid, string toUid, bool add, DateTime UpdateTime)
        {
            if (!Config.IsUseHbase) return;

            string tableName = "relation";

            using (var hclient = HBaseClientPool.GetHclient())
            {
                string desctime = H.Comm.TimeUtility.DescTimeStamp(UpdateTime).ToString();

                string rowkey = H.Comm.StringUtility.FixedLenString(fromUid, 20)
                    + H.Comm.StringUtility.FixedLenString(toUid, 20)
                    //+ desctime
                    ;
                if (add)
                {
                    hclient.Client.mutateRow(tableName.ToBytes(), rowkey.ToBytes(),
                        new List<Apache.Hadoop.Hbase.Mutation> { 
                    new Apache.Hadoop.Hbase.Mutation{ Column= "r:uid".ToBytes(), Value = fromUid.ToBytes() },
                    new Apache.Hadoop.Hbase.Mutation{ Column= "r:tid".ToBytes(), Value = toUid.ToBytes() },
                    new Apache.Hadoop.Hbase.Mutation{ Column= "r:dt".ToBytes(), Value = UpdateTime.ToString().ToBytes() }
                });
                }
                else
                {
                    hclient.Client.deleteAllRow(tableName.ToBytes(), rowkey.ToBytes());
                }
            }
        }
    }
}
