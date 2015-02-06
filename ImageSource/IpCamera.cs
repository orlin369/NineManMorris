using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Drawing;
using System.IO;

namespace ImageSource
{
    public class IpCamera : ICaptureDevice
    {
        /// <summary>
        /// Unifi
        /// </summary>
        private Uri uri;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="uri">Address of the image source.</param>
        public IpCamera(Uri uri)
        {
            // URL Image source.
            this.uri = uri;
        }

        /// <summary>
        /// Get image from IP Camera.
        /// </summary>
        /// <returns>The bitmap image.</returns>
        public Bitmap Capture()
        {
            WebRequest request = WebRequest.Create(this.uri.AbsoluteUri);
            WebResponse response = request.GetResponse();
            Stream stream = response.GetResponseStream();
            return new Bitmap(stream);
        }
    }
}
