using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Drawing;
using Robot.CorrdinateSystems;

namespace Robot.Application
{
    public class FigureShop : IDisposable
    {

        #region Variables

        private Joint[] Positions = new Joint[9];
        public int Index
        {
            get;
            private set;
        }

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="Positions">Absolute positions of the shop.</param>
        public FigureShop(Joint[] Positions)
        {
            this.Positions = Positions;
            this.Index = 0;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~FigureShop()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call by the destructor.
        /// </summary>
        public void Dispose()
        {
            this.Positions = null;
        }

        #endregion

        /// <summary>
        /// Get the 
        /// </summary>
        /// <returns></returns>
        public Joint GetNextFigure()
        {
            Joint ret = this.Positions[this.Index];

            this.Index++;

            if (this.Index > (this.Positions.Length - 1))
            {
                this.Index = this.Positions.Length - 1;
            }

            return ret;
        }

        /// <summary>
        /// Reset the start position of the shop.
        /// </summary>
        public void Reset()
        {
            this.Index = 0;
        }
    }
}
