using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    public class EvalSettings : IDisposable
    {
        #region Variables

        public int MillFormable;
        public int MillFormed;
        public int MillBlocked;
        public int MillOpponent;
        public int CapturedPiece;
        public int LostPiece;
        public int AdjacentSpot;
        public int BlockedOpponentSpot;
        public int WorstScore;
        public int BestScore;

        #endregion

        #region Constructor / Destructor

        public EvalSettings() 
        {
            this.MillFormable = 50;
            this.MillFormed = 70;
            this.MillBlocked = 60;
            this.MillOpponent = -80;
            this.CapturedPiece = 70;
            this.LostPiece = -110;
            this.AdjacentSpot = 2;
            this.BlockedOpponentSpot = 2;
            this.WorstScore = -10000;
            this.BestScore = 10000;
        }

        // stub for constructor that reads eval settings from a file
        public EvalSettings(string FilePath)
        {

        }

        ~EvalSettings()
        {
            this.Dispose();
        }

        public void Dispose()
        {

        }

        #endregion
    }
}
