using System;
using System.Diagnostics;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace Commons
{
    public class uc_Authcode
    {
        public enum DiscuzAuthcodeMode { Encode, Decode };


        /// <summary>
        /// 从字符串的指定位置截取指定长度的子字符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <param name="length">子字符串的长度</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex, int length)
        {
            if (startIndex >= 0)
            {
                if (length < 0)
                {
                    length = length * -1;
                    if (startIndex - length < 0)
                    {
                        length = startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        startIndex = startIndex - length;
                    }
                }


                if (startIndex > str.Length)
                {
                    return "";
                }


            }
            else
            {
                if (length < 0)
                {
                    return "";
                }
                else
                {
                    if (length + startIndex > 0)
                    {
                        length = length + startIndex;
                        startIndex = 0;
                    }
                    else
                    {
                        return "";
                    }
                }
            }

            if (str.Length - startIndex < length)
            {
                length = str.Length - startIndex;
            }

            return str.Substring(startIndex, length);
        }

        /// <summary>
        /// 从字符串的指定位置开始截取到字符串结尾的了符串
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <param name="startIndex">子字符串的起始位置</param>
        /// <returns>子字符串</returns>
        public static string CutString(string str, int startIndex)
        {
            return CutString(str, startIndex, str.Length);
        }


        /// <summary>
        /// 返回文件是否存在
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>是否存在</returns>
        public static bool FileExists(string filename)
        {
            return System.IO.File.Exists(filename);
        }

        /// <summary>
        /// MD5函数
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }

        /// <summary>
        /// 对字符串进行 base64 编码
        /// </summary>
        /// <param name="str">原始字串</param>
        /// <returns>结果</returns>
        public static string Base64Encode(string str)
        {
            try
            {
                byte[] bytes_1 = System.Text.Encoding.Default.GetBytes(str);
                return System.Convert.ToBase64String(bytes_1);
            }
            catch
            {
                return "";
            }

        }

        public static string Base64Decode(string str)
        {
            try
            {
                byte[] bytes_2 = Convert.FromBase64String(str);
                return System.Text.Encoding.Default.GetString(bytes_2);
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 字段串是否为Null或为""(空)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool StrIsNullOrEmpty(string str)
        {
            //#if NET1
            if (str == null || str.Trim() == "")
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 用于 RC4 处理密码
        /// </summary>
        /// <param name="pass">密码字串</param>
        /// <param name="kLen">密钥长度，一般为 256</param>
        /// <returns></returns>
        static private Byte[] GetKey(Byte[] pass, Int32 kLen)
        {
            Byte[] mBox = new Byte[kLen];

            for (Int64 i = 0; i < kLen; i++)
            {
                mBox[i] = (Byte)i;
            }
            Int64 j = 0;
            for (Int64 i = 0; i < kLen; i++)
            {
                j = (j + mBox[i] + pass[i % pass.Length]) % kLen;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
            }
            return mBox;
        }

        /// <summary>
        /// 生成随机字符
        /// </summary>
        /// <param name="lens">随机字符长度</param>
        /// <returns>随机字符</returns>
        public static string RandomString(int lens)
        {
            char[] CharArray = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int clens = CharArray.Length;
            string sCode = "";
            Random random = new Random();
            for (int i = 0; i < lens; i++)
            {
                sCode += CharArray[random.Next(clens)];
            }
            return sCode;
        }

        /// <summary>
        /// 使用 Discuz authcode 方法对字符串加密
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="expiry">加密字串有效时间，单位是秒</param>
        /// <returns>加密结果</returns>
        public static string DiscuzAuthcodeEncode(string source, string key, int expiry)
        {
            return DiscuzAuthcode(source, key, DiscuzAuthcodeMode.Encode, expiry);

        }

        /// <summary>
        /// 使用 Discuz authcode 方法对字符串加密
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>加密结果</returns>
        public static string DiscuzAuthcodeEncode(string source, string key)
        {
            return DiscuzAuthcode(source, key, DiscuzAuthcodeMode.Encode, 0);

        }

        /// <summary>
        /// 使用 Discuz authcode 方法对字符串解密
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="key">密钥</param>
        /// <returns>解密结果</returns>
        public static string DiscuzAuthcodeDecode(string source, string key)
        {
            return DiscuzAuthcode(source, key, DiscuzAuthcodeMode.Decode, 0);

        }

        /// <summary>
        /// 使用 变形的 rc4 编码方法对字符串进行加密或者解密
        /// </summary>
        /// <param name="source">原始字符串</param>
        /// <param name="key">密钥</param>
        /// <param name="operation">操作 加密还是解密</param>
        /// <param name="expiry">加密字串过期时间</param>
        /// <returns>加密或者解密后的字符串</returns>
        private static string DiscuzAuthcode(string source, string key, DiscuzAuthcodeMode operation, int expiry)
        {

            if (source == null || key == null)
            {
                return "";
            }

            int ckey_length = 4;
            string keya, keyb, keyc, cryptkey, result;
            string timestamp = UnixTimestamp();

            key = MD5(key);
            keya = MD5(CutString(key, 0, 16));
            keyb = MD5(CutString(key, 16, 16));
            keyc = ckey_length > 0 ? (operation == DiscuzAuthcodeMode.Decode ? CutString(source, 0, ckey_length) : RandomString(ckey_length)) : "";

            cryptkey = keya + MD5(keya + keyc);

            if (operation == DiscuzAuthcodeMode.Decode)
            {
                byte[] temp;
                try
                {
                    temp = System.Convert.FromBase64String(CutString(source, ckey_length));
                }
                catch
                {
                    try
                    {
                        temp = System.Convert.FromBase64String(CutString(source + "=", ckey_length));
                    }
                    catch
                    {
                        try
                        {
                            temp = System.Convert.FromBase64String(CutString(source + "==", ckey_length));
                        }
                        catch
                        {
                            return "";
                        }
                    }
                }

                result = System.Text.Encoding.Default.GetString(RC4(temp, cryptkey));
                if (CutString(result, 10, 16) == CutString(MD5(CutString(result, 26) + keyb), 0, 16))
                {
                    return CutString(result, 26);
                }
                else
                {
                    return "";
                }
            }
            else
            {
                source = "0000000000" + CutString(MD5(source + keyb), 0, 16) + source;
                byte[] temp = RC4(System.Text.Encoding.Default.GetBytes(source), cryptkey);
                return keyc + System.Convert.ToBase64String(temp);

            }

        }

        /// <summary>
        /// RC4 原始算法
        /// </summary>
        /// <param name="input">原始字串数组</param>
        /// <param name="pass">密钥</param>
        /// <returns>处理后的字串数组</returns>
        private static Byte[] RC4(Byte[] input, String pass)
        {
            if (input == null || pass == null) return null;

            Encoding enc_default = System.Text.Encoding.Default;

            byte[] output = new Byte[input.Length];
            byte[] mBox = GetKey(enc_default.GetBytes(pass), 256);

            // 加密
            Int64 i = 0;
            Int64 j = 0;
            for (Int64 offset = 0; offset < input.Length; offset++)
            {
                i = (i + 1) % mBox.Length;
                j = (j + mBox[i]) % mBox.Length;
                Byte temp = mBox[i];
                mBox[i] = mBox[j];
                mBox[j] = temp;
                Byte a = input[offset];
                //Byte b = mBox[(mBox[i] + mBox[j] % mBox.Length) % mBox.Length];
                // mBox[j] 一定比 mBox.Length 小，不需要在取模
                Byte b = mBox[(mBox[i] + mBox[j]) % mBox.Length];
                output[offset] = (Byte)((Int32)a ^ (Int32)b);
            }

            return output;
        }


        public static string AscArr2Str(byte[] b)
        {
            return System.Text.UnicodeEncoding.Unicode.GetString(
             System.Text.ASCIIEncoding.Convert(System.Text.Encoding.ASCII,
             System.Text.Encoding.Unicode, b)
             );
        }

        public static string UnixTimestamp()
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            DateTime dtNow = DateTime.Parse(DateTime.Now.ToString());
            TimeSpan toNow = dtNow.Subtract(dtStart);
            string timeStamp = toNow.Ticks.ToString();
            return timeStamp.Substring(0, timeStamp.Length - 7);
        }
    }
}
