using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.CorrdinateSystems
{
    public class Joint
    {
        /// <summary>
        /// Base angle in [deg].
        /// </summary>
        public double Base
        {
            get;
            set;
        }
 
        /// <summary>
        /// Elbow angle in [deg].
        /// </summary>
        public double Elbow
        {
            get;
            set;
        }

        /// <summary>
        /// Shoulder angle in [deg].
        /// </summary>
        public double Shoulder
        {
            get;
            set;
        }

        /// <summary>
        /// Pich angle in [deg].
        /// </summary>
        public double Wrist
        {
            get;
            set;
        }

        /// <summary>
        /// Pich angle in [deg].
        /// </summary>
        public double Gripper
        {
            get;
            set;
        }



        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Base">Base angle in [deg].</param>
        /// <param name="Shoulder">Shoulder angle in [deg].</param>
        /// <param name="Elbow">Elbow angle in [deg].</param>
        /// <param name="Wrist">Pich angle in [deg].</param>
        public Joint(double Base, double Shoulder, double Elbow, double Wrist, double Gripper)
        {
            this.Base = Base;
            this.Shoulder = Shoulder;
            this.Elbow = Elbow;
            this.Wrist = Wrist;
            this.Gripper = Gripper;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Configuration">Array joint representation of angle in [deg].</param>
        public Joint(double[] Configuration)
        {
            this.Base = Configuration[0];
            this.Shoulder = Configuration[1];
            this.Elbow = Configuration[2];
            this.Wrist = Configuration[3];
            this.Gripper = Configuration[4];
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public Joint()
        {
            this.Base = 0.0d;
            this.Shoulder = 0.0d;
            this.Elbow = 0.0d;
            this.Wrist = 0.0d;
            this.Gripper = 0.0d;
        }

        /// <summary>
        /// Convert to string.
        /// </summary>
        /// <returns>Return string that contains positions.</returns>
        public override string ToString()
        {
            return String.Format("Base: {0}; Shoulder: {1}; Elbow: {2}; Pich: {3}; Roll: {4}", this.Base, this.Shoulder, this.Elbow, this.Wrist, this.Gripper);
        }
    }
}
