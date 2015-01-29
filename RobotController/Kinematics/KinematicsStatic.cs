using Robot.CorrdinateSystems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Robot.Kinematics
{
    class KinematicsStatic
    {

        #region Constants

        /// <summary>
        /// Страница 67 таблица 190
        /// </summary>
        const double H = 90.0d;

        /// <summary>
        /// Не я знам от къде идва
        /// Константата трябва да се редактира!!!
        /// </summary>
        const double C = 1;

        /// <summary>
        /// Не я знам от къде идва
        /// Константата трябва да се редактира!!!
        /// </summary>
        const double LG = 1;

        // TODO: Da se nameri R1
        /// <summary>
        /// Не я знам от къде идва
        /// Константата трябва да се редактира!!!
        /// </summary>
        const double R1 = 1;

        /// <summary>
        /// Страница 80 фигура 4.12
        /// Константата трябва да се редактира!!!
        /// </summary>
        const double L1 = 141.0d;

        /// <summary>
        /// Страница 80 фигура 4.12
        /// Константата трябва да се редактира!!!
        /// </summary>
        const double L2 = 131.0;

        #endregion

        #region Iverse Kinematic Task

        /// <summary>
        /// Invers kinematics task.
        /// </summary>
        /// <param name="Configuration">The input position is in [mm].</param>
        /// <returns>The output configuration is in [deg].</returns>
        public static Joint IverseKinematicTask(Decart Position)
        {
            // Output variables
            double T1, T2, T3, T4, T5;
            // Support variables
            double RR, LF, RM, GA, AL;
            // Decart configuration
            double X0 = Position.X;
            double Y0 = Position.Y;
            double Z0 = Position.Z;
            double P0 = Position.P;
            double R0 = Position.R;

            if (Z0 < 0)
            {
                Z0 = 0;
            }

            if (Z0 < 300 && X0 < 0)
            {
                X0 = 100;
            }

            RR = Math.Sqrt(X0 * X0 + Y0 * Y0);
            LF = 2 * L1 + LG;

            if (Z0 == H)
            {
                RM = LF;
            }
            else if (Z0 == 0)
            {
                RM = Math.Sqrt(LF * LF - H * H);
            }
            else
            {
                RM = Math.Sqrt(LF * LF - (H - Z0) * (H - Z0));
            }

            if (RR > RM)
            {
                RR = RM;
            }

            P0 = P0 / C;
            R0 = R0 / C;
            R0 = RR - LG * Math.Cos(P0);
            Z0 = H - Z0 - LG * Math.Sin(P0);

            if (R0 == 0)
            {
                GA = Math.Sign(Z0) * Math.PI / 2;
            }
            else
            {
                GA = Math.Atan(Z0 / R0);
            }

            AL = Math.Sqrt(R0 * R0 + Z0 * Z0) / 2;
            AL = Math.Atan(Math.Sqrt(L1 * L1 - AL * AL) / AL);

            if (X0 == 0)
            {
                T1 = Math.Sign(Y0) * Math.PI / 2;
            }
            else
            {
                T1 = Math.Atan(Y0 / X0);
            }

            T2 = GA - AL;
            T3 = GA + AL;
            T4 = P0 + R0 + R1 * T1;
            T5 = P0 - R0 - R1 * T1;

            //return new double[5] { T1, T2, T3, T4, T5 };

            T1 = Rad2Deg(T1);
            T2 = Rad2Deg(T2);
            T3 = Rad2Deg(T3);
            T4 = Rad2Deg(T4);
            T5 = Rad2Deg(T5);

            return new Joint(T1, T2, T3, T4, 0.0d);
        }

        #endregion

        #region Rights Kinematic Task

        /// <summary>
        /// Calculate the right kinematics task.
        /// </summary>
        /// <param name="Configuration">Array of absolute joint angles.</param>
        /// <returns>Decart absolute position.</returns>
        public static Decart RightsKinematicTask(Joint Configuration)
        {

            Configuration.Base = Rad2Deg(Configuration.Base);
            Configuration.Shoulder = Rad2Deg(Configuration.Shoulder);
            Configuration.Elbow = Rad2Deg(Configuration.Elbow);
            Configuration.Wrist = Rad2Deg(Configuration.Wrist);

            double[] T = new double[5] { Configuration.Base, Configuration.Shoulder, Configuration.Elbow, Configuration.Wrist, 0.0d };

            // Output variables
            double XX, YY, ZZ, PP, RR;
            // ...
            double RP;

            PP = (T[3] + T[4]) / 2;
            RR = (T[3] - T[4]) / 2 - R1 * T[0];
            RP = L1 * Math.Cos(T[1]) + L2 * Math.Cos(T[2]) + LG * Math.Cos(PP);
            XX = RP * Math.Cos(T[0]);
            YY = RP * Math.Sin(T[0]);
            ZZ = H - L1 * Math.Sin(T[1]) - L2 * Math.Sin(T[2]) - LG * Math.Sin(PP);
            PP = PP * C;
            RR = RR * C;

            return new Decart(XX, YY, ZZ, PP, RR);
        }

        #endregion

        #region Conversions

        /// <summary>
        /// Convert Radians to Degree.
        /// </summary>
        /// <param name="Angle">Angle in [RAD].</param>
        /// <returns>Angle in [DEG].</returns>
        private static double Rad2Deg(double Angle)
        {
            return Angle * (180.0d / Math.PI);
        }

        /// <summary>
        /// Convert Degree in Radians.
        /// </summary>
        /// <param name="Angle">Angle in [DEG].</param>
        /// <returns>Angle in [RAD].</returns>
        private static double Deg2Rad(double Angle)
        {
            return Math.PI * Angle / 180.0d;
        }

        #endregion
    }
}
