using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// For the color
using System.Drawing;

namespace VisionSystem
{
    class FigureColorDefinitions
    {
        #region Private

        /// <summary>
        /// Red band filter.
        /// </summary>
        /// <param name="Hue">Input the hue chanel</param>
        /// <returns>Return true if is rwd</returns>
        private static bool IsItRed(int Hue)
        {
            // Wide specter of red
            int spectrum = 27;
            // Borders of red
            // The reg is fixed on 0/360 and the center is allways 0/360.
            int maximum = 360;
            int minimum = 0;


            // Calculate minimum and maximum borders.
            int minimum1 = maximum - spectrum;
            int maximum2 = minimum + spectrum;

            // Condition checker, true if it is reached.
            if (((Hue >= minimum1) && (Hue <= maximum)) || ((Hue >= minimum) && (Hue <= maximum2)))
            {
                return true;
            }

            // If condition is not reached then false.
            return false;
        }

        /// <summary>
        /// Green band filter.
        /// </summary>
        /// <param name="Hue">Input the hue chanel</param>
        /// <returns>Return true if is green</returns>
        private static bool IsItGreen(int Hue)
        {
            // Wide specter of green
            int spectrum = 35;
            // Color for search.
            int color = 100;
            // Borders
            int maximum = color + spectrum;
            int minimum = color - spectrum;

            // Condition checker, true if it is reached.
            if ((Hue >= minimum) && (Hue <= maximum))
            {
                return true;
            }

            // If condition is not reached then false.
            return false;
        }

        /// <summary>
        /// Yellow band filter.
        /// </summary>
        /// <param name="Hue">Input the hue chanel</param>
        /// <returns>Return true if is yellow</returns>
        private static bool IsItYelow(int Hue)
        {
            // Wide specter
            int spectrum = 15;
            // Color for search.
            int color = 45;
            // Borders
            int maximum = color + spectrum;
            int minimum = color - spectrum;

            // Condition checker, true if it is reached.
            if ((Hue >= minimum) && (Hue <= maximum))
            {
                return true;
            }

            // If condition is not reached then false.
            return false;
        }

        /// <summary>
        /// Blue band filter.
        /// </summary>
        /// <param name="Hue">Input the hue chanel</param>
        /// <returns>Return true if is blue</returns>
        private static bool IsItBlue(int Hue)
        {
            // Wide specter
            int spectrum = 50;
            // Color for search.
            int color = 220;
            // Borders
            int maximum = color + spectrum;
            int minimum = color - spectrum;

            // Condition checker, true if it is reached.
            if ((Hue >= minimum) && (Hue <= maximum))
            {
                return true;
            }

            // If condition is not reached then false.
            return false;

        }

        #endregion

        #region Public 

        /// <summary>
        /// Returns the 
        /// </summary>
        /// <param name="Hue">Hue chanel value.</param>
        /// <returns>Returns number representing the player</returns>
        public static int WhoIsThePlayer(Color Color)
        {
            int hue = Convert.ToInt32(Color.GetHue());

            // Is it red ?
            if (IsItRed(hue))
            {
                return 1;
            }

            // Is it green ?
            if (IsItGreen(hue))
            {
                return 2;
            }

            // Is it blue ?
            if (IsItBlue(hue))
            {
                return 3;
            }

            // Is it yellow ?
            if (IsItYelow(hue))
            {
                return 4;
            }

            return 0;
        }

        #endregion
    }
}
