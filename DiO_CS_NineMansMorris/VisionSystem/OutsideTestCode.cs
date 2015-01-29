using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing;
using Emgu.CV.CvEnum;

namespace VisionSystem
{
    class OutsideTestCode
    {
        public static bool ProcessShapeMatching(ref Image<Gray, Byte> templateImage, out Image<Gray, Byte> targetImage)
        {
            positions = new List<Polygon>();
            Contour<Point> templateContours = GetEdgeContours(templateImage, null, 100);
            Contour<Point> targetContours = GetEdgeContours(targetImage, maskImage, 100);

            MCvBox2D box = templateContours.GetMinAreaRect();

            double precision = 0.0001;
            double distanceThreshold = 0.4;
            int nCCompLevels = 2; // connected components have 2 levels. 1 of the outer boundary en possibly 1 for the inner boundaries (holes)

            using (MemStorage storage = new MemStorage()) //allocate storage for contour approximation
            {
                //Find largest connected component countour in the template
                Contour<Point> templateContour = new Contour<Point>(storage);
                double largestArea = 0;
                for (Contour<Point> cont = templateContours; cont != null; cont = cont.HNext)
                    if (cont.Area > largestArea)
                        templateContour = cont;
                if (templateContour == null)
                    throw new NullReferenceException("No contours found in the template image");

                // reduce the nr of vertices in the template polygons
                templateContour = templateContour.ApproxPoly(templateContour.Perimeter * precision, nCCompLevels, storage);

                // reduce the nr of  vertices in the target polygons
                if (templateContour.Area > targetImage.Height * targetImage.Width / 1000) //only consider contours plausible area
                {
                    double distance = 0;
                    for (Contour<Point> targetContour = targetContours; targetContour != null; targetContour = targetContour.HNext)
                    {
                        if (targetContour.Area < targetImage.Height * targetImage.Width / 1000) //only consider contours plausible area
                            continue;

                        Contour<Point> approximatedTargetContour = targetContour.ApproxPoly(targetContour.Perimeter * precision, nCCompLevels, storage);
                        distance += templateContour.MatchShapes(approximatedTargetContour, CONTOURS_MATCH_TYPE.CV_CONTOURS_MATCH_I3);

                        ImageViewer.Show(DrawContours(targetImage, approximatedTargetContour, new Bgr(Color.Cyan), 2, 0), "contour");

                        //This is the smelly part, it only works when the number of holes in the template is zero or one
                        // in the template and target shapes are the 
                        if ((templateContour.VNext != null) && (targetContour.VNext != null))
                            distance += templateContour.MatchShapes(approximatedTargetContour, CONTOURS_MATCH_TYPE.CV_CONTOURS_MATCH_I3);
                        if ((templateContour.VNext != null) && (targetContour.VNext == null))
                            continue; // no match
                        if ((templateContour.VNext == null) && (targetContour.VNext != null))
                            continue; // no match
                        //http://stackoverflow.com/questions/15555615/equivalent-of-hierarchy-in-emgu

                        if (distance > distanceThreshold)
                            continue;

                        // we have a match, let's add the position
                        positions.Add(MCvBox2D2Polygon(targetContour.GetMinAreaRect()));

                        // draw some annotation
                        Image<Bgr, Byte> imageAnotated = DrawContours(targetImage, approximatedTargetContour, new Bgr(Color.Salmon), 2, nCCompLevels);
                        MCvBox2D boxi = approximatedTargetContour.GetMinAreaRect();
                        imageAnotated.Draw(boxi, new Bgr(Color.Turquoise), 2);
                        ImageViewer.Show(imageAnotated, String.Format("current contour {0}", distance));
                    }
                }
            }
            return false;
        }

        private Image<Bgr, byte> DrawContours(Image<Gray, byte> image, Contour<Point> contours, Bgr color, int thickness, int maxLevel)
        {
            Image<Bgr, Byte> resultImage = new Image<Bgr, byte>(image.Size);
            resultImage = image.Convert<Bgr, Byte>();
            CvInvoke.cvDrawContours(resultImage, contours, color.MCvScalar, color.MCvScalar, maxLevel, thickness, LINE_TYPE.CV_AA, new Point(0, 0));
            return resultImage;
        }

        private Contour<Point> GetEdgeContours(Image<Gray, byte> image, Image<Gray, byte> mask, int threshold)
        {
            CvInvoke.cvThreshold(image, image, 255, 255, THRESH.CV_THRESH_BINARY | THRESH.CV_THRESH_OTSU);
            image = image.Dilate(3).Erode(3);
            Contour<Point> cont = image.FindContours(CHAIN_APPROX_METHOD.CV_CHAIN_APPROX_TC89_L1, RETR_TYPE.CV_RETR_CCOMP);
            return cont;
        }

        private Polygon MCvBox2D2Polygon(MCvBox2D box)
        {
            System.Windows.Media.PointCollection polyPoints = new System.Windows.Media.PointCollection();
            foreach (var p in box.GetVertices())
                polyPoints.Add(new System.Windows.Point(p.X, p.Y));
            Polygon poly = new Polygon();
            poly.Points = polyPoints;
            return poly;
        }
    }
}
