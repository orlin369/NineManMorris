using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArtifitialInteligence
{
    public class Move : IDisposable
    {
        #region Variables

        // the start position (only used for MOVE and MOVE_AND_CAPTURE)
        protected BoardIndex StartPosition;
        // the end position (used for DROP, and MOVE)
        protected BoardIndex EndPosition;
        // the capture position (used only for CAPTURE Types)
        protected BoardIndex CapturePosition;
        // the type of move this is
        protected MoveType Type;
        // Moves statistic variables.
        public static int OurMovesGenerated = 0;
        public static int OurMovesDeleted = 0;

        #endregion

        #region Constructor / Destructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Type">Current Type</param>
        /// <param name="Position1">Current position</param>
        public Move(MoveType Type, BoardIndex Position1)
        {
            this.Type = Type;
            this.EndPosition = Position1;
	        this.Init();
        }

        /// <summary>
        /// Construcotr
        /// </summary>
        /// <param name="Type">Current Type</param>
        /// <param name="Position1">Begin position</param>
        /// <param name="Position2">End reaction position</param>
        public Move(MoveType Type, BoardIndex Position1, BoardIndex Position2)
        {
            this.Type = Type;

	        if (Type == MoveType.DropAndCapture)
            {
		        this.EndPosition = Position1;
		        this.CapturePosition = Position2;
	        }
	        else if (Type == MoveType.Move)
            {
		        this.StartPosition = Position1;
                this.EndPosition = Position2;
	        }

	        this.Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Type">Current Type</param>
        /// <param name="StartPosition">Begin position</param>
        /// <param name="EndPosition">End reaction position</param>
        /// <param name="CapturePosition">End capture position</param>
        public Move(MoveType Type, BoardIndex StartPosition, BoardIndex EndPosition, BoardIndex CapturePosition)
        {
            this.Type = Type;
            this.StartPosition = StartPosition;
            this.EndPosition = EndPosition;
            this.CapturePosition = CapturePosition;
	        this.Init();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Move">Move object instance</param>
        public Move(Move Move)
        {
            this.StartPosition = Move.StartPosition;
            this.EndPosition = Move.EndPosition;
            this.CapturePosition = Move.CapturePosition;
            this.Type = Move.Type;
	        Move.OurMovesGenerated++;
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~Move()
        {
            this.Dispose();
        }

        /// <summary>
        /// Dispose, called by destructor.
        /// </summary>
        public void Dispose()
        {
            OurMovesDeleted++;
        }

        #endregion

        /// <summary>
        /// Comapare two moves
        /// </summary>
        /// <param name="a">Move 1</param>
        /// <param name="b">Move 2</param>
        /// <returns></returns>
        public static int CompareMoves(Move a, Move b)
        {
            if (b == null) return -1;
	        return (a.Type > b.Type) ? -1 : (a.Type < b.Type) ? 1 : 0;
        }

        /// <summary>
        /// Current Type move
        /// </summary>
        /// <returns>Move state Type</returns>
        public MoveType GetMoveType()
        {
	        return this.Type;
        }

        // TODO: Document the MOVE methods.
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoardIndex GetDropPosition()
        {
	        return this.EndPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoardIndex GetStartPosition()
        {
	        return this.StartPosition;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoardIndex GetEndPosition()
        {
	        return this.EndPosition;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public BoardIndex GetCapturePosition()
        {
	        return this.CapturePosition;
        }

        /// <summary>
        /// Incremtnt the ourMovesGenerated.
        /// </summary>
        private void Init()
        {
	        OurMovesGenerated++;
        }
    }
}
