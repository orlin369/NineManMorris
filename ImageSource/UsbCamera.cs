using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;
namespace ImageSource
{
    public class UsbCamera : ICaptureDevice
    {
        /// <summary>
        /// Camera index.
        /// </summary>
        private int cameraIndex = 0;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="cameraIndex"></param>
        public UsbCamera(int cameraIndex)
        {
            this.cameraIndex = cameraIndex;
        }

        /// <summary>
        /// Get image from USB Camera.
        /// </summary>
        /// <returns>The bitmap image.</returns>
        public Bitmap Capture()
        {
            // Create capture
            Capture myCapture = new Capture(this.cameraIndex);
            // Get image from capture device.
            Image<Bgr, Byte> myImage = myCapture.QueryFrame();
            //
            //myCapture.Dispose();
            // Return the image.
            return myImage.ToBitmap();
        }
    }
}
