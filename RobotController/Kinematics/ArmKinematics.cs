using Robot.CorrdinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
    class ArmKinematics : IKinematics
    {

        #region Variables

        /// <summary>
        /// Arm configuration.
        /// </summary>
        private KinematicsConfiguration configuration;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        public ArmKinematics(KinematicsConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #endregion

        #region Public

        /// <summary>
        /// Invers kinematics task.
        /// </summary>
        /// <param name="Configuration">The input position is in [mm].</param>
        /// <returns>The output configuration is in [deg].</returns>
        public Joint IverseKinematicTask(Decart position)
        {
            Joint configuration = new Joint(0.0d, 0.0d, 0.0d, 0.0d, 0.0d);

            // Support variables
            double RR, LF, RM, GA, AL;

            if (position.Z < 0)
            {
                position.Z = 0;
            }

            if (position.Z < 300 && position.X < 0)
            {
                position.X = 100;
            }

            RR = Math.Sqrt(position.X * position.X + position.Y * position.Y);
            LF = 2 * this.configuration.L1 + this.configuration.LG;

            if (position.Z == this.configuration.H)
            {
                RM = LF;
            }
            else if (position.Z == 0)
            {
                RM = Math.Sqrt(LF * LF - this.configuration.H * this.configuration.H);
            }
            else
            {
                RM = Math.Sqrt(LF * LF - (this.configuration.H - position.Z) * (this.configuration.H - position.Z));
            }

            if (RR > RM)
            {
                RR = RM;
            }

            position.P = position.P / this.configuration.C;
            position.R = position.R / this.configuration.C;
            position.R = RR - this.configuration.LG * Math.Cos(position.P);
            position.Z = this.configuration.H - position.Z - this.configuration.LG * Math.Sin(position.P);

            if (position.R == 0)
            {
                GA = Math.Sign(position.Z) * Math.PI / 2;
            }
            else
            {
                GA = Math.Atan(position.Z / position.R);
            }

            AL = Math.Sqrt(position.R * position.R + position.Z * position.Z) / 2;
            AL = Math.Atan(Math.Sqrt(this.configuration.L1 * this.configuration.L1 - AL * AL) / AL);

            if (position.X == 0)
            {
                configuration.Base = Math.Sign(position.Y) * Math.PI / 2;
            }
            else
            {
                configuration.Base = Math.Atan(position.Y / position.X);
            }

            configuration.Shoulder = GA - AL;
            configuration.Elbow = GA + AL;
            configuration.Wrist = position.P + position.R + this.configuration.R1 * configuration.Base;
            configuration.Gripper = position.P - position.R - this.configuration.R1 * configuration.Base;

            configuration.Base = Rad2Deg(configuration.Base);
            configuration.Shoulder = Rad2Deg(configuration.Shoulder);
            configuration.Elbow = Rad2Deg(configuration.Elbow);
            configuration.Wrist = Rad2Deg(configuration.Wrist);
            configuration.Gripper = Rad2Deg(configuration.Gripper);

            return configuration;
        }

        
        /// <summary>
        /// Calculate the right kinematics task.
        /// </summary>
        /// <param name="configuration">Array of absolute joint angles.</param>
        /// <returns>Decart absolute position.</returns>
        public Decart RightsKinematicTask(Joint configuration)
        {
            configuration.Base = this.Rad2Deg(configuration.Base);
            configuration.Shoulder = this.Rad2Deg(configuration.Shoulder);
            configuration.Elbow = this.Rad2Deg(configuration.Elbow);
            configuration.Wrist = this.Rad2Deg(configuration.Wrist);
            configuration.Gripper = this.Rad2Deg(configuration.Gripper);

            // Output variables
            double XX, YY, ZZ, PP, RR;
            // ...
            double RP;

            PP = (configuration.Wrist + configuration.Gripper) / 2;
            RR = (configuration.Wrist - configuration.Gripper) / 2 - this.configuration.R1 * configuration.Base;
            RP = this.configuration.L1 * Math.Cos(configuration.Shoulder) + this.configuration.L2 * Math.Cos(configuration.Elbow) + this.configuration.LG * Math.Cos(PP);
            XX = RP * Math.Cos(configuration.Base);
            YY = RP * Math.Sin(configuration.Base);
            ZZ = this.configuration.H - this.configuration.L1 * Math.Sin(configuration.Shoulder) - this.configuration.L2 * Math.Sin(configuration.Elbow) - this.configuration.LG * Math.Sin(PP);
            PP = PP * this.configuration.C;
            RR = RR * this.configuration.C;

            return new Decart(XX, YY, ZZ, PP, RR);
        }

        #endregion

        #region Conversions

        /// <summary>
        /// Convert Radians to Degree.
        /// </summary>
        /// <param name="Angle">Angle in [RAD].</param>
        /// <returns>Angle in [DEG].</returns>
        private double Rad2Deg(double Angle)
        {
            return Angle * (180.0d / Math.PI);
        }

        /// <summary>
        /// Convert Degree in Radians.
        /// </summary>
        /// <param name="Angle">Angle in [DEG].</param>
        /// <returns>Angle in [RAD].</returns>
        private double Deg2Rad(double Angle)
        {
            return Math.PI * Angle / 180.0d;
        }

        #endregion
    }
}
