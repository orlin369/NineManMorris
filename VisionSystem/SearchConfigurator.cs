using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionSystem
{
    public class SearchConfigurator : IDisposable
    {
        #region Construcotr / Destructor

        /// <summary>
        /// Constructor with predefined parameters
        /// </summary>
        public SearchConfigurator()
        {
            this.MaximumMarkerArea = 280;
            this.MinimumMarkerArea = 100;
            this.TreshholdLevel = 210;
            this.MarkerCircularity = 0.7d;
            this.ImageLightnes = 0.8d;
            this.SmoothKernelSize = 3;
        }

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="MaximumMarkerArea"></param>
        /// <param name="MinimumMarkerArea"></param>
        /// <param name="MaximumMarkerPerimeter"></param>
        /// <param name="MinimumMarkerPerimeter"></param>
        /// <param name="MarkerCircularity"></param>
        /// <param name="ImageLightnes"></param>
        /// <param name="TreshholdLevel"></param>
        public SearchConfigurator(
            int MaximumMarkerArea,
            int MinimumMarkerArea,
            int MaximumMarkerPerimeter,
            int MinimumMarkerPerimeter,
            double MarkerCircularity,
            double ImageLightnes,
            int TreshholdLevel)
        {
            this.MaximumMarkerArea = MaximumMarkerArea;
            this.MinimumMarkerArea = MinimumMarkerArea;
            this.MaximumMarkerPerimeter = MaximumMarkerPerimeter;
            this.MinimumMarkerPerimeter = MinimumMarkerPerimeter;
            this.MarkerCircularity = MarkerCircularity;
            this.ImageLightnes = ImageLightnes;
            this.TreshholdLevel = TreshholdLevel;
        }

        /// <summary>
        /// Destrucotr
        /// </summary>
        ~SearchConfigurator()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call from destructor.
        /// </summary>
        public void Dispose()
        {

        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public int MaximumMarkerArea
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MinimumMarkerArea
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MaximumMarkerPerimeter
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int MinimumMarkerPerimeter
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public double MarkerCircularity
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public double ImageLightnes
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int TreshholdLevel
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public int SmoothKernelSize
        {
            get;
            set;
        }
    }
}
