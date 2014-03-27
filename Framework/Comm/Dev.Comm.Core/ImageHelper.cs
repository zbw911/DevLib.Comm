// ***********************************************************************************
//  Created by zbw911 
//  创建于：2013年06月07日 14:25
//  
//  修改于：2013年09月17日 11:33
//  文件名：Dev.Libs/Dev.Comm.Core/ImageHelper.cs
//  
//  如果有更好的建议或意见请邮件至 zbw911#gmail.com
// ***********************************************************************************

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Dev.Comm
{
    /// <summary>
    ///   图片帮助方法
    /// </summary>
    public class ImageHelper
    {
        public static Size GetImageSize(string pathImageFrom)
        {
            Image imageFrom = null;
            try
            {
                imageFrom = Image.FromFile(pathImageFrom);

                return imageFrom.Size;
            }
            catch
            {
                throw;
            }
            finally
            {
                imageFrom.Dispose();
            }
        }

        public static Size GetImageSize(Stream pathImageFrom)
        {
            Image imageFrom = null;
            try
            {
                pathImageFrom.Seek(0, SeekOrigin.Begin);
                imageFrom = Image.FromStream(pathImageFrom);
                pathImageFrom.Seek(0, SeekOrigin.Begin);
                return imageFrom.Size;
            }
            catch
            {
                throw;
            }
            finally
            {
                imageFrom.Dispose();
            }
        }

        public static Size GetImageSize(byte[] temp_image)
        {
            return GetImageSize(new MemoryStream(temp_image));
        }

        /**/

        /// <summary>
        ///   生成缩略图 静态方法
        /// </summary>
        /// <param name="pathImageFrom"> 源图的路径(含文件名及扩展名) </param>
        /// <param name="pathImageTo"> 生成的缩略图所保存的路径(含文件名及扩展名) 注意：扩展名一定要与生成的缩略图格式相对应 </param>
        /// <param name="width"> 欲生成的缩略图 "画布" 的宽度(像素值) </param>
        /// <param name="height"> 欲生成的缩略图 "画布" 的高度(像素值) </param>
        public static void GenThumbnail(string pathImageFrom, string pathImageTo, int width, int height)
        {
            Image imageFrom = null;
            try
            {
                imageFrom = Image.FromFile(pathImageFrom);
            }
            catch
            {
                throw;
            }
            if (imageFrom == null)
            {
                return;
            }

            // 源图宽度及高度 
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;
            // 生成的缩略图实际宽度及高度 
            int bitmapWidth = width;
            int bitmapHeight = height;
            // 生成的缩略图在上述"画布"上的位置 
            int X = 0;
            int Y = 0;
            // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置 
            if (bitmapHeight*imageFromWidth > bitmapWidth*imageFromHeight)
            {
                bitmapHeight = imageFromHeight*width/imageFromWidth;
                Y = (height - bitmapHeight)/2;
            }
            else
            {
                bitmapWidth = imageFromWidth*height/imageFromHeight;
                X = (width - bitmapWidth)/2;
            }
            // 创建画布 
            var bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            // 用白色清空 
            g.Clear(Color.White);
            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // 指定高质量、低速度呈现。 
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
            g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight),
                        new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);
            try
            {
                //经测试 .jpg 格式缩略图大小与质量等最优 
                bmp.Save(pathImageTo, ImageFormat.Jpeg);
            }
            catch
            {
                throw;
            }
            finally
            {
                //显示释放资源 
                imageFrom.Dispose();
                bmp.Dispose();
                g.Dispose();
            }
        }

        /// <summary>
        ///   生成缩略后的流
        ///   zbw911
        /// </summary>
        /// <param name="ImageStreamFrom"> </param>
        /// <param name="width"> </param>
        /// <param name="height"> </param>
        /// <returns> </returns>
        public static Stream GenThumbnail(Stream ImageStreamFrom, int width, int height)
        {
            ImageStreamFrom.Seek(0, SeekOrigin.Begin);
            //var a = FileHelper.StreamToBytes(ImageStreamTo);
            Image imageFrom = null;
            try
            {
                imageFrom = Image.FromStream(ImageStreamFrom);
            }
            catch
            {
                throw;
            }
            if (imageFrom == null)
            {
                return null;
            }

            // 源图宽度及高度 
            int imageFromWidth = imageFrom.Width;
            int imageFromHeight = imageFrom.Height;
            // 生成的缩略图实际宽度及高度 
            int bitmapWidth = width;
            int bitmapHeight = height;

            if (imageFromWidth < width)
            {
                bitmapWidth = imageFromWidth;
                width = imageFromWidth;
            }
            if (imageFromHeight < height)
            {
                bitmapHeight = imageFromHeight;
                height = imageFromHeight;
            }
            // 生成的缩略图在上述"画布"上的位置 
            int X = 0;
            int Y = 0;
            // 根据源图及欲生成的缩略图尺寸,计算缩略图的实际尺寸及其在"画布"上的位置 
            if (bitmapHeight*imageFromWidth > bitmapWidth*imageFromHeight)
            {
                bitmapHeight = imageFromHeight*width/imageFromWidth;
                Y = (height - bitmapHeight)/2;
            }
            else
            {
                bitmapWidth = imageFromWidth*height/imageFromHeight;
                X = (width - bitmapWidth)/2;
            }
            // 创建画布 
            var bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            // 用白色清空 
            g.Clear(Color.White);
            // 指定高质量的双三次插值法。执行预筛选以确保高质量的收缩。此模式可产生质量最高的转换图像。 
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // 指定高质量、低速度呈现。 
            g.SmoothingMode = SmoothingMode.HighQuality;
            // 在指定位置并且按指定大小绘制指定的 Image 的指定部分。 
            g.DrawImage(imageFrom, new Rectangle(X, Y, bitmapWidth, bitmapHeight),
                        new Rectangle(0, 0, imageFromWidth, imageFromHeight), GraphicsUnit.Pixel);
            try
            {
                Stream ImageStreamTo = new MemoryStream();
                //经测试 .jpg 格式缩略图大小与质量等最优 
                bmp.Save(ImageStreamTo, ImageFormat.Jpeg);

                return ImageStreamTo;
            }
            catch
            {
                throw;
            }
            finally
            {
                //显示释放资源 
                imageFrom.Dispose();
                bmp.Dispose();
                g.Dispose();
            }
        }


        public static MemoryStream GetImage(string code, bool Chaos)
        {
            if (code == "")
            {
                return null;
            }
            int Padding = 2; //边框 默认4像素
            int fSize = 16; //字体大小，默认18像素
            int fWidth = fSize + Padding;
            int imageWidth = (code.Length*fWidth) + Padding*2;
            int imageHeight = fSize + Padding*3 + Padding*2;
            var image = new Bitmap(imageWidth, imageHeight);
            Graphics g = Graphics.FromImage(image);

            Color[] Colors =
                {
                    Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown,
                    Color.DarkCyan, Color.Purple
                };
            string[] Fonts = {"Arial", "Batang", "Verdana", "Microsoft Sans Serif", "Shruti"};
            //, "Georgia", "Corbel","Corbel", 
            var rnd = new Random(unchecked((int) DateTime.Now.Ticks));
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
                    int c = code.Length*10;
                    for (int i = 0; i < c; i++)
                    {
                        int x = rnd.Next(image.Width);
                        int y = rnd.Next(image.Height);
                        g.DrawRectangle(pen, x, y, 1, 1);
                    }

                    int x1 = rnd.Next(image.Width/4);
                    int y1 = rnd.Next(image.Height);
                    int x2 = rnd.Next(3*image.Width/4, image.Width);
                    int y2 = rnd.Next(image.Height);
                    var pen1 = new Pen(Colors[rnd.Next(Colors.Length - 1)], 1);
                    g.DrawLine(pen1, x1, y1, x2, y2);

                    for (int l = 0; l < 3; l++)
                    {
                        x1 = rnd.Next(image.Width/4);
                        y1 = rnd.Next(image.Height);
                        x2 = rnd.Next(3*image.Width/4, image.Width);
                        y2 = rnd.Next(image.Height);
                        pen1 = new Pen(Color.LightGray, 1);
                        g.DrawLine(pen1, x1, y1, x2, y2);
                    }
                }

                int left = 0, top = 0;
                int n1 = (imageHeight - fSize - Padding*2);

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
                    top = rnd.Next(Convert.ToInt32(n1*4/5));
                    left = i*fWidth;
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

        public static Bitmap TwistImage(Bitmap srcBmp, bool bXDir, double dMultValue, double dPhase)
        {
            double PI = 3.1415926535897932384626433832795;
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
                    dx = bXDir ? (PI2*j)/dBaseAxisLen : (PI2*i)/dBaseAxisLen;
                    dx += dPhase;
                    double dy = Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int) (dy*dMultValue) : i;
                    nOldY = bXDir ? j : j + (int) (dy*dMultValue);

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

        #region 添加黑边

        /// <summary>
        ///   去除黑边
        /// </summary>
        /// <param name="Img"> </param>
        /// <param name="AddPx"> 要添加的黑边宽度 像素 </param>
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
}