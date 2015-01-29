using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    public class MoveCommand
    {
        #region Fields

        /// <summary>
        /// Command type.
        /// </summary>
        public MoveType CommandType
        {
            get;
            private set;
        }

        /// <summary>
        /// Beginig position.
        /// </summary>
        public BoardIndex StartPosition
        {
            get;
            private set;
        }
        
        /// <summary>
        /// end position.
        /// </summary>
        public BoardIndex EndPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// Capture positon.
        /// </summary>
        public BoardIndex CapturePosition
        {
            get;
            private set;
        }

        #endregion
    }
}
