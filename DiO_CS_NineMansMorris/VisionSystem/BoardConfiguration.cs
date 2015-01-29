using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VisionSystem
{
    /// <summary>
    /// Represent board configuration
    /// </summary>
    public class BoardConfiguration : IDisposable
    {
        #region Variables

        /// <summary>
        /// Figure configuration on the board.
        /// </summary>
        public int[] Figure;
        /// <summary>
        /// Number of the white player.
        /// </summary>
        public int White;
        /// <summary>
        /// Number of the black player.
        /// </summary>
        public int Black;

        #endregion

        #region Constrictors / Destructors
        
        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="Figures">Int array representing figures on the board</param>
        /// <param name="White">White number</param>
        /// <param name="Black">Black number</param>
        public BoardConfiguration(int[] Figures, int White = 1, int Black = 2)
        {
            this.Figure = Figures;
            this.White = White;
            this.Black = Black;
        }

        /// <summary>
        /// Construcotr
        /// </summary>
        public BoardConfiguration()
        {
            this.Figure = new int[24];
            this.White = 1;
            this.Black = 2;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~BoardConfiguration()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call by destrucotr
        /// </summary>
        public void Dispose()
        {
            this.Figure = null;
        }

        #endregion

        public string GetBoardAsString()
        {
            string text = "";
            string termin = "\r\n";

            text = "    A   B   C   D   E   F   G" + termin + termin;
            text += String.Format("1   {0}-----------{1}-----------{2}", Figure[0], Figure[1], Figure[2]) + termin;
            text += "    |           |           |" + termin;
            text += String.Format("2   |   {0}-------{1}-------{2}   |", Figure[3], Figure[4], Figure[5]) + termin;
            text += "    |   |       |       |   |" + termin;
            text += String.Format("3   |   |   {0}---{1}---{2}   |   |", Figure[6], Figure[7], Figure[8]) + termin;
            text += "    |   |   |       |   |   |" + termin;
            text += String.Format("4   {0}---{1}---{2}       {3}---{4}---{5}", Figure[9], Figure[10], Figure[11], Figure[12], Figure[13], Figure[14]) + termin;
            text += "    |   |   |       |   |   |" + termin;
            text += String.Format("5   |   |   {0}---{1}---{2}   |   |", Figure[15], Figure[16], Figure[17]) + termin;
            text += "    |   |       |       |   |" + termin;
            text += String.Format("2   |   {0}-------{1}-------{2}   |", Figure[18], Figure[19], Figure[20]) + termin;
            text += "    |           |           |" + termin;
            text += String.Format("7   {0}-----------{1}-----------{2}", Figure[21], Figure[22], Figure[23]) + termin;

            return text;
        }
    }
}
