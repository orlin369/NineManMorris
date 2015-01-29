using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// For size.
using System.Drawing;
using Emgu.CV.Structure;
using Emgu.CV;

namespace VisionSystem
{
    /// <summary>
    /// Board figure positions acording "Nine man's Morris" positions.
    /// </summary>
    public class Board : IDisposable
    {
        #region Variables

        /// <summary>
        /// Blobs
        /// </summary>
        private Emgu.CV.Cvb.CvBlobs ImageBlobs;
        /// <summary>
        /// Blob detector
        /// </summary>
        private Emgu.CV.Cvb.CvBlobDetector BlobDetector;

        /// <summary>
        /// Output board image size.
        /// </summary>
        private Size BoardSize;

        /// <summary>
        /// Board width step parametesrs.
        /// </summary>
        private int dW;
        /// <summary>
        /// Board height step parametesrs.
        /// </summary>
        private int dH;

        /// <summary>
        /// Halh of square size of ROI image.
        /// </summary>
        private int BoxScaling;

        /// <summary>
        /// Output image size.
        /// </summary>
        private Size OutputImageSize;

        /// <summary>
        /// Font for image text writing.
        /// </summary>
        private MCvFont TextFont = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.5, 1.5);

        /// <summary>
        /// Markers count
        /// </summary>
        private const int MARKERS_COUNT = 4;
        /// <summary>
        /// Array of markers points.
        /// </summary>
        private PointF[] MarkersList = new PointF[MARKERS_COUNT];

        /// <summary>
        /// Saturation tresh.
        /// </summary>
        private double SaturationTreshFigureLevel = 0.25d;

        //Search configuraton
        SearchConfigurator SC;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Configuration">Search configuration parameters</param>
        public Board(SearchConfigurator Configuration)
        {
            this.ImageBlobs = new Emgu.CV.Cvb.CvBlobs();
            this.BlobDetector = new Emgu.CV.Cvb.CvBlobDetector();
            this.BoardSize = new Size(400, 400);
            this.dW = (int)(this.BoardSize.Width / 8);
            this.dH = (int)(this.BoardSize.Height / 8);
            this.BoxScaling = 15;
            this.OutputImageSize = new Size(640, 480);
            this.SC = Configuration;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Board()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call from destrucotr.
        /// </summary>
        public void Dispose()
        {
            this.BlobDetector = null;
            this.ImageBlobs = null;
        }

        #endregion

        #region Private

        /// <summary>
        /// Find the board markers.
        /// </summary>
        /// <param name="InputImage"></param>
        /// <returns></returns>
        private PointF[] GetBoardMarkers(Bitmap InputImage, SearchConfigurator Configuration)
        {
            #region Local variables

            // Main image.
            Image<Gray, Byte> ModelImage;

            // Center of image
            PointF centerOfImage = new PointF((InputImage.Width / 2), (InputImage.Height / 2));

            // For processing
            ModelImage = new Image<Gray, Byte>(InputImage);

            // Regionpoints
            PointF[] listOfEdges = new PointF[MARKERS_COUNT];

            #endregion

            // Smooth the image, with kernel size 3.
            ModelImage = ModelImage.SmoothGaussian(Configuration.SmoothKernelSize);

            // Show unperspective image
            //CvInvoke.cvShowImage("Stage 1", ModelImage);

            // Adaptive tresh
            ModelImage = ModelImage.ThresholdAdaptive(new Gray(Configuration.TreshholdLevel),
                Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_GAUSSIAN_C,
                Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY_INV,
                7,
                new Gray(4));


            // Dilate one iteration the image.
            ModelImage = ModelImage.Dilate(1);

            // Show unperspective image
            //CvInvoke.cvShowImage("Stage 2", ModelImage);

            // Get the numers of blobs on the image.
            uint numBlobsFound = BlobDetector.Detect(ModelImage, ImageBlobs);

            // Lets try and iterate the blobs
            foreach (Emgu.CV.Cvb.CvBlob targetBlob in ImageBlobs.Values)
            {
                // Calculate circularity
                MemStorage BlbobStorage = new MemStorage();
                Contour<Point> blobContour = targetBlob.GetContour(BlbobStorage);
                double blobCircularity = this.GetCircularity(blobContour);

                // Calculate area
                double blobArea = targetBlob.Area;
                // Center image
                PointF blobCenter = targetBlob.Centroid;

                // Filter the marker by area
                if ((blobArea >= Configuration.MinimumMarkerArea) && (blobArea <= Configuration.MaximumMarkerArea) && (blobCircularity > Configuration.MarkerCircularity))
                {

                    Console.WriteLine("A: {0} P: {1} C: {2}", blobArea, 1, blobCircularity);
                    #region Find msrker quadrant position

                    if ((blobCenter.X < centerOfImage.X) && (blobCenter.Y < centerOfImage.Y))
                    {
                        Console.WriteLine("Quadrant: I");
                        listOfEdges[0] = targetBlob.Centroid;
                    }

                    if ((blobCenter.X > centerOfImage.X) && (blobCenter.Y < centerOfImage.Y))
                    {
                        Console.WriteLine("Quadrant: II");
                        listOfEdges[1] = targetBlob.Centroid;
                    }

                    if ((blobCenter.X > centerOfImage.X) && (blobCenter.Y > centerOfImage.Y))
                    {
                        Console.WriteLine("Quadrant: III");
                        listOfEdges[2] = targetBlob.Centroid;
                    }

                    if ((blobCenter.X < centerOfImage.X) && (blobCenter.Y > centerOfImage.Y))
                    {
                        Console.WriteLine("Quadrant: IV");
                        listOfEdges[3] = targetBlob.Centroid;
                    }

                    #endregion
                }
            }

            // If there is 4 markers return them
            if (listOfEdges.Length == 4)
            {
                return listOfEdges;
            }

            // If not null
            return null;
        }

        /// <summary>
        /// Calculate circularity of the blob region.
        /// </summary>
        /// <param name="Object">Contour of the blob.</param>
        /// <returns>Circularity</returns>
        private double GetCircularity(Contour<Point> Object)
        {
            double c = 0.0d;

            if (Object != null)
            {
                // c = (2 * Math.PI * r) ^ 2 / (4 * Math.PI * Math.PI * r * r);
                c = ((4 * Math.PI * Object.Area) / Math.Pow(Object.Perimeter, 2));
            }

            return c;
        }

        /// <summary>
        /// Calculate center of the figure index.
        /// </summary>
        /// <param name="Index">Index of the figure.</param>
        /// <returns>Return the center of the figure.</returns>
        private Point FigurePosition(int Index)
        {
            switch (Index)
            {
                case 0:
                    return new Point(dW, dH);
                case 1:
                    return new Point(dW * 4, dH);
                case 2:

                    return new Point(dW * 7, dH);
                case 3:
                    return new Point(dW * 2, dH * 2);
                case 4:
                    return new Point(dW * 4, dH * 2);
                case 5:
                    return new Point(dW * 6, dH * 2);

                case 6:
                    return new Point(dW * 3, dH * 3);
                case 7:
                    return new Point(dW * 4, dH * 3);
                case 8:
                    return new Point(dW * 5, dH * 3);

                case 9:
                    return new Point(dW * 1, dH * 4);
                case 10:
                    return new Point(dW * 2, dH * 4);
                case 11:
                    return new Point(dW * 3, dH * 4);
                case 12:
                    return new Point(dW * 5, dH * 4);
                case 13:
                    return new Point(dW * 6, dH * 4);
                case 14:
                    return new Point(dW * 7, dH * 4);

                case 15:
                    return new Point(dW * 3, dH * 5);
                case 16:
                    return new Point(dW * 4, dH * 5);
                case 17:
                    return new Point(dW * 5, dH * 5);

                case 18:
                    return new Point(dW * 2, dH * 6);
                case 19:
                    return new Point(dW * 4, dH * 6);
                case 20:
                    return new Point(dW * 6, dH * 6);

                case 21:
                    return new Point(dW, dH * 7);
                case 22:
                    return new Point(dW * 4, dH * 7);
                case 23:
                    return new Point(dW * 7, dH * 7);

                default:
                    return new Point();
            }
        }

        /// <summary>
        /// Make region around center.
        /// </summary>
        /// <param name="Center">Center of region.</param>
        /// <returns>Rectangle box around center.</returns>
        private Rectangle GetBoundingBox(Point Center)
        {
            Point boxLocation = new Point((Center.X - BoxScaling), (Center.Y - BoxScaling));
            return new Rectangle(boxLocation, new Size((BoxScaling * 2), (BoxScaling * 2)));
        }

        /// <summary>
        /// Calculate the color of the figure.
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        private Color GetFigureColor(Image<Bgr, Byte> Image, Rectangle Box)
        {
            // Set ROI
            Image.ROI = Box;
            // Figure BGR color
            Bgr bgrFigureColor;
            // Scalar
            MCvScalar myScalar;
            // Get midrange of the color
            Image.AvgSdv(out bgrFigureColor, out myScalar);
            // Clear ROI
            Image.ROI = new Rectangle();
            // Return color
            return Color.FromArgb(255, (byte)bgrFigureColor.Red, (byte)bgrFigureColor.Green, (byte)bgrFigureColor.Blue);
        }

        /// <summary>
        /// Return perspective image crop, by 4 points.
        /// </summary>
        /// <param name="Image">Input image</param>
        /// <param name="OutImageSize">Output image size.</param>
        /// <param name="CropPoints"></param>
        /// <returns></returns>
        private Image<Bgr, Byte> GetPerspectiveFlatImage(Image<Bgr, Byte> Image, Size OutImageSize, PointF[] CropPoints)
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
        /// Equalize the image.
        /// </summary>
        /// <param name="Image">Image</param>
        /// <returns>Image</returns>
        private Image<Bgr, Byte> Equalize(Image<Bgr, Byte> Image, double Lightnes)
        {
            // Define minimum and maximum.
            double max = 0;
            double min = 255;

            // Convert the image to HSL
            Image<Hls, Byte> myImage = Image.Convert<Hls, Byte>();

            // Get minimum and maximum.
            for (int x = 0; x < myImage.Cols; x++)
            {
                for (int y = 0; y < myImage.Rows; y++)
                {
                    if (myImage[y, x].Lightness < min)
                    {
                        min = myImage[y, x].Lightness;
                    }

                    if (myImage[y, x].Lightness > max)
                    {
                        max = myImage[y, x].Lightness;
                    }
                }
            }

            // Calculate scaling.
            double usability = (255 - max) + min;
            double unussed = 255 / usability;

            // Aply the difference to the ima.
            for (int x = 0; x < myImage.Cols; x++)
            {
                for (int y = 0; y < myImage.Rows; y++)
                {
                    myImage[y, x] = new Hls(myImage[y, x].Hue, (myImage[y, x].Lightness - min) * unussed * Lightnes, myImage[y, x].Satuation);
                }
            }

            //CvInvoke.cvShowImage("Stage 3", myImage[1]);

            // Return the image.
            return myImage.Convert<Bgr, Byte>();
        }

        #region HSV to Color and back again

        /// <summary>
        /// Convert HSV format to RGBA
        /// </summary>
        /// <param name="Hue">Interval: 0 - 360</param>
        /// <param name="Saturation">Interval: 0 - 1</param>
        /// <param name="Value">Interval: 0 - 1</param>
        /// <returns></returns>
        private Color ColorFromHSV(double Hue, double Saturation, double Value)
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
        private void ColorToHSV(Color color, out double hue, out double saturation, out double value)
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
        private LineSegment2D[] GetCross(Point Center, int Size, bool VorH)
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
        private void DrawCross(ref Image<Bgr, Byte> Image, Point Center)
        {
            int thikness = 2;
            int size = 10;

            Bgr crossColor = new Bgr(Color.BurlyWood);
            LineSegment2D[] myCross = this.GetCross(Center, size, true);
            Image.Draw(myCross[0], crossColor, thikness);
            Image.Draw(myCross[1], crossColor, thikness);
        }

        #endregion

        #endregion

        #region Public

        /// <summary>
        /// Find the chessboard corners
        /// </summary>
        /// <param name="InputImage">Input image commpping from video camera.</param>
        /// <param name="OutputImage">Output image comming from image processor.</param>
        public BoardConfiguration FindBoard(Bitmap InputImage, out Bitmap OutputImage)
        {
            #region Local variables

            // Figure Center
            Point figureCenter;
            // Figure BGR color
            Color figureColor;
            // Figure index
            int figureIndex;
            // Vector image of figure
            CircleF circleFigure;
            // Board configuration
            BoardConfiguration currentConfiguration = new BoardConfiguration();
            // Image !
            Image<Bgr, Byte> WorkingImage;

            #endregion

            // Create working image.
            WorkingImage = new Image<Bgr, Byte>(InputImage);

            // Equlize the image.
            WorkingImage = this.Equalize(WorkingImage, this.SC.ImageLightnes);

            // Convert the image back to BGR range
            Emgu.CV.Image<Bgr, Byte> DrawingImage = WorkingImage;

            // Create the font.
            MCvFont TextFont = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);
            
            try
            {
                // Get bord marker points,
                MarkersList = this.GetBoardMarkers(new Image<Hls, Byte>(InputImage)[1].Convert<Gray, Byte>().ToBitmap(), this.SC);

                // If repers are 4 the proseed to describe the board.
                if (MarkersList.Length == MARKERS_COUNT)
                {
                    // Draw the points
                    foreach (PointF markerPoint in MarkersList)
                    {
                        // Create figure to draw.
                        circleFigure = new CircleF(markerPoint, 5.0f);
                        // Draw circle
                        DrawingImage.Draw(circleFigure, new Bgr(Color.Orange), 10);
                    }

                    // Show the image
                    //CvInvoke.cvShowImage("Test", DrawingImage);

                    #region Get perspective flat image.

                    // Get perspective flat image.
                    Image<Bgr, Byte> flatImage = this.GetPerspectiveFlatImage(WorkingImage, BoardSize, MarkersList);

                    #endregion

                    for (int indexFigure = 0; indexFigure < 24; indexFigure++)
                    {
                        // Set the figure center
                        figureCenter = this.FigurePosition(indexFigure);
                        //
                        figureColor = this.GetFigureColor(flatImage, this.GetBoundingBox(figureCenter));

                        // Tell that there is no figure
                        figureIndex = 0;

                        // If the luminosity is greater then 0.5.
                        if (figureColor.GetSaturation() > this.SaturationTreshFigureLevel)
                        {
                            // Find the playr
                            figureIndex = FigureColorDefinitions.WhoIsThePlayer(figureColor);
                        }

                        // Fill the board configuration
                        currentConfiguration.Figure[indexFigure] = figureIndex;

                        #region Draw

                        if (figureIndex != 0)
                        {
                            // Create figure to draw.
                            //circleFigure = new CircleF(figureCenter, 10.0f);
                            // Draw circle
                            //newImage.Draw(circleFigure, bgrInFigureColor, 5);
                            // Write text
                            //
                            #region Draw label to the processed image.

                            flatImage.Draw(String.Format("{0}", FigureColorDefinitions.WhoIsThePlayer(figureColor)), ref TextFont, figureCenter, new Bgr(Color.DarkOrange));
                            //drawingImage.Draw(vectorWithSmallestY, RedColor, 2);
                            #endregion
                        }

                        #endregion
                    }

                    // Show unperspective image
                    //CvInvoke.cvShowImage("Perspective", flatImage);
                    DrawingImage = flatImage;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: {0}", exception.Message);
                Loging.Log.CreateRecord("VisionSystm.Board.FindBoard", String.Format("Exception: {0}", exception.Message), Loging.LogMessageTypes.Error);

            }

            // Apply the output image.
            DrawingImage.ROI = new Rectangle(new Point(0, 0), new Size(400, 400));
            OutputImage = DrawingImage.ToBitmap();

            return currentConfiguration;
        }

        #endregion
    }
}
