using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Experimantal
{
    public class ArmConfiguration
    {

        #region Variables

        /// <summary>
        /// Distance between base and the first joint.
        /// </summary>
        private double h = 0.0d;

        /// <summary>
        /// Distabce of the base ofseted joint.
        /// </summary>
        private double l1 = 1.0d;

        /// <summary>
        /// Distabce between shoulder and elbow.
        /// </summary>
        private double l2 = 1.0d;

        /// <summary>
        /// Distabce between elbow and wrist.
        /// </summary>
        private double l3 = 1.0d;

        #endregion

        #region Property

        /// <summary>
        /// Distance between base and the first joint.
        /// </summary>
        public double H
        {
            get
            {
                return this.h;
            }
        }

        /// <summary>
        /// Distabce of the base ofseted joint.
        /// </summary>
        public double L1
        {
            get
            {
                return this.l1;
            }
        }

        /// <summary>
        /// Distabce between shoulder and elbow.
        /// </summary>
        public double L2
        {
            get
            {
                return this.l2;
            }
        }

        /// <summary>
        /// Distabce between elbow and wrist.
        /// </summary>
        public double L3
        {
            get
            {
                return this.l3;
            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ArmConfiguration()
        {
            this.h = 0.0d;
            this.l1 = 1.0d;
            this.l2 = 1.0d;
            this.l3 = 1.0d;
        }

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="h"></param>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <param name="l3"></param>
        public ArmConfiguration(double h, double l1, double l2, double l3)
        {
            this.h = h;
            this.l1 = l1;
            this.l2 = l2;
            this.l3 = l3;
        }

        #endregion

    }
}
