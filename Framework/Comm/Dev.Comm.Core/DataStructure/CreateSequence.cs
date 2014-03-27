// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年09月17日 9:55
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/CreateSequence.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dev.Comm.DataStructure
{
    /// <summary>
    ///   创建顺序的串
    /// </summary>
    public class CreateSequence
    {
        private readonly int _len;
        private readonly string[] _seed;

        /// <summary>
        /// </summary>
        /// <param name="len"> 长度 </param>
        /// <param name="seed"> 种子 </param>
        public CreateSequence(int len, string[] seed)
        {
            _len = len;
            _seed = seed;
        }


        /// <summary>
        ///   开始创建
        /// </summary>
        /// <returns> </returns>
        public IEnumerable<string> BeginCreate()
        {
            var List = this.CreateNo(this._len);

            return List.Where(x => x.Length == this._len);
        }

        /// <summary>
        /// </summary>
        /// <param name="position"> </param>
        private IEnumerable<string> CreateNo(int position)
        {
            if (position <= 0)
                yield break;

            foreach (var str in _seed)
            {
                yield return str;

                foreach (var poses in CreateNo(position - 1))
                {
                    yield return str + poses;
                }
            }
        }
    }
}