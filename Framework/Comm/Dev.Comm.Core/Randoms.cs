// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/Randoms.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Security.Cryptography;

namespace Dev.Comm
{
    /// <summary>
    /// 
    /// </summary>
    public class Randoms
    {
        #region 生成随机数

        /// <summary>
        ///   生成随机数
        /// </summary>
        /// <param name="codeCount"> 随机数个数 </param>
        /// <returns> STRING </returns>
        public static string CreateRandomCode(int codeCount)
        {
            string allChar = "2,3,4,5,6,7,8,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,w,x,y";
            //"A,B,C,D,E,F,G,H,I,J,K,M,N,P,Q,R,S,T,U,W,X,Y";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            var rand = new Random();
            for (int i = 0; i < codeCount; i++)
            {
                if (temp != -1)
                {
                    //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
                    var s = (int)DateTime.Now.Ticks;
                    rand = new Random(GetRandomSeed());
                }
                int t = rand.Next(29);
                if (temp == t)
                {
                    return CreateRandomCode(codeCount);
                }
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }


        /// <summary>
        /// </summary>
        /// <param name="max"> </param>
        /// <returns> </returns>
        public static int CreateRandomNumber(int max)
        {
            var rand = new Random(GetRandomSeed());
            return rand.Next(max);
        }

        ///// <summary>
        ///// 生成随机数
        ///// </summary>
        ///// <param name="codeCount">随机数个数</param>
        ///// <returns>STRING</returns>
        //public static string CreateRandomNum(int codeCount)
        //{
        //    string allChar = "0,1,2,3,4,5,6,7,8,9";
        //    //"A,B,C,D,E,F,G,H,I,J,K,M,N,P,Q,R,S,T,U,W,X,Y";
        //    string[] allCharArray = allChar.Split(',');
        //    string randomCode = "";
        //    int temp = -1;

        //    Random rand = new Random();
        //    for (int i = 0; i < codeCount; i++)
        //    {
        //        if (temp != -1)
        //        {
        //            //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
        //            int s = (int)DateTime.Now.Ticks;
        //            rand = new Random(GetRandomSeed());
        //        }
        //        int t = rand.Next(29);
        //        if (temp == t)
        //        {
        //            return CreateRandomCode(codeCount);
        //        }
        //        temp = t;
        //        randomCode += allCharArray[t];
        //    }
        //    return randomCode;
        //}

        #endregion

        #region 生成随机数

        /// <summary>
        ///   生成随机数
        /// </summary>
        /// <param name="codeCount"> 随机数个数 </param>
        /// <returns> STRING </returns>
        public static string CreateRandomCode()
        {
            string allChar = "0,1,2,3,4,5,6,7,8,9";
            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            var rand = new Random();
            for (int i = 0; i < 4; i++)
            {
                if (temp != -1)
                {
                    //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
                    rand = new Random(GetRandomSeed());
                }
                int t = rand.Next(10);
                //if (temp == t)
                //{
                //    return CreateRandomCode();
                //}
                temp = t;
                randomCode += allCharArray[t];
            }
            return randomCode;
        }

        public static int GetRandomSeed()
        {
            var bytes = new byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(bytes);
            return BitConverter.ToInt32(bytes, 0);
        }

        #endregion

        #region 生成随机数0-9的数字

        /// lianyee
        /// <summary>
        ///   生成随机数
        /// </summary>
        /// <param name="codeCount"> 随机数个数 </param>
        /// <returns> STRING </returns>
        public static string CreateRandomEleCode()
        {
            string allChar = "A1,A2,A3,A4,A5,A6,A7,A8," +
                             "B1,B2,B3,B4,B5,B6,B7,B8," +
                             "C1,C2,C3,C4,C5,C6,C7,C8," +
                             "D1,D2,D3,D4,D5,D6,D7,D8," +
                             "E1,E2,E3,E4,E5,E6,E7,E8," +
                             "F1,F2,F3,F4,F5,F6,F7,F8," +
                             "G1,G2,G3,G4,G5,G6,G7,G8," +
                             "H1,H2,H3,H4,H5,H6,H7,H8," +
                             "I1,I2,I3,I4,I5,I6,I7,I8," +
                             "J1,J2,J3,J4,J5,J6,J7,J8,";

            string[] allCharArray = allChar.Split(',');
            string randomCode = "";
            int temp = -1;

            var rand = new Random();
            if (temp != -1)
            {
                //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
                var s = (int)DateTime.Now.Ticks;
                rand = new Random(GetRandomSeed());
            }
            int t = rand.Next(80);
            if (temp == t)
            {
                return CreateRandomEleCode();
            }
            temp = t;
            randomCode += allCharArray[t];
            return randomCode;
        }

        #endregion

        public static int CreatRandNum(int i)
        {
            var rand = new Random();
            return rand.Next(i);
        }

        public static string GetID_20()
        {
            string OutID = "";
            try
            {
                OutID = DateTime.Now.ToString("yyMMddHHmmssfff") + CreateRandomCode();
                return OutID;
            }
            catch
            {
                OutID = DateTime.Now.ToString("yyMMddHHmmssfff") + CreateRandomCode();
                ;
                return OutID;
            }
        }
    }
}