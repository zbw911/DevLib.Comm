using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Comm.Extensions
{
    /// <summary>
    /// From:http://stackoverflow.com/questions/222598/how-do-i-clone-a-generic-list-in-c
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// 对列表进行克隆
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="listToClone"></param>
        /// <returns></returns>
        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }

        //private static void t()
        //{
        //    List<string> list = new List<string>();

        //    var l2 = list.Clone();

        //    int i = 0;
        //    List<int> ilist = new List<int>();
        //    // Error
        //    //var il2 = ilist.Clone();
        //}
    }
}
