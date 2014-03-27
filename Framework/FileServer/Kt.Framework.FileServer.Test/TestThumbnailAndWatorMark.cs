using System.Drawing;
using System.IO;
using Dev.FileServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Dev.Framework.FileServer.Test
{
    [TestClass]
    public class TestThumbnailAndWatorMark
    {
        [TestMethod]
        public void MyTestMethodThumbnail()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string mark = basepath + "mark.png";
            string imageDir = basepath + "animatedsample.gif";

            string outdir = basepath + "animatedsamplesmall.gif";
            string outdir2 = basepath + "animatedsamplesmall2.gif";
            string outdir3 = basepath + "animatedsamplesmall3.gif";
            string outdir4 = basepath + "animatedsamplesmall4.gif";

            var obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(imageDir), 400, 400);

            Save(obj, outdir);

            obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(imageDir), 300, 300);
            Save(obj, outdir2);

            obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(imageDir), 100, 100);
            Save(obj, outdir3);
        }


        [TestMethod]
        public void MyTestMethod_3200_3200()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string mark = basepath + "mark.png";
            string imageDir = basepath + "aaaa.jpg";

            string outdir = basepath + "animatedsamplesmall1.jpg";
            string outdir2 = basepath + "animatedsamplesmall2.jpg";
            string outdir3 = basepath + "animatedsamplesmall3.jpg";
            string outdir4 = basepath + "animatedsamplesmall4.gif";

            var obj = ImageServer.ImageFile.Watermark(File.OpenRead(imageDir), mark);

            Save(obj, outdir);

            obj = ImageServer.ImageFile.Watermark(File.OpenRead(imageDir), mark);

            Save(obj, outdir2);

            //    obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(imageDir), 300, 300);
            //    Save(obj, outdir2);

            //    obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(imageDir), 100, 100);
            //    Save(obj, outdir3);

        }


        [TestMethod]
        public void TestBatThumbnail()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string imageDir = basepath + "aaaa.jpg";

            var stream = File.OpenRead(imageDir);

            var streamtemp = Dev.Comm.IO.StreamHelper.CopyFrom(stream);
            //streamtemp.Seek(0, SeekOrigin.Begin);
            var a = ImageServer.ImageFile.Thumbnail(streamtemp, 100, 100);
            streamtemp = Dev.Comm.IO.StreamHelper.CopyFrom(stream);
            //streamtemp.Seek(0, SeekOrigin.Begin);
            var b = ImageServer.ImageFile.Thumbnail(streamtemp, 100, 100);

            streamtemp = Dev.Comm.IO.StreamHelper.CopyFrom(stream);
            //streamtemp.Seek(0, SeekOrigin.Begin);
            var c = ImageServer.ImageFile.Thumbnail(streamtemp, 100, 100);
        }


        [TestMethod]
        public void TestUploadResizBat()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string imageDir = basepath + "aaaa.jpg";

            var stream = File.OpenRead(imageDir);
            ImageServer.ImageFile.SaveImageFile(stream, "aaaa.jpg", new[]
                                                                        {
                                                                            new ImagesSize
                                                                                {
                                                                                    Height = 180,
                                                                                    Width = 180
                                                                                },
                                                                            new ImagesSize
                                                                                {
                                                                                    Height = 75,
                                                                                    Width = 75
                                                                                }, new ImagesSize
                                                                                       {
                                                                                           Height = 50,
                                                                                           Width = 50
                                                                                       },
                                                                            new ImagesSize
                                                                                {
                                                                                    Height = 25,
                                                                                    Width = 25
                                                                                },
                                                                        });
        }


        [TestMethod]
        public void MyTestMethodWatorMark()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";
            string mark = basepath + "mark.png";
            string imageDir = basepath + "animatedsample.gif";

            string outdir = basepath + "animatedsamplesmallWatorMar.gif";
            string outdir2 = basepath + "animatedsamplesmall2WatorMar.gif";
            string outdir3 = basepath + "animatedsamplesmall3WatorMar.gif";
            string outdir4 = basepath + "animatedsamplesmall4WatorMar.gif";

            var obj = ImageServer.ImageFile.Watermark(File.OpenRead(imageDir), mark);

            Save(obj, outdir);

            obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(outdir), 300, 300);
            Save(obj, outdir2);

            obj = ImageServer.ImageFile.Thumbnail(File.OpenRead(outdir), 100, 100);
            Save(obj, outdir3);
        }


        [TestMethod]
        public void TestResize()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string imageDir = basepath + "QQ½ØÍ¼20131225160035.png";

            string outdir = basepath + "animatedsamplesmallWatorMar.gif";
            string outdir2 = basepath + "animatedsamplesmall2WatorMar.gif";
            string outdir3 = basepath + "animatedsamplesmall3WatorMar.gif";
            string outdir4 = basepath + "animatedsamplesmall4WatorMar.gif";

            var obj = ImageServer.ImageFile.ResizeImage(File.OpenRead(imageDir), 50, 50);

            Save(obj, outdir);

            obj = ImageServer.ImageFile.ResizeImage(File.OpenRead(imageDir), 300, 300);
            Save(obj, outdir2);

            obj = ImageServer.ImageFile.ResizeImage(File.OpenRead(imageDir), 100, 100);
            Save(obj, outdir3);
        }


        [TestMethod]
        public void TestRotate()
        {
            string basepath = @"C:\Users\Administrator\Desktop\P\";

            string imageDir = basepath + "QQ½ØÍ¼20131225160035.png";

            string outdir = basepath + "animatedsamplesmallWatorMar.gif";
            string outdir2 = basepath + "animatedsamplesmall2WatorMar.gif";
            string outdir3 = basepath + "animatedsamplesmall3WatorMar.gif";
            string outdir4 = basepath + "animatedsamplesmall4WatorMar.gif";

            var obj = ImageServer.ImageFile.RotateImage(File.OpenRead(imageDir), 90);

            Save(obj, outdir);

            obj = ImageServer.ImageFile.RotateImage(File.OpenRead(imageDir), 180);
            Save(obj, outdir2);

            obj = ImageServer.ImageFile.RotateImage(File.OpenRead(imageDir), 360);
            Save(obj, outdir3);
        }

        public void Save(Stream stream, string path)
        {
            var image = Image.FromStream(stream);

            image.Save(path);

        }
    }
}