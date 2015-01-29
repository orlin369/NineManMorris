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
    public class Board
    {
        #region Variables

        // Blobs ...
        private static Emgu.CV.Cvb.CvBlobs ImageBlobs = new Emgu.CV.Cvb.CvBlobs();
        // Blob detector ...
        private static Emgu.CV.Cvb.CvBlobDetector BlobDetector = new Emgu.CV.Cvb.CvBlobDetector();

        // Board parametters.
        private static Size BoardSize = new Size(400, 400);

        // Board step parametesrs.
        private static int dW = (int)(BoardSize.Width / 8);
        private static int dH = (int)(BoardSize.Height / 8);

        // This is half of square side.
        private static int BoxScaling = 15;

        // Size
        private static int OutputImageWidth = 640; //348 // 640 - Old
        private static int OutputImageHeight = 480; // 480 // 480 - Old

        // Create the font
        private static MCvFont TextFont = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.5, 1.5);

        // List of repers
        private static PointF[] MarkersList = new PointF[4];
        private static int MarkersCount = 4;

        // Color filter by HUE chanel.
        // Reject a yellow green spectrumm.
        private static int HueChanelMax = 41;
        private static int HueChanelMin = 20;

        #endregion

        /// <summary>
        /// Find the chessboard corners
        /// </summary>
        /// <param name="InputImage">Input image commpping from video camera.</param>
        /// <param name="OutputImage">Output image comming from image processor.</param>
        public static BoardConfiguration FindBoard(Bitmap InputImage, out Bitmap OutputImage)
        {
            #region Local variables

            // Figure Center
            Point figureCenter;
            // Figure BGR color
            Bgr bgrInFigureColor;
            // Vector image of figure
            CircleF circleFigure;
            //
            Color colorTransformedColor;
            //
            double hueChanel;
            double saturationChanel;
            double valueChanel;
            //
            Color circleColor;

            // Board configuration
            BoardConfiguration currentConfiguration = new BoardConfiguration();
            
            #endregion

            #region Equalize the image

            // Convert a BGR image to HLS range
            Emgu.CV.Image<Hls, Byte> imageHsi = new Image<Hls, Byte>(InputImage);

            // Equalize the Intensity Channel
            imageHsi[1]._EqualizeHist();
            imageHsi[2]._EqualizeHist();

            // Convert the image back to BGR range
            Emgu.CV.Image<Bgr, Byte> DrawingImage = imageHsi.Convert<Bgr, Byte>();

            // Geometric orientation image
            Emgu.CV.Image<Gray, Byte> BlobImage = imageHsi[1].Convert<Gray, Byte>();

            // Create the font.
            MCvFont TextFont = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.5, 1.5);

            #endregion

            try
            {
                // Get bord marker points,
                MarkersList = Board.GetBoardMarkers(BlobImage.ToBitmap());

                // If repers are 4 the proseed to describe the board.
                if (MarkersList.Length == MarkersCount)
                {
                    // Set playboard size.
                    Board.SetSize(BoardSize);

                    #region Get perspective flat image.

                    // Get perspective flat image.
                    Image<Bgr, byte> newImage = SupportMethods.GetPerspectiveFlatImage(DrawingImage, BoardSize, MarkersList);

                    #endregion

                    for (int indexFigure = 0; indexFigure < 24; indexFigure++)
                    {
                        // Set the figure center
                        figureCenter = Board.FigurePosition(indexFigure);
                        // 
                        bgrInFigureColor = Board.GetFigureColor(newImage, Board.GetBoundingBox(figureCenter));
                        //
                        colorTransformedColor = Color.FromArgb(255, (byte)bgrInFigureColor.Red, (byte)bgrInFigureColor.Green, (byte)bgrInFigureColor.Blue);
                        //
                        SupportMethods.ColorToHSV(colorTransformedColor, out hueChanel, out saturationChanel, out valueChanel);
                        // Preset the color configuration
                        valueChanel = 1.0;
                        saturationChanel = 1.0;
                        // Find the playr
                        int figure = FigureColorDefinitions.WhoIsThePlayer((int)hueChanel);
                        // Fill the board configuration
                        currentConfiguration.Figure[indexFigure] = figure;

                        #region Draw

                        if (!((hueChanel > HueChanelMin) && (hueChanel < HueChanelMax)) && (figure != -1))
                        {
                            circleColor = SupportMethods.ColorFromHSV(hueChanel, saturationChanel, valueChanel);
                            // Create figure to draw.
                            circleFigure = new CircleF(figureCenter, 10.0f);
                            // Draw circle
                            newImage.Draw(circleFigure, new Bgr(circleColor), 5);
                            // Write text
                            //
                            #region Draw label to the processed image.

                            newImage.Draw(String.Format("{0}", FigureColorDefinitions.WhoIsThePlayer((int)hueChanel)), ref TextFont, figureCenter, new Bgr(0, 0, 0));
                            //drawingImage.Draw(vectorWithSmallestY, RedColor, 2);
                            #endregion
                        }

                        #endregion
                    }

                    // Show unperspective image
                    //CvInvoke.cvShowImage("Perspective", newImage);
                    DrawingImage = newImage;
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine("Exception: {0}", exception.Message);
            }

            // Apply the output image.
            OutputImage = new Bitmap(DrawingImage.Resize(OutputImageWidth, OutputImageHeight, Emgu.CV.CvEnum.INTER.CV_INTER_LINEAR).ToBitmap());

            return currentConfiguration;
        }

        /// <summary>
        /// Set the board size.
        /// </summary>
        /// <param name="BoardSize">Board size.</param>
        public static void SetSize(Size BoardSize)
        {
            Board.BoardSize = BoardSize;
        }

        /// <summary>
        /// Find the board markers.
        /// </summary>
        /// <param name="InputImage"></param>
        /// <returns></returns>
        private static PointF[] GetBoardMarkers(Bitmap InputImage)
        {
            #region Local variables

            // Main image.
            Image<Gray, Byte> ModelImage;

            // Blob area rejectionlevels.
            int maximumBlobArea = 100;
            int minimumBlobArea = 360;

            // Treshold level
            int treshLevel = 220;

            // Blob circularity
            double circularityLevel = 0.20d;

            // Center of image
            PointF centerOfImage = new PointF((InputImage.Width / 2), (InputImage.Height / 2));

            // For processing
            ModelImage = new Image<Gray, Byte>(InputImage);

            // Regionpoints
            PointF[] listOfEdges = new PointF[4];

            #endregion

            // Adaptive tresh
            ModelImage = ModelImage.ThresholdAdaptive(new Gray(treshLevel),
                Emgu.CV.CvEnum.ADAPTIVE_THRESHOLD_TYPE.CV_ADAPTIVE_THRESH_GAUSSIAN_C,
                Emgu.CV.CvEnum.THRESH.CV_THRESH_BINARY_INV,
                7,
                new Gray(4));

            // Dilate one iteration the image.
            ModelImage = ModelImage.Dilate(1);

            // Show unperspective image
            // CvInvoke.cvShowImage("Stage", ModelImage);

            // Get the numers of blobs on the image.
            uint numBlobsFound = BlobDetector.Detect(ModelImage, ImageBlobs);

            // Lets try and iterate the blobs
            foreach (Emgu.CV.Cvb.CvBlob targetBlob in ImageBlobs.Values)
            {
                // Calculate circularity
                MemStorage BlbobStorage = new MemStorage();
                Contour<Point> blobContour = targetBlob.GetContour(BlbobStorage);
                double blobCircularity = Board.GetCircularity(blobContour);

                // Calculate area
                double blobArea = targetBlob.Area;
                //Console.WriteLine("Area: {0}", blobArea);

                // Center image
                PointF blobCenter = targetBlob.Centroid;

                // Filter the marker by area
                if ((blobArea >= maximumBlobArea) && (blobArea <= minimumBlobArea))
                {

                    Console.WriteLine("Area: {0}", blobArea);
                    Console.WriteLine("Circularity: {0}", Board.GetCircularity(blobContour));

                    if (blobCircularity > circularityLevel)
                    {
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
        /// Make region around center.
        /// </summary>
        /// <param name="Center">Center of region.</param>
        /// <returns>Rectangle box around center.</returns>
        private static Rectangle GetBoundingBox(Point Center)
        {
            Point boxLocation = new Point((Center.X - BoxScaling), (Center.Y - BoxScaling));
            return new Rectangle(boxLocation, new Size((BoxScaling * 2), (BoxScaling * 2)));
        }

        /// <summary>
        /// Calculate center of the figure index.
        /// </summary>
        /// <param name="Index">Index of the figure.</param>
        /// <returns>Return the center of the figure.</returns>
        private static Point FigurePosition(int Index)
        {
            switch(Index)
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
        /// Calculate the color of the figure.
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
        private static Bgr GetFigureColor(Image<Bgr, Byte> Image, Rectangle Box)
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
            return bgrFigureColor;
        }

        /// <summary>
        /// Calculate circularity of the blob region.
        /// </summary>
        /// <param name="Object">Contour of the blob.</param>
        /// <returns>Circularity</returns>
        private static double GetCircularity(Contour<Point> Object)
        {
            double c = 0.0d;

            if (Object != null)
            {
                // c = (2 * Math.PI * r) ^ 2 / (4 * Math.PI * Math.PI * r * r);
                c = ((4 * Math.PI * Object.Area) / Math.Pow(Object.Perimeter, 2));
            }

            return c;
        }
    }
}
