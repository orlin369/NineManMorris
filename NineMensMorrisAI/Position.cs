using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    /// <summary>
    /// Contains position on play board.
    /// </summary>
    class Position : IDisposable
    {
        #region Variables

        // Location on board
        private BoardIndex location;

        // Statistic !
        public static int PositionsGenerated;
        public static int PositionsDeleted;

        // Who is on current position
        public Player Player = Player.Neutral;

        // Posible position in general
        public Position Up;
        public Position Down;
        public Position Right;
        public Position Left;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor. construct by board index.
        /// </summary>
        /// <param name="Location">Board index pointer.</param>
        public Position(BoardIndex Location)
        {
            this.location = Location;
        }

        /// <summary>
        /// Constructor. Construct by position.
        /// </summary>
        /// <param name="Position"></param>
        public Position(Position Position)
        {
            Position.PositionsGenerated++;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Position()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
	        Position.PositionsDeleted++;
        }

        #endregion 

        #region Methods ...

        /// <summary>
        /// Getter
        /// </summary>
        /// <returns>Current player</returns>
        public Player GetPlayer()
        {
            return this.Player;
        }

        /// <summary>
        /// Setter
        /// </summary>
        /// <param name="Player">Current player</param>
        public void SetPlayer(Player Player)
        {
            this.Player = Player;
        }

        /// <summary>
        /// Get current location.
        /// </summary>
        /// <returns>Current position</returns>
        public BoardIndex GetLocation()
        {
	        return this.location;
        }

        #endregion
    }
}
