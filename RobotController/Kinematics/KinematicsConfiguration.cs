using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
    class KinematicsConfiguration
    {

        #region Variables

        /// <summary>
        /// Не я знам от къде идва
        /// Константата трябва да се редактира!!!
        /// </summary>
        private double c = 1;

        /// <summary>
        /// Не я знам от къде идва
        /// Константата трябва да се редактира!!!
        /// </summary>
        private double lg = 1;

        /// <summary>
        /// R1 in the standart way must be 1.0.
        /// It is used for changing the working surface of the robot.
        /// </summary>
        private double r1 = 1.0d;

        /// <summary>
        /// Offset from the base to the first joint.
        /// </summary>
        private double h = 1.0d;

        /// <summary>
        /// Distance between shoulde and elbow joint.
        /// Страница 80 фигура 4.12
        /// Константата трябва да се редактира!!!
        /// </summary>
        private double l1 = 1.0d;

        /// <summary>
        /// Distance between elbow and wrist joint.
        /// Length of the griper finger.
        /// Страница 80 фигура 4.12
        /// Константата трябва да се редактира!!!
        /// </summary>
        private double l2 = 1.0d;

        #endregion

        #region Property

        /// <summary>
        /// Scaling for P and R axis.
        /// C in the standart way must be 1.0.
        /// </summary>
        public double C
        {
            get
            {
                return this.c;
            }
        }

        /// <summary>
        /// probably it is the length between 
        /// </summary>
        public double LG
        {
            get
            {
                return this.lg;
            }
        }

        /// <summary>
        /// It is used for changing the working surface of the robot.
        /// R1 in the standart way must be 1.0.
        /// </summary>
        public double R1
        {
            get
            {
                return this.r1;
            }
        }

        /// <summary>
        /// Offset from the base to the first joint.
        /// </summary>
        public double H
        {
            get
            {
                return this.h;
            }
        }

        public double L1
        {
            get
            {
                return this.l1;
            }
        }

        public double L2
        {
            get
            {
                return this.l2;
            }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public KinematicsConfiguration()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="c"></param>
        /// <param name="lg"></param>
        /// <param name="r1"></param>
        /// <param name="h"></param>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        public KinematicsConfiguration(double c, double lg, double r1, double h, double l1, double l2)
        {
            this.c = c;
            this.lg = lg;
            this.r1 = r1;
            this.h = h;
            this.l1 = l1;
            this.l2 = l2;
        }

        #endregion

    }
}
