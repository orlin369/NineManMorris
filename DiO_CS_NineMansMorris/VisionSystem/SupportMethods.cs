using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.CvEnum;

namespace VisionSystem
{
    class SupportMethods
    {
        /// <summary>
        /// Return perspective image crop, by 4 points.
        /// </summary>
        /// <param name="Image">Input image</param>
        /// <param name="OutImageSize">Output image size.</param>
        /// <param name="CropPoints"></param>
        /// <returns></returns>
        public static Image<Bgr, Byte> GetPerspectiveFlatImage(Image<Bgr, Byte> Image, Size OutImageSize, PointF[] CropPoints)
        {
            // Image size
            PointF[] destinationPoints = new PointF[4];
            destinationPoints[0] = new PointF(0, 0);
            destinationPoints[1] = new PointF(OutImageSize.Width, 0);
            destinationPoints[2] = new PointF(OutImageSize.Width, OutImageSize.Height);
            destinationPoints[3] = new PointF(0, OutImageSize.Height);

            // Generate wrap matrix.
            HomographyMatrix myWarpMatrix = CameraCalibration.GetPerspectiveTransform(CropPoints, destinationPoints);

            // Generateimage.
            Image<Bgr, byte> newImage = Image.WarpPerspective(myWarpMatrix, Emgu.CV.CvEnum.INTER.CV_INTER_NN, Emgu.CV.CvEnum.WARP.CV_WARP_FILL_OUTLIERS, new Bgr(20, 20, 20));
            newImage.ROI = new Rectangle(new Point(0, 0), OutImageSize);

            // Return the result image.
            return newImage;
        }

        /// <summary>
        /// Shits.
        /// </summary>
        /// <param name="InputImage"></param>
        /// <param name="OutputImage"></param>
        public static void RepareBoardImage(Bitmap InputImage, out Bitmap OutputImage)
        {
            Image<Gray, Byte> inImg = new Image<Gray, Byte>(InputImage);

            inImg = inImg.ThresholdBinary(new Gray(128), new Gray(255));

            inImg = inImg.Erode(1);

            //inImg = inImg.Dilate(1);

            OutputImage = inImg.ToBitmap();
        }

        #region HSV to Color and back again

        /// <summary>
        /// Convert HSV format to RGBA
        /// </summary>
        /// <param name="Hue">Interval: 0 - 360</param>
        /// <param name="Saturation">Interval: 0 - 1</param>
        /// <param name="Value">Interval: 0 - 1</param>
        /// <returns></returns>
        public static Color ColorFromHSV(double Hue, double Saturation, double Value)
        {
            int hi = Convert.ToInt32(Math.Floor(Hue / 60)) % 6;
            double f = Hue / 60 - Math.Floor(Hue / 60);

            Value = Value * 255;
            int v = Convert.ToInt32(Value);
            int p = Convert.ToInt32(Value * (1 - Saturation));
            int q = Convert.ToInt32(Value * (1 - f * Saturation));
            int t = Convert.ToInt32(Value * (1 - (1 - f) * Saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="color"></param>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        #endregion

        #region Draw cross on the image.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Center"></param>
        /// <returns></returns>
        private static LineSegment2D[] GetCross(Point Center, int Size, bool VorH)
        {
            LineSegment2D[] cross = new LineSegment2D[2];
            Point leftup;
            Point leftdown;
            Point rightup;
            Point rightdown;

            if (VorH)
            {
                leftup = new Point(-Size + Center.X, Center.Y);
                leftdown = new Point(+Size + Center.X, Center.Y);
                rightup = new Point(Center.X, Size + Center.Y);
                rightdown = new Point(Center.X, -Size + Center.Y);
                cross[0] = new LineSegment2D(leftdown, leftup);
                cross[1] = new LineSegment2D(rightdown, rightup);
            }
            else
            {
                leftup = new Point(-Size + Center.X, -Size + Center.Y);
                leftdown = new Point(-Size + Center.X, Size + Center.Y);
                rightup = new Point(Size + Center.X, Size + Center.Y);
                rightdown = new Point(Size + Center.X, -Size + Center.Y);
                cross[0] = new LineSegment2D(leftdown, rightdown);
                cross[1] = new LineSegment2D(leftup, rightup);
            }

            return cross;
        }

        /// <summary>
        /// Draw cross on the screen.
        /// </summary>
        /// <param name="Image"></param>
        /// <param name="Center"></param>
        public static void DrawCross(ref Image<Bgr, Byte> Image, Point Center)
        {
            int thikness = 2;
            int size = 10;

            Bgr crossColor = new Bgr(Color.BurlyWood);
            LineSegment2D[] myCross = SupportMethods.GetCross(Center, size, true);
            Image.Draw(myCross[0], crossColor, thikness);
            Image.Draw(myCross[1], crossColor, thikness);
        }

        #endregion
    }
}
