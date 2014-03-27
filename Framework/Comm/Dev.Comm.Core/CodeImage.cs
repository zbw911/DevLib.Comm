// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/CodeImage.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Dev.Comm.Utils;

namespace Dev.Comm
{
    /*
    /// <summary>
    /// 创建人：杨栋
    /// 2008年12月22日 16:30
    /// </summary>
    public class CodeImage
    {
        #region 获得附加码随机数

        /// <summary>
        /// 获得随机数 纯数字
        /// </summary>
        /// <returns></returns>
        public static string GetCode(int CodeNumber)
        {
            int number;
            char code;
            string checkCode = String.Empty;

            var random = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < CodeNumber; i++)
            {
                number = random.Next();

                code = (char)('0' + (char)(number % 10));

                checkCode += code.ToString();
            }

            return checkCode;
        }

        /// <summary>
        /// 获得随机数 数字＋字母
        /// </summary>
        /// <param name="CodeNumber"></param>
        /// <returns></returns>
        public static string GetCodeNumberLetter(int CodeNumber)
        {
            int number;
            char code;
            string checkCode = String.Empty;

            var random = new Random(unchecked((int)DateTime.Now.Ticks));

            for (int i = 0; i < CodeNumber; i++)
            {
                number = random.Next();

                if (number % 2 == 0)
                    code = (char)('0' + (char)(number % 10));
                else
                    code = (char)('A' + (char)(number % 26));

                checkCode += code.ToString();
            }

            return checkCode;
        }

        /// <summary>
        /// 生成随机字符码
        /// </summary>
        /// <param name="codeLen">字符串长度</param>
        /// <param name="zhCharsCount">中文字符数</param>
        /// <returns></returns>
        public static string CreateVerifyCode(int codeLen, int zhCharsCount)
        {
            var rnd = new Random(unchecked((int)DateTime.Now.Ticks));
            string ChineseChars =
                "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严";
            string EnglishOrNumChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var chs = new char[codeLen];
            int index;
            for (int i = 0; i < zhCharsCount; i++)
            {
                index = rnd.Next(0, codeLen);
                if (chs[index] == '\0')
                    chs[index] = ChineseChars[rnd.Next(0, ChineseChars.Length)];
                else
                    --i;
            }
            for (int i = 0; i < codeLen; i++)
            {
                if (chs[i] == '\0')
                    chs[i] = EnglishOrNumChars[rnd.Next(0, EnglishOrNumChars.Length)];
            }

            return new string(chs, 0, chs.Length);
        }

        #endregion

        #region 生成附加码图片

        public static MemoryStream GetImage(string code)
        {
            return GetImage(code, false);
        }

        /// <summary>
        /// 生成验证码图片
        /// </summary>
        /// <param name="code">字符串</param>
        /// <param name="Chaos">是否带背景干扰</param>
        /// <returns></returns>
        public static MemoryStream GetImage(string code, bool Chaos)
        {
            if (code == "")
            {
                return null;
            }
            int Padding = 2; //边框 默认4像素
            int fSize = 16; //字体大小，默认18像素
            int fWidth = fSize + Padding;
            int imageWidth = (code.Length * fWidth) + Padding * 2;
            int imageHeight = fSize + Padding * 3 + Padding * 2;
            var image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);

            Color[] Colors =
                {
                    Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown,
                    Color.DarkCyan, Color.Purple
                };
            string[] Fonts = { "Arial", "Batang", "Verdana", "Microsoft Sans Serif", "Shruti" };
            //, "Georgia", "Corbel","Corbel", 
            var rnd = new Random(unchecked((int)DateTime.Now.Ticks));
            try
            {
                //Color border = Color.FromArgb(220, 246, 193);
                g.Clear(Color.White); //背景颜色：白色
                //g.Clear(Color.FromArgb(250, 250, 250));//背景颜色：白色
                //给背景添加随机生成的燥点
                //bool Chaos = true;
                if (Chaos)
                {
                    //Pen pen = new Pen(Colors[rnd.Next(Colors.Length - 1)], 0);//噪点颜色：随机色，灰色噪点太简单。
                    var pen = new Pen(Color.LightGray, 0); //噪点颜色：灰色
                    int c = code.Length * 10;
                    for (int i = 0; i < c; i++)
                    {
                        int x = rnd.Next(image.Width);
                        int y = rnd.Next(image.Height);
                        g.DrawRectangle(pen, x, y, 1, 1);
                    }

                    int x1 = rnd.Next(image.Width / 4);
                    int y1 = rnd.Next(image.Height);
                    int x2 = rnd.Next(3 * image.Width / 4, image.Width);
                    int y2 = rnd.Next(image.Height);
                    var pen1 = new Pen(Colors[rnd.Next(Colors.Length - 1)], 1);
                    g.DrawLine(pen1, x1, y1, x2, y2);

                    for (int l = 0; l < 3; l++)
                    {
                        x1 = rnd.Next(image.Width / 4);
                        y1 = rnd.Next(image.Height);
                        x2 = rnd.Next(3 * image.Width / 4, image.Width);
                        y2 = rnd.Next(image.Height);
                        pen1 = new Pen(Color.LightGray, 1);
                        g.DrawLine(pen1, x1, y1, x2, y2);
                    }
                }

                int left = 0, top = 0;
                int n1 = (imageHeight - fSize - Padding * 2);

                Font f;
                Brush b;
                int cindex, findex;

                //随机字体和颜色的验证码字符
                for (int i = 0; i < code.Length; i++)
                {
                    cindex = rnd.Next(Colors.Length - 1);
                    findex = rnd.Next(Fonts.Length - 1);

                    if (code.Substring(i, 1) == "0") //如果是0则限制字体 否则看上去像 o
                    {
                        f = new Font(Fonts[0], fSize, FontStyle.Regular);
                    }
                    else
                    {
                        f = new Font(Fonts[findex], fSize, FontStyle.Bold);
                    }
                    b = new SolidBrush(Colors[cindex]);
                    top = rnd.Next(Convert.ToInt32(n1 * 4 / 5));
                    left = i * fWidth;
                    g.DrawString(code.Substring(i, 1), f, b, left, top);
                }

                //画一个边框 边框颜色为Color.Gainsboro
                //g.DrawRectangle(new Pen(Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
                g.Dispose();

                //产生波形（Add By 51aspx.com）
                //image = TwistImage(image, true, 8, 4);
                image = TwistImage(image, true, rnd.Next(0, 3), rnd.Next(0, 6));
                AddBlackBorder(image, 1);
                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Jpeg);
                return ms;
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #endregion

        #region 产生波形滤镜效果

        /// <summary>
        /// 正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="bXDir">如果扭曲则选择为True</param>
        /// <param name="nMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        public static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            //double PI = 3.1415926535897932384626433832795;
            double PI2 = 6.283185307179586476925286766559;

            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * j) / dBaseAxisLen : (PI2 * i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                        && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }

        #endregion

        #region 添加黑边

        /// <summary>
        /// 去除黑边
        /// </summary>
        /// <param name="Img"></param>
        /// <param name="AddPx">要添加的黑边宽度 像素</param>
        private static void AddBlackBorder(Bitmap Img, int AddPx) //去除黑边
        {
            int ImgWidth = Img.Width;
            int ImgHeight = Img.Height;
            Color border = Color.FromArgb(127, 157, 185);
            //Color border = Color.FromArgb(69, 120, 33);
            //Color border = Color.Black;


            for (int w = 0; w < AddPx; w++)
            {
                for (int h = 0; h < ImgHeight; h++)
                {
                    Img.SetPixel(w, h, border);
                }
            }

            for (int w = ImgWidth - AddPx; w < ImgWidth; w++)
            {
                for (int h = 0; h < ImgHeight; h++)
                {
                    Img.SetPixel(w, h, border);
                }
            }

            for (int h = 0; h < AddPx; h++)
            {
                for (int w = 0; w < ImgWidth; w++)
                {
                    Img.SetPixel(w, h, border);
                }
            }

            for (int h = ImgHeight - AddPx; h < ImgHeight; h++)
            {
                for (int w = 0; w < ImgWidth; w++)
                {
                    Img.SetPixel(w, h, border);
                }
            }
        }

        #endregion
    }
    */

    /// <summary>
    ///   生成验证码， from another project , added by zbw911
    /// </summary>
    public class ValidateCode
    {
        private double _withPerCode = 12.0;
        private double _heightCode = 22.0;

        public ValidateCode()
        {
        }

        public double WithPerCode
        {
            get { return _withPerCode; }
            set { _withPerCode = value; }
        }

        public double HeightCode
        {
            get { return _heightCode; }
            set { _heightCode = value; }
        }

        /// <summary>
        ///   生成验证码
        /// </summary>
        /// <param name="length"> 指定验证码的长度 </param>
        /// <returns> </returns>
        public string CreateValidateCode(int codeLen)
        {
            return CreateValidateCode(codeLen, 0);
        }


        /// <summary>
        ///   生成随机字符码
        /// </summary>
        /// <param name="codeLen"> 字符串总长度 </param>
        /// <param name="zhCharsCount"> 中文字符个数 </param>
        /// <returns> </returns>
        public string CreateValidateCode(int codeLen, int zhCharsCount)
        {
            var rnd = new Random(unchecked((int)DateTime.Now.Ticks));
            string ChineseChars =
                "的一是在不了有和人这中大为上个国我以要他时来用们生到作地于出就分对成会可主发年动同工也能下过子说产种面而方后多定行学法所民得经十三之进着等部度家电力里如水化高自二理起小物现实加量都两体制机当使点从业本去把性好应开它合还因由其些然前外天政四日那社义事平形相全表间样与关各重新线内数正心反你明看原又么利比或但质气第向道命此变条只没结解问意建月公无系军很情者最立代想已通并提直题党程展五果料象员革位入常文总次品式活设及管特件长求老头基资边流路级少图山统接知较将组见计别她手角期根论运农指几九区强放决西被干做必战先回则任取据处队南给色光门即保治北造百规热领七海口东导器压志世金增争济阶油思术极交受联什认六共权收证改清己美再采转更单风切打白教速花带安场身车例真务具万每目至达走积示议声报斗完类八离华名确才科张信马节话米整空元况今集温传土许步群广石记需段研界拉林律叫且究观越织装影算低持音众书布复容儿须际商非验连断深难近矿千周委素技备半办青省列习响约支般史感劳便团往酸历市克何除消构府称太准精值号率族维划选标写存候毛亲快效斯院查江型眼王按格养易置派层片始却专状育厂京识适属圆包火住调满县局照参红细引听该铁价严";
            string EnglishOrNumChars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var chs = new char[codeLen];
            int index;
            for (int i = 0; i < zhCharsCount; i++)
            {
                index = rnd.Next(0, codeLen);
                if (chs[index] == '\0')
                    chs[index] = ChineseChars[rnd.Next(0, ChineseChars.Length)];
                else
                    --i;
            }
            for (int i = 0; i < codeLen; i++)
            {
                if (chs[i] == '\0')
                    chs[i] = EnglishOrNumChars[rnd.Next(0, EnglishOrNumChars.Length)];
            }

            return new string(chs, 0, chs.Length);
        }


        /// <summary>
        ///   创建验证码的图片
        /// </summary>
        /// <param name="validateCode"> 验证码 </param>
        public byte[] CreateValidateGraphic(string validateCode)
        {
            //应考虑到中文的宽度，added by zbw911
            var len = StringUtil.GetGBStrLen(validateCode);
            var image = new Bitmap((int)Math.Ceiling(len * _withPerCode), (int)_heightCode);
            Graphics g = Graphics.FromImage(image);
            try
            {
                //生成随机生成器
                var random = new Random();
                //清空图片背景色
                g.Clear(Color.White);
                //画图片的干扰线
                for (int i = 0; i < 25; i++)
                {
                    int x1 = random.Next(image.Width);
                    int x2 = random.Next(image.Width);
                    int y1 = random.Next(image.Height);
                    int y2 = random.Next(image.Height);
                    g.DrawLine(new Pen(Color.Silver), x1, y1, x2, y2);
                }
                var font = new Font("Arial", (float)_withPerCode, (FontStyle.Bold | FontStyle.Italic));
                var brush = new LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height),
                                                    Color.Blue, Color.DarkRed, 1.2f, true);
                g.DrawString(validateCode, font, brush, 3, 2);
                //画图片的前景干扰点
                for (int i = 0; i < 100; i++)
                {
                    int x = random.Next(image.Width);
                    int y = random.Next(image.Height);
                    image.SetPixel(x, y, Color.FromArgb(random.Next()));
                }
                //画图片的边框线
                g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);

                TwistImage(image, true, random.Next(0, 3), random.Next(0, 6));
                //保存图片数据
                var stream = new MemoryStream();
                image.Save(stream, ImageFormat.Jpeg);
                //输出图片流
                return stream.ToArray();
            }
            finally
            {
                g.Dispose();
                image.Dispose();
            }
        }

        #region 产生波形滤镜效果

        /// <summary>
        ///   正弦曲线Wave扭曲图片（Edit By 51aspx.com）
        /// </summary>
        /// <param name="srcBmp"> 图片路径 </param>
        /// <param name="bXDir"> 如果扭曲则选择为True </param>
        /// <param name="nMultValue"> 波形的幅度倍数，越大扭曲的程度越高，一般为3 </param>
        /// <param name="dPhase"> 波形的起始相位，取值区间[0-2*PI) </param>
        /// <returns> </returns>
        private Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            //double PI = 3.1415926535897932384626433832795;
            double PI2 = 6.283185307179586476925286766559;

            var destBmp = new Bitmap(srcBmp.Width, srcBmp.Height);
            // 将位图背景填充为白色
            Graphics graph = Graphics.FromImage(destBmp);
            graph.FillRectangle(new SolidBrush(Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? destBmp.Height : destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * j) / dBaseAxisLen : (PI2 * i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                        && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }

        #endregion

        /// <summary>
        ///   得到验证码图片的长度
        /// </summary>
        /// <param name="validateNumLength"> 验证码的长度 </param>
        /// <returns> </returns>
        public static int GetImageWidth(int validateNumLength)
        {

            return (int)(validateNumLength * 12.0);
        }

        /// <summary>
        ///   得到验证码的高度
        /// </summary>
        /// <returns> </returns>
        public static double GetImageHeight()
        {
            return 22.5;
        }
    }
}