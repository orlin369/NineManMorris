using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Robot.CorrdinateSystems;

namespace Robot.Experimantal
{
    public class ArmKinematics
    {
        private ArmConfiguration configuration;

        public ArmKinematics(ArmConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public Decart Forward(Joint joints)
        {
            Joint jointsRad = new Joint();

            jointsRad.Base = this.Deg2Rad(joints.Base);
            jointsRad.Shoulder = this.Deg2Rad(joints.Shoulder + 90.0d);
            jointsRad.Elbow = this.Deg2Rad(joints.Elbow);
            jointsRad.Wrist = this.Deg2Rad(joints.Wrist);

            double pr = this.configuration.L1 * Math.Cos(jointsRad.Base) + this.configuration.L2 * Math.Cos(jointsRad.Shoulder) + this.configuration.L3 * Math.Cos(jointsRad.Elbow);
            double x = pr * Math.Cos(jointsRad.Base);
            double y = pr * Math.Sin(jointsRad.Base);
            double z = this.configuration.H + this.configuration.L1 * Math.Sin(jointsRad.Base) - this.configuration.L2 * Math.Sin(jointsRad.Shoulder) - this.configuration.L3 * Math.Sin(jointsRad.Elbow);
            
            return new Decart(x, y, z, 0.0, 0.0);
        }

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
