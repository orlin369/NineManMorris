using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    /// <summary>
    /// Node of the game.
    /// </summary>
    public class GameNode : IDisposable
    {
        #region Variable

        public int Score = 0;
        public Move Move;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Score">Scores ?</param>
        /// <param name="Move">Move ...</param>
        public GameNode(int Score, Move Move)
        {
            this.Move = Move;
            this.Score = Score;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~GameNode()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, call by destructor
        /// </summary>
        public void Dispose()
        {
            if (Move != null)
            {
                Move.Dispose();
            }
        }

        #endregion
    }
}
