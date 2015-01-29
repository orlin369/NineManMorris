using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.CorrdinateSystems
{
    public class Decart
    {
        public double X
        {
            get;
            set;
        }

        public double Y
        {
            get;
            set;
        }

        public double Z
        {
            get;
            set;
        }

        public double P
        {
            get;
            set;
        }

        public double R
        {
            get;
            set;
        }

        public Decart(double X, double Y, double Z, double P, double R)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
            this.P = P;
            this.R = R;
        }

        public Decart()
        {
            this.X = 0.0d;
            this.Y = 0.0d;
            this.Z = 0.0d;
            this.P = 0.0d;
            this.R = 0.0d;
        }

        public override string ToString()
        {
            return String.Format("X: {0:F3}; Y: {1:F3}; Z: {2:F3}; P: {3:F3}; R: {4:F3}", this.X, this.Y, this.Z, this.P, this.R);
        }
    }
}
