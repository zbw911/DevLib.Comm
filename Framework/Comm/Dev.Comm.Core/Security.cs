// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/Security.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Dev.Comm
{
    public class Security
    {
        #region MD5加密算法,加密后小写

        public static string GetMD5(string s)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding("utf-8").GetBytes(s));
            var sb = new StringBuilder(32);
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }
            return sb.ToString();
        }

        #endregion

        //private static readonly string strkey = ConfigurationSettings.AppSettings["DescKey"];

        ///// <summary>
        ///// 生成随机数
        ///// </summary>
        ///// <param name="codeCount">随机数个数</param>
        ///// <returns>STRING</returns>
        //public static string CreateRandomCode(int codeCount)
        //{
        //    string allChar = "2,3,4,5,6,7,8,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,w,x,y";
        //    //"A,B,C,D,E,F,G,H,I,J,K,M,N,P,Q,R,S,T,U,W,X,Y";
        //    string[] allCharArray = allChar.Split(',');
        //    string randomCode = "";
        //    int temp = -1;

        //    var rand = new Random();
        //    for (int i = 0; i < codeCount; i++)
        //    {
        //        if (temp != -1)
        //        {
        //            //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
        //            var s = (int) DateTime.Now.Ticks;
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

        //public static int GetRandomSeed()
        //{
        //    var bytes = new byte[4];
        //    var rng = new RNGCryptoServiceProvider();
        //    rng.GetBytes(bytes);
        //    return BitConverter.ToInt32(bytes, 0);
        //}


        //public static int CreateRandomNumber(int max)
        //{
        //    var rand = new Random(GetRandomSeed());
        //    return rand.Next(max);
        //}

        ///// <summary>
        ///// 生成随机数
        ///// </summary>
        ///// <param name="codeCount">随机数个数</param>
        ///// <returns>STRING</returns>
        //public static string CreateRandomCode()
        //{
        //    string allChar = "0,1,2,3,4,5,6,7,8,9";
        //    string[] allCharArray = allChar.Split(',');
        //    string randomCode = "";
        //    int temp = -1;

        //    var rand = new Random();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        if (temp != -1)
        //        {
        //            //rand = new Random(i*temp*((int)DateTime.Now.Ticks));
        //            rand = new Random(GetRandomSeed());
        //        }
        //        int t = rand.Next(10);
        //        //if (temp == t)
        //        //{
        //        //    return CreateRandomCode();
        //        //}
        //        temp = t;
        //        randomCode += allCharArray[t];
        //    }
        //    return randomCode;
        //}

        /// <summary>
        ///   检测是否有Sql危险字符
        /// </summary>
        /// <param name="str"> 要判断字符串 </param>
        /// <returns> 判断结果 </returns>
        public static bool IsSafeSqlString(string str)
        {
            return !Regex.IsMatch(str, @"[-|;|,|\/|\(|\)|\[|\]|\}|\{|%|@|\*|!|\']");
        }

        #region 加密解密

        public static string Encrypt(string pToDecrypt, string sKV)
        {
            return Encrypt(pToDecrypt, sKV, sKV);
        }

        //方法 
        //加密方法 
        public static string Encrypt(string pToEncrypt, string sKey, string sIV)
        {
            try
            {
                var des = new DESCryptoServiceProvider();
                //把字符串放到byte数组中 			
                byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

                //建立加密对象的密钥和偏移量
                var b_key = new byte[8];
                var s_keys = new string[8];
                s_keys = sKey.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_key[i] = Convert.ToByte(s_keys[i]);
                }

                des.Key = b_key;

                var b_iv = new byte[8];
                var s_ivs = new string[8];
                s_ivs = sIV.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_iv[i] = Convert.ToByte(s_ivs[i]);
                }

                des.IV = b_iv;


                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                //Write the byte array into the crypto stream 
                //(It will end up in the memory stream) 
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                //Get the data back from the memory stream, and into a string 
                var ret = new StringBuilder();
                foreach (var b in ms.ToArray())
                {
                    //Format as hex 
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
                throw;
                //return "";
            }
        }

        public static string Decrypt(string pToDecrypt, string sKV)
        {
            return Decrypt(pToDecrypt, sKV, sKV);
        }

        //解密方法 
        public static string Decrypt(string pToDecrypt, string sKey, string sIV)
        {
            try
            {
                var des = new DESCryptoServiceProvider();

                //Put the input string into the byte array 
                var inputByteArray = new byte[pToDecrypt.Length/2];
                for (int x = 0; x < pToDecrypt.Length/2; x++)
                {
                    int i = (Convert.ToInt32(pToDecrypt.Substring(x*2, 2), 16));
                    inputByteArray[x] = (byte) i;
                }

                //建立加密对象的密钥和偏移量，此值重要，不能修改 
                var b_key = new byte[8];
                var s_keys = new string[8];
                s_keys = sKey.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_key[i] = Convert.ToByte(s_keys[i]);
                }

                des.Key = b_key;

                var b_iv = new byte[8];
                var s_ivs = new string[8];
                s_ivs = sIV.Split(',');


                for (int i = 0; i <= 7; i++)
                {
                    b_iv[i] = Convert.ToByte(s_ivs[i]);
                }

                des.IV = b_iv;


                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                //Flush the data through the crypto stream into the memory stream 
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                //Get the decrypted data back from the memory stream 
                //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象 
                var ret = new StringBuilder();

                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                throw;
                //string s = exp.Message.ToString();
                //return "";
            }
        }

        #endregion

        #region base64位加密

        /// <summary>
        ///   base64位加密
        /// </summary>
        /// <param name="strInput"> 字符串 </param>
        /// <returns> STRING </returns>
        public static string ToBase64Encrypt(string strInput)
        {
            if (strInput.Trim() == string.Empty)
                return null;
            //加密
            byte[] msg = Encoding.Default.GetBytes(strInput);
            string strSend = Convert.ToBase64String(msg);
            byte[] SMS = Encoding.Default.GetBytes(strSend);
            return Encoding.Default.GetString(SMS); //返回信息
        }

        #endregion

        #region base64位解密

        /// <summary>
        ///   base64位解密
        /// </summary>
        /// <param name="strInput"> 字符串 </param>
        /// <returns> STRING </returns>
        public static string FormBase64Encrypt(string strInput)
        {
            if (strInput.Trim() == string.Empty)
                return null;
            byte[] by = Convert.FromBase64String(strInput);
            return Encoding.Default.GetString(by); //返回信息
        }

        #endregion

        #region RSA 私钥签名-ForPEM

        /// <summary>
        ///   RSA 私钥签名 杨栋添加
        /// </summary>
        /// <param name="str"> 需要加密的原始字符串 </param>
        /// <param name="privatefilepath"> 私钥.PEM 文件路径 </param>
        /// <returns> 加密后16进制字符串 </returns>
        public static string RSAEncodeForPEM(string str, string privatefilepath)
        {
            StreamReader prkey = null;
            BinaryReader binr = null;
            try
            {
                prkey = new StreamReader(privatefilepath);
                string PrivateKey = prkey.ReadToEnd();
                string newPrivateKey = PrivateKey;

                int start = PrivateKey.IndexOf("--\n");
                int end = PrivateKey.IndexOf("\n--");

                if ((start > -1) && (end > -1))
                {
                    newPrivateKey = PrivateKey.Substring(start + 3, end - start - 3);
                }


                byte[] privkey = Convert.FromBase64String(newPrivateKey);

                byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

                // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
                var mem = new MemoryStream(privkey);
                binr = new BinaryReader(mem); //wrap Memory Stream with BinaryReader for easy reading
                byte bt = 0;
                ushort twobytes = 0;
                int elems = 0;


                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return null;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return null;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return null;

                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                if (true)
                {
                    showBytes("\nModulus", MODULUS);
                    showBytes("\nExponent", E);
                    showBytes("\nD", D);
                    showBytes("\nP", P);
                    showBytes("\nQ", Q);
                    showBytes("\nDP", DP);
                    showBytes("\nDQ", DQ);
                    showBytes("\nIQ", IQ);
                }

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var RSA = new RSACryptoServiceProvider();
                var RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);


                byte[] PlainTextBArray;
                byte[] CypherTextBArray;

                //				UnicodeEncoding ByteConverter = new UnicodeEncoding();

                //				PlainTextBArray   =   ByteConverter.GetBytes(str); //
                PlainTextBArray = Encoding.Default.GetBytes(str); //			
                SHA1 mysha = SHA1.Create();

                CypherTextBArray = RSA.SignData(PlainTextBArray, mysha);

                char[] hexDigits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
                //byte转换为16进制字符串 CypherTextBArray
                byte[] bytes = CypherTextBArray;
                var chars = new char[bytes.Length*2];
                for (int i = 0; i < bytes.Length; i++)
                {
                    int b = bytes[i];
                    chars[i*2] = hexDigits[b >> 4];
                    chars[i*2 + 1] = hexDigits[b & 0xF];
                }
                return new string(chars);
            }
            catch
            {
                throw;
            }
            finally
            {
                binr.Close();
                prkey.Close();
            }
        }

        #endregion

        #region PEM 文件 转换成 XML 文件

        /// <summary>
        ///   PEM 文件 转换成 XML 文件 杨栋添加
        /// </summary>
        /// <param name="pemstr"> PEM文件原型,包含开始和结尾的注释 </param>
        /// <param name="xmlfilepath"> 要保存为 XML文件路径 </param>
        /// <returns> </returns>
        public static bool PemToXml(string pemstr, string xmlfilepath)
        {
            BinaryReader binr = null;
            try
            {
                string PrivateKey = pemstr;
                string newPrivateKey = PrivateKey;

                int start = PrivateKey.IndexOf("\n");
                int end = PrivateKey.IndexOf("\n--");

                if ((start == -1) || (end == -1))
                {
                    return false;
                }

                newPrivateKey = PrivateKey.Substring(start + 1, end - start - 1);


                byte[] privkey = Convert.FromBase64String(newPrivateKey);

                byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

                // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
                var mem = new MemoryStream(privkey);
                binr = new BinaryReader(mem); //wrap Memory Stream with BinaryReader for easy reading
                byte bt = 0;
                ushort twobytes = 0;
                int elems = 0;


                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
                    binr.ReadByte(); //advance 1 byte
                else if (twobytes == 0x8230)
                    binr.ReadInt16(); //advance 2 bytes
                else
                    return false;

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102) //version number
                    return false;
                bt = binr.ReadByte();
                if (bt != 0x00)
                    return false;

                //------  all private key components are Integer sequences ----
                elems = GetIntegerSize(binr);
                MODULUS = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                E = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                D = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                P = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                Q = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                DP = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                DQ = binr.ReadBytes(elems);
                elems = GetIntegerSize(binr);
                IQ = binr.ReadBytes(elems);

                if (true)
                {
                    showBytes("\nModulus", MODULUS);
                    showBytes("\nExponent", E);
                    showBytes("\nD", D);
                    showBytes("\nP", P);
                    showBytes("\nQ", Q);
                    showBytes("\nDP", DP);
                    showBytes("\nDQ", DQ);
                    showBytes("\nIQ", IQ);
                }

                // ------- create RSACryptoServiceProvider instance and initialize with public key -----
                var RSA = new RSACryptoServiceProvider();
                var RSAparams = new RSAParameters();
                RSAparams.Modulus = MODULUS;
                RSAparams.Exponent = E;
                RSAparams.D = D;
                RSAparams.P = P;
                RSAparams.Q = Q;
                RSAparams.DP = DP;
                RSAparams.DQ = DQ;
                RSAparams.InverseQ = IQ;
                RSA.ImportParameters(RSAparams);

                string prikey = RSA.ToXmlString(true);
                var fsFile = new FileStream(xmlfilepath, FileMode.Create);
                var fsWrite = new StreamWriter(fsFile);
                fsWrite.WriteLine(prikey);
                fsWrite.Close();
                fsFile.Close();


                return true;
            }
            finally
            {
                binr.Close();
            }
        }

        #region other

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02) //expect integer
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte(); // data size in next byte
            else if (bt == 0x82)
            {
                highbyte = binr.ReadByte(); // data size in next 2 bytes
                lowbyte = binr.ReadByte();
                byte[] modint = {lowbyte, highbyte, 0x00, 0x00};
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt; // we already have the data size
            }

            while (binr.PeekChar() == 0x00)
            {
                //remove high order zeros in data
                binr.ReadByte();
                count -= 1;
            }
            return count;
        }

        private static void showBytes(String info, byte[] data)
        {
            Console.WriteLine("{0}  [{1} bytes]", info, data.Length);
            for (int i = 1; i <= data.Length; i++)
            {
                Console.Write("{0:X2}  ", data[i - 1]);
                if (i%16 == 0)
                    Console.WriteLine();
            }
            Console.WriteLine("\n\n");
        }

        #endregion

        #endregion

        #region RSA 私钥签名

        /// <summary>
        ///   RSA 私钥签名 杨栋添加
        /// </summary>
        /// <param name="str"> 要签名的原始字符串 </param>
        /// <param name="PrivateXmlfilepath"> 私钥xml文件地址 </param>
        /// <returns> 签名后字符串 </returns>
        public static string RSAEncode(string str, string PrivateXmlfilepath)
        {
            if (File.Exists(PrivateXmlfilepath) == false)
            {
                return "";
            }
            StreamReader prkey = null;

            prkey = new StreamReader(PrivateXmlfilepath);
            string PrivateKey = prkey.ReadToEnd();
            prkey.Close();
            string newPrivateKey = PrivateKey;


            var RSA = new RSACryptoServiceProvider();
            RSA.FromXmlString(PrivateKey);

            byte[] PlainTextBArray;
            byte[] CypherTextBArray;

            //				UnicodeEncoding ByteConverter = new UnicodeEncoding();

            //				PlainTextBArray   =   ByteConverter.GetBytes(str); //
            PlainTextBArray = Encoding.Default.GetBytes(str); //			
            SHA1 mysha = SHA1.Create();

            CypherTextBArray = RSA.SignData(PlainTextBArray, mysha);

            char[] hexDigits = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};
            //byte转换为16进制字符串 CypherTextBArray
            byte[] bytes = CypherTextBArray;
            var chars = new char[bytes.Length*2];
            for (int i = 0; i < bytes.Length; i++)
            {
                int b = bytes[i];
                chars[i*2] = hexDigits[b >> 4];
                chars[i*2 + 1] = hexDigits[b & 0xF];
            }

            return new string(chars);
        }

        #endregion

        #region RSA 公钥验证算法

        /// <summary>
        ///   RSA 公钥验证 杨栋添加
        /// </summary>
        /// <param name="RsaEncodeStr"> 私钥加密后字符串 </param>
        /// <param name="RsaPublicfilepath"> RSA公钥PEM文件路径 </param>
        /// <param name="srcstr"> RSA私钥加密前原始字符串 </param>
        /// <returns> 验证结果 true or false </returns>
        public static bool RSACheck(string RsaEncodeStr, string RsaPublicfilepath, string srcstr)
        {
            StreamReader pukey = null;
            try
            {
                pukey = new StreamReader(RsaPublicfilepath);
                string PublicKey = pukey.ReadToEnd();

                int start = PublicKey.IndexOf("--\n");
                int end = PublicKey.IndexOf("\n--");
                string newPublicKey = PublicKey.Substring(start + 3, end - start - 3);


                var rsaP = new RSAParameters();

                byte[] tmpKeyNoB64 = Convert.FromBase64String(newPublicKey);

                int pemModulus = 128;
                int pemPublicExponent = 3;
                var arrPemModulus = new byte[128];
                var arrPemPublicExponent = new byte[3];

                for (int i = 0; i < pemModulus; i++)
                {
                    arrPemModulus[i] = tmpKeyNoB64[29 + i];
                }
                rsaP.Modulus = arrPemModulus;
                for (int i = 0; i < pemPublicExponent; i++)
                {
                    arrPemPublicExponent[i] = tmpKeyNoB64[159 + i];
                }
                rsaP.Exponent = arrPemPublicExponent;


                //				byte[]   PlainTextBArray;     
                //				UnicodeEncoding ByteConverter = new UnicodeEncoding();
                var rsa = new RSACryptoServiceProvider();
                //			rsa.FromXmlString(xmlPublicKey);  
                rsa.ImportParameters(rsaP);
                //				PlainTextBArray   =ByteConverter.GetBytes(RsaEncodeStr);

                string s = RsaEncodeStr;
                var array = new byte[s.Length/2];
                for (int i = 0; i < s.Length/2; i++)
                {
                    string str = s.Substring(2*i, 2);
                    array[i] = (byte) Convert.ToInt32(str, 16);
                }

                //				byte[] bsrc   =ByteConverter.GetBytes(srcstr);

                byte[] bsrc = Encoding.Default.GetBytes(srcstr);

                SHA1 mysha = SHA1.Create();
                bool Bresult = rsa.VerifyData(bsrc, mysha, array);
                return Bresult;
            }

            finally
            {
                pukey.Close();
            }
        }

        #endregion
    }
}